using NSOWater.HotMigration.Models;

namespace NSOFunction.Models
{
    public class PlumbingServiceUsage
    {
        /// <summary>
        /// 19.สถานที่ราชการทั้งหมด
        /// </summary>
        public int IsGovernment { get; set; }

        /// <summary>
        /// 20.สถานที่ราชการที่มีน้ำประปาใช้
        /// </summary>
        public int IsGovernmentUsage { get; set; }

        /// <summary>
        /// 21.สถานที่ราชการที่มีน้ำประปาที่มีคุณภาพมาตรฐาน
        /// </summary>
        public int IsGovernmentWaterQuality { get; set; }
    }
}