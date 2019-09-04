using BirdMigrationSimulation.Models.Inhabitants;
using BirdMigrationSimulation.Models.Area;
using BirdMigrationSimulation.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BirdMigrationSimulation.Models.Data
{
    class DataManager
    {
        public Simulation Simulation { get; private set; }
        public Population Population => Simulation.Population;
        public Territory Territory => Simulation.Territory;
        public Random Rng => Simulation.Rng;

        public string RootDirectory { get; private set; } = "C:/Users/ajgood/Desktop/Birdies/";

        private ulong CheckpointCounter = 0;

        public DataManager(Simulation simulation)
        {
            this.Simulation = simulation;
        }

        public void SaveState(int timestep)
        {
            string territoryFilePath = $"{RootDirectory}territory_{timestep}.csv";
            string populationFilePath = $"{RootDirectory}population_{timestep}.csv";
            string rngFilePath = $"{RootDirectory}randomstate_{timestep}.bin";
            SaveTerritory(territoryFilePath);
            SavePopulation(populationFilePath);
            SaveRngState(rngFilePath);

            Console.WriteLine($"Checkpoint {CheckpointCounter} Done!");
            CheckpointCounter++;
        }

        public void LoadState(int timestep)
        {
            string territroyFilePath = $"{RootDirectory}territory_{timestep}.csv";
            string populationFilePath = $"{RootDirectory}population_{timestep}.csv";
            string rngFilePath = $"{RootDirectory}randomstate_{timestep}.bin";

            LoadTerritory(territroyFilePath);
            LoadPopulation(populationFilePath);
            LoadRngState(rngFilePath);
        }

        public void SaveTerritory(string filepath)
        {
            StringBuilder stringBuilder = new StringBuilder();

            //stringBuilder.Append($"x,y,HQI\n");
            foreach (var habitat in Territory.HabitatGrid.OrderBy(h => h.Coordinates))
                stringBuilder.Append($"{habitat.Id}, {habitat.Coordinates.x}, {habitat.Coordinates.y}, {habitat.HabitatQualityIndex}\n");

            using (StreamWriter writer = new StreamWriter(new FileStream(filepath,FileMode.Create,FileAccess.Write)))
            {
                //writer.WriteLine("sep=,");
                writer.WriteLine(stringBuilder.ToString());
            }
        }

        public void SavePopulation(string filepath)
        {
            StringBuilder stringBuilder = new StringBuilder();

            //stringBuilder.Append($"ID,x,y,sex,age\n");
            foreach (var bird in Population.Birds)
                stringBuilder.Append($"{bird.birdId}, {bird.CurrentHabitat.Id}, {bird.Sex}, {bird.Age}\n");

            FileStream fileStream = new FileStream(filepath, FileMode.Create, FileAccess.Write);
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                //writer.WriteLine("sep=,");
                writer.WriteLine(stringBuilder.ToString());
            }
        }

        public void SaveRngState(string filepath)
        {
            RandomState state = Rng.Save();

            FileStream stream = new FileStream(filepath, FileMode.Create, FileAccess.Write);
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                foreach (var item in state.State)
                    writer.Write(item);
            }
        }

        public void LoadTerritory(string filepath)
        {
            List<Habitat> habitatList = new List<Habitat>();
            using (var reader = new StreamReader(filepath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    // Since there is one empty line at the end of the file this avoids it
                    if (line == String.Empty)
                        continue;
                    var values = line.Split(',');

                    (int id, int x, int y, double hqi) = (Int32.Parse(values[0]),
                                                  Int32.Parse(values[1]),
                                                  Int32.Parse(values[2]),
                                                  Double.Parse(values[3])
                                      );
                    Habitat habitat = new Habitat(Territory, hqi, x, y, id);
                    habitatList.Add(habitat);
                }
            }

            Territory territory = new Territory(this.Simulation, habitatList);
            this.Simulation.Territory = territory;
        }

        public void LoadPopulation(string filepath)
        {
            List<Inhabitant> inhabitants = new List<Inhabitant>();

            using (var reader = new StreamReader(filepath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    // Since there is one empty line at the end of the file this avoids it
                    if (line == String.Empty)
                        continue;
                    var values = line.Split(',');

                    (int birdId, int habitatId, Sex sex, Age age) = (
                            Int32.Parse(values[0]),
                            Int32.Parse(values[1]),
                            (Sex)Enum.Parse(typeof(Sex), values[2],true),
                            (Age)Enum.Parse(typeof(Age), values[3],true)
                        );

                    Bird bird = new Bird(this.Population, sex, age, birdId);
                    Habitat habitat = Territory.HabitatGrid.Where(h => h.Id == habitatId).First();
                    Simulation.InsertInhabitant(bird, habitat);
                    inhabitants.Add(bird);
                }
            }

            Population population = new Population(this.Simulation, inhabitants);
            this.Simulation.Population = population;
        }

        public void LoadRngState(string filepath)
        {
            RandomState state;

            FileStream stream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            using (BinaryReader reader = new BinaryReader(stream))
            {
                int count = (int)reader.BaseStream.Length;
                byte[] bytes = reader.ReadBytes(count);
                state = new RandomState(bytes);
            }

            Random random = state.Restore();
            this.Simulation.Rng = random;
        }
    }
}
