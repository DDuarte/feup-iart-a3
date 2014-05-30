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
        public LanduseType Type { get; set; }
    }
}
