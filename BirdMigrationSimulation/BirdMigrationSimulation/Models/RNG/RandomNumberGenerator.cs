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

        /// <summary>
        /// An Implementation of Constant Distribution
        /// </summary>
        public ConstantDistribution Constant { get; private set; }

        /// <summary>
        /// An Implementation of the Uniform Distribution
        /// </summary>
        public UniformDistribution Uniform { get; private set; }

        /// <summary>
        /// An Implementation of the Normal Distribution
        /// </summary>
        public NormalDistribution Normal { get; private set; }

        /// <summary>
        /// An Implementation of the Poisson Distribution
        /// </summary>
        public PoissonDistribution Poisson { get; private set; }

        public RandomNumberGenerator()
        {
            this.sourceRandom = new Random();
            PopulateGenerators();
        }

        public RandomNumberGenerator(int seed)
        {
            this.sourceRandom = new Random(seed);
            PopulateGenerators();
        }

        public RandomNumberGenerator(Random random)
        {
            this.sourceRandom = random;
            PopulateGenerators();
            
        }

        private void PopulateGenerators()
        {
            this.Uniform = new UniformDistribution(this.sourceRandom);
            this.Poisson = new PoissonDistribution(this.sourceRandom);
            this.Normal = new NormalDistribution(this.sourceRandom);
            this.Constant = new ConstantDistribution(this.sourceRandom);
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
