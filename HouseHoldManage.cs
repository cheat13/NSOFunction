using NSOWater.HotMigration.Models;
using System.Linq;

namespace NSOFunction
{
    public class HouseHoldManage
    {

        public bool IsCheckHasProblemPlumbing(Plumbing plumbing)
        {
            var plumbingMWA = plumbing?.MWA?.QualityProblem?.HasProblem == true;
            var plumbingPWA = plumbing?.PWA?.QualityProblem?.HasProblem == true;
            var plumbingOther = plumbing?.Other?.QualityProblem?.HasProblem == true;

            return plumbingMWA || plumbingPWA || plumbingOther;
        }

        public bool IsCheckProblem(WaterSources waterSources, Plumbing plumbing, GroundWater groundWater, River river, Pool pool, Irrigation irrigation)
        {
            return IsCheckPlumbing(waterSources?.Plumbing, plumbing) ||
                IsCheckUnderGround(waterSources?.UnderGround, groundWater) ||
                IsCheckPool(waterSources?.Pool, pool) ||
                IsCheckRiver(waterSources?.River, river) ||
                IsCheckIrrigation(waterSources?.Irrigation, irrigation);
        }

        public bool IsCheckPlumbing(bool? isPlumbing, Plumbing plumbing)
        {
            return (isPlumbing == true) ? IsCheckHasProblemPlumbing(plumbing) : false;
        }

        public bool IsCheckUnderGround(bool? underGround, GroundWater groundWater)
        {
            return (underGround == true) ? IsCheckHasProblemGroundWater(groundWater) : false;
        }

        public bool IsCheckPool(bool? isPool, Pool pool)
        {
            return (isPool == true) ? IsCheckHasProblemPool(pool) : false;
        }

        public bool IsCheckRiver(bool? isRiver, River river)
        {
            return (isRiver == true) ? IsCheckHasProblemRiver(river) : false;
        }

        public bool IsCheckIrrigation(bool? isIrrigation, Irrigation irrigation)
        {
            return (isIrrigation == true) ? IsCheckHasProblemIrrigation(irrigation) : false;
        }

        public bool IsCheckHasProblemGroundWater(GroundWater groundWater)
        {

            return groundWater?.PublicGroundWater.WaterResources.All(it => it.QualityProblem?.HasProblem == true) == true;
        }

        public bool IsCheckHasProblemRiver(River river)
        {
            return river?.QualityProblem.HasProblem == true;
        }

        public bool IsCheckHasProblemPool(Pool pool)
        {
            return pool?.WaterResources.All(it => it.QualityProblem.HasProblem == true) == true;
        }

        public bool IsCheckHasProblemIrrigation(Irrigation irrigation)
        {
            return irrigation?.QualityProblem.HasProblem == true;
        }
    }
}