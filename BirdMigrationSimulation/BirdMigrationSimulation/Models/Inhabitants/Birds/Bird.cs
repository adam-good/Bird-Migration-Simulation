using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirdMigrationSimulation.Models.Area;

namespace BirdMigrationSimulation.Models.Inhabitants.Birds
{
    /// <summary>
    /// Contains the possible sexes of the birds
    /// </summary>
    public enum Sex
    {
        Male,
        Female
    }

    /// <summary>
    /// Contains the possible ages of the birds
    /// </summary>
    public enum Age
    {
        Adult,
        Juvenile,
        NewBorn
    }

    /// <summary>
    /// This class shall represent the birds in the simulation.
    /// </summary>
    public abstract class Bird : Inhabitant
    {
        public Population Population { get; private set; }
        public Simulation Simulation => Population.Simulation;
        protected Random Rng => Simulation.Rng;
        public long birdId { get; private set; }
        public Habitat CurrentHabitat { get; set; }
        public double HabitatLethality { get; set; } = 0.5;
        public abstract Sex Sex { get; }
        public Age Age { get; private set; }
        public bool IsPaired { get; internal set; }

        public bool IsLive { get; internal set; } = true;
        
        public Bird(Population population, Age age, long id)
        {
            this.Population = population;
            this.Age = age;
            this.birdId = id;
        }

        public void HandleDeath()
        {
            var hqiThreshold = 1 - Math.Pow(0.95 * CurrentHabitat.HabitatQualityIndex, HabitatLethality);
            if (Rng.NextDouble() < hqiThreshold)
            {
                this.IsLive = false;
                //Population.RemoveInhabitant(this);
            }

            //throw new NotImplementedException();
        }

        internal void IncreaseAge()
        {
            if (Age == Age.NewBorn)
                Age = Age.Juvenile;
            else if (Age == Age.Juvenile)
                Age = Age.Adult;
        }

        public abstract void Migrate();
    }
}
