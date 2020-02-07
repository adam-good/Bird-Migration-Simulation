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
using BirdMigrationSimulation.Models.RNG;

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
            simulation.Territory.SaveImage(outputpath);

            Thread simThread = new Thread(() =>
            {
                // TODO: Wait for window to open if possible?
                simulation.Run(configuration.Timesteps, configuration.CheckpointTimestep);
            });

            // Set up GUI thread
            Thread guiThread = null;
            if (configuration.ShowGUI)
            {
                guiThread = new Thread(() =>
                {
                    Application app = new Application();
                    app.Run(new Views.SimulationWindow(simulation));
                });
                guiThread.SetApartmentState(ApartmentState.STA);
            }

            guiThread?.Start();
            simThread.Start();

            simThread.Join();
            guiThread?.Join();

            // TODO: Delete me later
            if (configuration.ShowGUI == false)
            {
                Console.WriteLine("Building Graphical Representation");
                guiThread = new Thread(() =>
                {
                    Application app = new Application();
                    app.Run(new Views.SimulationWindow(simulation));
                });
                guiThread.SetApartmentState(ApartmentState.STA);
                guiThread.Start();
                guiThread.Join();
            }


            Console.WriteLine("Press any key to EXIT...");
            Console.ReadKey();
        }
    }
}
