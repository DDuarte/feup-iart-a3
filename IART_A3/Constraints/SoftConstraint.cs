using System;
using System.Linq;
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

    public class SizeSoftConstraint : ISoftConstraint
    {
        public double BaseCost;
        public LanduseType[] LandusesTypes;
        public double Threshold;
        public bool CheckSmaller;
        private readonly Func<double, double, bool> _sizeCheck;

        private static readonly Func<double, double, bool> SmallerThan = (d, d1) => d <= d1;
        private static readonly Func<double, double, bool> LargerThan = (d, d1) => d > d1;

        public SizeSoftConstraint(double baseCost, LanduseType[] landusesTypes, bool checkSmaller, double threshold)
        {
            BaseCost = baseCost;
            LandusesTypes = landusesTypes;
            Threshold = threshold;
            CheckSmaller = checkSmaller;
            _sizeCheck = checkSmaller ? SmallerThan : LargerThan;
        }

        public double FeasibleCost(Landuse landuse, Lot lot)
        {
            var cost = BaseCost * (CheckSmaller ? (Threshold/lot.Size) : (lot.Size/Threshold));
            if (cost >= BaseCost * 10)
                cost = BaseCost * 10;
            if (LandusesTypes != null && LandusesTypes.Any(landuseType => landuseType == landuse.Type))
            {
                return _sizeCheck(lot.Size, Threshold) ? 0 : cost;
            }

            return 0;
        }
    }

    public class DistanceSoftConstraint : ISoftConstraint
    {
        public Place Place;
        public LanduseType[] LandusesTypes;
        public double Threshold;
        public double BaseCost;
        public bool CheckCloser;
        private readonly Func<double, double, bool> _distCheck;

        private static readonly Func<double, double, bool> CloserThan = (d, d1) => d <= d1;
        private static readonly Func<double, double, bool> FartherThan = (d, d1) => d > d1;

        public DistanceSoftConstraint(double baseCost, LanduseType[] landusesTypes, Place place, bool checkCloser, double threshold = Lot.NearKilometers)
        {
            LandusesTypes = landusesTypes;
            Place = place;
            Threshold = threshold;
            BaseCost = baseCost;
            CheckCloser = checkCloser;
            _distCheck = checkCloser ? CloserThan : FartherThan;
        }

        public double FeasibleCost(Landuse landuse, Lot lot)
        {
            var cost = BaseCost * (CheckCloser ? (Threshold/lot.DistanceLake) : (lot.DistanceLake/Threshold));
            if (cost >= BaseCost * 10)
                cost = BaseCost * 10;
            if (LandusesTypes != null && LandusesTypes.Any(landuseType => landuseType == landuse.Type))
            {
                switch (Place)
                {
                    case Place.Lake:
                        return _distCheck(lot.DistanceLake, Threshold) ? 0 : cost;
                    case Place.Highway:
                        return _distCheck(lot.DistanceHighway, Threshold) ? 0 : cost;
                }
            }

            return 0;
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

        public double FeasibleCost(Landuse landuse, Lot lot)
        {
            if (LandusesTypes != null && LandusesTypes.Any(landuseType => landuseType == landuse.Type))
                return SteepTypes.Any(steepType => steepType == lot.Steep) ? 0 : BaseCost;

            return 0;
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

        public double FeasibleCost(Landuse landuse, Lot lot)
        {
            if (LandusesTypes != null && LandusesTypes.Any(landuseType => landuseType == landuse.Type))
                return lot.PoorSoil == PoorSoil ? 0 : BaseCost;

            return 0;
        }
    }
}
