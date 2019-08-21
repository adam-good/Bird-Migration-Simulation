using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BirdMigrationSimulation.Models.Area;
using BirdMigrationSimulation.Models.Inhabitants;

namespace BirdMigrationSimulation.Models
{
    /// <summary>
    /// Represents the actual simulation that can be run.
    /// </summary>
    class Simulation
    {
        public Random Rng { get; private set; }

        public Territory Territory { get; private set; }
        public Population Population { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rng_seed">Optional seed for Random Number Generator. A value of 0 (the default value) will result in a random seed.</param>
        public Simulation(int rng_seed = 0)
        {
            if (rng_seed != 0)
                Rng = new Random(rng_seed);

            (int x, int y) = (64, 64); // This is temporary. These should come from a configuration file
            Init(x, y);
        }

        public void Init(int width, int height)
        {
            this.Territory = new Territory(this, width, height);
            this.Population = new Population(this, 10);
        }

        public void Run() => throw new NotImplementedException();

        public void Stop() => throw new NotImplementedException();
    }
}
