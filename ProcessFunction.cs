using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using NSOFunction.Models;
using NSOWater.HotMigration.Models;

namespace NSOFunction
{
    public class ProcessFunction
    {
        private BaseFunction baseFn { get; set; }
        public ProcessFunction()
        {
            this.baseFn = new BaseFunction();
        }

        public List<DataProcessed> UnitProcessing(string ea, HouseHoldSample unt, BuildingSample bld, List<CommunitySample> com)
        {
            var area_Code = ea.Substring(1, 6);
            var lastAccesses = unt?.SubUnit?.Accesses?.LastOrDefault();
            if (lastAccesses == 4 || lastAccesses == 5)
            {
                return new List<DataProcessed>
                    {
                        new DataProcessed {
                            SampleId= unt._id,
                            SampleType = "u",
                            EA = ea,
                            Area_Code = area_Code,
                            Status = StatusProcessed.Vacant,
                            CanComputeCubicMeterGroundWaterForAgriculture = StatusCompute.NA,
                            CanComputeCubicMeterGroundWaterForService = StatusCompute.NA,
                            CanComputeCubicMeterGroundWaterForProduct = StatusCompute.NA,
                            CanComputeCubicMeterGroundWaterForDrink = StatusCompute.NA,
                            CanComputeCubicMeterGroundWaterForUse = StatusCompute.NA,
                            CanComputeCubicMeterPlumbingForAgriculture = StatusCompute.NA,
                            CanComputeCubicMeterPlumbingForService = StatusCompute.NA,
                            CanComputeCubicMeterPlumbingForProduct = StatusCompute.NA,
                            CanComputeCubicMeterPlumbingForDrink = StatusCompute.NA,
                            CanComputeCubicMeterSurfaceForAgriculture = StatusCompute.NA,
                            CanComputeCubicMeterSurfaceForService = StatusCompute.NA,
                            CanComputeCubicMeterSurfaceForProduct = StatusCompute.NA,
                            CanComputeCubicMeterSurfaceForDrink = StatusCompute.NA,
                            Duplicate = false,
                        }
                    };
            }
            else
            {
                var agriculture = baseFn.Agriculture(unt);
                var isHouseHoldGoodPlumbing = baseFn.IsHouseHoldGoodPlumbing(unt);
                var isFactorialWaterQuality = baseFn.IsFactorialWaterQuality(unt);
                var isCommercialWaterQuality = baseFn.IsCommercialWaterQuality(unt);
                var isFactorial = baseFn.IsFactorial(unt);
                var isFactorialWaterTreatment = baseFn.IsFactorialWaterTreatment(unt);
                var countPopulation = baseFn.CountPopulation(unt);
                var disasterous = baseFn.Disasterous(unt);
                var hasntPlumbing = baseFn.HasntPlumbing(unt);
                var plumbingSeviceUsage = baseFn.PlumbingSeviceUsage(bld, unt);
                var industryHasWasteWaterTreatment = baseFn.IndustryHasWasteWaterTreatment(unt);
                var peopleInFloodedArea = baseFn.PeopleInFloodedArea(unt);
                var cubicMeterPlumbing = baseFn.CubicMeterPlumbing(unt, bld, com);
                var isAllHouseHoldCountryside = baseFn.IsAllHouseHoldCountryside(ea, unt);
                var isAllHouseHoldDistrict = baseFn.IsAllHouseHoldDistrict(ea, unt);
                var isAllFactorial = baseFn.IsAllFactorial(unt);
                var isAllCommercial = baseFn.IsAllCommercial(unt);

                var localDataProcessed = new DataProcessed
                {
                    SampleId = unt._id,
                    SampleType = "u",
                    EA = ea,
                    Area_Code = area_Code,
                    Status = StatusProcessed.Complete,
                    DoingMWA = cubicMeterPlumbing.DoingMWA,
                    CubicMeterPerMonthMWA = cubicMeterPlumbing.CubicMeterPerMonthMWA,
                    WaterBillMWA = cubicMeterPlumbing.WaterBillMWA,
                    TurbidWaterMWA = cubicMeterPlumbing.TurbidWaterMWA,
                    SaltWaterMWA = cubicMeterPlumbing.SaltWaterMWA,
                    SmellMWA = cubicMeterPlumbing.SmellMWA,
                    FilmOfOilMWA = cubicMeterPlumbing.FilmOfOilMWA,
                    FogWaterMWA = cubicMeterPlumbing.FogWaterMWA,
                    HardWaterMWA = cubicMeterPlumbing.HardWaterMWA,
                    AgricultureMWA = cubicMeterPlumbing.AgricultureMWA,
                    ServiceMWA = cubicMeterPlumbing.ServiceMWA,
                    ProductMWA = cubicMeterPlumbing.ProductMWA,
                    DrinkMWA = cubicMeterPlumbing.DrinkMWA,
                    PlantMWA = cubicMeterPlumbing.PlantMWA,
                    DoingPWA = cubicMeterPlumbing.DoingPWA,
                    CubicMeterPerMonthPWA = cubicMeterPlumbing.CubicMeterPerMonthPWA,
                    WaterBillPWA = cubicMeterPlumbing.WaterBillPWA,
                    TurbidWaterPWA = cubicMeterPlumbing.TurbidWaterPWA,
                    SaltWaterPWA = cubicMeterPlumbing.SaltWaterPWA,
                    SmellPWA = cubicMeterPlumbing.SmellPWA,
                    FilmOfOilPWA = cubicMeterPlumbing.FilmOfOilPWA,
                    FogWaterPWA = cubicMeterPlumbing.FogWaterPWA,
                    HardWaterPWA = cubicMeterPlumbing.HardWaterPWA,
                    AgriculturePWA = cubicMeterPlumbing.AgriculturePWA,
                    ServicePWA = cubicMeterPlumbing.ServicePWA,
                    ProductPWA = cubicMeterPlumbing.ProductPWA,
                    DrinkPWA = cubicMeterPlumbing.DrinkPWA,
                    PlantPWA = cubicMeterPlumbing.PlantPWA,
                    DoingOther = cubicMeterPlumbing.DoingOther,
                    CubicMeterPerMonthOther = cubicMeterPlumbing.CubicMeterPerMonthOther,
                    WaterBillOther = cubicMeterPlumbing.WaterBillOther,
                    TurbidWaterOther = cubicMeterPlumbing.TurbidWaterOther,
                    SaltWaterOther = cubicMeterPlumbing.SaltWaterOther,
                    SmellOther = cubicMeterPlumbing.SmellOther,
                    FilmOfOilOther = cubicMeterPlumbing.FilmOfOilOther,
                    FogWaterOther = cubicMeterPlumbing.FogWaterOther,
                    HardWaterOther = cubicMeterPlumbing.HardWaterOther,
                    AgricultureOther = cubicMeterPlumbing.AgricultureOther,
                    ServiceOther = cubicMeterPlumbing.ServiceOther,
                    ProductOther = cubicMeterPlumbing.ProductOther,
                    DrinkOther = cubicMeterPlumbing.DrinkOther,
                    PlantOther = cubicMeterPlumbing.PlantOther,
                    MeterRentalFee = cubicMeterPlumbing.MeterRentalFee,
                    PlumbingPrice = cubicMeterPlumbing.PlumbingPrice,
                    CanComputeCubicMeterPlumbingForAgriculture = cubicMeterPlumbing.CanComputeCubicMeterPlumbingForAgriculture,
                    CanComputeCubicMeterPlumbingForService = cubicMeterPlumbing.CanComputeCubicMeterPlumbingForService,
                    CanComputeCubicMeterPlumbingForProduct = cubicMeterPlumbing.CanComputeCubicMeterPlumbingForProduct,
                    CanComputeCubicMeterPlumbingForDrink = cubicMeterPlumbing.CanComputeCubicMeterPlumbingForDrink,
                    IsAgriculture = agriculture.IsAgriculture,
                    IsHouseHold = isHouseHoldGoodPlumbing.IsHouseHold,
                    IsHouseHoldGoodPlumbing = isHouseHoldGoodPlumbing.IsHouseHoldGoodPlumbing,
                    IsAgricultureHasIrrigationField = agriculture.IsAgricultureHasIrrigationField,
                    IsHouseHoldHasPlumbingDistrict = isHouseHoldGoodPlumbing.IsHouseHoldHasPlumbingDistrict,
                    IsHouseHoldHasPlumbingCountryside = isHouseHoldGoodPlumbing.IsHouseHoldHasPlumbingCountryside,
                    IsFactorialWaterQuality = isFactorialWaterQuality,
                    IsCommercialWaterQuality = isCommercialWaterQuality,
                    CountPopulation = countPopulation.countPopulation,
                    CountWorkingAge = countPopulation.countWorkingAge,
                    IsFactorial = isFactorial,
                    IsFactorialWaterTreatment = isFactorialWaterTreatment,
                    AvgWaterHeightCm = disasterous.AvgWaterHeightCm,
                    TimeWaterHeightCm = disasterous.TimeWaterHeightCm,
                    HasntPlumbing = hasntPlumbing,
                    IsGovernment = plumbingSeviceUsage.IsGovernment,
                    IsGovernmentUsage = plumbingSeviceUsage.IsGovernmentUsage,
                    IsGovernmentWaterQuality = plumbingSeviceUsage.IsGovernmentWaterQuality,
                    IndustryHasWasteWaterTreatment = industryHasWasteWaterTreatment,
                    PeopleInFloodedArea = peopleInFloodedArea,
                    CubicMeterPlumbingForAgriculture = cubicMeterPlumbing.CubicMeterPlumbingForAgriculture,
                    CubicMeterPlumbingForService = cubicMeterPlumbing.CubicMeterPlumbingForService,
                    CubicMeterPlumbingForProduct = cubicMeterPlumbing.CubicMeterPlumbingForProduct,
                    CubicMeterPlumbingForDrink = cubicMeterPlumbing.CubicMeterPlumbingForDrink,
                    CanComputeCubicMeterGroundWaterForAgriculture = StatusCompute.NA,
                    CanComputeCubicMeterGroundWaterForService = StatusCompute.NA,
                    CanComputeCubicMeterGroundWaterForProduct = StatusCompute.NA,
                    CanComputeCubicMeterGroundWaterForDrink = StatusCompute.NA,
                    CanComputeCubicMeterGroundWaterForUse = StatusCompute.NA,
                    CanComputeCubicMeterSurfaceForAgriculture = StatusCompute.NA,
                    CanComputeCubicMeterSurfaceForService = StatusCompute.NA,
                    CanComputeCubicMeterSurfaceForProduct = StatusCompute.NA,
                    CanComputeCubicMeterSurfaceForDrink = StatusCompute.NA,
                    IsAllHouseHoldCountryside = isAllHouseHoldCountryside,
                    IsAllHouseHoldDistrict = isAllHouseHoldDistrict,
                    IsAllFactorial = isAllFactorial,
                    IsAllCommercial = isAllCommercial,
                    Duplicate = false,
                };

                var countGroundWaterHouseHold = baseFn.CountGroundWaterHouseHold(ea, unt);
                var countGroundWaterProcessed = countGroundWaterHouseHold.Where(it => it.Area_Code != null)
                    .Select(it => new DataProcessed
                    {
                        SampleId = unt._id,
                        SampleType = "u",
                        Area_Code = it.Area_Code,
                        Status = StatusProcessed.Complete,
                        CountGroundWater = it.CountGroundWater,
                        CanComputeCubicMeterGroundWaterForAgriculture = StatusCompute.NA,
                        CanComputeCubicMeterGroundWaterForService = StatusCompute.NA,
                        CanComputeCubicMeterGroundWaterForProduct = StatusCompute.NA,
                        CanComputeCubicMeterGroundWaterForDrink = StatusCompute.NA,
                        CanComputeCubicMeterGroundWaterForUse = StatusCompute.NA,
                        CanComputeCubicMeterPlumbingForAgriculture = StatusCompute.NA,
                        CanComputeCubicMeterPlumbingForService = StatusCompute.NA,
                        CanComputeCubicMeterPlumbingForProduct = StatusCompute.NA,
                        CanComputeCubicMeterPlumbingForDrink = StatusCompute.NA,
                        CanComputeCubicMeterSurfaceForAgriculture = StatusCompute.NA,
                        CanComputeCubicMeterSurfaceForService = StatusCompute.NA,
                        CanComputeCubicMeterSurfaceForProduct = StatusCompute.NA,
                        CanComputeCubicMeterSurfaceForDrink = StatusCompute.NA,
                        Duplicate = false,
                    });

                var waterSourcesHouseHold = baseFn.WaterSourcesHouseHold(ea, unt);
                var waterSourcesProcessed = waterSourcesHouseHold.Where(it => it.Area_Code != null)
                    .Select(it => new DataProcessed
                    {
                        SampleId = unt._id,
                        SampleType = "u",
                        Area_Code = it.Area_Code,
                        Status = StatusProcessed.Complete,
                        WaterSources = it.Capacity,
                        CanComputeCubicMeterGroundWaterForAgriculture = StatusCompute.NA,
                        CanComputeCubicMeterGroundWaterForService = StatusCompute.NA,
                        CanComputeCubicMeterGroundWaterForProduct = StatusCompute.NA,
                        CanComputeCubicMeterGroundWaterForDrink = StatusCompute.NA,
                        CanComputeCubicMeterGroundWaterForUse = StatusCompute.NA,
                        CanComputeCubicMeterPlumbingForAgriculture = StatusCompute.NA,
                        CanComputeCubicMeterPlumbingForService = StatusCompute.NA,
                        CanComputeCubicMeterPlumbingForProduct = StatusCompute.NA,
                        CanComputeCubicMeterPlumbingForDrink = StatusCompute.NA,
                        CanComputeCubicMeterSurfaceForAgriculture = StatusCompute.NA,
                        CanComputeCubicMeterSurfaceForService = StatusCompute.NA,
                        CanComputeCubicMeterSurfaceForProduct = StatusCompute.NA,
                        CanComputeCubicMeterSurfaceForDrink = StatusCompute.NA,
                        Duplicate = false,
                    });

                var cubicMeterGroundWater = baseFn.CubicMeterGroundWater(ea, unt, bld);
                var cubicMeterGroundWaterProcessed = cubicMeterGroundWater.Where(it => it.Area_Code != null)
                    .Select(it => new DataProcessed
                    {
                        SampleId = unt._id,
                        SampleType = "u",
                        Area_Code = it.Area_Code,
                        Status = StatusProcessed.Complete,
                        Count = it.GroundWaterCount,
                        CubicMeterPerMonth = it.CubicMeterPerMonth,
                        WaterBill = it.WaterBill,
                        PumpCount = it.PumpCount,
                        PumpAuto = it.PumpAuto,
                        TurbidWater = it.TurbidWater,
                        SaltWater = it.SaltWater,
                        Smell = it.Smell,
                        FilmOfOil = it.FilmOfOil,
                        FogWater = it.FogWater,
                        HardWater = it.HardWater,
                        Agriculture = it.Agriculture,
                        Service = it.Service,
                        Product = it.Product,
                        Drink = it.Drink,
                        Plant = it.Plant,
                        Farm = it.Farm,
                        CubicMeterBuyingForAgriculture = it.CubicMeterBuyingForAgriculture,
                        CubicMeterBuyingForService = it.CubicMeterBuyingForService,
                        CubicMeterBuyingForProduct = it.CubicMeterBuyingForProduct,
                        CubicMeterBuyingForDrink = it.CubicMeterBuyingForDrink,
                        CanComputeCubicMeterGroundWaterForAgriculture = it.CanComputeCubicMeterGroundWaterForAgriculture,
                        CanComputeCubicMeterGroundWaterForService = it.CanComputeCubicMeterGroundWaterForService,
                        CanComputeCubicMeterGroundWaterForProduct = it.CanComputeCubicMeterGroundWaterForProduct,
                        CanComputeCubicMeterGroundWaterForDrink = it.CanComputeCubicMeterGroundWaterForDrink,
                        CanComputeCubicMeterGroundWaterForUse = it.CanComputeCubicMeterGroundWaterForUse,
                        CubicMeterGroundWaterForAgriculture = it.CubicMeterGroundWaterForAgriculture,
                        CubicMeterGroundWaterForService = it.CubicMeterGroundWaterForService,
                        CubicMeterGroundWaterForProduct = it.CubicMeterGroundWaterForProduct,
                        CubicMeterGroundWaterForDrink = it.CubicMeterGroundWaterForDrink,
                        CubicMeterGroundWaterForUse = it.CubicMeterGroundWaterForUse,
                        CanComputeCubicMeterPlumbingForAgriculture = StatusCompute.NA,
                        CanComputeCubicMeterPlumbingForService = StatusCompute.NA,
                        CanComputeCubicMeterPlumbingForProduct = StatusCompute.NA,
                        CanComputeCubicMeterPlumbingForDrink = StatusCompute.NA,
                        CanComputeCubicMeterSurfaceForAgriculture = StatusCompute.NA,
                        CanComputeCubicMeterSurfaceForService = StatusCompute.NA,
                        CanComputeCubicMeterSurfaceForProduct = StatusCompute.NA,
                        CanComputeCubicMeterSurfaceForDrink = StatusCompute.NA,
                        Duplicate = false,
                    });

                var cubicMeterSurface = baseFn.CubicMeterSurface(ea, unt);
                var cubicMeterSurfaceProcessed = cubicMeterSurface.Where(it => it.Area_Code != null)
                    .Select(it => new DataProcessed
                    {
                        SampleId = unt._id,
                        SampleType = "u",
                        Area_Code = it.Area_Code,
                        Status = StatusProcessed.Complete,
                        PoolCount = it.PoolCount,
                        WaterResourceCount = it.WaterResourceCount,
                        CubicMeterPerMonthPool = it.CubicMeterPerMonthPool,
                        PumpCountPool = it.PumpCountPool,
                        PumpAutoPool = it.PumpAutoPool,
                        SaltWaterPool = it.SaltWaterPool,
                        SmellPool = it.SmellPool,
                        FilmOfOilPool = it.FilmOfOilPool,
                        FogWaterPool = it.FogWaterPool,
                        AgriculturePool = it.AgriculturePool,
                        ServicePool = it.ServicePool,
                        ProductPool = it.ProductPool,
                        DrinkPool = it.DrinkPool,
                        PlantPool = it.PlantPool,
                        FarmPool = it.FarmPool,
                        CubicMeterPerMonthIrrigation = it.CubicMeterPerMonthIrrigation,
                        PumpCountIrrigation = it.PumpCountIrrigation,
                        PumpAutoIrrigation = it.PumpAutoIrrigation,
                        SaltWaterIrrigation = it.SaltWaterIrrigation,
                        SmellIrrigation = it.SmellIrrigation,
                        FilmOfOilIrrigation = it.FilmOfOilIrrigation,
                        FogWaterIrrigation = it.FogWaterIrrigation,
                        AgricultureIrrigation = it.AgricultureIrrigation,
                        ServiceIrrigation = it.ServiceIrrigation,
                        ProductIrrigation = it.ProductIrrigation,
                        DrinkIrrigation = it.DrinkIrrigation,
                        PlantIrrigation = it.PlantIrrigation,
                        FarmIrrigation = it.FarmIrrigation,
                        PumpCountRiver = it.PumpCountRiver,
                        PumpAutoRiver = it.PumpAutoRiver,
                        SaltWaterRiver = it.SaltWaterRiver,
                        SmellRiver = it.SmellRiver,
                        FilmOfOilRiver = it.FilmOfOilRiver,
                        FogWaterRiver = it.FogWaterRiver,
                        AgricultureRiver = it.AgricultureRiver,
                        ServiceRiver = it.ServiceRiver,
                        ProductRiver = it.ProductRiver,
                        DrinkRiver = it.DrinkRiver,
                        PlantRiver = it.PlantRiver,
                        FarmRiver = it.FarmRiver,
                        AgricultureRain = it.AgricultureRain,
                        ServiceRain = it.ServiceRain,
                        ProductRain = it.ProductRain,
                        DrinkRain = it.DrinkRain,
                        PlantRain = it.PlantRain,
                        CanComputeCubicMeterSurfaceForAgriculture = it.CanComputeCubicMeterSurfaceForAgriculture,
                        CanComputeCubicMeterSurfaceForService = it.CanComputeCubicMeterSurfaceForService,
                        CanComputeCubicMeterSurfaceForProduct = it.CanComputeCubicMeterSurfaceForProduct,
                        CanComputeCubicMeterSurfaceForDrink = it.CanComputeCubicMeterSurfaceForDrink,
                        CubicMeterSurfaceForAgriculture = it.CubicMeterSurfaceForAgriculture,
                        CubicMeterSurfaceForService = it.CubicMeterSurfaceForService,
                        CubicMeterSurfaceForProduct = it.CubicMeterSurfaceForProduct,
                        CubicMeterSurfaceForDrink = it.CubicMeterSurfaceForDrink,
                        CanComputeCubicMeterGroundWaterForAgriculture = StatusCompute.NA,
                        CanComputeCubicMeterGroundWaterForService = StatusCompute.NA,
                        CanComputeCubicMeterGroundWaterForProduct = StatusCompute.NA,
                        CanComputeCubicMeterGroundWaterForDrink = StatusCompute.NA,
                        CanComputeCubicMeterGroundWaterForUse = StatusCompute.NA,
                        CanComputeCubicMeterPlumbingForAgriculture = StatusCompute.NA,
                        CanComputeCubicMeterPlumbingForService = StatusCompute.NA,
                        CanComputeCubicMeterPlumbingForProduct = StatusCompute.NA,
                        CanComputeCubicMeterPlumbingForDrink = StatusCompute.NA,
                        Duplicate = false,
                    });

                var status = new StatusComputeManage();

                var dataProcessedList = countGroundWaterProcessed
                    .Concat(waterSourcesProcessed)
                    .Concat(cubicMeterGroundWaterProcessed)
                    .Concat(cubicMeterSurfaceProcessed)
                    .Prepend(localDataProcessed)
                    .Where(it => it.Area_Code != null)
                    .GroupBy(it => it.Area_Code)
                    .Select(it => new DataProcessed
                    {
                        SampleId = unt._id,
                        SampleType = "u",
                        EA = it.First().EA,
                        Area_Code = it.Key,
                        Status = StatusProcessed.Complete,
                        Count = it.Sum(i => i.Count),
                        CubicMeterPerMonth = it.Sum(i => i.CubicMeterPerMonth),
                        WaterBill = it.Sum(i => i.WaterBill),
                        PumpCount = it.Sum(i => i.PumpCount),
                        PumpAuto = it.Sum(i => i.PumpAuto),
                        TurbidWater = it.Any(i => i.TurbidWater == true),
                        SaltWater = it.Any(i => i.SaltWater == true),
                        Smell = it.Any(i => i.Smell == true),
                        FilmOfOil = it.Any(i => i.FilmOfOil == true),
                        FogWater = it.Any(i => i.FogWater == true),
                        HardWater = it.Any(i => i.HardWater == true),
                        Agriculture = it.Sum(i => i.Agriculture),
                        Service = it.Sum(i => i.Service),
                        Product = it.Sum(i => i.Product),
                        Drink = it.Sum(i => i.Drink),
                        Plant = it.Sum(i => i.Plant),
                        Farm = it.Sum(i => i.Farm),
                        CubicMeterBuyingForAgriculture = it.Sum(i => i.CubicMeterBuyingForAgriculture),
                        CubicMeterBuyingForService = it.Sum(i => i.CubicMeterBuyingForService),
                        CubicMeterBuyingForProduct = it.Sum(i => i.CubicMeterBuyingForProduct),
                        CubicMeterBuyingForDrink = it.Sum(i => i.CubicMeterBuyingForDrink),
                        CanComputeCubicMeterGroundWaterForAgriculture = status.CheckStatusCompute(it.Select(i => i.CanComputeCubicMeterGroundWaterForAgriculture)),
                        CanComputeCubicMeterGroundWaterForService = status.CheckStatusCompute(it.Select(i => i.CanComputeCubicMeterGroundWaterForService)),
                        CanComputeCubicMeterGroundWaterForProduct = status.CheckStatusCompute(it.Select(i => i.CanComputeCubicMeterGroundWaterForProduct)),
                        CanComputeCubicMeterGroundWaterForDrink = status.CheckStatusCompute(it.Select(i => i.CanComputeCubicMeterGroundWaterForDrink)),
                        CanComputeCubicMeterGroundWaterForUse = status.CheckStatusCompute(it.Select(i => i.CanComputeCubicMeterGroundWaterForUse)),
                        DoingMWA = it.Any(i => i.DoingMWA == true),
                        CubicMeterPerMonthMWA = it.Sum(i => i.CubicMeterPerMonthMWA),
                        WaterBillMWA = it.Sum(i => i.WaterBillMWA),
                        TurbidWaterMWA = it.Any(i => i.TurbidWaterMWA == true),
                        SaltWaterMWA = it.Any(i => i.SaltWaterMWA == true),
                        SmellMWA = it.Any(i => i.SmellMWA == true),
                        FilmOfOilMWA = it.Any(i => i.FilmOfOilMWA == true),
                        FogWaterMWA = it.Any(i => i.FogWaterMWA == true),
                        HardWaterMWA = it.Any(i => i.HardWaterMWA == true),
                        AgricultureMWA = it.Sum(i => i.AgricultureMWA),
                        ServiceMWA = it.Sum(i => i.ServiceMWA),
                        ProductMWA = it.Sum(i => i.ProductMWA),
                        DrinkMWA = it.Sum(i => i.DrinkMWA),
                        PlantMWA = it.Sum(i => i.PlantMWA),
                        DoingPWA = it.Any(i => i.DoingPWA == true),
                        CubicMeterPerMonthPWA = it.Sum(i => i.CubicMeterPerMonthPWA),
                        WaterBillPWA = it.Sum(i => i.WaterBillPWA),
                        TurbidWaterPWA = it.Any(i => i.TurbidWaterPWA == true),
                        SaltWaterPWA = it.Any(i => i.SaltWaterPWA == true),
                        SmellPWA = it.Any(i => i.SmellPWA == true),
                        FilmOfOilPWA = it.Any(i => i.FilmOfOilPWA == true),
                        FogWaterPWA = it.Any(i => i.FogWaterPWA == true),
                        HardWaterPWA = it.Any(i => i.HardWaterPWA == true),
                        AgriculturePWA = it.Sum(i => i.AgriculturePWA),
                        ServicePWA = it.Sum(i => i.ServicePWA),
                        ProductPWA = it.Sum(i => i.ProductPWA),
                        DrinkPWA = it.Sum(i => i.DrinkPWA),
                        PlantPWA = it.Sum(i => i.PlantPWA),
                        DoingOther = it.Any(i => i.DoingOther == true),
                        CubicMeterPerMonthOther = it.Sum(i => i.CubicMeterPerMonthOther),
                        WaterBillOther = it.Sum(i => i.WaterBillOther),
                        TurbidWaterOther = it.Any(i => i.TurbidWaterOther == true),
                        SaltWaterOther = it.Any(i => i.SaltWaterOther == true),
                        SmellOther = it.Any(i => i.SmellOther == true),
                        FilmOfOilOther = it.Any(i => i.FilmOfOilOther == true),
                        FogWaterOther = it.Any(i => i.FogWaterOther == true),
                        HardWaterOther = it.Any(i => i.HardWaterOther == true),
                        AgricultureOther = it.Sum(i => i.AgricultureOther),
                        ServiceOther = it.Sum(i => i.ServiceOther),
                        ProductOther = it.Sum(i => i.ProductOther),
                        DrinkOther = it.Sum(i => i.DrinkOther),
                        PlantOther = it.Sum(i => i.PlantOther),
                        MeterRentalFee = it.Sum(i => i.MeterRentalFee),
                        PlumbingPrice = it.Sum(i => i.PlumbingPrice),
                        CanComputeCubicMeterPlumbingForAgriculture = status.CheckStatusCompute(it.Select(i => i.CanComputeCubicMeterPlumbingForAgriculture)),
                        CanComputeCubicMeterPlumbingForService = status.CheckStatusCompute(it.Select(i => i.CanComputeCubicMeterPlumbingForService)),
                        CanComputeCubicMeterPlumbingForProduct = status.CheckStatusCompute(it.Select(i => i.CanComputeCubicMeterPlumbingForProduct)),
                        CanComputeCubicMeterPlumbingForDrink = status.CheckStatusCompute(it.Select(i => i.CanComputeCubicMeterPlumbingForDrink)),
                        PoolCount = it.Sum(i => i.PoolCount),
                        WaterResourceCount = it.Sum(i => i.WaterResourceCount),
                        CubicMeterPerMonthPool = it.Sum(i => i.CubicMeterPerMonthPool),
                        PumpCountPool = it.Sum(i => i.PumpCountPool),
                        PumpAutoPool = it.Sum(i => i.PumpAutoPool),
                        SaltWaterPool = it.Any(i => i.SaltWaterPool == true),
                        SmellPool = it.Any(i => i.SmellPool == true),
                        FilmOfOilPool = it.Any(i => i.FilmOfOilPool == true),
                        FogWaterPool = it.Any(i => i.FogWaterPool == true),
                        AgriculturePool = it.Sum(i => i.AgriculturePool),
                        ServicePool = it.Sum(i => i.ServicePool),
                        ProductPool = it.Sum(i => i.ProductPool),
                        DrinkPool = it.Sum(i => i.DrinkPool),
                        PlantPool = it.Sum(i => i.PlantPool),
                        FarmPool = it.Sum(i => i.FarmPool),
                        CubicMeterPerMonthIrrigation = it.Sum(i => i.CubicMeterPerMonthIrrigation),
                        PumpCountIrrigation = it.Sum(i => i.PumpCountIrrigation),
                        PumpAutoIrrigation = it.Sum(i => i.PumpAutoIrrigation),
                        SaltWaterIrrigation = it.Any(i => i.SaltWaterIrrigation == true),
                        SmellIrrigation = it.Any(i => i.SmellIrrigation == true),
                        FilmOfOilIrrigation = it.Any(i => i.FilmOfOilIrrigation == true),
                        FogWaterIrrigation = it.Any(i => i.FogWaterIrrigation == true),
                        AgricultureIrrigation = it.Sum(i => i.AgricultureIrrigation),
                        ServiceIrrigation = it.Sum(i => i.ServiceIrrigation),
                        ProductIrrigation = it.Sum(i => i.ProductIrrigation),
                        DrinkIrrigation = it.Sum(i => i.DrinkIrrigation),
                        PlantIrrigation = it.Sum(i => i.PlantIrrigation),
                        FarmIrrigation = it.Sum(i => i.FarmIrrigation),
                        PumpCountRiver = it.Sum(i => i.PumpCountRiver),
                        PumpAutoRiver = it.Sum(i => i.PumpAutoRiver),
                        SaltWaterRiver = it.Any(i => i.SaltWaterRiver == true),
                        SmellRiver = it.Any(i => i.SmellRiver == true),
                        FilmOfOilRiver = it.Any(i => i.FilmOfOilRiver == true),
                        FogWaterRiver = it.Any(i => i.FogWaterRiver == true),
                        AgricultureRiver = it.Sum(i => i.AgricultureRiver),
                        ServiceRiver = it.Sum(i => i.ServiceRiver),
                        ProductRiver = it.Sum(i => i.ProductRiver),
                        DrinkRiver = it.Sum(i => i.DrinkRiver),
                        PlantRiver = it.Sum(i => i.PlantRiver),
                        FarmRiver = it.Sum(i => i.FarmRiver),
                        AgricultureRain = it.Sum(i => i.AgricultureRain),
                        ServiceRain = it.Sum(i => i.ServiceRain),
                        ProductRain = it.Sum(i => i.ProductRain),
                        DrinkRain = it.Sum(i => i.DrinkRain),
                        PlantRain = it.Sum(i => i.PlantRain),
                        CanComputeCubicMeterSurfaceForAgriculture = status.CheckStatusCompute(it.Select(i => i.CanComputeCubicMeterSurfaceForAgriculture)),
                        CanComputeCubicMeterSurfaceForService = status.CheckStatusCompute(it.Select(i => i.CanComputeCubicMeterSurfaceForService)),
                        CanComputeCubicMeterSurfaceForProduct = status.CheckStatusCompute(it.Select(i => i.CanComputeCubicMeterSurfaceForProduct)),
                        CanComputeCubicMeterSurfaceForDrink = status.CheckStatusCompute(it.Select(i => i.CanComputeCubicMeterSurfaceForDrink)),
                        IsAgriculture = it.Sum(i => i.IsAgriculture),
                        IsHouseHold = it.Sum(i => i.IsHouseHold),
                        IsHouseHoldGoodPlumbing = it.Sum(i => i.IsHouseHoldGoodPlumbing),
                        IsAgricultureHasIrrigationField = it.Sum(i => i.IsAgricultureHasIrrigationField),
                        IsHouseHoldHasPlumbingDistrict = it.Sum(i => i.IsHouseHoldHasPlumbingDistrict),
                        IsHouseHoldHasPlumbingCountryside = it.Sum(i => i.IsHouseHoldHasPlumbingCountryside),
                        IsFactorialWaterQuality = it.Sum(i => i.IsFactorialWaterQuality),
                        IsCommercialWaterQuality = it.Sum(i => i.IsCommercialWaterQuality),
                        CountGroundWater = it.Sum(i => i.CountGroundWater),
                        CountPopulation = it.Sum(i => i.CountPopulation),
                        CountWorkingAge = it.Sum(i => i.CountWorkingAge),
                        IsFactorial = it.Sum(i => i.IsFactorial),
                        IsFactorialWaterTreatment = it.Sum(i => i.IsFactorialWaterTreatment),
                        IsCommunityWaterManagementHasWaterTreatment = it.Sum(i => i.IsCommunityWaterManagementHasWaterTreatment),
                        FieldCommunity = it.Sum(i => i.FieldCommunity),
                        AvgWaterHeightCm = it.Sum(i => i.AvgWaterHeightCm),
                        TimeWaterHeightCm = it.Sum(i => i.TimeWaterHeightCm),
                        HasntPlumbing = it.Sum(i => i.HasntPlumbing),
                        IsGovernment = it.Sum(i => i.IsGovernment),
                        IsGovernmentUsage = it.Sum(i => i.IsGovernmentUsage),
                        IsGovernmentWaterQuality = it.Sum(i => i.IsGovernmentWaterQuality),
                        CommunityNatureDisaster = it.Sum(i => i.CommunityNatureDisaster),
                        WaterSources = it.Sum(i => i.WaterSources),
                        IndustryHasWasteWaterTreatment = it.Sum(i => i.IndustryHasWasteWaterTreatment),
                        PeopleInFloodedArea = it.Sum(i => i.PeopleInFloodedArea),
                        CubicMeterGroundWaterForAgriculture = it.Sum(i => i.CubicMeterGroundWaterForAgriculture),
                        CubicMeterGroundWaterForService = it.Sum(i => i.CubicMeterGroundWaterForService),
                        CubicMeterGroundWaterForProduct = it.Sum(i => i.CubicMeterGroundWaterForProduct),
                        CubicMeterGroundWaterForDrink = it.Sum(i => i.CubicMeterGroundWaterForDrink),
                        CubicMeterPlumbingForAgriculture = it.Sum(i => i.CubicMeterPlumbingForAgriculture),
                        CubicMeterPlumbingForService = it.Sum(i => i.CubicMeterPlumbingForService),
                        CubicMeterPlumbingForProduct = it.Sum(i => i.CubicMeterPlumbingForProduct),
                        CubicMeterPlumbingForDrink = it.Sum(i => i.CubicMeterPlumbingForDrink),
                        CubicMeterSurfaceForAgriculture = it.Sum(i => i.CubicMeterSurfaceForAgriculture),
                        CubicMeterSurfaceForService = it.Sum(i => i.CubicMeterSurfaceForService),
                        CubicMeterSurfaceForProduct = it.Sum(i => i.CubicMeterSurfaceForProduct),
                        CubicMeterSurfaceForDrink = it.Sum(i => i.CubicMeterSurfaceForDrink),
                        CubicMeterGroundWaterForUse = it.Sum(i => i.CubicMeterGroundWaterForUse),
                        CountCommunity = it.Sum(i => i.CountCommunity),
                        CountCommunityHasDisaster = it.Sum(i => i.CountCommunityHasDisaster),
                        Duplicate = false,
                    }).ToList();

                dataProcessedList.ForEach(data =>
                {
                    var canComputeLst = new List<StatusCompute>
                    {
                        data.CanComputeCubicMeterGroundWaterForDrink,
                        data.CanComputeCubicMeterPlumbingForDrink,
                        data.CanComputeCubicMeterSurfaceForDrink
                    };

                    data.CanComputeCubicMeterForDrink =
                        (canComputeLst.Any(it => it == StatusCompute.False))
                        ? StatusCompute.False
                        : (canComputeLst.Any(it => it == StatusCompute.True))
                            ? StatusCompute.True
                            : StatusCompute.NA;

                    data.CubicMeterForDrink = data.CountPopulation == 0
                        ? (data.CubicMeterGroundWaterForDrink +
                        data.CubicMeterPlumbingForDrink +
                        data.CubicMeterSurfaceForDrink)
                        / data.CountPopulation
                        : 0;
                });

                return dataProcessedList;
            }
        }

        public DataProcessed CommunityProcessing(string ea, CommunitySample com)
        {
            var isCommunityWaterManagementHasWaterTreatment = baseFn.IsCommunityWaterManagementHasWaterTreatment(com);
            var countGroundWaterCommunity = baseFn.CountGroundWaterCommunity(com);
            var fieldCommunity = baseFn.FieldCommunity(com);
            var communityNatureDisaster = baseFn.CommunityNatureDisaster(com);
            var waterSourcesCommunity = baseFn.WaterSourcesCommunity(ea, com);
            var cubicMeterGroundWater = baseFn.CubicMeterGroundWater(com);
            var cubicMeterSurfaceForAgriculture = baseFn.CubicMeterSurfaceForAgriculture(com);
            var cubicMeterSurfaceForDrink = baseFn.CubicMeterSurfaceForDrink(com);
            var countCommunity = baseFn.CountCommunity(com);
            var countCommunityHasDisaster = baseFn.CountCommunityHasDisaster(com);

            return new DataProcessed
            {
                SampleId = com._id,
                SampleType = "c",
                EA = ea,
                Area_Code = ea.Substring(1, 6),
                Status = StatusProcessed.Complete,
                IsCommunityWaterManagementHasWaterTreatment = isCommunityWaterManagementHasWaterTreatment,
                CountGroundWater = countGroundWaterCommunity,
                FieldCommunity = fieldCommunity,
                CommunityNatureDisaster = communityNatureDisaster,
                WaterSources = waterSourcesCommunity,
                CubicMeterGroundWaterForAgriculture = cubicMeterGroundWater.CubicMeterGroundWaterForAgriculture,
                CubicMeterGroundWaterForDrink = cubicMeterGroundWater.CubicMeterGroundWaterForDrink,
                CubicMeterSurfaceForAgriculture = cubicMeterSurfaceForAgriculture,
                CubicMeterSurfaceForDrink = cubicMeterSurfaceForDrink,
                CubicMeterGroundWaterForUse = cubicMeterGroundWater.CubicMeterGroundWaterForUse,
                CountCommunity = countCommunity,
                CountCommunityHasDisaster = countCommunityHasDisaster,
                CanComputeCubicMeterGroundWaterForAgriculture = StatusCompute.NA,
                CanComputeCubicMeterGroundWaterForService = StatusCompute.NA,
                CanComputeCubicMeterGroundWaterForProduct = StatusCompute.NA,
                CanComputeCubicMeterGroundWaterForDrink = StatusCompute.NA,
                CanComputeCubicMeterGroundWaterForUse = StatusCompute.NA,
                CanComputeCubicMeterPlumbingForAgriculture = StatusCompute.NA,
                CanComputeCubicMeterPlumbingForService = StatusCompute.NA,
                CanComputeCubicMeterPlumbingForProduct = StatusCompute.NA,
                CanComputeCubicMeterPlumbingForDrink = StatusCompute.NA,
                CanComputeCubicMeterSurfaceForAgriculture = StatusCompute.NA,
                CanComputeCubicMeterSurfaceForService = StatusCompute.NA,
                CanComputeCubicMeterSurfaceForProduct = StatusCompute.NA,
                CanComputeCubicMeterSurfaceForDrink = StatusCompute.NA,
                CanComputeCubicMeterForDrink = StatusCompute.NA,
                Duplicate = false,
            };
        }

        public DataProcessed BuildingProcessing(string ea, BuildingSample bld)
        {
            var amp_Code = ea.Substring(1, 4);
            var isDistrict = ea.Substring(7, 1) == "1";
            var cubicMeter = CalcCubicMeter(ea, bld.WaterQuantity, bld.BuildingType);

            return new DataProcessed
            {
                SampleId = bld._id,
                SampleType = "b",
                EA = ea,
                Area_Code = ea.Substring(1, 6),
                Status = StatusProcessed.Partial,
                IsHouseHold = bld.OccupiedRoomCount ?? 0,
                IsHouseHoldGoodPlumbing = bld.OccupiedRoomCount ?? 0,
                IsHouseHoldHasPlumbingDistrict = isDistrict ? bld.OccupiedRoomCount ?? 0 : 0,
                IsHouseHoldHasPlumbingCountryside = !isDistrict ? bld.OccupiedRoomCount ?? 0 : 0,
                CountPopulation = bld.PeopleCount ?? 0,
                CountWorkingAge = bld.PeopleCount / 2 ?? 0,
                CubicMeterPlumbingForDrink = cubicMeter,
                CanComputeCubicMeterPlumbingForDrink = StatusCompute.True,
                CanComputeCubicMeterGroundWaterForAgriculture = StatusCompute.NA,
                CanComputeCubicMeterGroundWaterForService = StatusCompute.NA,
                CanComputeCubicMeterGroundWaterForProduct = StatusCompute.NA,
                CanComputeCubicMeterGroundWaterForDrink = StatusCompute.NA,
                CanComputeCubicMeterGroundWaterForUse = StatusCompute.NA,
                CanComputeCubicMeterPlumbingForAgriculture = StatusCompute.NA,
                CanComputeCubicMeterPlumbingForService = StatusCompute.NA,
                CanComputeCubicMeterPlumbingForProduct = StatusCompute.NA,
                CanComputeCubicMeterSurfaceForAgriculture = StatusCompute.NA,
                CanComputeCubicMeterSurfaceForService = StatusCompute.NA,
                CanComputeCubicMeterSurfaceForProduct = StatusCompute.NA,
                CanComputeCubicMeterSurfaceForDrink = StatusCompute.NA,
                Duplicate = false,
            };
        }

        public double CalcCubicMeter(string ea, PlumbingUsage data, BuildingType? type)
        {
            switch (data.WaterQuantity)
            {
                case WaterQuantity.CubicMeterPerMonth:
                    return (data.CubicMeterPerMonth ?? 0) * 12;
                case WaterQuantity.WaterBill:
                    return (data.WaterBill ?? 0) / divider(ea, type) * 12;
                default:
                    return 0;
            }
        }

        public double divider(string ea, BuildingType? type)
        {
            var bldType = type ?? 0;
            var BDType = new List<BuildingType>
                {
                    BuildingType.SingleHouse,BuildingType.TownHouse,BuildingType.ShopHouse,
                    BuildingType.Apartment,BuildingType.Religious,BuildingType.GreenHouse
                };
            return isCapital(ea)
                ? BDType.Contains(bldType) ? 10.5 : 13
                : BDType.Contains(bldType) ? 16.6 : 26;
        }

        public bool isCapital(string ea)
        {
            var CWT = ea.Substring(1, 2);
            var area_code = ea.Substring(1, 6);
            var cwtLst = new List<string> { "10", "11", "12" };
            var areaLst = new List<string>
                {
                    "130502","130505","130507","130602",
                    "130603","130604","130605","130606",
                    "130608","130114","730504","730610",
                    "730701","740112","740202","740209"
                };
            return cwtLst.Contains(CWT) || areaLst.Contains(area_code);
        }
    }
}