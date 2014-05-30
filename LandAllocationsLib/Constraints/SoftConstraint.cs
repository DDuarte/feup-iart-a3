using System;
using System.Linq;
using LandAllocationsLib.StateRepresentation;

namespace LandAllocationsLib.Constraints
{
    /// <summary>
    /// Criteria for land allocations (costs)
    /// </summary>
    public interface ISoftConstraint
    {
        double FeasibleCost(Landuse landuse, Lot lot, Problem problem);
    }

    public class SizeSoftConstraint : ISoftConstraint
    {
        public double BaseCost;
        public LanduseType[] LandusesTypes;
        public double Threshold;
        public bool CheckSmaller;

        private static readonly Func<double, double, bool> SmallerThan = (d, d1) => d <= d1;
        private static readonly Func<double, double, bool> LargerThan = (d, d1) => d > d1;

        public SizeSoftConstraint(double baseCost, LanduseType[] landusesTypes, bool checkSmaller, double threshold)
        {
            BaseCost = baseCost;
            LandusesTypes = landusesTypes;
            Threshold = threshold;
            CheckSmaller = checkSmaller;
        }

        public double FeasibleCost(Landuse landuse, Lot lot, Problem problem)
        {
            var cost = BaseCost * (CheckSmaller ? (Threshold/lot.Size) : (lot.Size/Threshold));
            if (cost >= BaseCost * 10)
                cost = BaseCost * 10;

            if (LandusesTypes != null && LandusesTypes.Any(landuseType => landuseType == landuse.Type))
            {
                var sizeCheck = CheckSmaller ? SmallerThan : LargerThan;
                return sizeCheck(lot.Size, Threshold) ? 0 : cost;
            }

            return 0;
        }

        public override string ToString()
        {
            var landuseTypes = LandusesTypes.Select(type => type.ToString()).Aggregate((s1, s2) => s1 + "," + s2);
            return string.Format("S({3}) [{0}] size {1} {2}", landuseTypes, CheckSmaller ? '<' : '>', Threshold, BaseCost);
        }
    }

    public class DistanceSoftConstraint : ISoftConstraint
    {
        public Place Place;
        public LanduseType[] LandusesTypes;
        public double Threshold;
        public double BaseCost;
        public bool CheckCloser;

        private static readonly Func<double, double, bool> CloserThan = (d, d1) => d <= d1;
        private static readonly Func<double, double, bool> FartherThan = (d, d1) => d > d1;

        public const double NearKilometers = 1;

        public DistanceSoftConstraint(double baseCost, LanduseType[] landusesTypes, Place place, bool checkCloser, double threshold = NearKilometers)
        {
            LandusesTypes = landusesTypes;
            Place = place;
            Threshold = threshold;
            BaseCost = baseCost;
            CheckCloser = checkCloser;
        }

        public double FeasibleCost(Landuse landuse, Lot lot, Problem problem)
        {
            var cost = BaseCost * (CheckCloser ? (Threshold / lot.DistanceLake(problem)) : (lot.DistanceLake(problem) / Threshold));
            if (cost >= BaseCost * 10)
                cost = BaseCost * 10;

            if (LandusesTypes != null && LandusesTypes.Any(landuseType => landuseType == landuse.Type))
            {
                var distCheck = CheckCloser ? CloserThan : FartherThan;

                switch (Place)
                {
                    case Place.Lake:
                        return distCheck(lot.DistanceLake(problem), Threshold) ? 0 : cost;
                    case Place.Highway:
                        return distCheck(lot.DistanceHighway(problem), Threshold) ? 0 : cost;
                }
            }

            return 0;
        }

        public override string ToString()
        {
            var landuseTypes = LandusesTypes.Select(type => type.ToString()).Aggregate((s1, s2) => s1 + "," + s2);
            return string.Format("S({4}) [{0}] distance({1}) {2} {3}",
                landuseTypes, Place, CheckCloser ? '<' : '>', Threshold, BaseCost);
        }
    }

    public class SteepSoftConstraint : ISoftConstraint
    {
        public LanduseType[] LandusesTypes;
        public SteepType[] SteepTypes;
        public double BaseCost;

        public SteepSoftConstraint(double baseCost, LanduseType[] landusesTypes, SteepType[] steepTypes)
        {
            LandusesTypes = landusesTypes;
            SteepTypes = steepTypes;
            BaseCost = baseCost;
        }

        public double FeasibleCost(Landuse landuse, Lot lot, Problem problem)
        {
            if (LandusesTypes != null && LandusesTypes.Any(landuseType => landuseType == landuse.Type))
                return SteepTypes.Any(steepType => steepType == lot.Steep) ? 0 : BaseCost;

            return 0;
        }

        public override string ToString()
        {
            var landuseTypes = LandusesTypes.Select(type => type.ToString()).Aggregate((s1, s2) => s1 + "," + s2);
            var steepTypes = SteepTypes.Select(type => type.ToString()).Aggregate((s1, s2) => s1 + "," + s2);
            return string.Format("S({2}) [{0}] steep [{1}]", landuseTypes, steepTypes, BaseCost);
        }
    }

    public class SoilSoftConstraint : ISoftConstraint
    {
        public double BaseCost;
        public LanduseType[] LandusesTypes;
        public bool PoorSoil;

        public SoilSoftConstraint(double baseCost, LanduseType[] landuseTypes, bool poorSoil)
        {
            BaseCost = baseCost;
            LandusesTypes = landuseTypes;
            PoorSoil = poorSoil;
        }

        public double FeasibleCost(Landuse landuse, Lot lot, Problem problem)
        {
            if (LandusesTypes != null && LandusesTypes.Any(landuseType => landuseType == landuse.Type))
                return lot.PoorSoil == PoorSoil ? 0 : BaseCost;

            return 0;
        }

        public override string ToString()
        {
            var landuseTypes = LandusesTypes.Select(type => type.ToString()).Aggregate((s1, s2) => s1 + "," + s2);
            return string.Format("S({2}) [{0}] soil {1}", landuseTypes, PoorSoil ? "poor" : "good", BaseCost);
        }
    }
}
