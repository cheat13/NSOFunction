using NSOWater.HotMigration.Models;

namespace NSOFunction.Models
{
    public class CubicMeterPlumbing
    {
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
        /// 30.1 ปริมาณการใช้น้ำประปาเพื่อการเกษตร-ประปานครหลวง
        /// </summary>
        public double CubicMeterPlumbingForAgricultureMWA { get; set; }

        /// <summary>
        /// 30.2 ปริมาณการใช้น้ำประปาเพื่อการเกษตร-ประปาส่วนภูมิภาค
        /// </summary>
        public double CubicMeterPlumbingForAgriculturePWA { get; set; }

        /// <summary>
        /// 30.3 ปริมาณการใช้น้ำประปาเพื่อการเกษตร-ประปาอื่นๆ
        /// </summary>
        public double CubicMeterPlumbingForAgricultureOther { get; set; }

        /// <summary>
        /// 30.4 ปริมาณการใช้น้ำประปาเพื่อการเกษตร
        /// </summary>
        public double CubicMeterPlumbingForAgriculture { get; set; }

        /// <summary>
        /// 31.1 ปริมาณการใช้น้ำประปาเพื่อการบริการ-ประปานครหลวง
        /// </summary>
        public double CubicMeterPlumbingForServiceMWA { get; set; }

        /// <summary>
        /// 31.2 ปริมาณการใช้น้ำประปาเพื่อการบริการ-ประปาส่วนภูมิภาค
        /// </summary>
        public double CubicMeterPlumbingForServicePWA { get; set; }

        /// <summary>
        /// 31.3 ปริมาณการใช้น้ำประปาเพื่อการบริการ-ประปาอื่นๆ
        /// </summary>
        public double CubicMeterPlumbingForServiceOther { get; set; }

        /// <summary>
        /// 31.4 ปริมาณการใช้น้ำประปาเพื่อการบริการ
        /// </summary>
        public double CubicMeterPlumbingForService { get; set; }

        /// <summary>
        /// 32.1 ปริมาณการใช้น้ำประปาเพื่อการอุตสาหกรรม-ประปานครหลวง
        /// </summary>
        public double CubicMeterPlumbingForProductMWA { get; set; }

        /// <summary>
        /// 32.2 ปริมาณการใช้น้ำประปาเพื่อการอุตสาหกรรม-ประปาส่วนภูมิภาค
        /// </summary>
        public double CubicMeterPlumbingForProductPWA { get; set; }

        /// <summary>
        /// 32.3 ปริมาณการใช้น้ำประปาเพื่อการอุตสาหกรรม-ประปาอื่นๆ
        /// </summary>
        public double CubicMeterPlumbingForProductOther { get; set; }

        /// <summary>
        /// 32.4 ปริมาณการใช้น้ำประปาเพื่อการอุตสาหกรรม
        /// </summary>
        public double CubicMeterPlumbingForProduct { get; set; }

        /// <summary>
        /// 33.1 ปริมาณการใช้น้ำประปาเพื่อการอุปโภคบริโภค-ประปานครหลวง
        /// </summary>
        public double CubicMeterPlumbingForDrinkMWA { get; set; }

        /// <summary>
        /// 33.2 ปริมาณการใช้น้ำประปาเพื่อการอุปโภคบริโภค-ประปาส่วนภูมิภาค
        /// </summary>
        public double CubicMeterPlumbingForDrinkPWA { get; set; }

        /// <summary>
        /// 33.3 ปริมาณการใช้น้ำประปาเพื่อการอุปโภคบริโภค-ประปาอื่นๆ
        /// </summary>
        public double CubicMeterPlumbingForDrinkOther { get; set; }

        /// <summary>
        /// 33.4 ปริมาณการใช้น้ำประปาเพื่อการอุปโภคบริโภค 
        /// </summary>
        public double CubicMeterPlumbingForDrink { get; set; }
    }
}