using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirdMigrationSimulation.Models.Area;
using BirdMigrationSimulation.Models.RNG;

namespace BirdMigrationSimulation.Models.Inhabitants.Birds
{
    public class BirdPair : Inhabitant
    {
        public Population Population { get; private set; }

        public Simulation Simulation => Population.Simulation;

        private RandomNumberGenerator Rng => Simulation.Rng;

        public Habitat CurrentHabitat { get; set; }

        private double MaxOffspring => Population.MaxOffspring;
        private double ReproductivePower => Population.ReproductivePower;

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
        /// This will determine how many offspring this pair will reproduce in this timestep via Poisson Distribution
        /// </summary>
        /// <returns>int: Number of offspring</returns>
        internal int Reproduce()
        {
            double mean = MaxOffspring * Math.Pow(this.CurrentHabitat.HabitatQualityIndex, ReproductivePower);
            int numOffspring = Rng.Poisson.DiscreteSample(mean);
            return numOffspring;
        }
    }
}
