using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using NSOFunction.Models;
using NSOWater.HotMigration.Models;

namespace NSOFunction
{
    public class WaterSourcesManage
    {
        public string Area_Code { get; set; }
        public List<DetailWaterManagement> Details { get; set; }
        public IMongoCollection<Reservoir> Reservoir { get; set; }

        public WaterSourcesManage(string ea, List<DetailWaterManagement> details, IMongoCollection<Reservoir> reservoir)
        {
            Area_Code = ea.Substring(1, 6);
            Details = details;
            Reservoir = reservoir;
        }

        public double GetWaterSources()
        {
            return Details.Any() ? Details.Where(it => it != null).Sum(it => CalcCapacity(it)) : 0;
        }

        public double CalcCapacity(DetailWaterManagement detail)
        {
            var name = detail?.Name;
            var reservoir = Reservoir.Find(it => it.Area_Code == Area_Code && it.Name == name).FirstOrDefaultAsync().GetAwaiter().GetResult();
            if (reservoir != null)
            {
                return reservoir.CubicMeter;
            }
            else
            {
                var projectArea = detail?.ProjectArea;
                switch (projectArea?.Shape)
                {
                    case FieldShape.Area:
                        return (((projectArea.Area.Rai ?? 0) * 1600) + ((projectArea.Area.Ngan ?? 0) * 400) + ((projectArea.Area.SqWa ?? 0) * 4)) * (projectArea.Depth ?? 0);
                    case FieldShape.Rectangle:
                        return (projectArea.Rectangle.Width ?? 0) * (projectArea.Rectangle.Length ?? 0) * (projectArea.Depth ?? 0);
                    case FieldShape.Circle:
                        return 3.14 * Math.Pow((projectArea.Diameter ?? 0) / 2, 2) * (projectArea.Depth ?? 0);
                    default:
                        return 0;
                }
            }
        }
    }
}