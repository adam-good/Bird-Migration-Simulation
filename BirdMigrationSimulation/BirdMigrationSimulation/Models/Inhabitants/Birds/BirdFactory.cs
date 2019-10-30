using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdMigrationSimulation.Models.Inhabitants.Birds
{
    class BirdFactory
    {
        public Population Population { get; private set; }

        public BirdFactory(Population pop)
        {
            this.Population = pop;
        }

        public Bird CreateBird(Sex sex, int age, long birdId)
        {
            switch (sex)
            {
                case Sex.Male:      return new MaleBird(Population, age, birdId);
                case Sex.Female:    return new FemaleBird(Population, age, birdId);
                default: throw new Exception($"Failed to create bird of type: {sex} -- {age}");
            }
        }
    }
}
