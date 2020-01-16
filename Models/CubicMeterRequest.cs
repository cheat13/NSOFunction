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

        /// <summary>
        /// ค่าปรับแต่ง
        /// </summary>
        public bool Adjusted { get; set; }
    }
}