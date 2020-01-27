using BirdMigrationSimulation.Models.Area;
using BirdMigrationSimulation.Models.Inhabitants.Birds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BirdMigrationSimulation.ViewModels
{
    class HabitatViewModel : ObservableObject
    {
        private readonly Habitat habitat;


        public double HabitatQualityIndex { get { return habitat.HabitatQualityIndex; } }

        public (int x, int y) Coordinates { get { return habitat.Coordinates; } }

        public SolidColorBrush DisplayColor { get { return new SolidColorBrush(DetermineColor()); } set { DisplayColor = value; RaisePropertyChangedEvent("HQIColor"); } }

        public HabitatViewModel(Habitat habitat)
        {
            this.habitat = habitat;
        }

        // TODO: This could be more efficient. Do that
        private Color DetermineColor()
        {
            var habitant = habitat.MainInhabitant;
            if (habitant is Bird)
            {
                Bird bird = habitant as Bird;
                if (bird.AgeClass != AgeClass.Adult) // If juvenile or newborn
                    return Color.FromRgb(255, 255, 255);
                else
                {
                    if (bird.Sex == Sex.Male)
                        return Color.FromRgb(0, 0, 255);
                    else
                        return Color.FromRgb(255, 0, 0);
                }
            }
            else if (habitant is BirdPair)
                return Color.FromRgb(0, 255, 0);
            else
                return Color.FromRgb(0, 0, 0);
        }
    }
}
