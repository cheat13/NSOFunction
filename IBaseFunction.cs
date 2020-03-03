using System.Collections.Generic;
using NSOFunction.Models;
using NSOWater.HotMigration.Models;

public interface IBaseFunction
{
    /// <summary>
    /// 1.ครัวเรือนเกษตรกรรม
    /// 4.ครัวเรือนที่มีพื้นที่เกษตรกรรมในพื้นที่ชลประทาน
    /// </summary>
    AgricultureModel Agriculture(HouseHoldSample unit);

    /// <summary>
    /// 2.ครัวเรือนทั้งหมด
    /// 3.ครัวเรือนที่มีน้ำประปาคุณภาพดี 
    /// 5.ครัวเรือนในเขตเมืองที่มีน้ำประปาใช้ (ในเขตเทศบาล)
    /// 6.ครัวเรือนในชนบทที่มีน้ำประปาใช้ (นอกเขตเทศบาล)
    /// </summary>
    HouseHoldModel IsHouseHoldGoodPlumbing(HouseHoldSample unit);

    /// <summary>
    /// 7.คุณภาพน้ำที่ใช้ในการผลิต (น้ำประปา ผิวดิน น้ำบาดาล)
    /// </summary>
    int IsFactorialWaterQuality(HouseHoldSample unit);

    /// <summary>
    /// 8.คุณภาพน้ำที่ใช้ในภาคบริการ (น้ำประปา ผิวดิน น้ำบาดาล)
    /// </summary>
    int IsCommercialWaterQuality(HouseHoldSample unit);

    /// <summary>
    /// 12.โรงงานอุตสาหกรรมทั้งหมด
    /// </summary>
    int IsFactorial(HouseHoldSample unit);

    /// <summary>
    /// 13.โรงงานอุตสาหกรรมที่มีระบบบำบัดน้ำเสีย
    /// </summary>
    int IsFactorialWaterTreatment(HouseHoldSample unit);

    /// <summary>
    /// 14.หมู่บ้านที่มีระบบบำบัดน้ำเสีย
    /// </summary>
    int IsCommunityWaterManagementHasWaterTreatment(CommunitySample com);

    /// <summary>
    /// 9.จำนวนบ่อน้ำบาดาล สน.1
    /// </summary>
    List<CountGroundWaterHouseHold> CountGroundWaterHouseHold(string ea, HouseHoldSample unit);

    /// <summary>
    /// 9.จำนวนบ่อน้ำบาดาล สน.2
    /// </summary>
    double CountGroundWaterCommunity(CommunitySample com);

    /// <summary>
    /// 10.จำนวนประชากรทั้งหมด
    /// 11.จำนวนประชากรวัยทำงาน
    /// </summary>
    PopulationCount CountPopulation(HouseHoldSample unit);

    /// <summary>
    /// 15.พื้นที่ชลประทาน สน.2
    /// </summary>
    double FieldCommunity(CommunitySample com);

    /// <summary>
    /// 16.ระดับความลึกของน้ำท่วม (ในเขตที่อยู่อาศัย) 
    /// 17.ระยะเวลาที่น้ำท่วมขัง (ในเขตที่อยู่อาศัย) 
    /// </summary>
    WaterFlood Disasterous(HouseHoldSample unit);

    /// <summary>
    /// 18.ระยะเวลาที่มีน้ำประปาใช้ (เดือน) 
    /// </summary>
    int HasntPlumbing(HouseHoldSample unit);

    /// <summary>
    /// 19.สถานที่ราชการทั้งหมด
    /// 20.สถานที่ราชการที่มีน้ำประปาใช้
    /// 21.สถานที่ราชการที่มีน้ำประปาที่มีคุณภาพมาตรฐาน
    /// </summary>
    PlumbingServiceUsage PlumbingSeviceUsage(BuildingSample building, HouseHoldSample unit);

    /// <summary>
    /// 22.หมู่บ้านในพื้นที่น้ำท่วมซ้ำซากที่มีการเตือนภัยและมาตรการช่วยเหลือ
    /// </summary>
    int CommunityNatureDisaster(CommunitySample com);

    /// <summary>
    /// 23.แหล่งน้ำขนาดใหญ่ กลาง และเล็ก สน.1
    /// </summary>
    List<WaterPoolHouseHold> WaterSourcesHouseHold(string ea, HouseHoldSample unit);

    /// <summary>
    /// 23.แหล่งน้ำขนาดใหญ่ กลาง และเล็ก สน.2
    /// </summary>
    double WaterSourcesCommunity(string ea, CommunitySample com);

    /// <summary>
    /// 24.จำนวนโรงงานอุตสาหกรรมที่มีน้ำเสียจากระบบ
    /// </summary>
    int IndustryHasWasteWaterTreatment(HouseHoldSample unit);

    /// <summary>
    /// 25.ประชากรที่อาศัยในครัวเรือนที่มีน้ำท่วม 
    /// </summary>
    int PeopleInFloodedArea(HouseHoldSample unit); // ???

    /// <summary>
    /// 26-29 ปริมาณการใช้น้ำบาดาล
    /// </summary>
    List<CubicMeterGroundWaterModel> CubicMeterGroundWater(string ea, HouseHoldSample unit, BuildingSample building);

    /// <summary>
    /// 26,29,38.ปริมาณการใช้น้ำบาดาล สน.2
    /// </summary>
    CubicMeterGroundWaterCommunity CubicMeterGroundWater(CommunitySample com);

    /// <summary>
    /// 30-33 ปริมาณการใช้น้ำประปา
    /// </summary>
    CubicMeterPlumbing CubicMeterPlumbing(HouseHoldSample unit, BuildingSample building, List<CommunitySample> com);

    /// <summary>
    /// 34-37 ปริมาณการใช้น้ำผิวดิน
    /// </summary>
    List<CubicMeterSurface> CubicMeterSurface(string ea, HouseHoldSample unit);

    /// <summary>
    /// 34.ปริมาณการใช้น้ำผิวดินเพื่อการเกษตร (สระน้ำ แม่น้ำ ชลประทาน น้ำฝนกักเก็บ) สน.2
    /// </summary>
    double CubicMeterSurfaceForAgriculture(CommunitySample com);

    /// <summary>
    /// 37.ปริมาณการใช้น้ำผิวดินเพื่อการอุปโภคบริโภค (สระน้ำ แม่น้ำ ชลประทาน น้ำฝนกักเก็บ) สน.2
    /// </summary>
    double CubicMeterSurfaceForDrink(CommunitySample com);

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
    int IsAllHouseHoldCountryside(string EA, HouseHoldSample unit);

    /// <summary>
    /// 42.ครัวเรือนในเขตเมืองทั้งหมด
    /// </summary>
    int IsAllHouseHoldDistrict(string EA, HouseHoldSample unit);

    /// <summary>
    /// 43.สถานประกอบการผลิตทั้งหมด
    /// </summary>
    int IsAllFactorial(HouseHoldSample unit);

    /// <summary>
    /// 44.สถานประกอบการบริการทั้งหมด
    /// </summary>
    int IsAllCommercial(HouseHoldSample unit);
}