using NSOWater.HotMigration.Models;

namespace NSOFunction.Models
{
    public class WaterFlood
    {
        /// <summary>
        /// 16.ระดับความลึกของน้ำท่วม (ในเขตที่อยู่อาศัย) สน.1
        /// </summary>
        public double AvgWaterHeightCm { get; set; }

        /// <summary>
        /// 17.ระยะเวลาที่น้ำท่วมขัง (ในเขตที่อยู่อาศัย) สน.1
        /// </summary>
        public double TimeWaterHeightCm { get; set; }
    }
}