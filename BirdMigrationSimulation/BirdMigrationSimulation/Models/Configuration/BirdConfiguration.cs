using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdMigrationSimulation.Models.Configuration
{
    class BirdConfiguration
    {
        public int AdultMigrationMoves { get; private set; }
        public double AdultHabitatSelectivity { get; private set; }
        public int JuvenileMigrationMoves { get; private set; }
        public double JuvenileHabitatSelectivity { get; private set; }
    }
}
