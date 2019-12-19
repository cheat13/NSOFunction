using System.Collections.Generic;
using NSOWater.HotMigration.Models;

namespace NSOFunction.Models
{
    public class CubicMeterSurface
    {
        public string Area_Code { get; set; }

        /// <summary>
        /// จำนวนสระน้ำทั้งหมด
        /// </summary>
        public int PoolCount { get; set; }

        /// <summary>
        /// จำนวนสระน้ำในตำบล
        /// </summary>
        public int WaterResourceCount { get; set; }

         /// <summary>
        /// ปริมาณน้ำสระน้ำ ลบ.ม./เดือน 
        /// </summary>
        public double CubicMeterPerMonthPool { get; set; }

        /// <summary>
        /// จำนวนเครื่องสูบน้ำสำหรับสระน้ำ
        /// </summary>
        public int PumpCountPool { get; set; }

        /// <summary>
        /// จำนวนเครื่องสูบน้ำอัตโนมัติสำหรับสระน้ำ
        /// </summary>
        public int PumpAutoPool { get; set; }
        
        /// <summary>
        /// สระน้ำ เค็ม/กร่อย  
        /// </summary>
        public bool SaltWaterPool { get; set; }

        /// <summary>
        /// สระน้ำ มีกลิ่น
        /// /// </summary>
        public bool SmellPool { get; set; }

        /// <summary>
        /// สระน้ำ คราบมัน
        /// </summary>
        public bool FilmOfOilPool { get; set; }

        /// <summary>
        /// สระน้ำ ฝ้าขาว
        /// </summary>
        public bool FogWaterPool { get; set; }

        /// <summary>
        /// สัดส่วนสระน้ำเพื่อการเกษตร
        /// </summary>
        public double AgriculturePool { get; set; }

        /// <summary>
        /// สัดส่วนสระน้ำเพื่อการบริการ
        /// </summary>
        public double ServicePool { get; set; }

        /// <summary>
        /// สัดส่วนสระน้ำเพื่อการอุตสาหกรรม
        /// </summary>
        public double ProductPool { get; set; }

        /// <summary>
        /// สัดส่วนสระน้ำเพื่อการอุปโภคบริโภค
        /// </summary>
        public double DrinkPool { get; set; }

        /// <summary>
        /// สัดส่วนสระน้ำเพื่อรดน้้าพืชในบริเวณที่อยู่อาศัย
        /// </summary>
        public double PlantPool { get; set; }

        /// <summary>
        /// สัดส่วนสระน้ำเพื่อการทำนา
        /// </summary>
        public double FarmPool { get; set; }

        /// <summary>
        /// ปริมาณน้ำชลประทาน ลบ.ม./เดือน
        /// </summary>
        public double CubicMeterPerMonthIrrigation { get; set; }

        /// <summary>
        /// จำนวนเครื่องสูบน้ำสำหรับชลประทาน
        /// </summary>
        public int PumpCountIrrigation { get; set; }

        /// <summary>
        /// จำนวนเครื่องสูบน้ำอัตโนมัติสำหรับชลประทาน
        /// </summary>
        public int PumpAutoIrrigation { get; set; }

        /// <summary>
        /// ชลประทาน เค็ม/กร่อย  
        /// </summary>
        public bool SaltWaterIrrigation { get; set; }

        /// <summary>
        /// ชลประทาน มีกลิ่น
        /// </summary>
        public bool SmellIrrigation { get; set; }

        /// <summary>
        /// ชลประทาน คราบมัน
        /// </summary>
        public bool FilmOfOilIrrigation { get; set; }

        /// <summary>
        /// ชลประทาน ฝ้าขาว
        /// </summary>
        public bool FogWaterIrrigation { get; set; }

        /// <summary>
        /// สัดส่วนชลประทานเพื่อการเกษตร
        /// </summary>
        public double AgricultureIrrigation { get; set; }

        /// <summary>
        /// สัดส่วนชลประทานเพื่อการบริการ
        /// </summary>
        public double ServiceIrrigation { get; set; }

        /// <summary>
        /// สัดส่วนชลประทานเพื่อการอุตสาหกรรม
        /// </summary>
        public double ProductIrrigation { get; set; }

        /// <summary>
        /// สัดส่วนชลประทานเพื่อการอุปโภคบริโภค
        /// </summary>
        public double DrinkIrrigation { get; set; }

        /// <summary>
        /// สัดส่วนชลประทานเพื่อรดน้้าพืชในบริเวณที่อยู่อาศัย
        /// </summary>
        public double PlantIrrigation { get; set; }

        /// <summary>
        /// สัดส่วนชลประทานเพื่อการทำนา
        /// </summary>
        public double FarmIrrigation { get; set; }

        /// <summary>
        /// จำนวนเครื่องสูบน้ำสำหรับแม่น้ำ
        /// </summary>
        public int PumpCountRiver { get; set; }

        /// <summary>
        /// จำนวนเครื่องสูบน้ำอัตโนมัติสำหรับแม่น้ำ
        /// </summary>
        public int PumpAutoRiver { get; set; }

        /// <summary>
        /// แม่น้ำ เค็ม/กร่อย  
        /// </summary>
        public bool SaltWaterRiver { get; set; }

        /// <summary>
        /// แม่น้ำ มีกลิ่น
        /// </summary>
        public bool SmellRiver { get; set; }

        /// <summary>
        /// แม่น้ำ คราบมัน
        /// </summary>
        public bool FilmOfOilRiver { get; set; }

        /// <summary>
        /// แม่น้ำ ฝ้าขาว
        /// </summary>
        public bool FogWaterRiver { get; set; }
        
        /// <summary>
        /// สัดส่วนแม่น้ำเพื่อการเกษตร
        /// </summary>
        public double AgricultureRiver { get; set; }

        /// <summary>
        /// สัดส่วนแม่น้ำเพื่อการบริการ
        /// </summary>
        public double ServiceRiver { get; set; }

        /// <summary>
        /// สัดส่วนแม่น้ำเพื่อการอุตสาหกรรม
        /// </summary>
        public double ProductRiver { get; set; }

        /// <summary>
        /// สัดส่วนแม่น้ำเพื่อการอุปโภคบริโภค
        /// </summary>
        public double DrinkRiver { get; set; }

        /// <summary>
        /// สัดส่วนแม่น้ำเพื่อรดน้้าพืชในบริเวณที่อยู่อาศัย
        /// </summary>
        public double PlantRiver { get; set; }

        /// <summary>
        /// สัดส่วนแม่น้ำเพื่อการทำนา
        /// </summary>
        public double FarmRiver { get; set; }

        /// <summary>
        /// สัดส่วนน้ำฝนกักเก็บเพื่อการเกษตร
        /// </summary>
        public double AgricultureRain { get; set; }

        /// <summary>
        /// สัดส่วนน้ำฝนกักเก็บเพื่อการบริการ
        /// </summary>
        public double ServiceRain { get; set; }

        /// <summary>
        /// สัดส่วนน้ำฝนกักเก็บเพื่อการอุตสาหกรรม
        /// </summary>
        public double ProductRain { get; set; }

        /// <summary>
        /// สัดส่วนน้ำฝนกักเก็บเพื่อการอุปโภคบริโภค
        /// </summary>
        public double DrinkRain { get; set; }

        /// <summary>
        /// สัดส่วนน้ำฝนกักเก็บเพื่อรดน้้าพืชในบริเวณที่อยู่อาศัย
        /// </summary>
        public double PlantRain { get; set; }
        
        /// <summary>
        /// คำนวณปริมาณการใช้น้ำผิวดินเพื่อการเกษตรได้
        /// </summary>
        public StatusCompute CanComputeCubicMeterSurfaceForAgriculture { get; set; }

        /// <summary>
        /// คำนวณปริมาณการใช้น้ำผิวดินเพื่อการบริการได้
        /// </summary>
        public StatusCompute CanComputeCubicMeterSurfaceForService { get; set; }

        /// <summary>
        /// คำนวณปริมาณการใช้น้ำผิวดินเพื่อการอุตสาหกรรมได้
        /// </summary>
        public StatusCompute CanComputeCubicMeterSurfaceForProduct { get; set; }

        /// <summary>
        /// คำนวณปริมาณการใช้น้ำผิวดินเพื่อการอุปโภคบริโภคได้
        /// </summary>
        public StatusCompute CanComputeCubicMeterSurfaceForDrink { get; set; }

        /// <summary>
        /// 34.ปริมาณการใช้น้ำผิวดินเพื่อการเกษตร (สระน้ำ แม่น้ำ ชลประทาน น้ำฝนกักเก็บ) สน.1
        /// </summary>
        public double CubicMeterSurfaceForAgriculture { get; set; }

        /// <summary>
        /// 35.ปริมาณการใช้น้ำผิวดินเพื่อการบริการ (สระน้ำ แม่น้ำ ชลประทาน น้ำฝนกักเก็บ)
        /// </summary>
        public double CubicMeterSurfaceForService { get; set; }

        /// <summary>
        /// 36.ปริมาณการใช้น้ำผิวดินเพื่อการอุตสาหกรรม (สระน้ำ แม่น้ำ ชลประทาน น้ำฝนกักเก็บ)
        /// </summary>
        public double CubicMeterSurfaceForProduct { get; set; }

        /// <summary>
        /// 37.ปริมาณการใช้น้ำผิวดินเพื่อการอุปโภคบริโภค (สระน้ำ แม่น้ำ ชลประทาน น้ำฝนกักเก็บ) สน.1
        /// </summary>
        public double CubicMeterSurfaceForDrink { get; set; }
    }
}