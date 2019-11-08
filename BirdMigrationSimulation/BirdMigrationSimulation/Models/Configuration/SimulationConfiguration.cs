using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdMigrationSimulation.Models.Configuration
{
    public class SimulationConfiguration
    {
        private Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

        const string RNG_SEED_KEY = "random_seed";
        const string TIMESTEPS_KEY = "timesteps";
        const string CHECKPOINT_TIMESTEP_KEY = "checkpoint_timestep";
        const string SHOW_GUI_KEY = "show_gui";
        const string GUI_UPDATE_TIME_KEY = "gui_update_time";
        const string TERRITORY_WIDTH_KEY = "territory_width";
        const string TERRITORY_HEIGHT_KEY = "territory_height";
        const string HQI_DIST_KEY = "hqi_distribution";
        const string INIT_POP_SIZE_KEY = "init_population_size";
        const string POP_INC_KEY = "annual_population_inc";

        const string ADULT_MALE_MIGRATION_KEY = "adult_male_migration_moves";
        const string ADULT_MALE_SELECTIVITY_KEY = "adult_male_habitat_selectivity";
        const string ADULT_MALE_LETHALITY_KEY = "adult_male_lethality";
        const string JUVENILE_MALE_MIGRATION_KEY = "juvenile_male_migration_moves";
        const string JUVENILE_MALE_SELECTIVITY_KEY = "juvenile_male_habitat_selectivity";
        const string JUVENILE_MALE_LETHALITY_KEY = "juvenile_male_lethality";

        const string ADULT_FEMALE_MIGRATION_KEY = "adult_female_migration_moves";
        const string ADULT_FEMALE_SELECTIVITY_KEY = "adult_female_habitat_selectivity";
        const string ADULT_FEMALE_LETHALITY_KEY = "adult_female_lethality";
        const string JUVENILE_FEMALE_MIGRATION_KEY = "juvenile_female_migration_moves";
        const string JUVENILE_FEMALE_SELECTIVITY_KEY = "juvenile_female_migration_selectivity";
        const string JUVENILE_FEMALE_LETHALITY_KEY = "juvenile_female_lethality";

        const string AVG_OFFSPRING_KEY = "avg_offspring";
        const string OFFPSRING_DISTRIBUTION_KEY = "offspring_distribution";

        // Simulation Settings
        public int RandomSeed => int.Parse(keyValuePairs[RNG_SEED_KEY]);
        public int Timesteps => int.Parse(keyValuePairs[TIMESTEPS_KEY]);
        public int CheckpointTimestep => int.Parse(keyValuePairs[CHECKPOINT_TIMESTEP_KEY]);
        public bool ShowGUI => bool.Parse(keyValuePairs[SHOW_GUI_KEY]);
        public double GUIUpdateTime => double.Parse(keyValuePairs[GUI_UPDATE_TIME_KEY]);

        // Territory Settings
        public (int Width, int Height) TerritorySize => (
                int.Parse(keyValuePairs[TERRITORY_WIDTH_KEY]),
                int.Parse(keyValuePairs[TERRITORY_HEIGHT_KEY])
            );
        public string HQIDistribution => keyValuePairs[HQI_DIST_KEY];

        // Population Settings
        public int InitialPopulationSize => int.Parse(keyValuePairs[INIT_POP_SIZE_KEY]);
        public int PopulationIncreaseSize => int.Parse(keyValuePairs[POP_INC_KEY]);

        // Bird Settings
        private BirdConfiguration maleBirdConfig;
        private BirdConfiguration femaleBirdConfig;
        public BirdConfiguration MaleBirdConfig => maleBirdConfig;
        public BirdConfiguration FemaleBirdConfig => femaleBirdConfig;

        public double AverageOffpsring => double.Parse(keyValuePairs[AVG_OFFSPRING_KEY]);

        public void LoadConfiguration(string filepath)
        {
            string line;
            using (StreamReader stream = new StreamReader(filepath))
            {
                while ((line = stream.ReadLine()) != null)
                {
                    //Console.WriteLine(line);
                    // If empty or comment, leave out. Else read.
                    if ( !(string.IsNullOrEmpty(line) || line.Contains("#")) )
                    {
                        var values = line.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        keyValuePairs[values[0]] = values[1].Trim();
                    }
                }
            }

            maleBirdConfig = new BirdConfiguration(
                    int.Parse(this.keyValuePairs[ADULT_MALE_MIGRATION_KEY]),
                    double.Parse(this.keyValuePairs[ADULT_MALE_SELECTIVITY_KEY]),
                    double.Parse(this.keyValuePairs[ADULT_MALE_LETHALITY_KEY]),
                    int.Parse(this.keyValuePairs[JUVENILE_MALE_MIGRATION_KEY]),
                    double.Parse(this.keyValuePairs[JUVENILE_MALE_SELECTIVITY_KEY]),
                    double.Parse(this.keyValuePairs[JUVENILE_MALE_LETHALITY_KEY])
                );
            femaleBirdConfig = new BirdConfiguration(
                    int.Parse(this.keyValuePairs[ADULT_FEMALE_MIGRATION_KEY]),
                    double.Parse(this.keyValuePairs[ADULT_FEMALE_SELECTIVITY_KEY]),
                    double.Parse(this.keyValuePairs[ADULT_FEMALE_LETHALITY_KEY]),
                    int.Parse(this.keyValuePairs[JUVENILE_FEMALE_MIGRATION_KEY]),
                    double.Parse(this.keyValuePairs[JUVENILE_FEMALE_SELECTIVITY_KEY]),
                    double.Parse(this.keyValuePairs[JUVENILE_FEMALE_LETHALITY_KEY])
                );
        }

    }
}
