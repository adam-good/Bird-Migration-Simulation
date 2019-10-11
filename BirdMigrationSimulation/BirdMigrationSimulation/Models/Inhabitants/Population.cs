using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BirdMigrationSimulation.Models.Area;
using BirdMigrationSimulation.Models.Inhabitants.Birds;

namespace BirdMigrationSimulation.Models.Inhabitants
{
    /// <summary>
    /// This class will act as a wrapper around the list of all inhabitants
    /// </summary>
    public class Population
    {
        public Simulation Simulation { get; private set; }

        public Random Rng => Simulation.Rng;

        public Territory Territory => Simulation.Territory;

        public List<Inhabitant> Inhabitants { get; private set; } = new List<Inhabitant>();

        public List<Bird> NewBorns { get; set; } = new List<Bird>();

        public long birdIDCounter = 0;

        public List<Bird> Birds => Inhabitants.Where(i => i is Bird).Cast<Bird>().ToList();
        public List<BirdPair> Pairs => Inhabitants.Where(i => i is BirdPair).Cast<BirdPair>().ToList();
        public List<Bird> SingleBirds => Birds.Where(b => b.IsPaired == false).ToList();
        public List<Bird> Males => Inhabitants.Where(i => i is Bird).Cast<Bird>().Where(b => b.Sex == Sex.Male).ToList();
        public List<Bird> Females => Inhabitants.Where(i => i is Bird).Cast<Bird>().Where(b => b.Sex == Sex.Female).ToList();
        public List<Bird> Adults => Inhabitants.Where(i => i is Bird).Cast<Bird>().Where(b => b.Age == Age.Adult).ToList();
        public List<Bird> Juveniles => Inhabitants.Where(i => i is Bird).Cast<Bird>().Where(b => b.Age == Age.Juvenile).ToList();

        public Population(Simulation simulation, int numBirds)
        {
            this.Simulation = simulation;
            PopulatePopulation(numBirds);
        }

        public Population(Simulation simulation, List<Inhabitant> inhabitants)
        {
            this.Simulation = simulation;
            this.Inhabitants = inhabitants;
        }

        private Bird AddBird(Habitat habitat, Sex sex, Age age)
        {
            //            Bird bird = new Bird(this, habitat, sex, age, birdIDCounter);
            //Bird bird = new Bird(this, sex, age, birdIDCounter);

            BirdFactory birdFactory = new BirdFactory(this);

            Bird bird = birdFactory.CreateBird(sex, age, birdIDCounter);
            birdIDCounter++;

            habitat.InsertInhabitant(bird);
            bird.CurrentHabitat = habitat;

            Inhabitants.Add(bird);
            return bird;
        }

        private void PopulatePopulation(int numBirds)
        {
            for (int i = 0; i < numBirds; i++)
            {
                Habitat habitat = this.Territory.GetHabitat(i, i);
                Sex sex = (Sex)(i % 2);
                AddBird(habitat, sex, Age.Adult);
            }
        }

        public void MigrateBirds(List<Bird> birds)
        {
            // shuffle list of birds randomly to avoid any bias in the order of the list
            birds = birds.OrderBy(c => Rng.Next()).ToList();

            foreach (var bird in birds)
                bird.Migrate();
        }

        public void MoveBird(Bird bird, Habitat newHabitat)
        {
            Habitat currentHabitat = bird.CurrentHabitat;
            currentHabitat.RemoveInhabitant(bird);
            newHabitat.InsertInhabitant(bird);
            bird.CurrentHabitat = newHabitat;
        }

        internal void PairBirds(MaleBird male, FemaleBird female, Habitat habitat)
        {
            BirdPair birdPair = new BirdPair(this, habitat, male, female);
            male.IsPaired = true;
            female.IsPaired = true;
            MoveBird(male, habitat);
            MoveBird(female, habitat);
            habitat.InsertInhabitant(birdPair);
            Inhabitants.Add(birdPair);
        }

        public void AddInhabitant(Inhabitant inhabitant) => throw new NotImplementedException();

        public void AddNewBorn(Bird newborn) => throw new NotImplementedException();
    }
}
