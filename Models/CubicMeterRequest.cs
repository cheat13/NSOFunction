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
        /// ปริมาณน้ำ MWA
        /// </summary>
        public double CubicMeterMWA { get; set; }

        /// <summary>
        /// ปริมาณน้ำ PWA
        /// </summary>
        public double CubicMeterPWA { get; set; }

        /// <summary>
        /// ปริมาณน้ำ Other
        /// </summary>
        public double CubicMeterOther { get; set; }

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