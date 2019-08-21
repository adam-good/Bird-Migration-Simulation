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
    interface Inhabitant
    {
        Population Population { get; }
        Simulation Simulation { get; }
        Habitat CurrentHabitat { get; }


        void Migrate();
        void Die();
    }
}
