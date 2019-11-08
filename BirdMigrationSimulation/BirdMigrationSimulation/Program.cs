using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using BirdMigrationSimulation.Models.Area;
using BirdMigrationSimulation.Models.Configuration;

namespace BirdMigrationSimulation
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Please Select Configuration File");
            Console.WriteLine("Press ENTER key to continue...");
            Console.ReadKey();

            System.Windows.Forms.OpenFileDialog fd = new System.Windows.Forms.OpenFileDialog();
            fd.ShowDialog();
            string filepath = fd.FileName;
            SimulationConfiguration configuration = new SimulationConfiguration();
            configuration.LoadConfiguration(filepath);

            string outputpath = Path.GetDirectoryName(filepath) + "\\data\\";
            if (!Directory.Exists(outputpath))
                Directory.CreateDirectory(outputpath);
            Console.WriteLine($"Output Path: {outputpath}");
            Console.WriteLine("Press ENTER key to START");
            Console.ReadKey();

            Models.Simulation simulation = new Models.Simulation(configuration, outputpath);
            Application app = new Application();
            Thread simThread = new Thread(() => {
                // TODO: Wait for window to open if possible?
                simulation.Run(configuration.Timesteps, configuration.CheckpointTimestep); 
            });

            simThread.Start();

            app.Run(new Views.SimulationWindow(simulation));

            simThread.Join();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
