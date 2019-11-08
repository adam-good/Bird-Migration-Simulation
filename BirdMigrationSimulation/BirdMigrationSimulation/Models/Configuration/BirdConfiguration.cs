using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdMigrationSimulation.Models.Configuration
{
    public class BirdConfiguration
    {
        public int AdultMigrationMoves { get; private set; }
        public double AdultHabitatSelectivity { get; private set; }
        public double AdultLethality { get; private set; }
        public int JuvenileMigrationMoves { get; private set; }
        public double JuvenileHabitatSelectivity { get; private set; }
        public double JuvenileLethality { get; private set; }

        public BirdConfiguration(int adultMigration, double adultSelectivity, double adultLethality,
                                 int juvenileMigration, double juvenileSelectivity, double juvenileLethality)
        {
            this.AdultMigrationMoves = adultMigration;
            this.JuvenileMigrationMoves = juvenileMigration;
            this.AdultHabitatSelectivity = adultSelectivity;
            this.JuvenileHabitatSelectivity = juvenileSelectivity;
            this.AdultLethality = adultLethality;
            this.JuvenileLethality = juvenileLethality;
        }
    }
}
