﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirdMigrationSimulation.Models.Area;

namespace BirdMigrationSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            int seed = 8675309;
            Models.Simulation sim = new Models.Simulation((32,32),5,seed);
            sim.Run(1000);

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
