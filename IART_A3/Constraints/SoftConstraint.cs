using IART_A3.StateRepresentation;

namespace IART_A3.Constraints
{
    /// <summary>
    /// Criteria for land allocations (costs)
    /// </summary>
    public interface ISoftConstraint
    {
        double FeasibleCost(Landuse landuse, Lot lot);
    }

    // TODO: implementations of SoftConstraint
}
