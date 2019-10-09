using BirdMigrationSimulation.Models.Area;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdMigrationSimulation.Models.Inhabitants
{
    /// <summary>
    /// This interface will represent the birds of the simulation.
    /// </summary>
    public interface Inhabitant
    {
        /// <summary>
        /// The population which this Inhabitant belongs to
        /// </summary>
        Population Population { get; }

        /// <summary>
        /// The simulation that this Inhabitant belongs to
        /// </summary>
        Simulation Simulation { get; }

        /// <summary>
        /// The habitat that this Inhabitant currently resides in
        /// </summary>
        Habitat CurrentHabitat { get; set; }

        /// <summary>
        /// Handles the migration step of each iteration in the simulation.
        /// </summary>
        void Migrate();

        /// <summary>
        /// Handles the death step of each iteration in the simulation.
        /// It does not necissarily kill the inhabitant, but executes logic to determine if it should die.
        /// </summary>
        void HandleDeath();
    }
}
