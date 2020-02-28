using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirdMigrationSimulation.Models.Area;
using BirdMigrationSimulation.Models.Configuration;

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
    public enum AgeClass
    {
        NewBorn,
        Juvenile,
        Adult,
    }

    /// <summary>
    /// This class shall represent the birds in the simulation.
    /// </summary>
    public abstract class Bird : Inhabitant
    {
        public Population Population { get; private set; }
        public Simulation Simulation => Population.Simulation;
        public BirdConfiguration Configuration => (Sex == Sex.Male) ? Simulation.Configuration.MaleBirdConfig : Simulation.Configuration.FemaleBirdConfig;
        protected Random Rng => Simulation.Rng;
        public long birdId { get; private set; }
        public Habitat CurrentHabitat { get; set; }
        public double HabitatLethality => (AgeClass == AgeClass.Adult) ? Configuration.AdultLethality : Configuration.JuvenileLethality;
        public double MaxSurvival => Population.MaxSurvival;
        public abstract Sex Sex { get; }
        public int Age { get; internal set; } = 0;
        public AgeClass AgeClass => DetermineAgeClass(this.Age);
        public bool IsPaired { get; internal set; }
        public bool IsLive { get; internal set; } = true;
        
        public Bird(Population population, int age, long id)
        {
            this.Population = population;
            this.Age = age;
            this.birdId = id;
        }

        public void HandleDeath()
        {
            var hqiThreshold = 1 - MaxSurvival * Math.PowCurrentHabitat.HabitatQualityIndex, HabitatLethality);
            if (Rng.NextDouble() < hqiThreshold)
                this.IsLive = false;

        }

        private AgeClass DetermineAgeClass(int age)
        {
            // TODO: Don't do magic numbers kiddo
            if (age < 1)
                return AgeClass.NewBorn;
            else if (age < 2)
                return AgeClass.Juvenile;
            else
                return AgeClass.Adult;
        }

        internal void IncreaseAge()
        {
            this.Age += 1;
        }

        public abstract void Migrate();
    }
}
