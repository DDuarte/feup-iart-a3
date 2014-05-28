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

    public class SizeHardConstraint : IHardConstraint
    {
        public LanduseType[] LandusesTypes;
        public double Threshold;
        public bool CheckSmaller;
        private readonly Func<double, double, bool> _sizeCheck;

        private static readonly Func<double, double, bool> SmallerThan = (d, d1) => d <= d1;
        private static readonly Func<double, double, bool> LargerThan = (d, d1) => d > d1;

        public SizeHardConstraint(LanduseType[] landusesTypes, bool checkSmaller, double threshold)
        {
            LandusesTypes = landusesTypes;
            Threshold = threshold;
            CheckSmaller = checkSmaller;
            _sizeCheck = checkSmaller ? SmallerThan : LargerThan;
        }

        public bool Feasible(Landuse landuse, Lot lot)
        {
            if (LandusesTypes != null && LandusesTypes.Any(landuseType => landuseType == landuse.Type))
            {
                return _sizeCheck(lot.Size, Threshold);
            }

            return true;
        }
    }

    public class DistanceHardConstraint : IHardConstraint
    {
        public Place Place;
        public LanduseType[] LandusesTypes;
        public double Threshold;
        public bool CheckCloser;
        private readonly Func<double, double, bool> _distCheck;

        private static readonly Func<double, double, bool> CloserThan = (d, d1) => d <= d1;
        private static readonly Func<double, double, bool> FartherThan = (d, d1) => d > d1;

        public DistanceHardConstraint(LanduseType[] landusesTypes, Place place, bool checkCloser, double threshold = Lot.NearKilometers)
        {
            LandusesTypes = landusesTypes;
            Place = place;
            Threshold = threshold;
            CheckCloser = checkCloser;
            _distCheck = checkCloser ? CloserThan : FartherThan;
        }

        public bool Feasible(Landuse landuse, Lot lot)
        {
            if (LandusesTypes != null && LandusesTypes.Any(landuseType => landuseType == landuse.Type))
            {
                switch (Place)
                {
                    case Place.Lake:
                        return _distCheck(lot.DistanceLake, Threshold);
                    case Place.Highway:
                        return _distCheck(lot.DistanceHighway, Threshold);
                }
            }

            return true;
        }
    }

    public class SteepHardConstraint : IHardConstraint
    {
        public LanduseType[] LandusesTypes;
        public SteepType[] SteepTypes;

        public SteepHardConstraint(LanduseType[] landusesTypes, SteepType[] steepTypes)
        {
            LandusesTypes = landusesTypes;
            SteepTypes = steepTypes;
        }

        public bool Feasible(Landuse landuse, Lot lot)
        {
            if (LandusesTypes != null && LandusesTypes.Any(landuseType => landuseType == landuse.Type))
                return SteepTypes.Any(steepType => steepType == lot.Steep);

            return true;
        }
    }

    public class SoilHardConstraint : IHardConstraint
    {
        public LanduseType[] LandusesTypes;
        public bool PoorSoil;

        public SoilHardConstraint(LanduseType[] landuseTypes, bool poorSoil)
        {
            LandusesTypes = landuseTypes;
            PoorSoil = poorSoil;
        }

        public bool Feasible(Landuse landuse, Lot lot)
        {
            if (LandusesTypes != null && LandusesTypes.Any(landuseType => landuseType == landuse.Type))
                return lot.PoorSoil == PoorSoil;

            return true;
        }
    }
}
