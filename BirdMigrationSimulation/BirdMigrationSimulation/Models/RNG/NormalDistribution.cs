using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdMigrationSimulation.Models.RNG
{
    public class NormalDistribution
    {
        private Random sourceRandom;

        public NormalDistribution(Random source)
        {
            this.sourceRandom = source;
        }

        /// <summary>
        /// Samples a normal distribution defined by a mean and standard deviation.
        /// 
        /// Implementation of Box-Muller transform.
        /// https://en.wikipedia.org/wiki/Box–Muller_transform
        /// </summary>
        /// <param name="mean">Mean of Normal Distribution</param>
        /// <param name="stdev">Standard Deviation of Normal Distribution</param>
        /// <returns>Double value from Normal Distribution</returns>
        public double Sample(double mean, double stdev)
        {
            double u1 = sourceRandom.NextDouble();
            double u2 = sourceRandom.NextDouble();

            double theta = 2 * Math.PI * u2;
            double r = Math.Sqrt(-2 * Math.Log(u1));

            double z0 = r * Math.Cos(theta);
            //double z1 = r * Math.Sin(theta);

            return z0 * stdev + mean;
        }


        /// <summary>
        /// Generates random variable from standard normal distribution (mean=0, stdev=1).
        /// </summary>
        /// <returns>Double values from normal distribution</returns>
        public double SampleStandard()
        {
            return Sample(0.0, 1.0);
        }
    }
}
