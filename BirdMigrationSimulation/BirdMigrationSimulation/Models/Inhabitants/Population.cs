using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BirdMigrationSimulation.Models.Area;

namespace BirdMigrationSimulation.Models.Inhabitants
{
    /// <summary>
    /// This class will act as a wrapper around the list of all inhabitants
    /// </summary>
    class Population
    {
        public Simulation Simulation { get; private set; }

        public Random Rng => Simulation.Rng;

        public Territory Territory => Simulation.Territory;

        public List<Inhabitant> Inhabitants { get; private set; } = new List<Inhabitant>();

        public List<Bird> NewBorns { get; set; } = new List<Bird>();

        public List<BirdPair> Pairs => Inhabitants.Where(i => i is BirdPair).Cast<BirdPair>().ToList();
        public List<Bird> Males => Inhabitants.Where(i => i is Bird).Cast<Bird>().Where(b => b.Sex == Sex.Male).ToList();
        public List<Bird> Females => Inhabitants.Where(i => i is Bird).Cast<Bird>().Where(b => b.Sex == Sex.Female).ToList();
        public List<Bird> Adults => Inhabitants.Where(i => i is Bird).Cast<Bird>().Where(b => b.Age == Age.Adult).ToList();
        public List<Bird> Juveniles => Inhabitants.Where(i => i is Bird).Cast<Bird>().Where(b => b.Age == Age.Juvenile).ToList();

        public Population(Simulation simulation, int numBirds)
        {
            this.Simulation = simulation;
            PopulatePopulation(numBirds);
        }

        private void PopulatePopulation(int numBirds)
        {
            for (int i = 0; i < numBirds; i++)
            {
                Habitat habitat = this.Territory.GetHabitat(i, i);
                Bird bird = new Bird(this, habitat, Sex.Male, Age.Adult);
                this.Inhabitants.Add(bird);
            }
        }

        public void AddInhabitant(Inhabitant inhabitant) => throw new NotImplementedException();

        public void AddNewBorn(Bird newborn) => throw new NotImplementedException();
    }
}
