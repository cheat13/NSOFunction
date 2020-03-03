using NSOWater.HotMigration.Models;

namespace NSOFunction.Models
{
    public class CubicMeterSurfaceRequest
    {
        /// <summary>
        /// คำนวณปริมาณน้ำได้
        /// </summary>
        public StatusCompute CanCompute { get; set; }

        /// <summary>
        /// ปริมาณน้ำ สระน้ำ
        /// </summary>
        public double CubicMeterPool { get; set; }

        /// <summary>
        /// ปริมาณน้ำ แม่น้ำ
        /// </summary>
        public double CubicMeterRiver { get; set; }

        /// <summary>
        /// ปริมาณน้ำ ชลประทาน
        /// </summary>
        public double CubicMeterIrrigation { get; set; }

        /// <summary>
        /// ปริมาณน้ำ น้ำฝน
        /// </summary>
        public double CubicMeterRain { get; set; }

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