using System.Collections.Generic;
using NSOWater.HotMigration.Models;

namespace NSOFunction.Models
{
    public class DataProcessed
    {
        public string SampleId { get; set; }
        public string SampleType { get; set; }
        public string EA { get; set; }
        public string Area_Code { get; set; }
        public StatusProcessed Status { get; set; }

        #region GroundWater
        /// <summary>
        /// จำนวนบ่อบาดาลทั้งหมดในตำบล
        /// </summary>
        public int Count { get; set; }

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
        #endregion

        #region Plumbing
        /// <summary>
        /// ใช้น้ำประปานครหลวง
        /// </summary>
        public bool DoingMWA { get; set; }

        /// <summary>
        /// ปริมาณน้ำประปานครหลวง ลบ.ม./เดือน 
        /// </summary>
        public double CubicMeterPerMonthMWA { get; set; }

        /// <summary>
        /// ค่าน้้าประปานครหลวง
        /// </summary>
        public double WaterBillMWA { get; set; }

        /// <summary>
        /// ขุ่น/ตะกอน ประปานครหลวง
        /// </summary>
        public bool TurbidWaterMWA { get; set; }

        /// <summary>
        /// เค็ม/กร่อย ประปานครหลวง
        /// </summary>
        public bool SaltWaterMWA { get; set; }

        /// <summary>
        /// มีกลิ่น ประปานครหลวง
        /// </summary>
        public bool SmellMWA { get; set; }

        /// <summary>
        /// คราบมัน ประปานครหลวง
        /// </summary>
        public bool FilmOfOilMWA { get; set; }

        /// <summary>
        /// ฝ้าขาว ประปานครหลวง
        /// </summary>
        public bool FogWaterMWA { get; set; }

        /// <summary>
        /// น้ำกระด้าง ประปานครหลวง
        /// </summary>
        public bool HardWaterMWA { get; set; }

        /// <summary>
        /// สัดส่วนเพื่อการเกษตร ประปานครหลวง
        /// </summary>
        public double AgricultureMWA { get; set; }

        /// <summary>
        /// สัดส่วนเพื่อการบริการ ประปานครหลวง
        /// </summary>
        public double ServiceMWA { get; set; }

        /// <summary>
        /// สัดส่วนเพื่อการอุตสาหกรรม ประปานครหลวง
        /// </summary>
        public double ProductMWA { get; set; }

        /// <summary>
        /// สัดส่วนเพื่อการอุปโภคบริโภค ประปานครหลวง
        /// </summary>
        public double DrinkMWA { get; set; }

        /// <summary>
        /// สัดส่วนเพื่อรดน้้าพืชในบริเวณที่อยู่อาศัย ประปานครหลวง
        /// </summary>
        public double PlantMWA { get; set; }

        /// <summary>
        /// ใช้น้ำประปาส่วนภูมิภาค
        /// </summary>
        public bool DoingPWA { get; set; }

        /// <summary>
        /// ปริมาณน้ำประปาส่วนภูมิภาค ลบ.ม./เดือน 
        /// </summary>
        public double CubicMeterPerMonthPWA { get; set; }

        /// <summary>
        /// ค่าน้้าประปาส่วนภูมิภาค
        /// </summary>
        public double WaterBillPWA { get; set; }

        /// <summary>
        /// ขุ่น/ตะกอน ประปาส่วนภูมิภาค
        /// </summary>
        public bool TurbidWaterPWA { get; set; }

        /// <summary>
        /// เค็ม/กร่อย ประปาส่วนภูมิภาค
        /// </summary>
        public bool SaltWaterPWA { get; set; }

        /// <summary>
        /// มีกลิ่น ประปาส่วนภูมิภาค
        /// </summary>
        public bool SmellPWA { get; set; }

        /// <summary>
        /// คราบมัน ประปาส่วนภูมิภาค
        /// </summary>
        public bool FilmOfOilPWA { get; set; }

        /// <summary>
        /// ฝ้าขาว ประปาส่วนภูมิภาค
        /// </summary>
        public bool FogWaterPWA { get; set; }

        /// <summary>
        /// น้ำกระด้าง ประปาส่วนภูมิภาค
        /// </summary>
        public bool HardWaterPWA { get; set; }

        /// <summary>
        /// สัดส่วนเพื่อการเกษตร ประปาส่วนภูมิภาค
        /// </summary>
        public double AgriculturePWA { get; set; }

        /// <summary>
        /// สัดส่วนเพื่อการบริการ ประปาส่วนภูมิภาค
        /// </summary>
        public double ServicePWA { get; set; }

        /// <summary>
        /// สัดส่วนเพื่อการอุตสาหกรรม ประปาส่วนภูมิภาค
        /// </summary>
        public double ProductPWA { get; set; }

        /// <summary>
        /// สัดส่วนเพื่อการอุปโภคบริโภค ประปาส่วนภูมิภาค
        /// </summary>
        public double DrinkPWA { get; set; }

        /// <summary>
        /// สัดส่วนเพื่อรดน้้าพืชในบริเวณที่อยู่อาศัย ประปาส่วนภูมิภาค
        /// </summary>
        public double PlantPWA { get; set; }

        /// <summary>
        /// ใช้น้ำประปาอื่น ๆ
        /// </summary>
        public bool DoingOther { get; set; }

        /// <summary>
        /// ปริมาณน้ำประปาอื่น ๆ ลบ.ม./เดือน 
        /// </summary>
        public double CubicMeterPerMonthOther { get; set; }

        /// <summary>
        /// ค่าน้้าประปาอื่น ๆ
        /// </summary>
        public double WaterBillOther { get; set; }

        /// <summary>
        /// ขุ่น/ตะกอน ประปาอื่น ๆ
        /// </summary>
        public bool TurbidWaterOther { get; set; }

        /// <summary>
        /// เค็ม/กร่อย ประปาอื่น ๆ
        /// </summary>
        public bool SaltWaterOther { get; set; }

        /// <summary>
        /// มีกลิ่น ประปาอื่น ๆ
        /// </summary>
        public bool SmellOther { get; set; }

        /// <summary>
        /// คราบมัน ประปาอื่น ๆ
        /// </summary>
        public bool FilmOfOilOther { get; set; }

        /// <summary>
        /// ฝ้าขาว ประปาอื่น ๆ
        /// </summary>
        public bool FogWaterOther { get; set; }

        /// <summary>
        /// น้ำกระด้าง ประปาอื่น ๆ
        /// </summary>
        public bool HardWaterOther { get; set; }

        /// <summary>
        /// สัดส่วนเพื่อการเกษตร ประปาอื่น ๆ
        /// </summary>
        public double AgricultureOther { get; set; }

        /// <summary>
        /// สัดส่วนเพื่อการบริการ ประปาอื่น ๆ
        /// </summary>
        public double ServiceOther { get; set; }

        /// <summary>
        /// สัดส่วนเพื่อการอุตสาหกรรม ประปาอื่น ๆ
        /// </summary>
        public double ProductOther { get; set; }

        /// <summary>
        /// สัดส่วนเพื่อการอุปโภคบริโภค ประปาอื่น ๆ
        /// </summary>
        public double DrinkOther { get; set; }

        /// <summary>
        /// สัดส่วนเพื่อรดน้้าพืชในบริเวณที่อยู่อาศัย ประปาอื่น ๆ
        /// </summary>
        public double PlantOther { get; set; }

        /// <summary>
        ///  ค่าเช่ามิเตอร์คิดเดือนละเท่าไร  
        /// </summary>
        public double MeterRentalFee { get; set; }

        /// <summary>
        /// น้้าประปาราคาขายหน่วยละเท่าไร 
        /// </summary>
        public double PlumbingPrice { get; set; }
        #endregion

        #region Surface
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
        #endregion

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
        /// คำนวณปริมาณการใช้น้ำประปาเพื่อการเกษตรได้
        /// </summary>
        public StatusCompute CanComputeCubicMeterPlumbingForAgriculture { get; set; }

        /// <summary>
        /// คำนวณปริมาณการใช้น้ำประปาเพื่อการบริการได้
        /// </summary>
        public StatusCompute CanComputeCubicMeterPlumbingForService { get; set; }

        /// <summary>
        /// คำนวณปริมาณการใช้น้ำประปาเพื่อการอุตสาหกรรมได้
        /// </summary>
        public StatusCompute CanComputeCubicMeterPlumbingForProduct { get; set; }

        /// <summary>
        /// คำนวณปริมาณการใช้น้ำประปาเพื่อการอุปโภคบริโภคได้
        /// </summary>
        public StatusCompute CanComputeCubicMeterPlumbingForDrink { get; set; }

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
        /// คำนวณปริมาณการใช้น้ำบาดาลที่พัฒนามาใช้ได้
        /// </summary>
        public StatusCompute CanComputeCubicMeterGroundWaterForUse { get; set; }

        /// <summary>
        /// คำนวณปริมาณการใช้น้ำเพื่อการอุปโภคบริโภคได้
        /// </summary>
        public StatusCompute CanComputeCubicMeterForDrink { get; set; }

        /// <summary>
        /// 1.ครัวเรือนเกษตรกรรม
        /// </summary>
        public int IsAgriculture { get; set; }

        /// <summary>
        /// 2.ครัวเรือนทั้งหมด
        /// </summary>
        public int IsHouseHold { get; set; }

        /// <summary>
        /// 3.ครัวเรือนที่มีน้ำประปาคุณภาพดี 
        /// </summary>
        public int IsHouseHoldGoodPlumbing { get; set; }

        /// <summary>
        /// 4.ครัวเรือนที่มีพื้นที่เกษตรกรรมในพื้นที่ชลประทาน
        /// </summary>
        public int IsAgricultureHasIrrigationField { get; set; }

        /// <summary>
        /// 5.ครัวเรือนในเขตเมืองที่มีน้ำประปาใช้ (ในเขตเทศบาล)
        /// </summary>
        public int IsHouseHoldHasPlumbingDistrict { get; set; }

        /// <summary>
        /// 6.ครัวเรือนในชนบทที่มีน้ำประปาใช้ (นอกเขตเทศบาล)
        /// </summary>
        public int IsHouseHoldHasPlumbingCountryside { get; set; }

        /// <summary>
        /// 7.คุณภาพน้ำที่ใช้ในการผลิต (น้ำประปา ผิวดิน น้ำบาดาล)
        /// </summary>
        public int IsFactorialWaterQuality { get; set; }

        /// <summary>
        /// 8.คุณภาพน้ำที่ใช้ในภาคบริการ (น้ำประปา ผิวดิน น้ำบาดาล)
        /// </summary>
        public int IsCommercialWaterQuality { get; set; }

        /// <summary>
        /// 9.จำนวนบ่อน้ำบาดาล
        /// </summary>
        public double CountGroundWater { get; set; }

        /// <summary>
        /// 10.จำนวนประชากรทั้งหมด
        /// </summary>
        public int CountPopulation { get; set; }

        /// <summary>
        /// 11.จำนวนประชากรวัยทำงาน
        /// </summary>
        public int CountWorkingAge { get; set; }

        /// <summary>
        /// 12.โรงงานอุตสาหกรรมทั้งหมด
        /// </summary>
        public int IsFactorial { get; set; }

        /// <summary>
        /// 13.โรงงานอุตสาหกรรมที่มีระบบบำบัดน้ำเสีย
        /// </summary>
        public int IsFactorialWaterTreatment { get; set; }

        /// <summary>
        /// 14.หมู่บ้านที่มีระบบบำบัดน้ำเสีย
        /// </summary>
        public int IsCommunityWaterManagementHasWaterTreatment { get; set; }

        /// <summary>
        /// 15.พื้นที่ชลประทาน
        /// </summary>
        public double FieldCommunity { get; set; }

        /// <summary>
        /// 16.ระดับความลึกของน้ำท่วม (ในเขตที่อยู่อาศัย)
        /// </summary>
        public double AvgWaterHeightCm { get; set; }

        /// <summary>
        /// 17.ระยะเวลาที่น้ำท่วมขัง (ในเขตที่อยู่อาศัย)
        /// </summary>
        public double TimeWaterHeightCm { get; set; }

        /// <summary>
        /// 18.ระยะเวลาที่มีน้ำประปาใช้ (เดือน) 
        /// </summary>
        public int HasntPlumbing { get; set; }

        /// <summary>
        /// 19.สถานที่ราชการทั้งหมด
        /// </summary>
        public int IsGovernment { get; set; }

        /// <summary>
        /// 20.สถานที่ราชการที่มีน้ำประปาใช้
        /// </summary>
        public int IsGovernmentUsage { get; set; }

        /// <summary>
        /// 21.สถานที่ราชการที่มีน้ำประปาที่มีคุณภาพมาตรฐาน
        /// </summary>
        public int IsGovernmentWaterQuality { get; set; }

        /// <summary>
        /// 22.หมู่บ้านในพื้นที่น้ำท่วมซ้ำซากที่มีการเตือนภัยและมาตรการช่วยเหลือ
        /// </summary>
        public int CommunityNatureDisaster { get; set; }

        /// <summary>
        /// 23.แหล่งน้ำขนาดใหญ่ กลาง และเล็ก
        /// </summary>
        public double WaterSources { get; set; }

        /// <summary>
        /// 24.จำนวนโรงงานอุตสาหกรรมที่มีน้ำเสียจากระบบ
        /// </summary>
        public int IndustryHasWasteWaterTreatment { get; set; }

        /// <summary>
        /// 25.ประชากรที่อาศัยในครัวเรือนที่มีน้ำท่วม 
        /// </summary>
        public int PeopleInFloodedArea { get; set; }

        /// <summary>
        /// 26.ปริมาณการใช้น้ำบาดาลเพื่อการเกษตร(น้ำบาดาล น้ำซื้อ)
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
        /// 29.ปริมาณการใช้น้ำบาดาลเพื่อการอุปโภคบริโภค(น้ำบาดาล น้ำซื้อ)
        /// </summary>
        public double CubicMeterGroundWaterForDrink { get; set; }

        /// <summary>
        /// 30.ปริมาณการใช้น้ำประปาเพื่อการเกษตร
        /// </summary>
        public double CubicMeterPlumbingForAgriculture { get; set; }

        /// <summary>
        /// 31.ปริมาณการใช้น้ำประปาเพื่อการบริการ
        /// </summary>
        public double CubicMeterPlumbingForService { get; set; }

        /// <summary>
        /// 32.ปริมาณการใช้น้ำประปาเพื่อการอุตสาหกรรม
        /// </summary>
        public double CubicMeterPlumbingForProduct { get; set; }

        /// <summary>
        /// 33.ปริมาณการใช้น้ำประปาเพื่อการอุปโภคบริโภค 
        /// </summary>
        public double CubicMeterPlumbingForDrink { get; set; }

        /// <summary>
        /// 34.ปริมาณการใช้น้ำผิวดินเพื่อการเกษตร (สระน้ำ แม่น้ำ ชลประทาน น้ำฝนกักเก็บ)
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
        /// 37.ปริมาณการใช้น้ำผิวดินเพื่อการอุปโภคบริโภค (สระน้ำ แม่น้ำ ชลประทาน น้ำฝนกักเก็บ)
        /// </summary>
        public double CubicMeterSurfaceForDrink { get; set; }

        /// <summary>
        /// 38.ปริมาณน้ำบาดาลที่พัฒนามาใช้ (ปริมาณน้ำจากรายการ 26-29)
        /// </summary>
        public double CubicMeterGroundWaterForUse { get; set; }

        /// <summary>
        /// 39.จำนวนหมู่บ้าน/ชุมชนทั้งหมด
        /// </summary>
        public int CountCommunity { get; set; }

        /// <summary>
        /// 40.จำนวนหมู่บ้าน/ชุมชนที่มีอุทกภัย ดินโคลนถล่ม
        /// </summary>
        public int CountCommunityHasDisaster { get; set; }

        /// <summary>
        /// 41.ครัวเรือนในชนบททั้งหมด
        /// </summary>
        public int IsAllHouseHoldCountryside { get; set; }

        /// <summary>
        /// 42.ครัวเรือนในเขตเมืองทั้งหมด
        /// </summary>
        public int IsAllHouseHoldDistrict { get; set; }

        /// <summary>
        /// 43.สถานประกอบการผลิตทั้งหมด
        /// </summary>
        public int IsAllFactorial { get; set; }

        /// <summary>
        /// 44.สถานประกอบการบริการทั้งหมด
        /// </summary>
        public int IsAllCommercial { get; set; }

        /// <summary>
        /// ปริมาณการใช้น้ำเพื่อการอุปโภคบริโภค
        /// </summary>
        public double CubicMeterForDrink { get; set; }

        /// <summary>
        /// ข้อมูลตัวซ้ำ
        /// </summary>
        public bool Duplicate { get; set; }

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
        /// ค่าปรับแต่ง 34.ปริมาณการใช้น้ำผิวดินเพื่อการเกษตร (สระน้ำ แม่น้ำ ชลประทาน น้ำฝนกักเก็บ)
        /// </summary>
        public bool AdjustedCubicMeterSurfaceForAgriculture { get; set; }

        /// <summary>
        /// ค่าปรับแต่ง 35.ปริมาณการใช้น้ำผิวดินเพื่อการบริการ (สระน้ำ แม่น้ำ ชลประทาน น้ำฝนกักเก็บ)
        /// </summary>
        public bool AdjustedCubicMeterSurfaceForService { get; set; }

        /// <summary>
        /// ค่าปรับแต่ง 36.ปริมาณการใช้น้ำผิวดินเพื่อการอุตสาหกรรม (สระน้ำ แม่น้ำ ชลประทาน น้ำฝนกักเก็บ)
        /// </summary>
        public bool AdjustedCubicMeterSurfaceForProduct { get; set; }

        /// <summary>
        /// ค่าปรับแต่ง 37.ปริมาณการใช้น้ำผิวดินเพื่อการอุปโภคบริโภค (สระน้ำ แม่น้ำ ชลประทาน น้ำฝนกักเก็บ)
        /// </summary>
        public bool AdjustedCubicMeterSurfaceForDrink { get; set; }

        /// <summary>
        /// ค่าปรับแต่ง 38.ปริมาณน้ำบาดาลที่พัฒนามาใช้ (ปริมาณน้ำจากรายการ 26-29)
        /// </summary>
        public bool AdjustedCubicMeterGroundWaterForUse { get; set; }
    }
}