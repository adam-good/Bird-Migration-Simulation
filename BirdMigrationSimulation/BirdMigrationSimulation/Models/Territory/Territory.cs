using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdMigrationSimulation.Models.Territory
{
    /// <summary>
    /// This represents the land that birds can move around
    /// It is implemented as a a wrapper around a list of habitats
    /// </summary>
    class Territory
    {
        /// <summary>
        /// The Simulation this Territory belongs to
        /// </summary>
        public Simulation Simulation { get; private set; }

        /// <summary>
        /// A collection of the habitats in this territory.
        /// Indexing is spacially irrelevant to the habitat location.
        /// </summary>
        public List<Habitat> HabitatGrid { get; private set; } = new List<Habitat>();

        /// <summary>
        /// Create a Territory with specified shape
        /// </summary>
        /// <param name="simulation">The simulation this territory will belong to</param>
        /// <param name="width">The width of the habitat grid</param>
        /// <param name="height">The height of the habitat grid</param>
        public Territory(Simulation simulation, int width, int height)
        {
            this.Simulation = simulation;
            PopulateHabitatGrid(width, height);
        }

        /// <summary>
        /// Create a territory with specified size
        /// </summary>
        /// <param name="simulation">The simulation this territory will belong to</param>
        /// <param name="numHabitats">The number of habitats in the habitat grid</param>
        public Territory(Simulation simulation, int numHabitats)
        {
            this.Simulation = simulation;
            PopulateHabitatGrid(numHabitats);
        }

        /// <summary>
        /// Create a territory based off of the data in a configuration file
        /// </summary>
        /// <param name="simulation">The simulation this territory will belong to</param>
        /// <param name="configPath">The path to a configuration file that contains habitat data</param>
        public Territory(Simulation simulation, string configPath)
        {
            this.Simulation = simulation;
            PopulateHabitatGrid(configPath);
        }

        /// <summary>
        /// Populates the habitat grid by generating habitats.
        /// Grid shape is defined by maxX and maxY parameters
        /// </summary>
        /// <param name="width">The width for the habitat grid</param>
        /// <param name="height">The height for the habitat grid</param>
        private void PopulateHabitatGrid(int width, int height)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    double hqi = Simulation.Rng.NextDouble();
                    Habitat habitat = new Habitat(this, hqi, x, y);
                    HabitatGrid.Add(habitat);
                }
            }
        }

        /// <summary>
        /// Populates the habitat grid by generating habitats.
        /// Assumes grid shape is square.
        /// </summary>
        /// <param name="numHabitats">Number of habitats. Must be a perfect square. </param>
        private void PopulateHabitatGrid(int numHabitats)
        {
            double sqrt = Math.Sqrt(numHabitats);
            
            // If numHabitats is a perfect square than sqrt % 1 = 0
            if (sqrt % 1 != 0)
                throw new Exception($"The Number of Habitats ({numHabitats}) must be a perfect square if dimensions are not specified!");

            int maxX = (int)sqrt;
            int maxY = (int)sqrt;

            // Man it's interesting that C# can do this. Yay overloading!
            PopulateHabitatGrid(maxX, maxY);
        }


        /// <summary>
        /// Populates the habitat grid by generating habitats.
        /// Generates habitats by reading habitat data from config file
        /// </summary>
        /// <param name="configPath"></param>
        private void PopulateHabitatGrid(string configPath) => throw new NotImplementedException();

    }
}
