using System;
using System.Collections.Generic;
using System.Linq;
using NSOFunction.Models;
using NSOWater.HotMigration.Models;

namespace NSOFunction
{
    public class CubicMeterSurfaceManage
    {
        public River River { get; set; }
        public Irrigation Irrigation { get; set; }
        public Rain Rain { get; set; }
        private StatusCompute canCumpute { get; set; }
        private WaterCharacter character { get; set; }

        public CubicMeterSurfaceManage(Pool pool, River river, Irrigation irrigation, Rain rain)
        {
            River = river;
            Irrigation = irrigation;
            Rain = rain;
        }

        public CubicMeterRequest CalcPool(IEnumerable<WaterConsumptionUsingPump> resources, WaterCharacter character)
        {
            this.canCumpute = StatusCompute.True;
            this.character = character;
            var cubicMeter = resources.Where(it => it != null).Sum(it =>
                {
                    try
                    {
                        if (it.HasCubicMeterPerMonth == true)
                        {
                            return (it.CubicMeterPerMonth ?? 0) * 12.0 * WaterActivity(it.WaterActivities) / 100;
                        }
                        else if (it.HasPump == true)
                        {
                            return CalcPumps(it.Pumps, false) * WaterActivity(it.WaterActivities) / 100;
                        }
                        else
                        {
                            this.canCumpute = StatusCompute.False;
                            return 0;
                        }
                    }
                    catch (System.Exception) { }
                    return 0;
                });

            return new CubicMeterRequest
            {
                CanCompute = this.canCumpute,
                CubicMeter = cubicMeter
            };
        }

        public double CubicMeterSurface(WaterCharacter character)
        {
            this.character = character;
            return CalcRiver() + CalcIrrigation() + CalcRain();
        }

        public double CalcRiver()
        {
            return River?.HasPump == true ? (CalcPumps(River.Pumps, false) * WaterActivity(River?.WaterActivities) / 100) : 0;
        }

        public double CalcIrrigation()
        {
            if (Irrigation?.HasCubicMeterPerMonth == true)
            {
                return (Irrigation.CubicMeterPerMonth ?? 0) * 12.0 * WaterActivity(Irrigation.WaterActivities) / 100;
            }
            else if (Irrigation?.HasPump == true)
            {
                return CalcPumps(Irrigation.Pumps, false) * WaterActivity(Irrigation.WaterActivities) / 100;
            }
            return 0;
        }

        public double CalcRain()
        {
            return Rain != null && Rain.RainContainers.Any() ?
                Rain.RainContainers
                    .Where(it => it.Size != null)
                    .Sum(it =>
                        it.Size
                            .Replace(",", "")
                            .Split(new[] { " - " }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(i => int.Parse(i)).Average()
                        / 1000.0 * (it.Count ?? 0))
                * WaterActivity(Rain.WaterActivities) / 100.0
                : 0;
        }

        private double CalcPumps(List<Pump> pumps, bool isGround)
        {
            return pumps.Where(it => it != null).Sum(it =>
            {
                if (!it.PumpAuto == true)
                {
                    var pumpsPerYear = it.NumberOfPumpsPerYear ?? 0;
                    if (pumpsPerYear > 10) pumpsPerYear = 10;
                    return (it.HoursPerPump ?? 0) * pumpsPerYear * CalcPumpRate(it, isGround);
                }
                else
                {
                    canCumpute = StatusCompute.False;
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

        private double WaterActivity(WaterActivity waterAct)
        {
            switch (this.character)
            {
                case WaterCharacter.IsAgriculture: return waterAct?.Agriculture ?? 0;
                case WaterCharacter.IsCommercial: return waterAct?.Service ?? 0;
                case WaterCharacter.IsFactorial: return waterAct?.Product ?? 0;
                case WaterCharacter.IsHouseHold: return waterAct?.Drink ?? 0;
                default: return 0;
            }
        }
    }
}