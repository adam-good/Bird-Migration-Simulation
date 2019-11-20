using BirdMigrationSimulation.Models;
using BirdMigrationSimulation.Models.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace BirdMigrationSimulation.Views
{
    public partial class SimulationWindow : Window
    {
        private Simulation simulation;
        private SimulationConfiguration Configuration => simulation.Configuration;

        DispatcherTimer updateTimer;

        public SimulationWindow(Simulation simulation)
        {
            InitializeComponent();
            this.simulation = simulation;
            this.Grid.InitGrid(this.simulation.Territory);

            this.updateTimer = new DispatcherTimer();
            updateTimer.Interval = Configuration.GUIUpdateTime;
            updateTimer.Tick += delegate
            {
                this.Grid.Update();
            };
            updateTimer.Start();
        }
    }
}
