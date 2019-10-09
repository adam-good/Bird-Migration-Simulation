using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirdMigrationSimulation.Models.Area;

namespace BirdMigrationSimulation.Models.Inhabitants
{
    /// <summary>
    /// Contains the possible sexes of the birds
    /// </summary>
    public enum Sex
    {
        Male,
        Female
    }

    /// <summary>
    /// Contains the possible ages of the birds
    /// </summary>
    public enum Age
    {
        Adult,
        Juvenile
    }

    /// <summary>
    /// This class shall represent the birds in the simulation.
    /// </summary>
    public class Bird : Inhabitant
    {
        public Population Population { get; private set; }
        public Simulation Simulation => Population.Simulation;
        private Random Rng => Simulation.Rng;

        public long birdId { get; private set; }
        public Habitat CurrentHabitat { get; set; }
        public Sex Sex { get; private set; }
        public Age Age { get; private set; }

        public Bird(Population population, Sex sex, Age age, long id)
        {
            this.Population = population;
            this.Sex = sex;
            this.Age = age;
            this.birdId = id;
        }

        public void HandleDeath()
        {
            throw new NotImplementedException();
        }

        public void Migrate()
        {
            List<Habitat> neighbors = CurrentHabitat.GetNeighbors().Where(h => h.IsEmpty).ToList();
            int idx = Rng.Next(neighbors.Count);
            Habitat nextHabitat = neighbors[idx];
            Population.MoveBird(this, nextHabitat);
        }
    }
}
