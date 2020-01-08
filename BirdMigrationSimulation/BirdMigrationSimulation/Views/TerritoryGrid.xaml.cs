using BirdMigrationSimulation.Models;
using BirdMigrationSimulation.Models.Area;
using BirdMigrationSimulation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BirdMigrationSimulation.Views
{
    /// <summary>
    /// Interaction logic for Territory.xaml
    /// </summary>
    public partial class TerritoryGrid : UserControl
    {
        private (int Width, int Height) Dimensions { get; set; }

        private Dictionary<(int x, int y), HabitatViewModel> habitatViewModels { get; set; }
        private Dictionary<HabitatViewModel, Grid> Cells { get; set; }

        public TerritoryGrid()
        {
            InitializeComponent();
        }

        public void InitGrid(Territory territory)
        {
            genViewModels(territory);
            Dimensions = territory.Dimensions;
            CreateGrid();
        }

        private void genViewModels(Territory territory)
        {
            habitatViewModels = territory.HabitatGrid.Select(h => new HabitatViewModel(h)).ToDictionary(h => h.Coordinates);
        }

        private void CreateGrid()
        {
            Cells = new Dictionary<HabitatViewModel, Grid>();
            for (int x=0; x < Dimensions.Width; x++)
            {
                for (int y=0; y < Dimensions.Height; y++)
                {
                    HabitatViewModel habitat = habitatViewModels[(x, y)];
                    Grid cell = new Grid();
                    cell.Background = habitat.DisplayColor;
                    Grid.Children.Add(cell);
                    Cells.Add(habitat, cell);
                }
            }
        }

        // TODO: Make this more efficient
        internal void Update()
        {
            foreach (var habitat in Cells.Keys.ToList())
                Cells[habitat].Background = habitat.DisplayColor;
        }
    }
}
