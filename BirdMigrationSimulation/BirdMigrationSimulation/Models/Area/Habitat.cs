using BirdMigrationSimulation.Models.Inhabitants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdMigrationSimulation.Models.Area
{
    /// <summary>
    /// This class represents a section of land that can support a single bird nest
    /// </summary>
    class Habitat
    {
        /// <summary>
        /// The Simulation this habitat belongs to.
        /// </summary>
        public Simulation Simulation => Territory.Simulation;

        /// <summary>
        /// The Territory which this habitat exists in
        /// </summary>
        private Territory Territory { get; set; }

        /// <summary>
        /// A value in the range [0,1] representing how well suited the habitat is to supporting an inhabitant.
        /// </summary>
        public double HabitatQualityIndex { get; private set; }

        /// <summary>
        /// Coordinates to denote the location of the habitat
        /// </summary>
        public (int x, int y) Coordinates { get; private set; }

        public Inhabitant CurrentInhabitant { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="territory">The Territory object which this habitat will belong to</param>
        /// <param name="hqi">The Habitat Quality Index for this habitat</param>
        /// <param name="x">This Habitat's X coordinate</param>
        /// <param name="y">This Habitat's Y coordinate</param>
        public Habitat(Territory territory, double hqi, int x, int y)
        {
            this.Territory = territory;
            this.HabitatQualityIndex = hqi;
            this.Coordinates = (x, y);
        }
    }
}
