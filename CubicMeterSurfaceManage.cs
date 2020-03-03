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
        private WaterCharacter character { get; set; }

        public CubicMeterSurfaceManage(Pool pool, River river, Irrigation irrigation, Rain rain)
        {
            River = river;
            Irrigation = irrigation;
            Rain = rain;
        }

        public CubicMeterRequest CalcPool(IEnumerable<WaterConsumptionUsingPump> resources, WaterCharacter character)
        {
            this.character = character;
            var canCumpute = StatusCompute.True;
            var adjusted = false;
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
                            var PumpRequest = CalcPumps(it.Pumps, false);
                            if (PumpRequest.Adjusted == true) adjusted = true;
                            return PumpRequest.CubicMeter * WaterActivity(it.WaterActivities) / 100;
                        }
                        else
                        {
                            canCumpute = StatusCompute.False;
                            return 0;
                        }
                    }
                    catch (System.Exception) { }
                    return 0;
                });

            return new CubicMeterRequest
            {
                CanCompute = canCumpute,
                CubicMeter = cubicMeter,
                Adjusted = adjusted
            };
        }

        public CubicMeterRequest CubicMeterSurface(WaterCharacter character)
        {
            this.character = character;
            var river = CalcRiver();
            var irrigation = CalcIrrigation();

            var status = new StatusComputeManage();
            var canComputeLst = new List<StatusCompute> { river.CanCompute, irrigation.CanCompute };

            return new CubicMeterRequest
            {
                CanCompute = status.CheckStatusCompute(canComputeLst),
                CubicMeter = river.CubicMeter + irrigation.CubicMeter + CalcRain(),
                Adjusted = river.Adjusted || irrigation.Adjusted
            };
        }

        public CubicMeterRequest CalcRiver()
        {
            var PumpRequest = CalcPumps(River?.Pumps, false);

            return new CubicMeterRequest
            {
                CanCompute = PumpRequest.CanCompute,
                CubicMeter = River?.HasPump == true ? (PumpRequest.CubicMeter * WaterActivity(River?.WaterActivities) / 100) : 0,
                Adjusted = PumpRequest.Adjusted
            };
        }

        public CubicMeterRequest CalcIrrigation()
        {
            var PumpRequest = CalcPumps(Irrigation?.Pumps, false);

            var cubicMeter = (Irrigation?.HasCubicMeterPerMonth == true)
                ? (Irrigation.CubicMeterPerMonth ?? 0) * 12.0 * WaterActivity(Irrigation.WaterActivities) / 100
                : (Irrigation?.HasPump == true)
                    ? PumpRequest.CubicMeter * WaterActivity(Irrigation.WaterActivities) / 100
                    : 0;

            return new CubicMeterRequest
            {
                CanCompute = PumpRequest.CanCompute,
                CubicMeter = cubicMeter,
                Adjusted = PumpRequest.Adjusted
            };
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

        private CubicMeterRequest CalcPumps(List<Pump> pumps, bool isGround)
        {
            var canCumpute = StatusCompute.True;
            var adjusted = false;
            var cubicMeter = pumps != null
                ? pumps.Where(it => it != null).Sum(it =>
                {
                    if (it?.PumpAuto == false)
                    {
                        var pumpsPerYear = it.NumberOfPumpsPerYear ?? 0;
                        if (pumpsPerYear < 0)
                        {
                            adjusted = true;
                            pumpsPerYear = Math.Abs(pumpsPerYear);
                        };
                        if (pumpsPerYear > 10) pumpsPerYear = 10;
                        return (it.HoursPerPump ?? 0) * pumpsPerYear * CalcPumpRate(it, isGround);
                    }
                    else
                    {
                        canCumpute = StatusCompute.False;
                        return 0;
                    }
                })
                : 0;

            if (pumps.Any(it => it?.PumpAuto == true))
            {
                switch (character)
                {
                    case WaterCharacter.IsHouseHold:
                        cubicMeter += 131.4;
                        break;
                    case WaterCharacter.IsAgriculture:
                        cubicMeter += 3840;
                        break;
                    case WaterCharacter.IsFactorial:
                        cubicMeter += 2496;
                        break;
                    case WaterCharacter.IsCommercial:
                        cubicMeter += 468;
                        break;
                    default: break;
                }
            }

            return new CubicMeterRequest
            {
                CanCompute = canCumpute,
                CubicMeter = cubicMeter,
                Adjusted = adjusted,
            };
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