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

        private Color DetermineColor()
        {
            if (habitat.MainInhabitant is MaleBird)
                return Color.FromRgb(0, 0, 255);
            else if (habitat.MainInhabitant is FemaleBird)
                return Color.FromRgb(255, 0, 0);
            else if (habitat.MainInhabitant is BirdPair)
                return Color.FromRgb(0, 255, 0);
            else
            {
                //byte g = Convert.ToByte(Math.Floor(HabitatQualityIndex * 255));
                //Color color = Color.FromRgb(g, g, g);
                //return color;
                return Color.FromRgb(0,0,0);
            }
        }
    }
}
