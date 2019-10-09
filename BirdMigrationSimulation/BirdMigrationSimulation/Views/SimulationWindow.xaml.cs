using BirdMigrationSimulation.Models;
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

        DispatcherTimer updateTimer;

        public SimulationWindow(Simulation simulation)
        {
            InitializeComponent();
            this.simulation = simulation;
            this.Grid.InitGrid(this.simulation.Territory);

            this.updateTimer = new DispatcherTimer();
            updateTimer.Interval = new TimeSpan(0, 0, 1);
            updateTimer.Tick += delegate
            {
                this.Grid.Update();
            };
            updateTimer.Start();
        }
    }
}
