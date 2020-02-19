using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdMigrationSimulation.Models.RNG
{
    public class ConstantDistribution
    {
        private Random sourceRandom;

        public ConstantDistribution(Random source)
        {
            this.sourceRandom = source;
        }

        public double Sample(double a)
        {
            return a;
        }

    }
}
