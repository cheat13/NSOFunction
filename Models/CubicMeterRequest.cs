using NSOWater.HotMigration.Models;

namespace NSOFunction.Models
{
    public class CubicMeterRequest
    {
        /// <summary>
        /// คำนวณปริมาณน้ำได้
        /// </summary>
        public StatusCompute CanCompute { get; set; }

        /// <summary>
        /// ปริมาณน้ำ
        /// </summary>
        public double CubicMeter { get; set; }
    }
}