using System;
using System.Linq;
using IART_A3.StateRepresentation;

namespace IART_A3.Constraints
{
    /// <summary>
    /// Criteria for land allocations (yes/no)
    /// </summary>
    public interface IHardConstraint
    {
        bool Feasible(Landuse landuse, Lot lot);
    }

    public class DistanceHardConstraint : IHardConstraint
    {
        private readonly Place _place;
        private readonly LanduseType[] _landusesTypes;
        private readonly double _threshold;
        private readonly Func<double, double, bool> _distCheck;

        public static readonly Func<double, double, bool> CloserThan = (d, d1) => d <= d1;
        public static readonly Func<double, double, bool> FartherThan = (d, d1) => d > d1;

        public DistanceHardConstraint(LanduseType[] landusesTypes, Place place, Func<double, double, bool> distCheck, double threshold = Lot.NearKilometers)
        {
            _landusesTypes = landusesTypes;
            _place = place;
            _threshold = threshold;
            _distCheck = distCheck;
        }

        public bool Feasible(Landuse landuse, Lot lot)
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

    public class SteepHardConstraint : IHardConstraint
    {
        private readonly LanduseType[] _landusesTypes;
        private readonly SteepType[] _steepTypes;

        public SteepHardConstraint(LanduseType[] landusesTypes, SteepType[] steepTypes)
        {
            _landusesTypes = landusesTypes;
            _steepTypes = steepTypes;
        }

        public bool Feasible(Landuse landuse, Lot lot)
        {
            if (_landusesTypes.Any(landuseType => landuseType == landuse.Type))
                return _steepTypes.Any(steepType => steepType == lot.Steep);

            return true;
        }
    }

    public class SoilHardConstraint : IHardConstraint
    {
        private readonly LanduseType[] _landusesTypes;
        private readonly bool _poorSoil;

        public SoilHardConstraint(LanduseType[] landuseTypes, bool poorSoil)
        {
            _landusesTypes = landuseTypes;
            _poorSoil = poorSoil;
        }

        public bool Feasible(Landuse landuse, Lot lot)
        {
            if (_landusesTypes.Any(landuseType => landuseType == landuse.Type))
                return lot.PoorSoil == _poorSoil;

            return true;
        }
    }
}
