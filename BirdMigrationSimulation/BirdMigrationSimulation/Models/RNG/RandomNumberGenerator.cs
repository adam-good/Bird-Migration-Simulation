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
    public class RandomNumberGenerator : Random
    {
        public Random sourceRandom { get; private set; }
        public UniformDistribution Uniform { get; private set; }
        public PoissonDistribution Poisson { get; private set; }

        public RandomNumberGenerator()
        {
            this.sourceRandom = new Random();
            this.Uniform = new UniformDistribution(sourceRandom);
            this.Poisson = new PoissonDistribution(sourceRandom);
        }

        public RandomNumberGenerator(int seed)
        {
            this.sourceRandom = new Random(seed);
            this.Uniform = new UniformDistribution(sourceRandom);
            this.Poisson = new PoissonDistribution(sourceRandom);
        }

        public RandomNumberGenerator(Random random)
        {
            this.sourceRandom = random;
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
