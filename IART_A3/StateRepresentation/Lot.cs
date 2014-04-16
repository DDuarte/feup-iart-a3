namespace IART_A3.StateRepresentation
{
    /// <summary>
    /// Slope classification
    /// </summary>
    public enum SteepType
    {
        /// <summary>
        /// No slope
        /// </summary>
        Flat,
        /// <summary>
        /// Almost flat
        /// </summary>
        ModeratelySteep,
        /// <summary>
        /// Steep slope
        /// </summary>
        Steep,
        /// <summary>
        /// Very steep slope
        /// </summary>
        VerySteep
    }

    /// <summary>
    /// Lots of land
    /// </summary>
    public class Lot
    {
        /// <summary>
        /// Maximum distance to something to consider it "near".
        /// </summary>
        public const double NEAR_KILOMETERS = 0.5;

        /// <summary>
        /// Price of lot in millions of Euro
        /// </summary>
        public double Cost { get; set; }

        /// <summary>
        /// Soil not suitable for construction
        /// </summary>
        public bool PoorSoil { get; set; }

        /// <summary>
        /// Steep classification
        /// </summary>
        public SteepType Steep { get; set; }

        /// <summary>
        /// Distance in kilometers to the nearest lake
        /// </summary>
        public double DistanceLake { get; set; }

        /// <summary>
        /// Distance in kilometers to the nearest highway
        /// </summary>
        public double DistanceHighway { get; set; }
    }
}
