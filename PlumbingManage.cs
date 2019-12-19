using System.Linq;
using NSOFunction.Models;
using NSOWater.HotMigration.Models;

namespace NSOFunction
{
    public class PlumbingManage
    {
        public PlumbingModel GetPlumbingInfo(PlumbingInfo info, WaterActivity waterActivity)
        {
            return new PlumbingModel
            {
                Doing = info?.Doing == true,
                CubicMeterPerMonth = info?.PlumbingUsage?.CubicMeterPerMonth ?? 0,
                WaterBill = info?.PlumbingUsage?.WaterBill ?? 0,
                TurbidWater = info?.QualityProblem?.Problem?.TurbidWater == true,
                SaltWater = info?.QualityProblem?.Problem?.SaltWater == true,
                Smell = info?.QualityProblem?.Problem?.Smell == true,
                FilmOfOil = info?.QualityProblem?.Problem?.FilmOfOil == true,
                FogWater = info?.QualityProblem?.Problem?.FogWater == true,
                HardWater = info?.QualityProblem?.Problem?.HardWater == true,
                Agriculture = waterActivity?.Agriculture ?? 0,
                Service = waterActivity?.Service ?? 0,
                Product = waterActivity?.Product ?? 0,
                Drink = waterActivity?.Drink ?? 0,
                Plant = waterActivity?.Plant ?? 0
            };
        }
    }

    public class PlumbingModel
    {
        /// <summary>
        /// ใช้น้ำประปา
        /// </summary>
        public bool Doing { get; set; }

        /// <summary>
        /// ปริมาณน้ำ ลบ.ม./เดือน 
        /// </summary>
        public double CubicMeterPerMonth { get; set; }

        /// <summary>
        /// ค่าน้้า
        /// </summary>
        public double WaterBill { get; set; }

        /// <summary>
        /// ขุ่น/ตะกอน  
        /// </summary>
        public bool TurbidWater { get; set; }

        /// <summary>
        /// เค็ม/กร่อย  
        /// </summary>
        public bool SaltWater { get; set; }

        /// <summary>
        ///  มีกลิ่น
        /// </summary>
        public bool Smell { get; set; }

        /// <summary>
        ///  คราบมัน
        /// </summary>
        public bool FilmOfOil { get; set; }

        /// <summary>
        /// ฝ้าขาว
        /// </summary>
        public bool FogWater { get; set; }

        /// <summary>
        ///  น้ำกระด้าง
        /// </summary>
        public bool HardWater { get; set; }

        /// <summary>
        /// สัดส่วนเพื่อการเกษตร
        /// </summary>
        public double Agriculture { get; set; }

        /// <summary>
        /// สัดส่วนเพื่อการบริการ
        /// </summary>
        public double Service { get; set; }

        /// <summary>
        /// สัดส่วนเพื่อการอุตสาหกรรม
        /// </summary>
        public double Product { get; set; }

        /// <summary>
        /// สัดส่วนเพื่อการอุปโภคบริโภค
        /// </summary>
        public double Drink { get; set; }

        /// <summary>
        /// สัดส่วนเพื่อรดน้้าพืชในบริเวณที่อยู่อาศัย
        /// </summary>
        public double Plant { get; set; }
    }

    public class WaterActivityModel
    {
        public double? Agriculture { get; set; }
        public double? Service { get; set; }
        public double? Product { get; set; }
        public double? Drink { get; set; }
        public double? Plant { get; set; }
        public double? Farm { get; set; }
    }
}