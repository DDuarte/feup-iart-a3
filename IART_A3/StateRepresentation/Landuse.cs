using System.Collections.Generic;

namespace IART_A3.StateRepresentation
{
    public enum LanduseType
    {
        Recreational,
        Apartments,
        HousingComplex,
        Dump,
        Cemetery
    }

    /// <summary>
    /// Land allocation
    /// </summary>
    public class Landuse
    {
        /// <summary>
        /// Coordinates X, Y representing the shape of this landuse
        /// </summary>
        public List<Point> Terrain;

        public LanduseType Type { get; set; }
    }
}
