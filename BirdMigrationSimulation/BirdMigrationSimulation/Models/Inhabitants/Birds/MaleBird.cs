using BirdMigrationSimulation.Models.Area;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdMigrationSimulation.Models.Inhabitants.Birds
{
    internal class MaleBird : Bird
    {
        public override Sex Sex => Sex.Male;

        public int MigrationMoves { get; private set; } = 3;
        public double HabitatSelectivity { get; private set; } = 0.5;

        public MaleBird(Population population, int age, long id) : base(population, age, id) { }

        public override void Migrate()
        {
            bool settle = false;
            int moves = 0;
            while (settle == false && moves < MigrationMoves)
            {
                double hqiThreshold = Math.Pow(CurrentHabitat.HabitatQualityIndex, HabitatSelectivity);
                if (Rng.NextDouble() < hqiThreshold)
                    settle = true;
                else
                {
                    List<Habitat> neighbors = CurrentHabitat.GetNeighbors().Where(h => h.IsEmpty).ToList();
                    if (neighbors.Count == 0) // If we can't move anywhere just chill here I guess?
                        return;
                    int idx = Rng.Next(neighbors.Count);
                    Habitat nextHabitat = neighbors[idx];
                    Population.MoveBird(this, nextHabitat);

                    moves += 1;
                }
            }
        }
    }
}
