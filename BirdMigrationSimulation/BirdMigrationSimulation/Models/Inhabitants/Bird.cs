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
    enum Sex
    {
        Male,
        Female
    }

    /// <summary>
    /// Contains the possible ages of the birds
    /// </summary>
    enum Age
    {
        Adult,
        Juvenile
    }

    /// <summary>
    /// This class shall represent the birds in the simulation.
    /// </summary>
    class Bird : Inhabitant
    {
        public Population Population { get; private set; }
        public Simulation Simulation => Population.Simulation;
        private Random Rng => Simulation.Rng;
        public Habitat CurrentHabitat { get; private set; }
        public Sex Sex { get; private set; }
        public Age Age { get; private set; }


        public Bird(Population population, Habitat habitat, Sex sex, Age age)
        {
            this.Population = population;
            this.CurrentHabitat = habitat;
            this.Sex = sex;
            this.Age = age;
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
