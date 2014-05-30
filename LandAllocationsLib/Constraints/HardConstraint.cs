using System;
using System.Linq;
using LandAllocationsLib.StateRepresentation;

namespace LandAllocationsLib.Constraints
{
    /// <summary>
    /// Criteria for land allocations (yes/no)
    /// </summary>
    public interface IHardConstraint
    {
        bool Feasible(Landuse landuse, Lot lot, Problem problem);
    }

    public class SizeHardConstraint : IHardConstraint
    {
        public LanduseType[] LandusesTypes;
        public double Threshold;
        public bool CheckSmaller;

        private static readonly Func<double, double, bool> SmallerThan = (d, d1) => d <= d1;
        private static readonly Func<double, double, bool> LargerThan = (d, d1) => d > d1;

        public SizeHardConstraint(LanduseType[] landusesTypes, bool checkSmaller, double threshold)
        {
            LandusesTypes = landusesTypes;
            Threshold = threshold;
            CheckSmaller = checkSmaller;
        }

        public bool Feasible(Landuse landuse, Lot lot, Problem problem)
        {
            var sizeCheck = CheckSmaller ? SmallerThan : LargerThan;

            if (LandusesTypes != null && LandusesTypes.Any(landuseType => landuseType == landuse.Type))
                return sizeCheck(lot.Size, Threshold);

            return true;
        }
    }

    public class DistanceHardConstraint : IHardConstraint
    {
        public Place Place;
        public LanduseType[] LandusesTypes;
        public double Threshold;
        public bool CheckCloser;

        private static readonly Func<double, double, bool> CloserThan = (d, d1) => d <= d1;
        private static readonly Func<double, double, bool> FartherThan = (d, d1) => d > d1;

        public const double NearKilometers = 1;

        public DistanceHardConstraint(LanduseType[] landusesTypes, Place place, bool checkCloser, double threshold = NearKilometers)
        {
            LandusesTypes = landusesTypes;
            Place = place;
            Threshold = threshold;
            CheckCloser = checkCloser;
        }

        public bool Feasible(Landuse landuse, Lot lot, Problem problem)
        {
            if (LandusesTypes != null && LandusesTypes.Any(landuseType => landuseType == landuse.Type))
            {
                var distCheck = CheckCloser ? CloserThan : FartherThan;

                switch (Place)
                {
                    case Place.Lake:
                        return distCheck(lot.DistanceLake(problem), Threshold);
                    case Place.Highway:
                        return distCheck(lot.DistanceHighway(problem), Threshold);
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

        public bool Feasible(Landuse landuse, Lot lot, Problem problem)
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

        public bool Feasible(Landuse landuse, Lot lot, Problem problem)
        {
            if (LandusesTypes != null && LandusesTypes.Any(landuseType => landuseType == landuse.Type))
                return lot.PoorSoil == PoorSoil;

            return true;
        }
    }
}
