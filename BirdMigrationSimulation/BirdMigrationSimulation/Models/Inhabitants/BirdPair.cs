using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirdMigrationSimulation.Models.Area;

namespace BirdMigrationSimulation.Models.Inhabitants
{
    class BirdPair : Inhabitant
    {
        public Population Population { get; private set; }

        public Simulation Simulation => Population.Simulation;

        public Habitat CurrentHabitat { get; private set; }

        public (Bird MaleBird, Bird FemaleBird) Pair { get; private set; }

        public BirdPair(Population population, Habitat currentHabitat, Bird maleBird, Bird femaleBird)
        {
            this.Population = population;
            this.CurrentHabitat = currentHabitat;
            this.Pair = (maleBird, femaleBird);
        }

        public void Die()
        {
            throw new NotImplementedException();
        }

        public void Migrate()
        {
            throw new NotImplementedException();
        }
    }
}
