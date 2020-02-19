using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdMigrationSimulation.Models.RNG
{
    public class UniformDistribution
    {
        private Random sourceRandom;
        public UniformDistribution(Random source)
        {
            this.sourceRandom = source;
        }

        public double ContinuousSample(double a, double b)
        {
            return sourceRandom.NextDouble() * (b - a) + a;
        }

        public int DiscreteSample(int a, int b)
        {
            return sourceRandom.Next(a, b);
        }
    }
}
