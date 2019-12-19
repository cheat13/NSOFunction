using NSOWater.HotMigration.Models;

namespace NSOFunction.Models
{
    public class PopulationCount
    {
        /// <summary>
        /// 10.จำนวนประชากรทั้งหมด
        /// </summary>
        public int countPopulation { get; set; }

        /// <summary>
        /// 11.จำนวนประชากรวัยทำงาน
        /// </summary>
        public int countWorkingAge { get; set; }
    }
}