using System;
using System.Linq;

namespace IART_A3.StateRepresentation
{
    /// <summary>
    /// "Something" on the map that isn't a lot or landuse
    /// </summary>
    public enum Place
    {
        Lake,
        Highway
    }

    /// <summary>
    /// Criteria for land allocations
    /// TODO: Hard and soft contraints
    /// </summary>
    public abstract class Constraint
    {
        public abstract bool Feasible(Landuse landuse, Lot lot);
    }

    public class DistanceConstraint : Constraint
    {
        private readonly Place _place;
        private readonly LanduseType[] _landusesTypes;
        private readonly double _threshold;
        private readonly Func<double, double, bool> _distCheck;

        public static readonly Func<double, double, bool> CloserThan = (d, d1) => d <= d1;
        public static readonly Func<double, double, bool> FartherThan = (d, d1) => d > d1; 

        public DistanceConstraint(LanduseType[] landusesTypes, Place place, Func<double, double, bool> distCheck, double threshold = Lot.NEAR_KILOMETERS)
        {
            _landusesTypes = landusesTypes;
            _place = place;
            _threshold = threshold;
            _distCheck = distCheck;
        }

        public override bool Feasible(Landuse landuse, Lot lot)
        {
            if (_landusesTypes.Any(landuseType => landuseType == landuse.Type))
            {
                switch (_place)
                {
                    case Place.Lake:
                        return _distCheck(lot.DistanceLake, _threshold);
                    case Place.Highway:
                        return _distCheck(lot.DistanceHighway, _threshold);
                }
            }

            return true;
        }
    }

    public class SteepConstraint : Constraint
    {
        private readonly LanduseType[] _landusesTypes;
        private readonly SteepType[] _steepTypes;

        public SteepConstraint(LanduseType[] landusesTypes, SteepType[] steepTypes)
        {
            _landusesTypes = landusesTypes;
            _steepTypes = steepTypes;
        }

        public override bool Feasible(Landuse landuse, Lot lot)
        {
            if (_landusesTypes.Any(landuseType => landuseType == landuse.Type))
                return _steepTypes.Any(steepType => steepType == lot.Steep);

            return true;
        }
    }

    public class SoilConstraint : Constraint
    {
        private readonly LanduseType[] _landusesTypes;
        private readonly bool _poorSoil;

        public SoilConstraint(LanduseType[] landuseTypes, bool poorSoil)
        {
            _landusesTypes = landuseTypes;
            _poorSoil = poorSoil;
        }

        public override bool Feasible(Landuse landuse, Lot lot)
        {
            if (_landusesTypes.Any(landuseType => landuseType == landuse.Type))
                return lot.PoorSoil == _poorSoil;

            return true;
        }
    }
}
