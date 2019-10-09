using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdMigrationSimulation.Models.Area
{
    /// <summary>
    /// This represents the land that birds can move around
    /// It is implemented as a a wrapper around a list of habitats
    /// </summary>
    public class Territory
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

        public (int width, int height) Dimensions { get; private set; }

        /// <summary>
        /// Create a Territory with specified shape
        /// </summary>
        /// <param name="simulation">The simulation this territory will belong to</param>
        /// <param name="width">The width of the habitat grid</param>
        /// <param name="height">The height of the habitat grid</param>
        public Territory(Simulation simulation, int width, int height)
        {
            this.Simulation = simulation;
            this.Dimensions = (width, height);
            PopulateHabitatGrid();
        }

        /// <summary>
        /// Create a territory with specified size
        /// </summary>
        /// <param name="simulation">The simulation this territory will belong to</param>
        /// <param name="numHabitats">The number of habitats in the habitat grid</param>
        public Territory(Simulation simulation, int numHabitats)
        {
            this.Simulation = simulation;
            double sqrt = Math.Sqrt(numHabitats);

            // If numHabitats is a perfect square than sqrt % 1 = 0
            if (sqrt % 1 != 0)
                throw new Exception($"The Number of Habitats ({numHabitats}) must be a perfect square if dimensions are not specified!");

            int maxX = (int)sqrt;
            int maxY = (int)sqrt;

            this.Dimensions = (maxX, maxY);

            PopulateHabitatGrid();
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
        /// Create a territory with the specified habitat list
        /// </summary>
        /// <param name="simulation">The simulation this territory will belong to</param>
        /// <param name="habitatList">The list of habitats for this territory</param>
        public Territory(Simulation simulation, List<Habitat> habitatList)
        {
            this.Simulation = simulation;
            this.HabitatGrid = habitatList;
        }

        /// <summary>
        /// Populates the habitat grid by generating habitats.
        /// Grid shape is defined by the Dimensions property
        /// </summary>
        private void PopulateHabitatGrid()
        {
            int width = this.Dimensions.width;
            int height = this.Dimensions.height;
            long counter = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    double hqi = Simulation.Rng.NextDouble();
                    Habitat habitat = new Habitat(this, hqi, x, y, counter);
                    HabitatGrid.Add(habitat);
                    counter++;
                }
            }
        }


        /// <summary>
        /// Populates the habitat grid by generating habitats.
        /// Generates habitats by reading habitat data from config file
        /// </summary>
        /// <param name="configPath"></param>
        private void PopulateHabitatGrid(string configPath) => throw new NotImplementedException();

        /// <summary>
        /// Returns the Habitat at the specified coordinates. Throws exception if coordinates are invalid.
        /// </summary>
        /// <param name="x">X coordinate of desired habitat</param>
        /// <param name="y">Y coordinate of desired habitat</param>
        /// <returns>Habitat at the specified coordinates</returns>
        public Habitat GetHabitat(int x, int y)
        {
            return HabitatGrid.Where(h => h.Coordinates.x == x && h.Coordinates.y == y).First();
        }
    }
}
