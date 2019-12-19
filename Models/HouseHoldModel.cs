using NSOWater.HotMigration.Models;

namespace NSOFunction.Models
{
    public class HouseHoldModel
    {
        /// <summary>
        /// 2.ครัวเรือนทั้งหมด
        /// </summary>
        public int IsHouseHold { get; set; }

        /// <summary>
        /// 3.ครัวเรือนที่มีน้ำประปาคุณภาพดี 
        /// </summary>
        public int IsHouseHoldGoodPlumbing { get; set; }

        /// <summary>
        /// 5.ครัวเรือนในเขตเมืองที่มีน้ำประปาใช้ (ในเขตเทศบาล)
        /// </summary>
        public int IsHouseHoldHasPlumbingDistrict { get; set; }

        /// <summary>
        /// 6.ครัวเรือนในชนบทที่มีน้ำประปาใช้ (นอกเขตเทศบาล)
        /// </summary>
        public int IsHouseHoldHasPlumbingCountryside { get; set; }
    }
}