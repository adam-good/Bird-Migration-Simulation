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

            Console.WriteLine("Please Select Data Output Path");
            Console.WriteLine("Press ENTER key to continue...");
            Console.ReadKey();

            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.ShowDialog();
            string outputpath = fbd.SelectedPath + '\\';
            Console.WriteLine(outputpath);
            Console.WriteLine("Press ENTER key to START");
            Console.ReadKey();

            int seed = 8675309;
            Models.Simulation sim = new Models.Simulation(outputpath, (32, 32), 32, seed);
            Thread simThread = new Thread(() => sim.Run(300, 100));
            simThread.Start();

            Application app = new Application();
            app.Run(new Views.SimulationWindow(sim));

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
