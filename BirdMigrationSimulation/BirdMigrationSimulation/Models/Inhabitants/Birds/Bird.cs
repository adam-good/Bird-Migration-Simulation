using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirdMigrationSimulation.Models.Area;

namespace BirdMigrationSimulation.Models.Inhabitants.Birds
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
    public abstract class Bird : Inhabitant
    {
        public Population Population { get; private set; }
        public Simulation Simulation => Population.Simulation;
        protected Random Rng => Simulation.Rng;

        public long birdId { get; private set; }
        public Habitat CurrentHabitat { get; set; }
        public abstract Sex Sex { get; }
        public Age Age { get; private set; }
        public bool IsPaired { get; protected set; }

        //public Bird(Population population, Sex sex, Age age, long id)
        //{
        //    this.Population = population;
        //    this.Sex = sex;
        //    this.Age = age;
        //    this.birdId = id;
        //}

        public Bird(Population population, Age age, long id)
        {
            this.Population = population;
            this.Age = age;
            this.birdId = id;
        }

        public void HandleDeath()
        {
            throw new NotImplementedException();
        }

        public abstract void Migrate();
        //{
        //    List<Habitat> neighbors = CurrentHabitat.GetNeighbors().Where(h => h.IsEmpty).ToList();
        //    int idx = Rng.Next(neighbors.Count);
        //    Habitat nextHabitat = neighbors[idx];
        //    Population.MoveBird(this, nextHabitat);
        //}
    }
}
