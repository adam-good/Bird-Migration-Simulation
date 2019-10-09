using BirdMigrationSimulation.Models.Area;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdMigrationSimulation.ViewModels
{
    class TerritoryViewModel : ObservableObject
    {
        private readonly Territory territory;
        private List<Habitat> habitats => territory.HabitatGrid;

        public List<HabitatViewModel> HabitatViews = new List<HabitatViewModel>();

        public TerritoryViewModel(Territory territory)
        {
            this.territory = territory;
            foreach (var habitat in habitats)
                HabitatViews.Add(new HabitatViewModel(habitat));
        }
    }
}
