using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdMigrationSimulation.Models
{
    class Simulation
    {
        public Random Rng = new Random();

        public Simulation(int rng_seed = 0)
        {
            if (rng_seed != 0)
                Rng = new Random(rng_seed);
        }

        public void Run() => throw new NotImplementedException();

        public void Stop() => throw new NotImplementedException();
    }
}
