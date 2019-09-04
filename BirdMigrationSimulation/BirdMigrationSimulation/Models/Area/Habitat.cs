using BirdMigrationSimulation.Models.Inhabitants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdMigrationSimulation.Models.Area
{
    /// <summary>
    /// This class represents a section of land that can support a single bird nest
    /// </summary>
    class Habitat
    {
        /// <summary>
        /// The Simulation this habitat belongs to.
        /// </summary>
        public Simulation Simulation => Territory.Simulation;

        /// <summary>
        /// The Territory which this habitat exists in
        /// </summary>
        private Territory Territory { get; set; }

        /// <summary>
        /// A unique id for this habitat
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// A value in the range [0,1] representing how well suited the habitat is to supporting an inhabitant.
        /// </summary>
        public double HabitatQualityIndex { get; private set; }

        /// <summary>
        /// Coordinates to denote the location of the habitat
        /// </summary>
        public (int x, int y) Coordinates { get; private set; }

        public List<Inhabitant> Inhabitants { get; private set; } = new List<Inhabitant>();

        public Inhabitant MainInhabitant { get; set; }

        /// <summary>
        /// Value representing whether the habitat is inhabited (false) or it is uninhabited (true)
        /// </summary>
        public bool IsEmpty => Inhabitants.Count == 0 ? true : false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="territory">The Territory object which this habitat will belong to</param>
        /// <param name="hqi">The Habitat Quality Index for this habitat</param>
        /// <param name="x">This Habitat's X coordinate</param>
        /// <param name="y">This Habitat's Y coordinate</param>
        public Habitat(Territory territory, double hqi, int x, int y, long id)
        {
            this.Territory = territory;
            this.HabitatQualityIndex = hqi;
            this.Coordinates = (x, y);
            this.Id = id;
        }

        public List<Habitat> GetNeighbors()
        {
            int thisX = Coordinates.x;
            int thisY = Coordinates.y;
            List<Habitat> habitats = Territory.HabitatGrid.Where(h => thisX-1 <= h.Coordinates.x && thisX+1 >= h.Coordinates.x)
                                                          .Where(h => thisY-1 <= h.Coordinates.y && thisY+1 >= h.Coordinates.y)
                                                          .Where(h => h != this)
                                                          .ToList();

            return habitats;
        }

        public void RemoveInhabitant(Inhabitant inhabitant)
        {
            // If the inhabitant wasn't here to start with, throw an error
            if (!Inhabitants.Contains(inhabitant))
                throw new Exception("Inhabitant being removed does not exist in this habitat");

            this.Inhabitants.Remove(inhabitant);
        }

        public void InsertInhabitant(Inhabitant inhabitant)
        {
            // If the inhabitant is already here, throw an error
            if (Inhabitants.Contains(inhabitant))
                throw new Exception("Inhabitant being inserted already exists in this habitat");

            this.Inhabitants.Add(inhabitant);
        }
    }
}
