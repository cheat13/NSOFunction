using NSOWater.HotMigration.Models;

namespace NSOFunction.Models
{
    public class CubicMeterGroundWaterCommunity
    {
        /// <summary>
        /// 26.ปริมาณการใช้น้ำบาดาลเพื่อการเกษตร(น้ำบาดาล น้ำซื้อ) สน.2
        /// </summary>
        public double CubicMeterGroundWaterForAgriculture { get; set; }

        /// <summary>
        /// 29.ปริมาณการใช้น้ำบาดาลเพื่อการอุปโภคบริโภค(น้ำบาดาล น้ำซื้อ) สน.2
        /// </summary>
        public double CubicMeterGroundWaterForDrink { get; set; }

        /// <summary>
        /// 38.ปริมาณน้ำบาดาลที่พัฒนามาใช้ สน.2 (ปริมาณน้ำจากรายการ 26,29)
        /// </summary>
        public double CubicMeterGroundWaterForUse { get; set; }
    }
}