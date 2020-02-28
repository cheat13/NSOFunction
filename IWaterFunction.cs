using System.Collections.Generic;
using NSOFunction.Models;
using NSOWater.HotMigration.Models;

public interface IWaterFunction
{
    /// <summary>
    /// 1.ครัวเรือนเกษตรกรรม
    /// 4.ครัวเรือนที่มีพื้นที่เกษตรกรรมในพื้นที่ชลประทาน
    /// </summary>
    AgricultureModel Agriculture(bool? isAgriculture, Agriculture agriculture);

    /// <summary>
    /// 2.ครัวเรือนทั้งหมด
    /// 3.ครัวเรือนที่มีน้ำประปาคุณภาพดี 
    /// 5.ครัวเรือนในเขตเมืองที่มีน้ำประปาใช้ (ในเขตเทศบาล)
    /// 6.ครัวเรือนในชนบทที่มีน้ำประปาใช้ (นอกเขตเทศบาล)
    /// </summary>
    HouseHoldModel IsHouseHoldGoodPlumbing(bool? isHouseHold, string EA, Plumbing plumbing, WaterSources waterSources);

    /// <summary>
    /// 7.คุณภาพน้ำที่ใช้ในการผลิต (น้ำประปา ผิวดิน น้ำบาดาล)
    /// </summary>
    int IsFactorialWaterQuality(bool? IsFactorial, WaterSources waterSources, Plumbing plumbing, GroundWater groundWater, River river, Pool pool, Irrigation irrigation);

    /// <summary>
    /// 8.คุณภาพน้ำที่ใช้ในภาคบริการ (น้ำประปา ผิวดิน น้ำบาดาล)
    /// </summary>
    int IsCommercialWaterQuality(bool? IsCommercial, WaterSources waterSources, Plumbing plumbing, GroundWater groundWater, River river, Pool pool, Irrigation irrigation);

    /// <summary>
    /// 12.โรงงานอุตสาหกรรมทั้งหมด
    /// </summary>
    int IsFactorial(bool? IsFactorial, int? WorkersCount, bool? HeavyMachine);

    /// <summary>
    /// 13.โรงงานอุตสาหกรรมที่มีระบบบำบัดน้ำเสีย
    /// </summary>
    int IsFactorialWaterTreatment(bool? IsFactorial, int? WorkersCount, bool? HeavyMachine, bool? HasWasteWaterFromProduction, bool? HasWasteWaterTreatment);

    /// <summary>
    /// 14.หมู่บ้านที่มีระบบบำบัดน้ำเสีย
    /// </summary>
    int IsCommunityWaterManagementHasWaterTreatment(bool? HasWaterTreatment);

    /// <summary>
    /// 9.จำนวนบ่อน้ำบาดาล สน.1
    /// </summary>
    List<CountGroundWaterHouseHold> CountGroundWaterHouseHold(string ea, GroundWater groundWater);

    /// <summary>
    /// 9.จำนวนบ่อน้ำบาดาล สน.2
    /// </summary>
    double CountGroundWaterCommunity(CommunityWaterManagement Management, ManagementForFarming CommunityProject);

    /// <summary>
    /// 10.จำนวนประชากรทั้งหมด
    /// 11.จำนวนประชากรวัยทำงาน
    /// </summary>
    PopulationCount CountPopulation(bool? IsHouseHold, Population Population, Residential Residence);

    /// <summary>
    /// 15.พื้นที่ชลประทาน สน.2
    /// </summary>
    double FieldCommunity(bool? Doing, List<DetailManagementForFarming> Details);

    /// <summary>
    /// 16.ระดับความลึกของน้ำท่วม (ในเขตที่อยู่อาศัย) สน.1
    /// 17.ระยะเวลาที่น้ำท่วมขัง (ในเขตที่อยู่อาศัย) สน.1
    /// </summary>
    WaterFlood Disasterous(Disasterous Disaster, bool? IsHouseHold);

    /// <summary>
    /// 18.ระยะเวลาที่มีน้ำประปาใช้ (เดือน) สน.1
    /// </summary>
    int HasntPlumbing(Plumbing Plumbing);

    /// <summary>
    /// 19.สถานที่ราชการทั้งหมด
    /// 20.สถานที่ราชการที่มีน้ำประปาใช้
    /// 21.สถานที่ราชการที่มีน้ำประปาที่มีคุณภาพมาตรฐาน
    /// </summary>
    PlumbingServiceUsage PlumbingSeviceUsage(BuildingType? BuildingType, Commercial Commerce, Plumbing Plumbing);

    /// <summary>
    /// 22.หมู่บ้านในพื้นที่น้ำท่วมซ้ำซากที่มีการเตือนภัยและมาตรการช่วยเหลือ
    /// </summary>
    int CommunityNatureDisaster(bool? HasDisaster, bool? HasDisasterWarning);

    /// <summary>
    /// 23.แหล่งน้ำขนาดใหญ่ กลาง และเล็ก สน.1
    /// </summary>
    List<WaterPoolHouseHold> WaterSourcesHouseHold(string ea, bool? IsHouseHold, Pool Pools);

    /// <summary>
    /// 23.แหล่งน้ำขนาดใหญ่ กลาง และเล็ก สน.2
    /// </summary>
    double WaterSourcesCommunity(string ea, List<DetailWaterManagement> details);

    /// <summary>
    /// 24.จำนวนโรงงานอุตสาหกรรมที่มีน้ำเสียจากระบบ
    /// </summary>
    int IndustryHasWasteWaterTreatment(bool? IsFactorial, Factorial Factory);

    /// <summary>
    /// 25.ประชากรที่อาศัยในครัวเรือนที่มีน้ำท่วม สน.1
    /// </summary>
    int PeopleInFloodedArea(bool? IsHouseHold, bool? Flooded, Population Population, Residential Residence); // ???

    /// <summary>
    /// 26.1 ปริมาณการใช้น้ำบาดาลเพื่อการเกษตร-น้ำบาดาล 
    /// </summary>
    List<CubicMeterGroundWaterModel> CubicMeterGroundWaterForAgriculture();

    /// <summary>
    /// 26.2 ปริมาณการใช้น้ำบาดาลเพื่อการเกษตร- น้ำซื้อ
    /// </summary>
    List<CubicMeterGroundWaterModel> CubicMeterGroundWaterForAgricultureBuying();

    /// <summary>
    /// 27.1 ปริมาณการใช้น้ำบาดาลเพื่อการบริการ-น้ำบาดาล 
    /// </summary>
    List<CubicMeterGroundWaterModel> CubicMeterGroundWaterForService();

    /// <summary>
    /// 27.2 ปริมาณการใช้น้ำบาดาลเพื่อการบริการ-น้ำซื้อ
    /// </summary>
    List<CubicMeterGroundWaterModel> CubicMeterGroundWaterForServiceBuying();

    /// <summary>
    /// 28.1 ปริมาณการใช้น้ำบาดาลเพื่อการอุตสาหกรรม-น้ำบาดาล  
    /// </summary>
    List<CubicMeterGroundWaterModel> CubicMeterGroundWaterForProduct();

    /// <summary>
    /// 28.2 ปริมาณการใช้น้ำบาดาลเพื่อการอุตสาหกรรม--น้ำซื้อ
    /// </summary>
    List<CubicMeterGroundWaterModel> CubicMeterGroundWaterForProductBuying();

    /// <summary>
    /// 29.1 ปริมาณการใช้น้ำบาดาลเพื่อการอุปโภคบริโภค-น้ำบาดาล 
    /// </summary>
    List<CubicMeterGroundWaterModel> CubicMeterGroundWaterForDrink();

    /// <summary>
    /// 29.2 ปริมาณการใช้น้ำบาดาลเพื่อการอุปโภคบริโภค- น้ำซื้อ
    /// </summary>
    List<CubicMeterGroundWaterModel> CubicMeterGroundWaterForDrinkBuying();

    /// <summary>
    /// 26-29,38 ปริมาณการใช้น้ำบาดาล
    /// </summary>
    List<CubicMeterGroundWaterModel> CubicMeterGroundWater(string ea, bool? isAgriculture, bool? isCommercial, bool? isFactorial, bool? isHouseHold, Agriculture agriculture, Commercial commercial, Factorial factorial, Residential residential, GroundWater groundWater, Buying buying, BuildingType? buildingType);

    /// <summary>
    /// 26,29,38.ปริมาณการใช้น้ำบาดาล สน.2
    /// </summary>
    CubicMeterGroundWaterCommunity CubicMeterGroundWater(ManagementForFarming managementForFarming, CommunityWaterManagement communityWaterManagement);

    /// <summary>
    /// 30.1 ปริมาณการใช้น้ำประปาเพื่อการเกษตร-ประปานครหลวง
    /// </summary>
    CubicMeterPlumbing CubicMeterPlumbingDoingMWAForAgriculture();

    /// <summary>
    /// 30.2 ปริมาณการใช้น้ำประปาเพื่อการเกษตร-ประปาส่วนภูมิภาค
    /// </summary>
    CubicMeterPlumbing CubicMeterPlumbingDoingPWAForAgriculture();

    /// <summary>
    /// 30.3 ปริมาณการใช้น้ำประปาเพื่อการเกษตร-ประปาอื่นๆ
    /// </summary>
    CubicMeterPlumbing CubicMeterPlumbingDoingOtherForAgriculture();

    /// <summary>
    /// 31.1 ปริมาณการใช้น้ำประปาเพื่อการบริการ-ประปานครหลวง
    /// </summary>
    CubicMeterPlumbing CubicMeterPlumbingDoingMWAForService();

    /// <summary>
    /// 31.2 ปริมาณการใช้น้ำประปาเพื่อการบริการ-ประปาส่วนภูมิภาค
    /// </summary>
    CubicMeterPlumbing CubicMeterPlumbingDoingPWAForService();

    /// <summary>
    /// 31.3 ปริมาณการใช้น้ำประปาเพื่อการบริการ-ประปาอื่นๆ
    /// </summary>
    CubicMeterPlumbing CubicMeterPlumbingDoingOtherForService();

    // <summary>
    /// 32.1 ปริมาณการใช้น้ำประปาเพื่อการอุตสาหกรรม-ประปานครหลวง
    /// </summary>
    CubicMeterPlumbing CubicMeterPlumbingDoingMWAForProduct();

    /// <summary>
    /// 32.2 ปริมาณการใช้น้ำประปาเพื่อการอุตสาหกรรม-ประปาส่วนภูมิภาค
    /// </summary>
    CubicMeterPlumbing CubicMeterPlumbingDoingPWAForProduct();

    /// <summary>
    /// 32.3 ปริมาณการใช้น้ำประปาเพื่อการอุตสาหกรรม-ประปาอื่นๆ
    /// </summary>
    CubicMeterPlumbing CubicMeterPlumbingDoingOtherForProduct();

    // <summary>
    /// 33.1 ปริมาณการใช้น้ำประปาเพื่อการอุปโภคบริโภค-ประปานครหลวง
    /// </summary>
    CubicMeterPlumbing CubicMeterPlumbingDoingMWAForDrink();

    /// <summary>
    /// 33.2 ปริมาณการใช้น้ำประปาเพื่อการอุปโภคบริโภค-ประปาส่วนภูมิภาค 
    /// </summary>
    CubicMeterPlumbing CubicMeterPlumbingDoingPWAForDrink();

    /// <summary>
    /// 33.3 ปริมาณการใช้น้ำประปาเพื่อการอุปโภคบริโภค-ประปาอื่นๆ
    /// </summary>
    CubicMeterPlumbing CubicMeterPlumbingDoingOtherForDrink();

    /// <summary>
    /// 30-33 ปริมาณการใช้น้ำประปา
    /// </summary>
    CubicMeterPlumbing CubicMeterPlumbing(bool? isAgriculture, bool? isCommercial, bool? isFactorial, bool? isHouseHold, Agriculture agriculture, Commercial commercial, Factorial factorial, Residential residential, Plumbing plumbing, BuildingType? buildingType, List<DetailOrgWaterSupply> waterServices);

    // <summary>
    /// 34.1 ปริมาณการใช้น้ำผิวดินเพื่อการเกษตร -สระน้ำ 
    /// </summary>
    List<CubicMeterSurface> CubicMeterSurfacePoolForAgriculture();

    /// <summary>
    /// 34.2 ปริมาณการใช้น้ำผิวดินเพื่อการเกษตร - แม่น้ำ 
    /// </summary>
    List<CubicMeterSurface> CubicMeterSurfaceRiverForAgriculture();

    /// <summary>
    /// 34.3 ปริมาณการใช้น้ำผิวดินเพื่อการเกษตร -ชลประทาน 
    /// </summary>
    List<CubicMeterSurface> CubicMeterSurfaceIrrigationForAgriculture();

    /// <summary>
    /// 34.4 ปริมาณการใช้น้ำผิวดินเพื่อการเกษตร - น้ำฝนกักเก็บ
    /// </summary>
    List<CubicMeterSurface> CubicMeterSurfaceRainForAgriculture();

    // <summary>
    /// 35.1 ปริมาณการใช้น้ำผิวดินเพื่อการบริการ-สระน้ำ
    /// </summary>
    List<CubicMeterSurface> CubicMeterSurfacePoolForService();

    /// <summary>
    /// 35.2 ปริมาณการใช้น้ำผิวดินเพื่อการบริการ - แม่น้ำ 
    /// </summary>
    List<CubicMeterSurface> CubicMeterSurfaceRiverForService();

    /// <summary>
    /// 35.3 ปริมาณการใช้น้ำผิวดินเพื่อการบริการ -ชลประทาน 
    /// </summary>
    List<CubicMeterSurface> CubicMeterSurfaceIrrigationForService();

    /// <summary>
    /// 35.4 ปริมาณการใช้น้ำผิวดินเพื่อการบริการ - น้ำฝนกักเก็บ
    /// </summary>
    List<CubicMeterSurface> CubicMeterSurfaceRainForService();

    // <summary>
    /// 36.1 ปริมาณการใช้น้ำผิวดินเพื่อการอุตสาหกรรม -สระน้ำ 
    /// </summary>
    List<CubicMeterSurface> CubicMeterSurfacePoolForProduct();

    /// <summary>
    /// 36.2 ปริมาณการใช้น้ำผิวดินเพื่อการอุตสาหกรรม - แม่น้ำ  
    /// </summary>
    List<CubicMeterSurface> CubicMeterSurfaceRiverForProduct();

    /// <summary>
    /// 36.3 ปริมาณการใช้น้ำผิวดินเพื่อการอุตสาหกรรม  -ชลประทาน 
    /// </summary>
    List<CubicMeterSurface> CubicMeterSurfaceIrrigationForProduct();

    /// <summary>
    /// 36.4 ปริมาณการใช้น้ำผิวดินเพื่อการอุตสาหกรรม - น้ำฝนกักเก็บ
    /// </summary>
    List<CubicMeterSurface> CubicMeterSurfaceRainForProduct();

    // <summary>
    /// 37.1 ปริมาณการใช้น้ำผิวดินเพื่อการอุปโภคบริโภค -สระน้ำ 
    /// </summary>
    List<CubicMeterSurface> CubicMeterSurfacePoolForDrink();

    /// <summary>
    /// 37.2 ปริมาณการใช้น้ำผิวดินเพื่อการอุปโภคบริโภค  - แม่น้ำ 
    /// </summary>
    List<CubicMeterSurface> CubicMeterSurfaceRiverForDrink();

    /// <summary>
    /// 37.3 ปริมาณการใช้น้ำผิวดินเพื่อการอุปโภคบริโภค  -ชลประทาน
    /// </summary>
    List<CubicMeterSurface> CubicMeterSurfaceIrrigationForDrink();

    /// <summary>
    /// 37.4 ปริมาณการใช้น้ำผิวดินเพื่อการอุปโภคบริโภค  - น้ำฝนกักเก็บ
    /// </summary>
    List<CubicMeterSurface> CubicMeterSurfaceRainForDrink();

    /// <summary>
    /// 34-37 ปริมาณการใช้น้ำผิวดิน
    /// </summary>
    List<CubicMeterSurface> CubicMeterSurface(string ea, bool? isAgriculture, bool? isCommercial, bool? isFactorial, bool? isHouseHold, Agriculture agriculture, Commercial commercial, Factorial factorial, Residential residential, Pool pool, River river, Irrigation irrigation, Rain rain);

    /// <summary>
    /// 34.ปริมาณการใช้น้ำผิวดินเพื่อการเกษตร (สระน้ำ แม่น้ำ ชลประทาน น้ำฝนกักเก็บ) สน.2
    /// </summary>
    double CubicMeterSurfaceForAgriculture(ManagementForFarming managementForFarming);

    /// <summary>
    /// 37.ปริมาณการใช้น้ำผิวดินเพื่อการอุปโภคบริโภค (สระน้ำ แม่น้ำ ชลประทาน น้ำฝนกักเก็บ) สน.2
    /// </summary>
    double CubicMeterSurfaceForDrink(CommunityWaterManagement communityWaterManagement);

    /// <summary>
    /// 39.จำนวนหมู่บ้าน/ชุมชนทั้งหมด
    /// </summary>
    int CountCommunity(CommunitySample com);

    /// <summary>
    /// 40.จำนวนหมู่บ้าน/ชุมชนที่มีอุทกภัย ดินโคลนถล่ม
    /// </summary>
    int CountCommunityHasDisaster(CommunitySample com);

    /// <summary>
    /// 41.ครัวเรือนในชนบททั้งหมด
    /// </summary>
    int IsAllHouseHoldCountryside(string EA, bool? isHouseHold);

    /// <summary>
    /// 42.ครัวเรือนในเขตเมืองทั้งหมด
    /// </summary>
    int IsAllHouseHoldDistrict(string EA, bool? isHouseHold);

    /// <summary>
    /// 43.สถานประกอบการผลิตทั้งหมด
    /// </summary>
    int IsAllFactorial(bool? IsFactorial);

    /// <summary>
    /// 44.สถานประกอบการบริการทั้งหมด
    /// </summary>
    int IsAllCommercial(bool? IsCommercial);
}