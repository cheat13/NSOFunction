using System;
using System.Collections.Generic;
using System.Linq;
using NSOFunction.Models;
using NSOWater.HotMigration.Models;

namespace NSOFunction
{
    public class StatusComputeManage
    {
        public StatusCompute CheckStatusCompute(IEnumerable<StatusCompute> statuses)
        {
            return statuses.Any(it => it == StatusCompute.False)
                ? StatusCompute.False
                : statuses.Any(it => it == StatusCompute.True)
                    ? StatusCompute.True
                    : StatusCompute.NA;
        }
    }
}