using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BirdMigrationSimulation.Models.Area;
using BirdMigrationSimulation.Models.Inhabitants;
using BirdMigrationSimulation.Models.Inhabitants.Birds;
using BirdMigrationSimulation.Utilities;

namespace BirdMigrationSimulation.Models
{
    /// <summary>
    /// Represents the actual simulation that can be run.
    /// </summary>
    public class Simulation
    {
        public Random Rng { get; set; }
        public Territory Territory { get; set; }
        public Population Population { get; set; }

        private SimulationStateManager StateManager { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataPath">Path where data will be saved when saving state</param>
        /// <param name="gridDims">The dimensions to be used for the territory grid</param>
        /// <param name="numInitialBirds">The number of birds to initially populate the simulation with</param>
        /// <param name="rng_seed">Optional seed for Random Number Generator. A value of 0 (the default value) will result in a random seed.</param>
        public Simulation(string dataPath, (int x, int y) gridDims, int numInitialBirds, int rng_seed = 0)
        {
            if (rng_seed != 0)
                Rng = new Random(rng_seed);

            this.StateManager = new SimulationStateManager(dataPath, this);


            // This is temporary. These should come from a configuration file
            (int x, int y) = gridDims;
            Init(x, y, numInitialBirds);
        }

        public void LoadState(int timestep)
        {
            StateManager.LoadState(timestep);
        }

        /// <summary>
        /// This method is meant to place inhabitants in proper habitats for STATE RESTORATION ONLY
        /// TODO: Find a better solution
        /// </summary>
        /// <param name="inhabitant"></param>
        /// <param name="habitat"></param>
        public void InsertInhabitant(Inhabitant inhabitant, Habitat habitat)
        {
            if (inhabitant.CurrentHabitat != null)
                throw new Exception("Inhabitant is already in a habitat!!!");

            inhabitant.CurrentHabitat = habitat;
            habitat.InsertInhabitant(inhabitant);
        }

        public void Init(int width, int height, int numInitialBirds)
        {
            this.Territory = new Territory(this, width, height);
            this.Population = new Population(this, numInitialBirds);
        }

        public void Run(int timesteps, int checkpointStep)
        {
            for (int i = 0; i < timesteps; i++)
            {
                Console.WriteLine($"Running Iteration {i}");
                Console.WriteLine($"    Birds: {Population.Birds.Count}");
                Console.WriteLine($"    Singles: {Population.SingleBirds.Count}");
                Console.WriteLine($"    Pairs: {Population.Pairs.Count}");
                Console.WriteLine($"    New Borns:\t {Population.NewBorns.Count}");

                // Insert new migrants
                Population.PopulatePopulation(5);

                // Migration
                Population.MigrateBirds(Population.SingleBirds);

                // Birthdays!
                Population.IncreaseAge(Population.Birds);

                // Reproduction
                Population.Reproduce(Population.Pairs);

                // Death
                Population.HandleDeath(Population.Birds);
                Population.RemoveDeadBirds();
                Population.RemoveInactivePairs();

                if (i % checkpointStep == 0)
                    StateManager.SaveState(i);

                //Console.WriteLine("Press any key to continue...");
                //Console.ReadKey();
            }
        }

        public void Stop() => throw new NotImplementedException();

        private class SimulationStateManager
        {
            public string RootDirectory { get; private set; }
            public Simulation Simulation { get; private set; }
            private Territory Territory => Simulation.Territory;
            private Population Population => Simulation.Population;
            private Random Random => Simulation.Rng;

            public SimulationStateManager(string rootDir, Simulation simulation)
            {
                this.RootDirectory = rootDir;
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

                Console.WriteLine($"Checkpoint {timestep} Done!");
            }

            public void LoadState(int timestep)
            {
                string territoryFilePath = $"{RootDirectory}territory_{timestep}.csv";
                string populationFilePath = $"{RootDirectory}population_{timestep}.csv";
                string rngFilePath = $"{RootDirectory}randomstate_{timestep}.bin";

                LoadTerritory(territoryFilePath);
                LoadPopulation(populationFilePath);
                LoadRngState(rngFilePath);
            }

            private void SaveTerritory(string territoryFilePath)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var habitat in Territory.HabitatGrid)
                    stringBuilder.Append($"{habitat.Id}, {habitat.Coordinates.x}, {habitat.Coordinates.y}, {habitat.HabitatQualityIndex}\n");

                FileStream file = new FileStream(territoryFilePath, FileMode.Create, FileAccess.Write);
                using (StreamWriter writer = new StreamWriter(file))
                    writer.WriteLine(stringBuilder.ToString());
            }

            private void SavePopulation(string populationFilePath)
            {
                StringBuilder stringBuilder = new StringBuilder();

                foreach (var bird in Population.Birds)
                    stringBuilder.Append($"{bird.birdId}, {bird.CurrentHabitat.Id}, {bird.Sex}, {bird.AgeClass}\n");

                FileStream fileStream = new FileStream(populationFilePath, FileMode.Create, FileAccess.Write);
                using (StreamWriter writer = new StreamWriter(fileStream))
                    writer.WriteLine(stringBuilder.ToString());
            }

            private void SaveRngState(string rngFilePath)
            {
                RandomState state = Random.Save();

                FileStream stream = new FileStream(rngFilePath, FileMode.Create, FileAccess.Write);
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    foreach (var item in state.State)
                        writer.Write(item);
                }
            }

            private void LoadTerritory(string territroyFilePath)
            {
                List<Habitat> habitatList = new List<Habitat>();
                using (var reader = new StreamReader(territroyFilePath))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        // Since there is one empty line at the end of the file this avoids it
                        if (line == String.Empty) continue;
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

            private void LoadPopulation(string populationFilePath)
            {
                List<Inhabitant> inhabitants = new List<Inhabitant>();
                BirdFactory birdFactory = new BirdFactory(this.Population);

                using (var reader = new StreamReader(populationFilePath))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        // Since there is one empty line at the end of the file this avoids it
                        if (line == String.Empty)
                            continue;
                        var values = line.Split(',');

                        (int birdId, int habitatId, Sex sex, int age) = (
                                Int32.Parse(values[0]),
                                Int32.Parse(values[1]),
                                (Sex)Enum.Parse(typeof(Sex), values[2], true),
                                Int32.Parse(values[3])
                            );

                        //Bird bird = new Bird(this.Population, sex, age, birdId);
                        Bird bird = birdFactory.CreateBird(sex, age, birdId);
                        Habitat habitat = Territory.HabitatGrid.Where(h => h.Id == habitatId).First();
                        Simulation.InsertInhabitant(bird, habitat);
                        inhabitants.Add(bird);
                    }
                }

                Population population = new Population(this.Simulation, inhabitants);
                this.Simulation.Population = population;
            }

            private void LoadRngState(string rngFilePath)
            {
                RandomState state;

                FileStream stream = new FileStream(rngFilePath, FileMode.Open, FileAccess.Read);
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
}
