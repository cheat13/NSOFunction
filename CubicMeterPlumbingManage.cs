using System.Collections.Generic;
using System.Linq;
using NSOFunction.Models;
using NSOWater.HotMigration.Models;

namespace NSOFunction
{
    public class CubicMeterPlumbingManage
    {
        public Plumbing Plumbing { get; set; }
        public BuildingType? BuildingType { get; set; }
        public double MeterRentalFee { get; set; }
        public double PlumbingPrice { get; set; }
        public StatusCompute CanCumputePlumbing { get; set; }
        public List<BuildingType> BDTypes { get; set; }
        private WaterCharacter character { get; set; }

        public CubicMeterPlumbingManage(Plumbing plumbing, BuildingType? buildingType, double meterRentalFee, double plumbingPrice)
        {
            Plumbing = plumbing;
            BuildingType = buildingType;
            MeterRentalFee = meterRentalFee;
            PlumbingPrice = plumbingPrice;
            CanCumputePlumbing = StatusCompute.True;
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

        /// <summary>
        /// 30-33.ปริมาณการใช้น้ำประปา
        /// /// </summary>
        public CubicMeterRequest CubicMeterPlumbing(bool isChecked, WaterCharacter character)
        {
            this.character = character;

            var cubicMeterMWA = CubicMeter(Plumbing?.MWA, "M");
            var cubicMeterPWA = CubicMeter(Plumbing?.PWA, "P");
            var cubicMeterOther = CubicMeter(Plumbing?.Other, "O");

            var waterActivityMWA = WaterActivity(Plumbing?.WaterActivityMWA);
            var waterActivityPWA = WaterActivity(Plumbing?.WaterActivityPWA);
            var waterActivityOther = WaterActivity(Plumbing?.WaterActivityOther);

            var cubicMeter = cubicMeterMWA * (waterActivityMWA / 100) +
                cubicMeterPWA * (waterActivityPWA / 100) +
                cubicMeterOther * (waterActivityOther / 100);

            if (cubicMeter < 1 && cubicMeter != 0) cubicMeter = 1;

            return new CubicMeterRequest
            {
                CanCompute = isChecked ? CanCumputePlumbing : StatusCompute.NA,
                CubicMeter = isChecked ? cubicMeter : 0
            };
        }

        private double CubicMeter(PlumbingInfo info, string pType)
        {
            if (info?.Doing == true)
            {
                switch (info.PlumbingUsage.WaterQuantity)
                {
                    case WaterQuantity.CubicMeterPerMonth: return (info.PlumbingUsage.CubicMeterPerMonth ?? 0) * 12.0;
                    case WaterQuantity.WaterBill: return CalcWaterBill(info.PlumbingUsage.WaterBill ?? 0, pType);
                    default:
                        CanCumputePlumbing = StatusCompute.False;
                        return 0;
                }
            }
            return 0;
        }

        private double CalcWaterBill(double waterBill, string pType)
        {
            switch (pType)
            {
                case "M": return BDTypes.Any(it => it == BuildingType) ? waterBill * 12.0 / 10.5 : waterBill * 12.0 / 13;
                case "P": return BDTypes.Any(it => it == BuildingType) ? waterBill * 12.0 / 16.6 : waterBill * 12.0 / 26;
                case "O": return (waterBill - MeterRentalFee) / PlumbingPrice;
                default: return 0;
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