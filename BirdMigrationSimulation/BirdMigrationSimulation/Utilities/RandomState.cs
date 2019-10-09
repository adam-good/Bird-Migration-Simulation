using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BirdMigrationSimulation.Utilities
{
    // https://stackoverflow.com/questions/19512210/how-to-save-the-state-of-a-random-generator-in-c

    /// <summary>
    /// A wrapper around a byte array that is used to represent the state of a Random object
    /// </summary>
    public class RandomState
    {
        /// <summary>
        /// Literally a byte array representing the state of the Random object
        /// </summary>
        public readonly byte[] State;

        public RandomState(byte[] state)
        {
            State = state;
        }
    }

    public static class RandomExtensions
    {
        /// <summary>
        /// Converts a Random object to a RandomState object that represents its current state
        /// </summary>
        /// <param name="rng">The Random object who's state shall be saved</param>
        /// <returns></returns>
        public static RandomState Save(this Random rng)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (var memory = new MemoryStream())
            {
                formatter.Serialize(memory, rng);
                RandomState state = new RandomState(memory.ToArray());
                return state;
            }
        }

        /// <summary>
        /// Creates a Random object composed of the RandomState object
        /// </summary>
        /// <param name="state">The RandomState object which will be used to reconstruct the Random object</param>
        /// <returns></returns>
        public static Random Restore(this RandomState state)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (var memory = new MemoryStream(state.State))
            {
                Random rng = (Random)formatter.Deserialize(memory);
                return rng;
            }
        }
    }

}
