using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirdMigrationSimulation.Models.Area;

namespace BirdMigrationSimulation.Models.Inhabitants
{
    enum Sex
    {
        Male,
        Female
    }
    enum Age
    {
        Adult,
        Juvenile
    }

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
