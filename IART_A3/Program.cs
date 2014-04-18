using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using IART_A3.Constraints;
using IART_A3.SearchAlgorithms;
using IART_A3.StateRepresentation;

namespace IART_A3
{
    public static class Program
    {
        static void Main()
        {
            var problem = new Problem
            {
                Lots = new ReadOnlyDictionary<string, Lot>(new Dictionary<string, Lot>
                {
                    { "lot3",  new Lot {Price = 1.2, DistanceLake = 10,  DistanceHighway = 0.1, PoorSoil = false, Steep = SteepType.Flat}},
                    { "lot5",  new Lot {Price = 1.3, DistanceLake = 10,  DistanceHighway = 10,  PoorSoil = false, Steep = SteepType.Flat}},
                    { "lot7",  new Lot {Price = 0.9, DistanceLake = 0.1, DistanceHighway = 0.1, PoorSoil = false, Steep = SteepType.Flat}},
                    { "lot9",  new Lot {Price = 1.6, DistanceLake = 10,  DistanceHighway = 10,  PoorSoil = false, Steep = SteepType.Flat}},
                    { "lot10", new Lot {Price = 1.7, DistanceLake = 0.1, DistanceHighway = 10,  PoorSoil = true,  Steep = SteepType.ModeratelySteep}},
                    { "lot11", new Lot {Price = 1.0, DistanceLake = 10,  DistanceHighway = 10,  PoorSoil = false, Steep = SteepType.Steep}},
                    { "lot12", new Lot {Price = 1.4, DistanceLake = 0.1, DistanceHighway = 10,  PoorSoil = true,  Steep = SteepType.ModeratelySteep}},
                    { "lot17", new Lot {Price = 0.8, DistanceLake = 10,  DistanceHighway = 10,  PoorSoil = false, Steep = SteepType.VerySteep}}
                }),

                Landuses = new ReadOnlyDictionary<string, Landuse>(new Dictionary<string, Landuse>
                {
                    {"R", new Landuse {Type = LanduseType.Recreational}},
                    {"A", new Landuse {Type = LanduseType.Apartments}},
                    {"H", new Landuse {Type = LanduseType.HousingComplex}},
                    {"C", new Landuse {Type = LanduseType.Cemetery}},
                    {"D", new Landuse {Type = LanduseType.Dump}}
                }),

                HardConstraints = new ReadOnlyDictionary<string, IHardConstraint>(new Dictionary<string, IHardConstraint>
                {
                    {"C1", new DistanceHardConstraint(new [] {LanduseType.Recreational}, Place.Lake, DistanceHardConstraint.CloserThan)},
                    {"C2", new SteepHardConstraint(new[] {LanduseType.Apartments, LanduseType.HousingComplex, LanduseType.Cemetery, LanduseType.Dump}, new[] {SteepType.Flat, SteepType.ModeratelySteep})},
                    {"C3", new SoilHardConstraint(new[] {LanduseType.Apartments, LanduseType.HousingComplex}, false)},
                    {"C4", new DistanceHardConstraint(new[] {LanduseType.Apartments, LanduseType.HousingComplex, LanduseType.Recreational}, Place.Highway, DistanceHardConstraint.FartherThan)}
                }),

                SoftConstraints = new ReadOnlyDictionary<string, ISoftConstraint>(new Dictionary<string, ISoftConstraint>())
            };

            problem.Init();

            var algorithms = new List<SearchAlgorithm>
            {
                new AStarSearchAlgorithm(problem),
                new BruteforceSearchAlgorithm(problem)
            };

            algorithms.ForEach(algorithm => algorithm.TimedSearch(Console.Out));

            Console.WriteLine("Press ENTER to exit.");
            Console.ReadKey(true);
        }
    }
}
