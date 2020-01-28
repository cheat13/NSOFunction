using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSOWater.HotMigration.Models
{
    /// <summary>
    /// สำมะโนประชากร
    /// </summary>
    public class Population
    {
        /// <summary>
        /// ครัวเรือนนี้เป็นครัวเรือนสถาบันหรือไม่
        /// </summary>
        public bool? Skip { get; set; }
        
        /// <summary>
        /// จำนวนผู้อยู่อาศัย
        /// </summary>
        public int? PersonCount { get; set; }
        
        /// <summary>
        /// จำนวนทั้งหมด
        /// </summary>
        public int? AllPersonCount { get; set; }
        
        /// <summary>
        /// ชาย
        /// </summary>
        public int? MalePerson { get; set; }
        
        /// <summary>
        /// หญิง
        /// </summary>
        public int? FemalePerson { get; set; }

        /// <summary>
        /// ข้อมูลบุคคล
        /// </summary>
        public List<Person> Persons { get; set; }

    }
}