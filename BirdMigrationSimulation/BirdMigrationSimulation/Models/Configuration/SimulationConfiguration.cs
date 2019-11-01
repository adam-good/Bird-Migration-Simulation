using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdMigrationSimulation.Models.Configuration
{
    class SimulationConfiguration
    {
        private Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

        const string RNG_SEED_KEY = "random_seed";
        const string TIMESTEPS_KEY = "timesteps";
        const string SHOW_GUI_KEY = "show_gui";
        const string GUI_UPDATE_TIME_KEY = "gui_update_time";
        const string TERRITORY_WIDTH_KEY = "territory_width";
        const string TERRITORY_HEIGHT_KEY = "territory_height";
        const string HQI_DIST_KEY = "hqi_distribution";
        const string INIT_POP_SIZE_KEY = "init_population_size";
        const string POP_INC_KEY = "annual_population_inc";


        // Simulation Settings
        public int RandomSeed => int.Parse(keyValuePairs[RNG_SEED_KEY]);
        public int Timesteps => int.Parse(keyValuePairs[TIMESTEPS_KEY]);
        public bool ShowGUI => bool.Parse(keyValuePairs[SHOW_GUI_KEY]);
        public double GUIUpdateTime => double.Parse(keyValuePairs[GUI_UPDATE_TIME_KEY]);

        // Territory Settings
        public (int width, int height) TerritorySize => (
                int.Parse(keyValuePairs[TERRITORY_WIDTH_KEY]),
                int.Parse(keyValuePairs[TERRITORY_HEIGHT_KEY])
            );
        public string HQIDistribution => keyValuePairs[HQI_DIST_KEY];

        // Population Settings
        public int InitialPopulationSize => int.Parse(keyValuePairs[INIT_POP_SIZE_KEY]);
        public int PopulationIncreaseSize => int.Parse(keyValuePairs[POP_INC_KEY]);

        // Bird Settings
        public BirdConfiguration MaleBirdConfig { get; private set; }
        public BirdConfiguration FemaleBirdConfig { get; private set; }

        public void LoadConfiguration(string filepath)
        {
            string line;
            using (StreamReader stream = new StreamReader(filepath))
            {
                while ((line = stream.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                    // If empty or comment, leave out. Else read.
                    if ( !(string.IsNullOrEmpty(line) || line.Contains("#")) )
                    {
                        var values = line.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        keyValuePairs[values[0]] = values[1].Trim();
                    }
                }
            }
        }

    }
}
