using BirdMigrationSimulation.Models.Area;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdMigrationSimulation.Models.Inhabitants.Birds
{
    internal class FemaleBird : Bird
    {
        public override Sex Sex => Sex.Female;
        public int MigrationMoves { get; private set; } = 3;
        public double MigrationSelectivity { get; private set; } = 0.5;

        public FemaleBird(Population population, int age, long id) : base(population, age, id) { }

        public override void Migrate()
        {
            bool settle = false;
            int moves = 0;
            while (settle == false && moves < MigrationMoves)
            {
                List<Habitat> neighbors = CurrentHabitat.GetNeighbors();
                List<Habitat> potentialMates = neighbors.Where(h => hasPotentialMate(h)).ToList();
                if (potentialMates.Count > 0)
                {
                    int idx = Rng.Next(potentialMates.Count);
                    Habitat nextHabitat = potentialMates[idx];
                    MaleBird potentialMate = nextHabitat.MainInhabitant as MaleBird;

                    // Check if mate and habitat are suitable 
                    double hqiThreshold = Math.Pow(CurrentHabitat.HabitatQualityIndex, MigrationSelectivity);
                    if (Rng.NextDouble() < hqiThreshold)
                    {
                        settle = true;
                        Population.PairBirds(potentialMate, this, nextHabitat);
                        //Population.MoveBird(this, nextHabitat); // Do pairing here
                    }
                }
                else
                {
                    int idx = Rng.Next(neighbors.Count);
                    Habitat nextHabitat = neighbors[idx];
                    Population.MoveBird(this, nextHabitat);
                }
                moves += 1;
            }
        }

        // TODO: This should probably exist in the habitat class
        private bool hasPotentialMate(Habitat habitat)
        {
            if (habitat?.MainInhabitant is MaleBird)
            {
                MaleBird male = (MaleBird)habitat.MainInhabitant;
                if (male.IsPaired == false)
                    return true;
            }

            return false;
        }
    }
}
