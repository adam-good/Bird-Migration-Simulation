using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BirdMigrationSimulation.Models.Area;
using BirdMigrationSimulation.Models.Data;
using BirdMigrationSimulation.Models.Inhabitants;

namespace BirdMigrationSimulation.Models
{
    /// <summary>
    /// Represents the actual simulation that can be run.
    /// </summary>
    class Simulation
    {
        public Random Rng { get; set; }
        public Territory Territory { get; set; }
        public Population Population { get; set; }

        public DataManager DataManager { get; private set; }

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

            this.DataManager = new DataManager(this);


            // This is temporary. These should come from a configuration file
            (int x, int y) = gridDims;
            Init(x, y, numInitialBirds);
        }

        public void LoadState(int timestep)
        {
            DataManager.LoadState(timestep);
        }

        /// <summary>
        /// This method is meant to place inhabitants in proper habitats for STATE RESTORATION ONLY
        /// TODO: Find a better solution
        /// </summary>
        /// <param name="inhabitant"></param>
        /// <param name="habitat"></param>
        public void InsertInhabitant(Inhabitant inhabitant, Habitat habitat)
        {
            if (inhabitant.CurrentHabitat != null)
                throw new Exception("Inhabitant is already in a habitat!!!");

            inhabitant.CurrentHabitat = habitat;
            habitat.InsertInhabitant(inhabitant);
        }

        public void Init(int width, int height, int numInitialBirds)
        {
            this.Territory = new Territory(this, width, height);
            this.Population = new Population(this, numInitialBirds);
        }

        public void Run(int timesteps, int checkpointStep)
        {
            for (int i = 0; i < timesteps; i++)
            {
                var bird = Population.Birds.First();
                Console.WriteLine($"Running Iteration {i}; Bird Location: ({bird.CurrentHabitat.Coordinates})");
                Population.MigrateBirds(Population.Birds);

                if (i % checkpointStep == 0)
                    DataManager.SaveState(i);
            }
        }

        public void Stop() => throw new NotImplementedException();
    }
}
