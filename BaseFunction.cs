using System.Collections.Generic;
using System.Linq;
using NSOFunction.Models;
using NSOWater.HotMigration.Models;

namespace NSOFunction
{
    public class BaseFunction : IBaseFunction
    {
        public WaterFunction Water { get; set; }

        public BaseFunction()
        {
            this.Water = new WaterFunction();
        }

        public AgricultureModel Agriculture(HouseHoldSample unit)
        {
            return Water.Agriculture(unit?.IsAgriculture, unit?.Agriculture);
        }

        public HouseHoldModel IsHouseHoldGoodPlumbing(HouseHoldSample unit)
        {
            return Water.IsHouseHoldGoodPlumbing(unit?.IsHouseHold, unit?.EA, unit?.WaterUsage?.Plumbing, unit?.Residence?.WaterSources);
        }

        public int IsFactorialWaterQuality(HouseHoldSample unit)
        {
            return Water.IsFactorialWaterQuality(unit?.IsFactorial, unit?.Residence?.WaterSources, unit?.WaterUsage?.Plumbing, unit?.WaterUsage?.GroundWater, unit?.WaterUsage?.River, unit?.WaterUsage?.Pool, unit?.WaterUsage?.Irrigation);
        }

        public int IsCommercialWaterQuality(HouseHoldSample unit)
        {
            return Water.IsCommercialWaterQuality(unit?.IsCommercial, unit?.Residence?.WaterSources, unit?.WaterUsage?.Plumbing, unit?.WaterUsage?.GroundWater, unit?.WaterUsage?.River, unit?.WaterUsage?.Pool, unit?.WaterUsage?.Irrigation);
        }

        public int IsFactorial(HouseHoldSample unit)
        {
            return Water.IsFactorial(unit?.IsFactorial, unit?.Factory?.WorkersCount, unit?.Factory?.HeavyMachine);
        }

        public int IsFactorialWaterTreatment(HouseHoldSample unit)
        {
            return Water.IsFactorialWaterTreatment(unit?.IsFactorial, unit?.Factory?.WorkersCount, unit?.Factory?.HeavyMachine, unit?.Factory?.HasWasteWaterFromProduction, unit?.Factory?.HasWasteWaterTreatment);
        }

        public int IsCommunityWaterManagementHasWaterTreatment(CommunitySample com)
        {
            return Water.IsCommunityWaterManagementHasWaterTreatment(com?.Management?.HasWaterTreatment);
        }

        public List<CountGroundWaterHouseHold> CountGroundWaterHouseHold(string ea, HouseHoldSample unit)
        {
            return Water.CountGroundWaterHouseHold(ea, unit?.WaterUsage?.GroundWater);
        }

        public double CountGroundWaterCommunity(CommunitySample com)
        {
            return Water.CountGroundWaterCommunity(com?.Management, com?.CommunityProject);
        }

        public PopulationCount CountPopulation(HouseHoldSample unit)
        {
            return Water.CountPopulation(unit?.IsHouseHold, unit?.Residence);
        }

        public WaterFlood Disasterous(HouseHoldSample unit)
        {
            return Water.Disasterous(unit?.Disaster, unit?.IsHouseHold);
        }

        public int HasntPlumbing(HouseHoldSample unit)
        {
            return Water.HasntPlumbing(unit?.WaterUsage?.Plumbing);
        }

        public PlumbingServiceUsage PlumbingSeviceUsage(BuildingSample building, HouseHoldSample unit)
        {
            return Water.PlumbingSeviceUsage(building?.BuildingType, unit?.Commerce, unit?.WaterUsage?.Plumbing);
        }

        public int CommunityNatureDisaster(CommunitySample com)
        {
            return Water.CommunityNatureDisaster(com?.Management?.HasDisaster, com?.Management?.HasDisasterWarning);
        }

        public int IndustryHasWasteWaterTreatment(HouseHoldSample unit)
        {
            return Water.IndustryHasWasteWaterTreatment(unit?.IsFactorial, unit?.Factory);
        }

        public int PeopleInFloodedArea(HouseHoldSample unit)
        {
            return Water.PeopleInFloodedArea(unit?.IsHouseHold, unit?.Disaster?.Flooded, unit?.Residence?.MemberCount);
        }

        public double FieldCommunity(CommunitySample com)
        {
            return Water.FieldCommunity(com?.CommunityProject?.Doing, com?.CommunityProject?.Details);
        }

        public List<WaterPoolHouseHold> WaterSourcesHouseHold(string ea, HouseHoldSample unit)
        {
            return Water.WaterSourcesHouseHold(ea, unit?.IsHouseHold, unit?.WaterUsage?.Pool);
        }

        public double WaterSourcesCommunity(string ea, CommunitySample com)
        {
            return Water.WaterSourcesCommunity(ea, com?.Management?.Details);
        }

        public List<CubicMeterGroundWaterModel> CubicMeterGroundWater(string ea, HouseHoldSample unit, BuildingSample building)
        {
            return Water.CubicMeterGroundWater(ea, unit?.IsAgriculture, unit?.IsCommercial, unit?.IsFactorial, unit?.IsHouseHold, unit?.Agriculture, unit?.Commerce, unit?.Factory, unit?.Residence, unit?.WaterUsage?.GroundWater, unit?.WaterUsage?.Buying, building?.BuildingType);
        }

        public CubicMeterGroundWaterCommunity CubicMeterGroundWater(CommunitySample com)
        {
            return Water.CubicMeterGroundWater(com?.CommunityProject, com?.Management);
        }

        public CubicMeterPlumbing CubicMeterPlumbing(HouseHoldSample unit, BuildingSample building, List<CommunitySample> com)
        {
            return Water.CubicMeterPlumbing(unit?.IsAgriculture, unit?.IsCommercial, unit?.IsFactorial, unit?.IsHouseHold, unit?.Agriculture, unit?.Commerce, unit?.Factory, unit?.Residence, unit?.WaterUsage.Plumbing, building?.BuildingType, com?.Where(it => it != null).SelectMany(it => it.Management.WaterServices).ToList());
        }

        public List<CubicMeterSurface> CubicMeterSurface(string ea, HouseHoldSample unit)
        {
            return Water.CubicMeterSurface(ea, unit?.IsAgriculture, unit?.IsCommercial, unit?.IsFactorial, unit?.IsHouseHold, unit?.Agriculture, unit?.Commerce, unit?.Factory, unit?.Residence, unit?.WaterUsage?.Pool, unit?.WaterUsage?.River, unit?.WaterUsage?.Irrigation, unit?.WaterUsage?.Rain);
        }

        public double CubicMeterSurfaceForAgriculture(CommunitySample com)
        {
            return Water.CubicMeterSurfaceForAgriculture(com?.CommunityProject);
        }

        public double CubicMeterSurfaceForDrink(CommunitySample com)
        {
            return Water.CubicMeterSurfaceForDrink(com?.Management);
        }

        public int CountCommunity(CommunitySample com)
        {
            return Water.CountCommunity(com);
        }

        public int CountCommunityHasDisaster(CommunitySample com)
        {
            return Water.CountCommunityHasDisaster(com);
        }

        public int IsAllHouseHoldCountryside(string EA, HouseHoldSample unit)
        {
            return Water.IsAllHouseHoldCountryside(EA, unit?.IsAgriculture);
        }

        public int IsAllHouseHoldDistrict(string EA, HouseHoldSample unit)
        {
            return Water.IsAllHouseHoldDistrict(EA, unit?.IsAgriculture);
        }

        public int IsAllFactorial(HouseHoldSample unit)
        {
            return Water.IsAllFactorial(unit?.IsFactorial);
        }

        public int IsAllCommercial(HouseHoldSample unit)
        {
            return Water.IsAllCommercial(unit?.IsCommercial);
        }
    }
}