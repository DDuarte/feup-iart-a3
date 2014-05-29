using System;
using System.Collections.Generic;
using System.Linq;

namespace IART_A3.StateRepresentation
{
    /// <summary>
    /// Lots of land
    /// </summary>
    public class Lot
    {
        /// <summary>
        /// Coordinates X, Y representing the shape of this lot
        /// </summary>
        public List<Point> Terrain;

        /// <summary>
        /// Size of the lot in square Kilometers
        /// </summary>
        public double Size
        {
            get { return Terrain.Count; }
        }

        /// <summary>
        /// Price of lot in millions of Euro
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Soil not suitable for construction
        /// </summary>
        public bool PoorSoil { get; set; }

        /// <summary>
        /// Steep classification
        /// </summary>
        public SteepType Steep { get; set; }

        private double _distanceLake = double.NaN;
        private double _distanceHighway = double.NaN;

        /// <summary>
        /// Distance in kilometers to the nearest lake
        /// </summary>
        public double DistanceLake(Problem problem)
        {
            if (!double.IsNaN(_distanceLake))
                return _distanceLake;

            if (problem.Lakes.Count == 0)
                return double.PositiveInfinity;

            _distanceLake = Terrain.Aggregate(double.PositiveInfinity,
                (current, point) => problem.Lakes.Select(point.Distance)
                    .Concat(new[] {current}).Min());
            return _distanceLake;
        }

        /// <summary>
        /// Distance in kilometers to the nearest highway
        /// </summary>
        public double DistanceHighway(Problem problem)
        {
            if (!double.IsNaN(_distanceHighway))
                return _distanceHighway;

            if (problem.Highways.Count == 0)
                return double.PositiveInfinity;

            _distanceHighway = Terrain.Aggregate(double.PositiveInfinity,
                (current, point) => problem.Highways.Select(point.Distance)
                    .Concat(new[] {current}).Min());
            return _distanceHighway;
        }
    }
}
