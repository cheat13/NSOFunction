using NSOWater.HotMigration.Models;

namespace NSOFunction.Models
{
    public class PumpRequest
    {
        /// <summary>
        /// จำนวนเครื่องสูบน้ำ
        /// </summary>
        public int PumpCount { get; set; }

        /// <summary>
        /// จำนวนเครื่องสูบน้ำอัตโนมัติ
        /// </summary>
        public int PumpAuto { get; set; }
    }
}