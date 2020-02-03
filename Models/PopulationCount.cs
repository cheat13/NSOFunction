using NSOWater.HotMigration.Models;

namespace NSOFunction.Models
{
    public class PopulationCount
    {
        /// <summary>
        /// 10.จำนวนประชากรทั้งหมด
        /// </summary>
        public int CountPopulation { get; set; }

        /// <summary>
        /// 11.จำนวนประชากรวัยทำงาน
        /// </summary>
        public int CountWorkingAge { get; set; }
        
        /// <summary>
        /// ครัวเรือนนี้เป็นครัวเรือนสถาบันหรือไม่
        /// </summary>
        public string Skip { get; set; }

        /// <summary>
        /// จำนวนประชากรทั้งหมดจาก Residential
        /// </summary>
        public double ResidentialPersonCount { get; set; }

        /// <summary>
        /// จำนวนประชากรทั้งหมดจาก Population
        /// </summary>
        public double PopulationPersonCount { get; set; }
    }
}