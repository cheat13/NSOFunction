using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using NSOFunction.Models;
using NSOWater.HotMigration.Models;

namespace NSOFunction
{
    public class WaterFunction : IWaterFunction
    {
        public IMongoCollection<Reservoir> Reservoir { get; set; }
        public IEnumerable<EAInfo> EAInfo { get; set; }
        public WaterFunction()
        {
            var client = new MongoClient("mongodb://dbagent:Nso4Passw0rd5@mongodbproykgte5e7lvm7y-vm0.southeastasia.cloudapp.azure.com/nso");
            var database = client.GetDatabase("nso");
            var collectionEA = database.GetCollection<EAInfo>("ea");
            EAInfo = collectionEA.Find(x => true).ToListAsync().GetAwaiter().GetResult();
            Reservoir = database.GetCollection<Reservoir>("reservoir");
        }

        public AgricultureModel Agriculture(bool? isAgriculture, Agriculture agriculture)
        {
            var manage = new AgricultureManage(agriculture);
            var anyIrrigationField = manage.AnyIrrigationField();

            return new AgricultureModel
            {
                IsAgriculture = (isAgriculture == true) ? 1 : 0,
                IsAgricultureHasIrrigationField = (isAgriculture == true) && anyIrrigationField ? 1 : 0
            };
        }

        public int CommunityNatureDisaster(bool? HasDisaster, bool? HasDisasterWarning)
        {
            return HasDisaster == true && HasDisasterWarning == true ? 1 : 0;
        }

        public double CountGroundWaterCommunity(CommunityWaterManagement Management, ManagementForFarming CommunityProject)
        {
            if (CommunityProject?.Doing == true && Management?.HasWaterService == true)
            {
                var GroundWaterCount =
                    CommunityProject.Details.Sum(it => it.GroundWaterCount).Value +
                    Management.WaterServices.Where(it => it.UseGroundWater == true)
                        .Sum(it => it.GroundWaterCount).Value;

                return GroundWaterCount > 0 ? GroundWaterCount + 1 : 0;
            }
            return 0;
        }

        public List<CountGroundWaterHouseHold> CountGroundWaterHouseHold(string ea, GroundWater groundWater)
        {
            if (groundWater?.PrivateGroundWater?.Doing == true
                && groundWater.PrivateGroundWater.WaterResourceCount == groundWater.PrivateGroundWater.WaterResources.Count())
            {
                var waterResources = groundWater.PrivateGroundWater.WaterResources;
                return waterResources.GroupBy(it => GetAreaCode(ea, it.Location),
                    it => it.Location,
                    (area_Code, locationLst) => new CountGroundWaterHouseHold
                    {
                        CountGroundWater = locationLst.Count(),
                        Area_Code = area_Code,
                    }).ToList();
            }
            else
            {
                return new List<CountGroundWaterHouseHold>();
            }
        }

        public List<WaterPoolHouseHold> WaterSourcesHouseHold(string ea, bool? IsHouseHold, Pool Pools)
        {
            if (IsHouseHold == true && Pools?.Doing == true && Pools?.PoolCount > 0)
            {
                return Pools.PoolSizes.GroupBy(it => GetAreaCode(ea, it.Location),
                    it => it,
                    (area_Code, poolSizes) => new WaterPoolHouseHold
                    {
                        Capacity = poolSizes.Sum(it =>
                            {
                                var depth = it?.Depth;
                                switch (it?.Shape)
                                {
                                    case FieldShape.Area:
                                        return (it?.Area?.Rai * 1600 + it?.Area?.Ngan * 400 + it?.Area?.SqWa * 4) * depth ?? 0;
                                    case FieldShape.Rectangle:
                                        return it?.Rectangle?.Width * it?.Rectangle?.Length * depth ?? 0;
                                    case FieldShape.Circle:
                                        return 3.14 * Math.Pow((it?.Diameter ?? 0) / 2, 2) * depth ?? 0;
                                    default:
                                        return 0;
                                }
                            }),
                        Area_Code = area_Code,
                    }
                ).ToList();
            }
            else
            {
                return new List<WaterPoolHouseHold>();
            }
        }

        public double WaterSourcesCommunity(string ea, List<DetailWaterManagement> details)
        {
            var wsManage = new WaterSourcesManage(ea, details, Reservoir);
            return wsManage.GetWaterSources();
        }

        public PopulationCount CountPopulation(bool? IsHouseHold, Population Population, Residential Residence)
        {
            var count = Population?.AllPersonCount ?? Residence?.MemberCount ?? 0;
            if (count == 3000000000) count = 3;
            var skip = Population?.Skip == "true"
                ? "1"
                : Population?.Skip == "false"
                    ? "2"
                    : Population?.Skip;
            return new PopulationCount
            {
                CountPopulation = IsHouseHold == true ? (int)count : 0,
                CountWorkingAge = IsHouseHold == true ? Residence?.WorkingAge ?? 0 : 0,
                Skip = skip,
                ResidentialPersonCount = Residence?.MemberCount ?? 0,
                PopulationPersonCount = Population?.AllPersonCount ?? 0,
            };
        }

        public List<CubicMeterGroundWaterModel> CubicMeterGroundWater(string ea, bool? isAgriculture, bool? isCommercial, bool? isFactorial, bool? isHouseHold, Agriculture agriculture, Commercial commercial, Factorial factorial, Residential residential, GroundWater groundWater, Buying buying, BuildingType? buildingType)
        {
            var cmgManage = new CubicMeterGroundWaterManage(buying, buildingType);
            var area_Code = ea.Substring(1, 6);
            var cubicMeterLst = new List<CubicMeterGroundWaterModel>();

            if (groundWater?.PrivateGroundWater?.Doing == true
                && groundWater.PrivateGroundWater.WaterResourceCount == groundWater.PrivateGroundWater.WaterResources.Count())
            {
                var cubicMeterPrivate = groundWater.PrivateGroundWater.WaterResources
                    .Where(it => it != null)
                    .GroupBy(it => GetAreaCode(ea, it.Location),
                        (area, resources) =>
                        {
                            var agr = isAgriculture == true
                                ? cmgManage.CalcPrivate(resources, WaterCharacter.IsAgriculture)
                                : new CubicMeterRequest { CanCompute = StatusCompute.NA, CubicMeter = 0 };
                            var ser = isCommercial == true && commercial?.WaterSources?.UnderGround == true
                                ? cmgManage.CalcPrivate(resources, WaterCharacter.IsCommercial)
                                : new CubicMeterRequest { CanCompute = StatusCompute.NA, CubicMeter = 0 };
                            var pro = isFactorial == true && factorial?.WaterSources?.UnderGround == true
                                ? cmgManage.CalcPrivate(resources, WaterCharacter.IsFactorial)
                                : new CubicMeterRequest { CanCompute = StatusCompute.NA, CubicMeter = 0 };
                            var dri = isHouseHold == true && residential?.WaterSources?.UnderGround == true
                                ? cmgManage.CalcPrivate(resources, WaterCharacter.IsHouseHold)
                                : new CubicMeterRequest { CanCompute = StatusCompute.NA, CubicMeter = 0 };

                            return new CubicMeterGroundWaterModel
                            {
                                Area_Code = area,
                                GroundWaterCount = resources.Count(),
                                CubicMeterPerMonth = resources.Sum(it => it.UsageType.UsageCubicMeters ?? 0),
                                WaterBill = resources.Sum(it => it.UsageType.WaterBill ?? 0),
                                PumpCount = resources.Sum(it => it.PumpCount ?? 0),
                                PumpAuto = resources.Sum(it => it.Pumps.Count(i => i?.PumpAuto == true)),
                                TurbidWater = resources.Where(it => it.QualityProblem.HasProblem == true).Any(it => it.QualityProblem.Problem.TurbidWater == true),
                                SaltWater = resources.Where(it => it.QualityProblem.HasProblem == true).Any(it => it.QualityProblem.Problem.SaltWater == true),
                                Smell = resources.Where(it => it.QualityProblem.HasProblem == true).Any(it => it.QualityProblem.Problem.Smell == true),
                                FilmOfOil = resources.Where(it => it.QualityProblem.HasProblem == true).Any(it => it.QualityProblem.Problem.FilmOfOil == true),
                                FogWater = resources.Where(it => it.QualityProblem.HasProblem == true).Any(it => it.QualityProblem.Problem.FogWater == true),
                                HardWater = resources.Where(it => it.QualityProblem.HasProblem == true).Any(it => it.QualityProblem.Problem.HardWater == true),
                                Agriculture = resources.Average(it => it.WaterActivities.Agriculture ?? 0),
                                Service = resources.Average(it => it.WaterActivities.Service ?? 0),
                                Product = resources.Average(it => it.WaterActivities.Product ?? 0),
                                Drink = resources.Average(it => it.WaterActivities.Drink ?? 0),
                                Plant = resources.Average(it => it.WaterActivities.Plant ?? 0),
                                Farm = resources.Average(it => it.WaterActivities.Farm ?? 0),
                                CanComputeCubicMeterGroundWaterForAgriculture = agr.CanCompute,
                                CanComputeCubicMeterGroundWaterForService = ser.CanCompute,
                                CanComputeCubicMeterGroundWaterForProduct = pro.CanCompute,
                                CanComputeCubicMeterGroundWaterForDrink = dri.CanCompute,
                                CanComputeCubicMeterGroundWaterForUse = agr.CanCompute != StatusCompute.False && ser.CanCompute != StatusCompute.False && pro.CanCompute != StatusCompute.False && dri.CanCompute != StatusCompute.False
                                    ? StatusCompute.True
                                    : StatusCompute.False,
                                CubicMeterGroundWaterForAgriculture = agr.CubicMeter,
                                CubicMeterGroundWaterForService = ser.CubicMeter,
                                CubicMeterGroundWaterForProduct = pro.CubicMeter,
                                CubicMeterGroundWaterForDrink = dri.CubicMeter,
                                CubicMeterGroundWaterForUse = agr.CubicMeter + ser.CubicMeter + pro.CubicMeter + dri.CubicMeter,
                                AdjustedCubicMeterGroundWaterForAgriculture = agr.Adjusted,
                                AdjustedCubicMeterGroundWaterForService = ser.Adjusted,
                                AdjustedCubicMeterGroundWaterForProduct = pro.Adjusted,
                                AdjustedCubicMeterGroundWaterForDrink = dri.Adjusted,
                                AdjustedCubicMeterGroundWaterForUse = agr.Adjusted || ser.Adjusted || pro.Adjusted || dri.Adjusted,
                            };
                        }
                    ).ToList();
                cubicMeterLst.AddRange(cubicMeterPrivate);
            }

            if (groundWater?.PublicGroundWater?.Doing == true
                && groundWater.PublicGroundWater.WaterResourceCount == groundWater.PublicGroundWater.WaterResources.Count())
            {
                var cubicMeterPublic = groundWater.PublicGroundWater.WaterResources
                    .Where(it => it != null)
                    .GroupBy(it => GetAreaCode(ea, it.Location),
                        (area, resources) =>
                        {
                            var agr = isAgriculture == true
                                ? cmgManage.CalcPublic(resources, WaterCharacter.IsAgriculture)
                                : new CubicMeterRequest { CanCompute = StatusCompute.NA, CubicMeter = 0 };
                            var ser = isCommercial == true && commercial?.WaterSources?.UnderGround == true
                                ? cmgManage.CalcPublic(resources, WaterCharacter.IsCommercial)
                                : new CubicMeterRequest { CanCompute = StatusCompute.NA, CubicMeter = 0 };
                            var pro = isFactorial == true && factorial?.WaterSources?.UnderGround == true
                                ? cmgManage.CalcPublic(resources, WaterCharacter.IsFactorial)
                                : new CubicMeterRequest { CanCompute = StatusCompute.NA, CubicMeter = 0 };
                            var dri = isHouseHold == true && residential?.WaterSources?.UnderGround == true
                                ? cmgManage.CalcPublic(resources, WaterCharacter.IsHouseHold)
                                : new CubicMeterRequest { CanCompute = StatusCompute.NA, CubicMeter = 0 };
                            return new CubicMeterGroundWaterModel
                            {
                                Area_Code = area,
                                GroundWaterCount = resources.Count(),
                                CubicMeterPerMonth = resources.Sum(it => it.CubicMeterPerMonth ?? 0),
                                PumpCount = resources.Sum(it => it.PumpCount ?? 0),
                                PumpAuto = resources.Sum(it => it.Pumps.Count(i => i?.PumpAuto == true)),
                                TurbidWater = resources.Where(it => it.QualityProblem.HasProblem == true).Any(it => it.QualityProblem.Problem.TurbidWater == true),
                                SaltWater = resources.Where(it => it.QualityProblem.HasProblem == true).Any(it => it.QualityProblem.Problem.SaltWater == true),
                                Smell = resources.Where(it => it.QualityProblem.HasProblem == true).Any(it => it.QualityProblem.Problem.Smell == true),
                                FilmOfOil = resources.Where(it => it.QualityProblem.HasProblem == true).Any(it => it.QualityProblem.Problem.FilmOfOil == true),
                                FogWater = resources.Where(it => it.QualityProblem.HasProblem == true).Any(it => it.QualityProblem.Problem.FogWater == true),
                                HardWater = resources.Where(it => it.QualityProblem.HasProblem == true).Any(it => it.QualityProblem.Problem.HardWater == true),
                                Agriculture = resources.Average(it => it.WaterActivities.Agriculture ?? 0),
                                Service = resources.Average(it => it.WaterActivities.Service ?? 0),
                                Product = resources.Average(it => it.WaterActivities.Product ?? 0),
                                Drink = resources.Average(it => it.WaterActivities.Drink ?? 0),
                                Plant = resources.Average(it => it.WaterActivities.Plant ?? 0),
                                Farm = resources.Average(it => it.WaterActivities.Farm ?? 0),
                                CanComputeCubicMeterGroundWaterForAgriculture = agr.CanCompute,
                                CanComputeCubicMeterGroundWaterForService = ser.CanCompute,
                                CanComputeCubicMeterGroundWaterForProduct = pro.CanCompute,
                                CanComputeCubicMeterGroundWaterForDrink = dri.CanCompute,
                                CanComputeCubicMeterGroundWaterForUse = agr.CanCompute,
                                CubicMeterGroundWaterForAgriculture = agr.CubicMeter,
                                CubicMeterGroundWaterForService = ser.CubicMeter,
                                CubicMeterGroundWaterForProduct = pro.CubicMeter,
                                CubicMeterGroundWaterForDrink = dri.CubicMeter,
                                CubicMeterGroundWaterForUse = agr.CubicMeter + ser.CubicMeter + pro.CubicMeter + dri.CubicMeter,
                                AdjustedCubicMeterGroundWaterForAgriculture = agr.Adjusted,
                                AdjustedCubicMeterGroundWaterForService = ser.Adjusted,
                                AdjustedCubicMeterGroundWaterForProduct = pro.Adjusted,
                                AdjustedCubicMeterGroundWaterForDrink = dri.Adjusted,
                                AdjustedCubicMeterGroundWaterForUse = agr.Adjusted || ser.Adjusted || pro.Adjusted || dri.Adjusted,
                            };
                        })
                    .ToList();
                cubicMeterLst.AddRange(cubicMeterPublic);
            }

            var status = new StatusComputeManage();

            if (cubicMeterLst.Any())
            {
                var cubicMeterGroundWater = cubicMeterLst
                    .Where(it => it != null)
                    .GroupBy(it => it.Area_Code,
                        (area, items) =>
                        {
                            return new CubicMeterGroundWaterModel
                            {
                                Area_Code = area,
                                GroundWaterCount = items.Count(),
                                CubicMeterPerMonth = items.Sum(it => it.CubicMeterPerMonth),
                                PumpCount = items.Sum(it => it.PumpCount),
                                PumpAuto = items.Sum(it => it.PumpAuto),
                                TurbidWater = items.Any(it => it.TurbidWater),
                                SaltWater = items.Any(it => it.SaltWater),
                                Smell = items.Any(it => it.Smell),
                                FilmOfOil = items.Any(it => it.FilmOfOil),
                                FogWater = items.Any(it => it.FogWater),
                                HardWater = items.Any(it => it.HardWater),
                                Agriculture = items.Average(it => it.Agriculture),
                                Service = items.Average(it => it.Service),
                                Product = items.Average(it => it.Product),
                                Drink = items.Average(it => it.Drink),
                                Plant = items.Average(it => it.Plant),
                                Farm = items.Average(it => it.Farm),
                                CanComputeCubicMeterGroundWaterForAgriculture = status.CheckStatusCompute(items.Select(it => it.CanComputeCubicMeterGroundWaterForAgriculture)),
                                CanComputeCubicMeterGroundWaterForService = status.CheckStatusCompute(items.Select(it => it.CanComputeCubicMeterGroundWaterForService)),
                                CanComputeCubicMeterGroundWaterForProduct = status.CheckStatusCompute(items.Select(it => it.CanComputeCubicMeterGroundWaterForProduct)),
                                CanComputeCubicMeterGroundWaterForDrink = status.CheckStatusCompute(items.Select(it => it.CanComputeCubicMeterGroundWaterForDrink)),
                                CanComputeCubicMeterGroundWaterForUse = status.CheckStatusCompute(items.Select(it => it.CanComputeCubicMeterGroundWaterForUse)),
                                CubicMeterGroundWaterForAgriculture = items.Sum(it => it.CubicMeterGroundWaterForAgriculture),
                                CubicMeterGroundWaterForService = items.Sum(it => it.CubicMeterGroundWaterForService),
                                CubicMeterGroundWaterForProduct = items.Sum(it => it.CubicMeterGroundWaterForProduct),
                                CubicMeterGroundWaterForDrink = items.Sum(it => it.CubicMeterGroundWaterForDrink),
                                CubicMeterGroundWaterForUse = items.Sum(it => it.CubicMeterGroundWaterForUse),
                                AdjustedCubicMeterGroundWaterForAgriculture = items.Any(it => it.AdjustedCubicMeterGroundWaterForAgriculture),
                                AdjustedCubicMeterGroundWaterForService = items.Any(it => it.AdjustedCubicMeterGroundWaterForService),
                                AdjustedCubicMeterGroundWaterForProduct = items.Any(it => it.AdjustedCubicMeterGroundWaterForProduct),
                                AdjustedCubicMeterGroundWaterForDrink = items.Any(it => it.AdjustedCubicMeterGroundWaterForDrink),
                                AdjustedCubicMeterGroundWaterForUse = items.Any(it => it.AdjustedCubicMeterGroundWaterForUse),
                            };
                        })
                    .ToList();

                var cubicMeterBuying = new CubicMeterGroundWaterModel
                {
                    Area_Code = area_Code,
                    CubicMeterBuyingForAgriculture = cmgManage.CalcBuying(WaterCharacter.IsAgriculture),
                    CubicMeterBuyingForService = cmgManage.CalcBuying(WaterCharacter.IsCommercial),
                    CubicMeterBuyingForProduct = cmgManage.CalcBuying(WaterCharacter.IsFactorial),
                    CubicMeterBuyingForDrink = cmgManage.CalcBuying(WaterCharacter.IsHouseHold),
                };

                var haveLocalGroundWater = cubicMeterGroundWater.Any(it => it.Area_Code == area_Code);
                if (haveLocalGroundWater)
                {
                    cubicMeterGroundWater.ForEach(it =>
                    {
                        var ari = cubicMeterBuying.CubicMeterBuyingForAgriculture;
                        var ser = cubicMeterBuying.CubicMeterBuyingForService;
                        var pro = cubicMeterBuying.CubicMeterBuyingForProduct;
                        var dri = cubicMeterBuying.CubicMeterBuyingForDrink;
                        it.CubicMeterBuyingForAgriculture = ari;
                        it.CubicMeterBuyingForService = ser;
                        it.CubicMeterBuyingForProduct = pro;
                        it.CubicMeterBuyingForDrink = dri;
                        it.CubicMeterGroundWaterForAgriculture += ari;
                        it.CubicMeterGroundWaterForService += ser;
                        it.CubicMeterGroundWaterForProduct += pro;
                        it.CubicMeterGroundWaterForDrink += dri;
                        it.CubicMeterGroundWaterForUse += ari + ser + pro + dri;
                    });
                }
                else
                {
                    cubicMeterGroundWater.Add(cubicMeterBuying);
                }

                return cubicMeterGroundWater;
            }
            else
            {
                return new List<CubicMeterGroundWaterModel>();
            }
        }

        public CubicMeterGroundWaterCommunity CubicMeterGroundWater(ManagementForFarming managementForFarming, CommunityWaterManagement communityWaterManagement)
        {
            var cubicMeterGroundWaterForAgriculture = managementForFarming?.Doing == true && managementForFarming.Details.Any() ?
                managementForFarming.Details.Where(it => it != null).Sum(it => it.AvgGroundWaterUse ?? 0) * 12 : 0;
            var cubicMeterGroundWaterForDrink = communityWaterManagement?.OtherPlumbing == true && (communityWaterManagement.HasWaterService == true) && communityWaterManagement.WaterServices.Any() ?
                communityWaterManagement.WaterServices.Where(it => it.UseGroundWater == true).Sum(it => it.GroundWaterUsePerMonth ?? 0) * 12 : 0;
            return new CubicMeterGroundWaterCommunity
            {
                CubicMeterGroundWaterForAgriculture = cubicMeterGroundWaterForAgriculture,
                CubicMeterGroundWaterForDrink = cubicMeterGroundWaterForDrink,
                CubicMeterGroundWaterForUse = cubicMeterGroundWaterForAgriculture + cubicMeterGroundWaterForDrink
            };
        }

        public CubicMeterPlumbing CubicMeterPlumbing(bool? isAgriculture, bool? isCommercial, bool? isFactorial, bool? isHouseHold, Agriculture agriculture, Commercial commercial, Factorial factorial, Residential residential, Plumbing plumbing, BuildingType? buildingType, List<DetailOrgWaterSupply> waterServices)
        {
            var meterRentalFee = 0;
            var plumbingPrice = waterServices != null && waterServices.Any() ? waterServices.Where(it => it != null).Average(it => it.PlumbingPrice ?? 0) : 5;
            if (plumbingPrice == 0) plumbingPrice = 5;

            var cmpManage = new CubicMeterPlumbingManage(plumbing, buildingType, meterRentalFee, plumbingPrice);
            var plumbingForAgriculture = isAgriculture == true;
            var plumbingForCommercial = isCommercial == true && commercial?.WaterSources?.Plumbing == true;
            var plumbingForFactorial = isFactorial == true && factorial?.WaterSources?.Plumbing == true;
            var plumbingForResidential = isHouseHold == true && residential?.WaterSources?.Plumbing == true;
            var forAgriculture = cmpManage.CubicMeterPlumbing(plumbingForAgriculture, WaterCharacter.IsAgriculture);
            var forService = cmpManage.CubicMeterPlumbing(plumbingForCommercial, WaterCharacter.IsCommercial);
            var forProduct = cmpManage.CubicMeterPlumbing(plumbingForFactorial, WaterCharacter.IsFactorial);
            var forDrink = cmpManage.CubicMeterPlumbing(plumbingForResidential, WaterCharacter.IsHouseHold);

            var pbManage = new PlumbingManage();
            var mwa = pbManage.GetPlumbingInfo(plumbing?.MWA, plumbing?.WaterActivityMWA);
            var pwa = pbManage.GetPlumbingInfo(plumbing?.PWA, plumbing?.WaterActivityPWA);
            var other = pbManage.GetPlumbingInfo(plumbing?.Other, plumbing?.WaterActivityOther);
            return new CubicMeterPlumbing
            {
                DoingMWA = mwa.Doing,
                CubicMeterPerMonthMWA = mwa.CubicMeterPerMonth,
                WaterBillMWA = mwa.WaterBill,
                TurbidWaterMWA = mwa.TurbidWater,
                SaltWaterMWA = mwa.SaltWater,
                SmellMWA = mwa.Smell,
                FilmOfOilMWA = mwa.FilmOfOil,
                FogWaterMWA = mwa.FogWater,
                HardWaterMWA = mwa.HardWater,
                AgricultureMWA = mwa.Agriculture,
                ServiceMWA = mwa.Service,
                ProductMWA = mwa.Product,
                DrinkMWA = mwa.Drink,
                PlantMWA = mwa.Plant,
                DoingPWA = pwa.Doing,
                CubicMeterPerMonthPWA = pwa.CubicMeterPerMonth,
                WaterBillPWA = pwa.WaterBill,
                TurbidWaterPWA = pwa.TurbidWater,
                SaltWaterPWA = pwa.SaltWater,
                SmellPWA = pwa.Smell,
                FilmOfOilPWA = pwa.FilmOfOil,
                FogWaterPWA = pwa.FogWater,
                HardWaterPWA = pwa.HardWater,
                AgriculturePWA = pwa.Agriculture,
                ServicePWA = pwa.Service,
                ProductPWA = pwa.Product,
                DrinkPWA = pwa.Drink,
                PlantPWA = pwa.Plant,
                DoingOther = other.Doing,
                CubicMeterPerMonthOther = other.CubicMeterPerMonth,
                WaterBillOther = other.WaterBill,
                TurbidWaterOther = other.TurbidWater,
                SaltWaterOther = other.SaltWater,
                SmellOther = other.Smell,
                FilmOfOilOther = other.FilmOfOil,
                FogWaterOther = other.FogWater,
                HardWaterOther = other.HardWater,
                AgricultureOther = other.Agriculture,
                ServiceOther = other.Service,
                ProductOther = other.Product,
                DrinkOther = other.Drink,
                PlantOther = other.Plant,
                MeterRentalFee = meterRentalFee,
                PlumbingPrice = plumbingPrice,
                CanComputeCubicMeterPlumbingForAgriculture = forAgriculture.CanCompute,
                CubicMeterPlumbingForAgriculture = forAgriculture.CubicMeter,
                CanComputeCubicMeterPlumbingForService = forService.CanCompute,
                CubicMeterPlumbingForService = forService.CubicMeter,
                CanComputeCubicMeterPlumbingForProduct = forProduct.CanCompute,
                CubicMeterPlumbingForProduct = forProduct.CubicMeter,
                CanComputeCubicMeterPlumbingForDrink = forDrink.CanCompute,
                CubicMeterPlumbingForDrink = forDrink.CubicMeter
            };
        }

        public List<CubicMeterSurface> CubicMeterSurface(string ea, bool? isAgriculture, bool? isCommercial, bool? isFactorial, bool? isHouseHold, Agriculture agriculture, Commercial commercial, Factorial factorial, Residential residential, Pool pool, River river, Irrigation irrigation, Rain rain)
        {
            var anyChecked = pool != null || irrigation != null || river != null || rain != null;
            if (anyChecked)
            {
                var cmsManage = new CubicMeterSurfaceManage(pool, river, irrigation, rain);
                var area_Code = ea.Substring(1, 6);

                var poolInfoLst = new List<CubicMeterSurface>();
                if (pool?.WaterResources.Any() == true)
                {
                    var agr = isAgriculture == true
                        ? cmsManage.CalcPool(pool?.WaterResources, WaterCharacter.IsAgriculture)
                        : new CubicMeterRequest { CanCompute = StatusCompute.NA, CubicMeter = 0 };
                    var ser = isCommercial == true && commercial?.WaterSources?.Pool == true
                        ? cmsManage.CalcPool(pool?.WaterResources, WaterCharacter.IsCommercial)
                        : new CubicMeterRequest { CanCompute = StatusCompute.NA, CubicMeter = 0 };
                    var pro = isFactorial == true && factorial?.WaterSources?.Pool == true
                        ? cmsManage.CalcPool(pool?.WaterResources, WaterCharacter.IsFactorial)
                        : new CubicMeterRequest { CanCompute = StatusCompute.NA, CubicMeter = 0 };
                    var dri = isHouseHold == true && residential?.WaterSources?.Pool == true
                        ? cmsManage.CalcPool(pool?.WaterResources, WaterCharacter.IsHouseHold)
                        : new CubicMeterRequest { CanCompute = StatusCompute.NA, CubicMeter = 0 };

                    var waterResourceCount = pool?.WaterResources?.Count() ?? 0;
                    var cubicMeterPerMonthPool = pool?.WaterResources?.Sum(it => it?.CubicMeterPerMonth ?? 0) ?? 0;
                    var pumpCountPool = pool?.WaterResources?.Sum(it => it?.PumpCount ?? 0) ?? 0;
                    var pumpAutoPool = pool?.WaterResources?.Sum(it => it?.Pumps?.Count(i => i.PumpAuto == true) ?? 0) ?? 0;
                    var saltWaterPool = pool?.WaterResources?.Any(it => it?.QualityProblem?.HasProblem == true && it?.QualityProblem?.Problem?.SaltWater == true) == true;
                    var smellPool = pool?.WaterResources?.Any(it => it?.QualityProblem?.HasProblem == true && it?.QualityProblem?.Problem?.Smell == true) == true;
                    var filmOfOilPool = pool?.WaterResources?.Any(it => it?.QualityProblem?.HasProblem == true && it?.QualityProblem?.Problem?.FilmOfOil == true) == true;
                    var fogWaterPool = pool?.WaterResources?.Any(it => it?.QualityProblem?.HasProblem == true && it?.QualityProblem?.Problem?.FogWater == true) == true;
                    var agriculturePool = pool?.WaterResources?.Average(it => it?.WaterActivities?.Agriculture ?? 0) ?? 0;
                    var servicePool = pool?.WaterResources?.Average(it => it?.WaterActivities?.Service ?? 0) ?? 0;
                    var productPool = pool?.WaterResources?.Average(it => it?.WaterActivities?.Product ?? 0) ?? 0;
                    var drinkPool = pool?.WaterResources?.Average(it => it?.WaterActivities?.Drink ?? 0) ?? 0;
                    var plantPool = pool?.WaterResources?.Average(it => it?.WaterActivities?.Plant ?? 0) ?? 0;
                    var farmPool = pool?.WaterResources?.Average(it => it?.WaterActivities?.Farm ?? 0) ?? 0;

                    poolInfoLst = pool?.PoolSizes
                        .Where(it => it != null)
                        .GroupBy(it => GetAreaCode(ea, it.Location),
                            (area, items) =>
                            {
                                var poolCount = pool.PoolCount ?? 0;
                                var count = items.Count();
                                return new CubicMeterSurface
                                {
                                    Area_Code = area,
                                    PoolCount = poolCount,
                                    WaterResourceCount = waterResourceCount,
                                    CubicMeterPerMonthPool = cubicMeterPerMonthPool,
                                    PumpCountPool = pumpCountPool,
                                    PumpAutoPool = pumpAutoPool,
                                    SaltWaterPool = saltWaterPool,
                                    SmellPool = smellPool,
                                    FilmOfOilPool = filmOfOilPool,
                                    FogWaterPool = fogWaterPool,
                                    AgriculturePool = agriculturePool,
                                    ServicePool = servicePool,
                                    ProductPool = productPool,
                                    DrinkPool = drinkPool,
                                    PlantPool = plantPool,
                                    FarmPool = farmPool,
                                    CanComputeCubicMeterSurfaceForAgriculture = agr.CanCompute,
                                    CanComputeCubicMeterSurfaceForService = ser.CanCompute,
                                    CanComputeCubicMeterSurfaceForProduct = pro.CanCompute,
                                    CanComputeCubicMeterSurfaceForDrink = dri.CanCompute,
                                    CubicMeterSurfaceForAgriculture = agr.CubicMeter / poolCount * count,
                                    CubicMeterSurfaceForService = ser.CubicMeter / poolCount * count,
                                    CubicMeterSurfaceForProduct = pro.CubicMeter / poolCount * count,
                                    CubicMeterSurfaceForDrink = dri.CubicMeter / poolCount * count,
                                    AdjustedCubicMeterSurfaceForAgriculture = agr.Adjusted,
                                    AdjustedCubicMeterSurfaceForService = ser.Adjusted,
                                    AdjustedCubicMeterSurfaceForProduct = pro.Adjusted,
                                    AdjustedCubicMeterSurfaceForDrink = dri.Adjusted,
                                };
                            }
                    ).ToList();
                }

                var localPool = poolInfoLst.FirstOrDefault(it => it.Area_Code == area_Code) ?? new CubicMeterSurface();
                poolInfoLst.Remove(localPool);

                var agrSurvey = cmsManage.CubicMeterSurface(WaterCharacter.IsAgriculture);
                var serSurvey = cmsManage.CubicMeterSurface(WaterCharacter.IsCommercial);
                var proSurvey = cmsManage.CubicMeterSurface(WaterCharacter.IsFactorial);
                var driSurvey = cmsManage.CubicMeterSurface(WaterCharacter.IsHouseHold);

                var status = new StatusComputeManage();

                var localSurvey = new CubicMeterSurface
                {
                    Area_Code = area_Code,
                    PoolCount = pool?.PoolCount ?? 0,
                    WaterResourceCount = localPool.WaterResourceCount,
                    CubicMeterPerMonthPool = localPool.CubicMeterPerMonthPool,
                    PumpCountPool = localPool.PumpCountPool,
                    PumpAutoPool = localPool.PumpAutoPool,
                    SaltWaterPool = localPool.SaltWaterPool,
                    SmellPool = localPool.SmellPool,
                    FilmOfOilPool = localPool.FilmOfOilPool,
                    FogWaterPool = localPool.FogWaterPool,
                    AgriculturePool = localPool.AgriculturePool,
                    ServicePool = localPool.ServicePool,
                    ProductPool = localPool.ProductPool,
                    DrinkPool = localPool.DrinkPool,
                    PlantPool = localPool.PlantPool,
                    FarmPool = localPool.FarmPool,
                    CubicMeterPerMonthIrrigation = irrigation?.CubicMeterPerMonth ?? 0,
                    PumpCountIrrigation = irrigation?.PumpCount ?? 0,
                    PumpAutoIrrigation = irrigation?.Pumps.Count(it => it.PumpAuto == true) ?? 0,
                    SaltWaterIrrigation = irrigation?.QualityProblem.HasProblem == true && irrigation?.QualityProblem.Problem.SaltWater == true,
                    SmellIrrigation = irrigation?.QualityProblem.HasProblem == true && irrigation?.QualityProblem.Problem.Smell == true,
                    FilmOfOilIrrigation = irrigation?.QualityProblem.HasProblem == true && irrigation?.QualityProblem.Problem.FilmOfOil == true,
                    FogWaterIrrigation = irrigation?.QualityProblem.HasProblem == true && irrigation?.QualityProblem.Problem.FogWater == true,
                    AgricultureIrrigation = irrigation?.WaterActivities.Agriculture ?? 0,
                    ServiceIrrigation = irrigation?.WaterActivities.Service ?? 0,
                    ProductIrrigation = irrigation?.WaterActivities.Product ?? 0,
                    DrinkIrrigation = irrigation?.WaterActivities.Drink ?? 0,
                    PlantIrrigation = irrigation?.WaterActivities.Plant ?? 0,
                    FarmIrrigation = irrigation?.WaterActivities.Farm ?? 0,
                    PumpCountRiver = river?.PumpCount ?? 0,
                    PumpAutoRiver = river?.Pumps.Count(it => it.PumpAuto == true) ?? 0,
                    SaltWaterRiver = river?.QualityProblem.HasProblem == true && river?.QualityProblem.Problem.SaltWater == true,
                    SmellRiver = river?.QualityProblem.HasProblem == true && river?.QualityProblem.Problem.Smell == true,
                    FilmOfOilRiver = river?.QualityProblem.HasProblem == true && river?.QualityProblem.Problem.FilmOfOil == true,
                    FogWaterRiver = river?.QualityProblem.HasProblem == true && river?.QualityProblem.Problem.FogWater == true,
                    AgricultureRiver = river?.WaterActivities.Agriculture ?? 0,
                    ServiceRiver = river?.WaterActivities.Service ?? 0,
                    ProductRiver = river?.WaterActivities.Product ?? 0,
                    DrinkRiver = river?.WaterActivities.Drink ?? 0,
                    PlantRiver = river?.WaterActivities.Plant ?? 0,
                    FarmRiver = river?.WaterActivities.Farm ?? 0,
                    AgricultureRain = rain?.WaterActivities.Agriculture ?? 0,
                    ServiceRain = rain?.WaterActivities.Service ?? 0,
                    ProductRain = rain?.WaterActivities.Product ?? 0,
                    DrinkRain = rain?.WaterActivities.Drink ?? 0,
                    PlantRain = rain?.WaterActivities.Plant ?? 0,
                    CanComputeCubicMeterSurfaceForAgriculture = status.CheckStatusCompute(new List<StatusCompute> { agrSurvey.CanCompute, localPool.CanComputeCubicMeterSurfaceForAgriculture }),
                    CanComputeCubicMeterSurfaceForService = status.CheckStatusCompute(new List<StatusCompute> { serSurvey.CanCompute, localPool.CanComputeCubicMeterSurfaceForService }),
                    CanComputeCubicMeterSurfaceForProduct = status.CheckStatusCompute(new List<StatusCompute> { proSurvey.CanCompute, localPool.CanComputeCubicMeterSurfaceForProduct }),
                    CanComputeCubicMeterSurfaceForDrink = status.CheckStatusCompute(new List<StatusCompute> { driSurvey.CanCompute, localPool.CanComputeCubicMeterSurfaceForDrink }),
                    CubicMeterSurfaceForAgriculture = agrSurvey.CubicMeter + localPool.CubicMeterSurfaceForAgriculture,
                    CubicMeterSurfaceForService = serSurvey.CubicMeter + localPool.CubicMeterSurfaceForService,
                    CubicMeterSurfaceForProduct = proSurvey.CubicMeter + localPool.CubicMeterSurfaceForProduct,
                    CubicMeterSurfaceForDrink = driSurvey.CubicMeter + localPool.CubicMeterSurfaceForDrink,
                    AdjustedCubicMeterSurfaceForAgriculture = agrSurvey.Adjusted || localPool.AdjustedCubicMeterSurfaceForAgriculture,
                    AdjustedCubicMeterSurfaceForService = serSurvey.Adjusted || localPool.AdjustedCubicMeterSurfaceForService,
                    AdjustedCubicMeterSurfaceForProduct = proSurvey.Adjusted || localPool.AdjustedCubicMeterSurfaceForProduct,
                    AdjustedCubicMeterSurfaceForDrink = driSurvey.Adjusted || localPool.AdjustedCubicMeterSurfaceForDrink,
                };

                return poolInfoLst.Prepend(localPool).ToList();
            }
            else
            {
                return new List<CubicMeterSurface>();
            }
        }

        public double CubicMeterSurfaceForAgriculture(ManagementForFarming managementForFarming)
        {
            return managementForFarming?.Doing == true && managementForFarming.Details.Any() ?
                managementForFarming.Details.Where(it => it != null).Sum(it => it.AvgSurfaceWaterUse ?? 0) * 12 : 0;
        }

        public double CubicMeterSurfaceForDrink(CommunityWaterManagement communityWaterManagement)
        {
            return communityWaterManagement?.OtherPlumbing == true && (communityWaterManagement?.HasWaterService == true) && communityWaterManagement.WaterServices.Any()
                ? communityWaterManagement.WaterServices.Where(it => it.HasSurfaceWater == true).Sum(it => it.SurfaceWaterPerMonth ?? 0) * 12
                : 0;
        }

        public WaterFlood Disasterous(Disasterous Disaster, bool? IsHouseHold)
        {
            var isChecked = IsHouseHold == true && Disaster?.Flooded == true;
            return new WaterFlood
            {
                AvgWaterHeightCm = isChecked
                    ? Disaster.YearsDisasterous.Average(it => it.WaterHeightCm ?? 0) / 100
                    : 0,
                TimeWaterHeightCm = isChecked
                    ? Disaster.YearsDisasterous.Average(it => (it.Count ?? 0) * (it.AvgDay ?? 0) * 24 + (it.AvgHour ?? 0))
                    : 0,
            };
        }

        public double FieldCommunity(bool? Doing, List<DetailManagementForFarming> Details)
        {
            return Doing == true && Details != null
                ? Details.Sum(it => (it.Area?.Rai ?? 0) + ((it.Area?.Ngan ?? 0) / 4) + ((it.Area?.SqWa ?? 0) / 400))
                : 0.0;
        }

        public int HasntPlumbing(Plumbing Plumbing)
        {
            var count = Plumbing?.WaterNotRunningCount ?? 0;
            if (count > 12) count = 12;
            return 12 - (int)count;
        }

        public int IndustryHasWasteWaterTreatment(bool? IsFactorial, Factorial Factory)
        {
            return IsFactorial == true &&
                ((Factory?.WorkersCount ?? 0) >= 7 || (Factory?.HeavyMachine == true)) &&
                Factory?.HasWasteWaterFromProduction == true
                ? 1 : 0;
        }

        public int IsCommercialWaterQuality(bool? IsCommercial, WaterSources waterSources, Plumbing plumbing, GroundWater groundWater, River river, Pool pool, Irrigation irrigation)
        {
            var householdManage = new HouseHoldManage();
            var isCheckProblem = householdManage.IsCheckProblem(waterSources, plumbing, groundWater, river, pool, irrigation);
            return IsCommercial == true && !isCheckProblem ? 1 : 0;
        }

        public int IsCommunityWaterManagementHasWaterTreatment(bool? HasWaterTreatment)
        {
            return (HasWaterTreatment == true) ? 1 : 0;
        }

        public int IsFactorial(bool? IsFactorial, int? WorkersCount, bool? HeavyMachine)
        {
            return (IsFactorial == true) && (WorkersCount >= 7 || HeavyMachine == true) ? 1 : 0;
        }

        public int IsFactorialWaterQuality(bool? IsFactorial, WaterSources waterSources, Plumbing plumbing, GroundWater groundWater, River river, Pool pool, Irrigation irrigation)
        {
            var householdManage = new HouseHoldManage();
            var isCheckProblem = householdManage.IsCheckProblem(waterSources, plumbing, groundWater, river, pool, irrigation);
            return (IsFactorial == true) && !isCheckProblem ? 1 : 0;
        }

        public int IsFactorialWaterTreatment(bool? IsFactorial, int? WorkersCount, bool? HeavyMachine, bool? HasWasteWaterFromProduction, bool? HasWasteWaterTreatment)
        {
            return (IsFactorial == true) && (WorkersCount >= 7 || HeavyMachine == true) && HasWasteWaterFromProduction == true && HasWasteWaterTreatment == true ? 1 : 0;
        }

        public HouseHoldModel IsHouseHoldGoodPlumbing(bool? isHouseHold, string EA, Plumbing plumbing, WaterSources waterSources)
        {
            var householdManage = new HouseHoldManage();
            var isPlumbing = waterSources?.Plumbing == true;
            var isCheckHouseHoldGoodPlumbing = !householdManage.IsCheckHasProblemPlumbing(plumbing);
            var district = EA.Substring(7, 1);

            if (isHouseHold == true)
            {
                return new HouseHoldModel
                {
                    IsHouseHold = 1,
                    IsHouseHoldGoodPlumbing = (isPlumbing == true) && isCheckHouseHoldGoodPlumbing ? 1 : 0,
                    IsHouseHoldHasPlumbingDistrict = (isPlumbing == true) && district == "1" ? 1 : 0,
                    IsHouseHoldHasPlumbingCountryside = (isPlumbing == true) && district == "2" ? 1 : 0
                };
            }
            return new HouseHoldModel();
        }

        public int PeopleInFloodedArea(bool? IsHouseHold, bool? Flooded, Population Population, Residential Residence)
        {
            var count = Population?.AllPersonCount ?? Residence?.MemberCount ?? 0;
            if (count == 3000000000) count = 3;
            return IsHouseHold == true && Flooded == true ? (int)count : 0;
        }

        public PlumbingServiceUsage PlumbingSeviceUsage(BuildingType? buildingType, Commercial Commerce, Plumbing Plumbing)
        {

            var isCheckedBDType = buildingType == BuildingType.PublicHospital ||
                buildingType == BuildingType.GovernmentOffice ||
                buildingType == BuildingType.PublicSchool;

            var isCheckedPlumbing = Commerce?.WaterSources?.Plumbing == true;

            var isCheckedAnyProblem = Plumbing?.MWA?.QualityProblem?.HasProblem == true ||
                Plumbing?.PWA?.QualityProblem?.HasProblem == true ||
                Plumbing?.Other?.QualityProblem?.HasProblem == true;

            return new PlumbingServiceUsage
            {
                IsGovernment = isCheckedBDType ? 1 : 0,
                IsGovernmentUsage = isCheckedBDType && isCheckedPlumbing ? 1 : 0,
                IsGovernmentWaterQuality = isCheckedBDType && isCheckedPlumbing && isCheckedAnyProblem ? 1 : 0,
            };
        }

        public int CountCommunity(CommunitySample com)
        {
            return com != null ? 1 : 0;
        }

        public int CountCommunityHasDisaster(CommunitySample com)
        {
            return com?.Management?.HasDisaster == true ? 1 : 0;
        }

        public string GetAreaCode(string ea, Location location)
        {
            var province = location.Province;
            var district = location.District; ;
            var subDistrict = location.SubDistrict;

            if (subDistrict == "ปวนพุ") subDistrict = "ปวนผุ";

            var result = EAInfo.FirstOrDefault(x => x.CWT_NAME == province && x.AMP_NAME == district && x.TAM_NAME == subDistrict);

            return result?.Area_Code ?? ea.Substring(1, 6);
        }

        public int IsAllHouseHoldCountryside(string EA, bool? isHouseHold)
        {
            return (EA[7] == '2' && isHouseHold == true) ? 1 : 0;
        }

        public int IsAllHouseHoldDistrict(string EA, bool? isHouseHold)
        {
            return (EA[7] == '1' && isHouseHold == true) ? 1 : 0;
        }

        public int IsAllFactorial(bool? IsFactorial)
        {
            return IsFactorial == true ? 1 : 0;
        }

        public int IsAllCommercial(bool? IsCommercial)
        {
            return IsCommercial == true ? 1 : 0;
        }
    }
}