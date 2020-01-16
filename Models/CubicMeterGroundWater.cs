using System.Collections.Generic;
using NSOWater.HotMigration.Models;

namespace NSOFunction.Models
{
    public class CubicMeterGroundWaterModel
    {
        public string Area_Code { get; set; }

        /// <summary>
        /// จำนวนบ่อบาดาลทั้งหมดในตำบล
        /// </summary>
        public int GroundWaterCount { get; set; }

        /// <summary>
        /// ปริมาณน้ำ ลบ.ม./เดือน 
        /// </summary>
        public double CubicMeterPerMonth { get; set; }

        /// <summary>
        /// ค่าน้ำ
        /// </summary>
        public double WaterBill { get; set; }

        /// <summary>
        /// จำนวนเครื่องสูบน้ำ
        /// </summary>
        public int PumpCount { get; set; }

        /// <summary>
        /// จำนวนเครื่องสูบน้ำอัตโนมัติ
        /// </summary>
        public int PumpAuto { get; set; }

        /// <summary>
        /// ขุ่น/ตะกอน  
        /// </summary>
        public bool TurbidWater { get; set; }

        /// <summary>
        /// เค็ม/กร่อย  
        /// </summary>
        public bool SaltWater { get; set; }

        /// <summary>
        /// มีกลิ่น
        /// /// </summary>
        public bool Smell { get; set; }

        /// <summary>
        /// คราบมัน
        /// </summary>
        public bool FilmOfOil { get; set; }

        /// <summary>
        /// ฝ้าขาว
        /// </summary>
        public bool FogWater { get; set; }

        /// <summary>
        /// น้ำกระด้าง
        /// </summary>
        public bool HardWater { get; set; }

        /// <summary>
        /// สัดส่วนเพื่อการเกษตร
        /// </summary>
        public double Agriculture { get; set; }

        /// <summary>
        /// สัดส่วนเพื่อการบริการ
        /// </summary>
        public double Service { get; set; }

        /// <summary>
        /// สัดส่วนเพื่อการอุตสาหกรรม
        /// </summary>
        public double Product { get; set; }

        /// <summary>
        /// สัดส่วนเพื่อการอุปโภคบริโภค
        /// </summary>
        public double Drink { get; set; }

        /// <summary>
        /// สัดส่วนเพื่อรดน้้าพืชในบริเวณที่อยู่อาศัย
        /// </summary>
        public double Plant { get; set; }

        /// <summary>
        /// สัดส่วนเพื่อการทำนา
        /// </summary>
        public double Farm { get; set; }

        /// <summary>
        /// ปริมาณน้ำซื้อที่ใช้เพื่อการเกษตร
        /// </summary>
        /// <value></value>
        public double CubicMeterBuyingForAgriculture { get; set; }

        /// <summary>
        /// ปริมาณน้ำซื้อที่ใช้เพื่อการบริการ
        /// </summary>
        /// <value></value>
        public double CubicMeterBuyingForService { get; set; }

        /// <summary>
        /// ปริมาณน้ำซื้อที่ใช้เพื่อการอุตสาหกรรม
        /// </summary>
        /// /// <value></value>
        public double CubicMeterBuyingForProduct { get; set; }

        /// <summary>
        /// ปริมาณน้ำซื้อที่ใช้เพื่อการอุปโภคบริโภค
        /// </summary>
        /// <value></value>
        public double CubicMeterBuyingForDrink { get; set; }

        /// <summary>
        /// คำนวณปริมาณการใช้น้ำบาดาลเพื่อการเกษตรได้
        /// </summary>
        public StatusCompute CanComputeCubicMeterGroundWaterForAgriculture { get; set; }

        /// <summary>
        /// คำนวณปริมาณการใช้น้ำบาดาลเพื่อการบริการได้
        /// </summary>
        public StatusCompute CanComputeCubicMeterGroundWaterForService { get; set; }

        /// <summary>
        /// คำนวณปริมาณการใช้น้ำบาดาลเพื่อการอุตสาหกรรมได้
        /// </summary>
        public StatusCompute CanComputeCubicMeterGroundWaterForProduct { get; set; }

        /// <summary>
        /// คำนวณปริมาณการใช้น้ำบาดาลเพื่อการอุปโภคบริโภคได้
        /// </summary>
        public StatusCompute CanComputeCubicMeterGroundWaterForDrink { get; set; }

        /// <summary>
        /// คำนวณปริมาณการใช้น้ำบาดาลที่พัฒนามาใช้ได้
        /// </summary>
        public StatusCompute CanComputeCubicMeterGroundWaterForUse { get; set; }

        /// <summary>
        /// 26.ปริมาณการใช้น้ำบาดาลเพื่อการเกษตร(น้ำบาดาล น้ำซื้อ) สน.1
        /// </summary>
        public double CubicMeterGroundWaterForAgriculture { get; set; }

        /// <summary>
        /// 27.ปริมาณการใช้น้ำบาดาลเพื่อการบริการ(น้ำบาดาล น้ำซื้อ)
        /// </summary>
        public double CubicMeterGroundWaterForService { get; set; }

        /// <summary>
        /// 28.ปริมาณการใช้น้ำบาดาลเพื่อการอุตสาหกรรม(น้ำบาดาล น้ำซื้อ)
        /// </summary>
        public double CubicMeterGroundWaterForProduct { get; set; }

        /// <summary>
        /// 29.ปริมาณการใช้น้ำบาดาลเพื่อการอุปโภคบริโภค(น้ำบาดาล น้ำซื้อ) สน.1
        /// </summary>
        public double CubicMeterGroundWaterForDrink { get; set; }

        /// <summary>
        /// 38.ปริมาณน้ำบาดาลที่พัฒนามาใช้ สน.1 (ปริมาณน้ำจากรายการ 26-29)
        /// </summary>
        public double CubicMeterGroundWaterForUse { get; set; }

        /// <summary>
        /// ค่าปรับแต่ง 26.ปริมาณการใช้น้ำบาดาลเพื่อการเกษตร(น้ำบาดาล น้ำซื้อ)
        /// </summary>
        public bool AdjustedCubicMeterGroundWaterForAgriculture { get; set; }

        /// <summary>
        /// ค่าปรับแต่ง 27.ปริมาณการใช้น้ำบาดาลเพื่อการบริการ(น้ำบาดาล น้ำซื้อ)
        /// </summary>
        public bool AdjustedCubicMeterGroundWaterForService { get; set; }

        /// <summary>
        /// ค่าปรับแต่ง 28.ปริมาณการใช้น้ำบาดาลเพื่อการอุตสาหกรรม(น้ำบาดาล น้ำซื้อ)
        /// </summary>
        public bool AdjustedCubicMeterGroundWaterForProduct { get; set; }

        /// <summary>
        /// ค่าปรับแต่ง 29.ปริมาณการใช้น้ำบาดาลเพื่อการอุปโภคบริโภค(น้ำบาดาล น้ำซื้อ)
        /// </summary>
        public bool AdjustedCubicMeterGroundWaterForDrink { get; set; }

        /// <summary>
        /// ค่าปรับแต่ง 38.ปริมาณน้ำบาดาลที่พัฒนามาใช้ (ปริมาณน้ำจากรายการ 26-29)
        /// </summary>
        public bool AdjustedCubicMeterGroundWaterForUse { get; set; }
    }
}
