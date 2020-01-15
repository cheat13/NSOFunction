using System;
using System.Collections.Generic;
using System.Linq;
using NSOFunction.Models;
using NSOWater.HotMigration.Models;

namespace NSOFunction
{
    public class CubicMeterGroundWaterManage
    {
        public Buying Buying { get; set; }
        public BuildingType? BuildingType { get; set; }
        public StatusCompute CanCumpute { get; set; }
        public List<BuildingType> BDTypes { get; set; }

        public CubicMeterGroundWaterManage(Buying buying, BuildingType? buildingType)
        {
            Buying = buying;
            BuildingType = buildingType;
            BDTypes = new List<BuildingType>
            {
                NSOWater.HotMigration.Models.BuildingType.SingleHouse,
                NSOWater.HotMigration.Models.BuildingType.TownHouse,
                NSOWater.HotMigration.Models.BuildingType.ShopHouse,
                NSOWater.HotMigration.Models.BuildingType.Apartment,
                NSOWater.HotMigration.Models.BuildingType.Religious,
                NSOWater.HotMigration.Models.BuildingType.GreenHouse
            };
        }

        public CubicMeterRequest CalcPrivate(IEnumerable<GroundWaterWell> resources, WaterCharacter character)
        {
            this.CanCumpute = StatusCompute.True;
            var cubicMeter = resources.Where(it => it != null).Sum(it =>
                 {
                     try
                     {
                         if (it.UsageType.GroundWaterQuantity == GroundWaterQuantity.CubicMeterPerMonth)
                         {
                             return (it.UsageType.UsageCubicMeters ?? 0) * 12 * WaterActivity(it.WaterActivities, character) / 100;
                         }
                         else if (it.UsageType.GroundWaterQuantity == GroundWaterQuantity.WaterBill)
                         {
                             return (it.UsageType.WaterBill ?? 0) / WaterPrice(it.Location) * 12 * WaterActivity(it.WaterActivities, character) / 100;
                         }
                         else if (it.UsageType.GroundWaterQuantity == GroundWaterQuantity.Unknown && (it.HasPump == true))
                         {
                             return CalcPumps(it.Pumps, true) * WaterActivity(it.WaterActivities, character) / 100;
                         }
                         else
                         {
                             this.CanCumpute = StatusCompute.False;
                             return 0;
                         }
                     }
                     catch (System.Exception) { }
                     return 0;
                 });

            return new CubicMeterRequest
            {
                CanCompute = this.CanCumpute,
                CubicMeter = cubicMeter
            };
        }

        public CubicMeterRequest CalcPublic(IEnumerable<WaterConsumptionUsingPump> resources, WaterCharacter character)
        {
            this.CanCumpute = StatusCompute.True;
            var cubicMeter = resources.Where(it => it != null).Sum(it =>
              {
                  try
                  {
                      if (it.HasCubicMeterPerMonth == true)
                      {
                          return (it.CubicMeterPerMonth ?? 0) * 12 * WaterActivity(it.WaterActivities, character) / 100;
                      }
                      else if (it.HasPump == true)
                      {
                          return CalcPumps(it.Pumps, true) * WaterActivity(it.WaterActivities, character) / 100;
                      }
                      else
                      {
                          this.CanCumpute = StatusCompute.False;
                          return 0;
                      }
                  }
                  catch (System.Exception) { }
                  return 0;
              });

            return new CubicMeterRequest
            {
                CanCompute = this.CanCumpute,
                CubicMeter = cubicMeter
            };
        }

        private double WaterActivity(WaterActivity waterAct, WaterCharacter character)
        {
            switch (character)
            {
                case WaterCharacter.IsAgriculture: return waterAct?.Agriculture ?? 0;
                case WaterCharacter.IsCommercial: return waterAct?.Service ?? 0;
                case WaterCharacter.IsFactorial: return waterAct?.Product ?? 0;
                case WaterCharacter.IsHouseHold: return waterAct?.Drink ?? 0;
                default: return 0;
            }
        }

        private double WaterPrice(Location location)
        {
            return isIn7Areas(location) ? BDTypes.Any(it => it == BuildingType) ? 8.5 : 13 : 3.5;
        }

        private bool isIn7Areas(Location location)
        {
            var areas = new List<string>
            {
                "กรุงเทพมหานคร", "พระนครศรีอยุธยา", "ปทุมธานี", "สมุทรสาคร", "สมุทรปราการ", "นนทบุรี", "นครปฐม"
            };
            return areas.Any(it => it == location.Province);
        }

        private double CalcPumps(List<Pump> pumps, bool isGround)
        {
            return pumps.Where(it => it != null).Sum(it =>
            {
                if (!it.PumpAuto == true)
                {
                    var pumpsPerYear = it.NumberOfPumpsPerYear ?? 0;
                    if (pumpsPerYear < 0) pumpsPerYear = Math.Abs(pumpsPerYear);
                    if (pumpsPerYear > 10) pumpsPerYear = 10;
                    return (it.HoursPerPump ?? 0) * pumpsPerYear * CalcPumpRate(it, isGround);
                }
                else
                {
                    this.CanCumpute = StatusCompute.False;
                    return 0;
                }
            });
        }

        private double CalcPumpRate(Pump pump, bool isGround)
        {
            if (pump.HasPumpRate == true)
            {
                return (pump.PumpRate ?? 0) * 1000;
            }
            else
            {
                var pumpcal = new PumpCal();
                var listPump = isGround ? pumpcal.listPumpGroundWater : pumpcal.listSurfaceWater;
                try
                {
                    var pumpRate = listPump.FirstOrDefault(it => it.EnergyFromPump == pump.EnergySource
                        && it.PumpType == pump.PumpType && it.Power == pump.HorsePower
                        && it.SuctionPipeSize == pump.SuctionPipeSize && it.PipelineSize == pump.PipelineSize).PumpRate;
                    return (pumpRate ?? 0) * 60;
                }
                catch (System.Exception)
                {
                    var pumpRate = listPump.FirstOrDefault(it => it.PumpType == pump.PumpType && it.Power == pump.HorsePower
                           && it.SuctionPipeSize == pump.SuctionPipeSize && it.PipelineSize == pump.PipelineSize)?.PumpRate
                           ?? (pump.PumpType.StartsWith("ปั๊มหอยโข่ง") ? 1500 : 0);
                    return pumpRate * 60;
                }
            }
        }

        public double CalcBuying(WaterCharacter character)
        {
            return Buying != null
                ? Buying.Package
                    .Where(it => double.TryParse(it?.Size, out double size))
                    .Sum(it => double.Parse(it.Size) * BuyingActivity(it, character) / (!string.IsNullOrEmpty(it.Name) && it.Name.Contains("ขวด") ? 1000000 : 1000)) * 12
                : 0;
        }

        private double BuyingActivity(Package package, WaterCharacter character)
        {
            switch (character)
            {
                case WaterCharacter.IsAgriculture: return package?.Agriculture ?? 0;
                case WaterCharacter.IsCommercial: return package?.Service ?? 0;
                case WaterCharacter.IsFactorial: return package?.Factory ?? 0;
                case WaterCharacter.IsHouseHold: return package?.Drink ?? 0;
                default: return 0;
            }
        }
    }
}