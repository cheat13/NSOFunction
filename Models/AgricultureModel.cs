using NSOWater.HotMigration.Models;

namespace NSOFunction.Models
{
    public class AgricultureModel
    {
        /// <summary>
        /// 1.ครัวเรือนเกษตรกรรม
        /// </summary>
        public int IsAgriculture { get; set; }

        /// <summary>
        /// 4.ครัวเรือนที่มีพื้นที่เกษตรกรรมในพื้นที่ชลประทาน
        /// </summary>
        public int IsAgricultureHasIrrigationField { get; set; }
    }
}