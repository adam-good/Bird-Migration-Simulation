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

        internal List<Bird> Reproduce()
        {
            BirdFactory birdFactory = new BirdFactory(this.Population);
            int numOffspring = Rng.Next(0, 3);
            List<Bird> newborns = new List<Bird>();
            for (int i = 0; i < numOffspring; i++)
            {
                Sex sex = (Sex)Rng.Next(0, 1);
                Bird bird = birdFactory.CreateBird(sex, Age.NewBorn, 0); // TODO: Fix bird id problem here
                newborns.Add(bird);
            }

            //return newborns;
            return new List<Bird>();
            //throw new NotImplementedException();
        }
    }
}
