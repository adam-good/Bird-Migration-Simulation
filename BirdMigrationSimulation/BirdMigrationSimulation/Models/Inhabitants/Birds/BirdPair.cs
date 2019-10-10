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

        public Habitat CurrentHabitat { get; set; }

        public (Bird MaleBird, Bird FemaleBird) Pair { get; private set; }

        public BirdPair(Population population, Habitat currentHabitat, Bird maleBird, Bird femaleBird)
        {
            this.Population = population;
            this.CurrentHabitat = currentHabitat;
            this.Pair = (maleBird, femaleBird);
        }

        public void HandleDeath()
        {
            throw new NotImplementedException();
        }

        public void Migrate()
        {
            throw new NotImplementedException();
        }
    }
}
