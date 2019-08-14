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
        public List<Habitat> Habitats { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="simulation">The simulation this territory will belong to</param>
        public Territory(Simulation simulation)
        {
            this.Simulation = simulation;
        }
    }
}
