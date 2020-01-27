using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdMigrationSimulation.Models.RNG
{
    /// <summary>
    /// Class that contains various types of random number generators / distributions
    /// </summary>
    class RNG : Random
    {
        private Random sourceRandom = new Random();
        public UniformDistribution Uniform { get; private set; }
        public PoissonDistribution Poisson { get; private set; }

        public RNG()
        {
            this.Uniform = new UniformDistribution(sourceRandom);
            this.Poisson = new PoissonDistribution(sourceRandom);
        }

        public override double NextDouble()
        {
            return sourceRandom.NextDouble();
        }

        public override int Next()
        {
            return sourceRandom.Next();
        }

        public override int Next(int maxValue)
        {
            return sourceRandom.Next(maxValue);
        }

        public override int Next(int minValue, int maxValue)
        {
            return sourceRandom.Next(minValue, maxValue);
        }

        public override void NextBytes(byte[] buffer)
        {
            sourceRandom.NextBytes(buffer);
        }
    }
}
