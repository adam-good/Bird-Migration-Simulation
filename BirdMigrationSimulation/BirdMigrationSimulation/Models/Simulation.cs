using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BirdMigrationSimulation.Models.Area;

namespace BirdMigrationSimulation.Models
{
    /// <summary>
    /// Represents the actual simulation that can be run.
    /// </summary>
    class Simulation
    {
        public Random Rng { get; private set; }

        private Territory Territory { get; set; }

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
        }

        public void Run() => throw new NotImplementedException();

        public void Stop() => throw new NotImplementedException();
    }
}
