using System.Linq;
using NSOFunction.Models;
using NSOWater.HotMigration.Models;

namespace NSOFunction
{
    public class AgricultureManage
    {
        public Agriculture agriculture { get; set; }

        public AgricultureManage(Agriculture agriculture)
        {
            this.agriculture = agriculture;
        }

        public bool AnyIrrigationField()
        {
            return IsIrrigationField(agriculture?.RicePlant) ||
                IsIrrigationField(agriculture?.RubberTree) ||
                IsIrrigationField(agriculture?.AgronomyPlant) ||
                IsIrrigationField(agriculture?.PerennialPlant) ||
                IsIrrigationField(agriculture?.HerbsPlant) ||
                IsIrrigationField(agriculture?.FlowerCrop) ||
                IsIrrigationField(agriculture?.MushroomPlant);
        }

        public bool IsIrrigationField(PlantingInfo<RicePlantingField> plant)
        {
            return plant?.Doing == true && plant.FieldCount > 0 ? plant.Fields.Any(it => it.IrrigationField == true) : false;
        }

        public bool IsIrrigationField(PlantingInfo<GrowingField> plant)
        {
            return plant?.Doing == true && plant.FieldCount > 0 ? plant.Fields.Any(it => it.IrrigationField == true) : false;
        }

        public bool IsIrrigationField(PlantingInfo<GrowingFieldWithNames> plant)
        {
            return plant?.Doing == true && plant.FieldCount > 0 ? plant.Fields.Any(it => it.IrrigationField == true) : false;
        }

        public bool IsIrrigationField(PlantingInfo<MixablePlantingField> plant)
        {
            return plant?.Doing == true && plant.FieldCount > 0 ? plant.Fields.Any(it => it.IrrigationField == true) : false;
        }

        public bool IsIrrigationField(PlantingInfo<MushroomField> plant)
        {
            return plant?.Doing == true && plant.FieldCount > 0 ? plant.Fields.Any(it => it.IrrigationField == true) : false;
        }
    }
}