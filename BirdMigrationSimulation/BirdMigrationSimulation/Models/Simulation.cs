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
        /// <param name="gridDims">The dimensions to be used for the territory grid</param>
        /// <param name="numInitialBirds">The number of birds to initially populate the simulation with</param>
        /// <param name="rng_seed">Optional seed for Random Number Generator. A value of 0 (the default value) will result in a random seed.</param>
        public Simulation((int x, int y) gridDims, int numInitialBirds, int rng_seed = 0)
        {
            if (rng_seed != 0)
                Rng = new Random(rng_seed);

            // This is temporary. These should come from a configuration file
            (int x, int y) = gridDims;
            Init(x, y, numInitialBirds);
        }

        public void Init(int width, int height, int numInitialBirds)
        {
            this.Territory = new Territory(this, width, height);
            this.Population = new Population(this, numInitialBirds);
        }

        public void Run(int timesteps)
        {
            for (int i = 0; i < timesteps; i++)
            {
                var bird = Population.Birds.First();
                Console.WriteLine($"Running Iteration {i}; Bird Location: ({bird.CurrentHabitat.Coordinates})");
                Population.MigrateBirds(Population.Birds);
            }
        }

        public void Stop() => throw new NotImplementedException();
    }
}
