using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirdMigrationSimulation.Models.Area;

namespace BirdMigrationSimulation.Models.Inhabitants.Birds
{
    public class BirdPair : Inhabitant
    {
        public Population Population { get; private set; }

        public Simulation Simulation => Population.Simulation;

        private Random Rng => Simulation.Rng;

        public Habitat CurrentHabitat { get; set; }

        private double averageOffspring => Population.AvgOffspring;

        public (Bird MaleBird, Bird FemaleBird) Pair { get; private set; }

        public bool IsActive { get; private set; } = true;

        public BirdPair(Population population, Habitat currentHabitat, Bird maleBird, Bird femaleBird)
        {
            this.Population = population;
            this.CurrentHabitat = currentHabitat;
            this.Pair = (maleBird, femaleBird);
        }

        /// <summary>
        /// Handles the unpairing in the event of death
        /// </summary>
        public void HandleDeath()
        {
            if (Pair.MaleBird.IsLive == false || Pair.FemaleBird.IsLive == false)
                this.IsActive = false;
        }

        public void Migrate()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO: This needs renamed
        /// 
        /// This will determine how many offspring this pair will reproduce in this timestep
        /// </summary>
        /// <returns>int: Number of offspring</returns>
        internal int Reproduce()
        {
            int min = 0;
            int max = (int)Math.Round(2 * averageOffspring);
            int numOffspring = Rng.Next(min, max);
            return numOffspring;
        }
    }
}
