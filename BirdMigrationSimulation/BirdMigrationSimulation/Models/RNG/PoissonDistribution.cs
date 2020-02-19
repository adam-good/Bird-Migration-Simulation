using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdMigrationSimulation.Models.RNG
{
    public class PoissonDistribution
    {
        private Random sourceRandom;
        public PoissonDistribution(Random source)
        {
            this.sourceRandom = source;
        }

        /// <summary>
        /// Knuth Algorithm: https://en.wikipedia.org/wiki/Poisson_distribution
        /// Sample's a Poisson Distribution with mean `mean`
        /// </summary>
        /// <param name="mean">Mean of the Piosson Distribution</param>
        /// <returns>Sample</returns>
        public int DiscreteSample(double mean)
        {
            double L = Math.Exp(-mean);
            double p = 1;
            int k = 0;

            while (p > L)
            {
                k++;
                p = p * sourceRandom.NextDouble();
            }
            return k - 1;
        }
    }
}
