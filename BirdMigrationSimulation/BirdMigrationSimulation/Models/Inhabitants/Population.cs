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


        public long birdIDCounter = 0;

        public List<Bird> Birds => Inhabitants.Where(i => i is Bird).Cast<Bird>().ToList();
        public List<BirdPair> Pairs => Inhabitants.Where(i => i is BirdPair).Cast<BirdPair>().ToList();
        public List<Bird> NewBorns => Inhabitants.Where(i => i is Bird).Cast<Bird>().Where(b => b.Age == Age.NewBorn).ToList();
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

        internal void PopulatePopulation(int numBirds)
        {
            var habitats = Territory.HabitatGrid.Where(h => h.IsEmpty).OrderBy(x => Rng.Next()).Take(numBirds);
            foreach (var habitat in habitats)
            {
                Sex sex = (Sex)Rng.Next(0, 2);
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

        // TODO: This could probably be more efficient
        internal void Reproduce(List<BirdPair> pairs)
        {
            pairs = pairs.OrderBy(c => Rng.Next()).ToList();

            foreach (var pair in pairs)
            {
                int numOffspring = pair.Reproduce();
                for (int i = 0; i < numOffspring; i++)
                {
                    Sex sex = (Sex)Rng.Next(0, 2);
                    Bird babyBird = AddBird(pair.CurrentHabitat, sex, Age.NewBorn);
                    NewBorns.Add(babyBird);
                }
            }
        }

        public void HandleDeath(List<Bird> birds)
        {
            birds = birds.OrderBy(c => Rng.Next()).ToList();

            foreach (var bird in birds)
                bird.HandleDeath();

            foreach (var pair in Pairs)
                pair.HandleDeath();
        }

        public void MoveBird(Bird bird, Habitat newHabitat)
        {
            Habitat currentHabitat = bird.CurrentHabitat;
            currentHabitat.RemoveInhabitant(bird);
            newHabitat.InsertInhabitant(bird);
            bird.CurrentHabitat = newHabitat;
        }

        public void RemoveDeadBirds()
        {
            var deadBirds = Birds.Where(b => b.IsLive == false).ToList();
            foreach (var bird in deadBirds)
                RemoveInhabitant(bird);
        }

        public void RemoveInactivePairs()
        {
            var pairs = Pairs.Where(p => p.IsActive == false).ToList();
            foreach (var pair in pairs)
            {
                pair.Pair.MaleBird.IsPaired = false;
                pair.Pair.FemaleBird.IsPaired = false;
                RemoveInhabitant(pair);
            }
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

        internal void UnpairBirds(BirdPair pair)
        {
            // Maybe this shouldn't happen here...
            Inhabitants.Remove(pair);
            pair.CurrentHabitat.Inhabitants.Remove(pair);
            pair.Pair.MaleBird.IsPaired = false;
            pair.Pair.FemaleBird.IsPaired = false;
        }

        public void AddInhabitant(Inhabitant inhabitant) => throw new NotImplementedException();

        public void AddNewBorn(Bird newborn) => throw new NotImplementedException();

        public void RemoveInhabitant(Inhabitant inhabitant)
        {
            Inhabitants.Remove(inhabitant);
            inhabitant.CurrentHabitat.RemoveInhabitant(inhabitant);
        }
    }
}
