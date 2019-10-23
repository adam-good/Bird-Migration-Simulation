using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using BirdMigrationSimulation.Models.Area;

namespace BirdMigrationSimulation
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            //Console.WriteLine(System.IO.Directory.GetCurrentDirectory());
            int seed = 8675309;
            Models.Simulation sim = new Models.Simulation((32, 32), 32, seed);
            Thread simThread = new Thread(() => sim.Run(1000, 1000));
            simThread.Start();

            Application app = new Application();
            app.Run(new Views.SimulationWindow(sim));

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
