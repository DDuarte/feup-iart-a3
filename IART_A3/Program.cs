using System;
using System.Collections.Generic;
using IART_A3.Constraints;
using IART_A3.SearchAlgorithms;
using IART_A3.StateRepresentation;

namespace IART_A3
{
    public static class Program
    {
        static void Main()
        {
            var lots = new Dictionary<string, Lot>
            {
                { "lot3",  new Lot {Price = 1.2, DistanceLake = 10,  DistanceHighway = 0.1, PoorSoil = false, Steep = SteepType.Flat}},
                { "lot5",  new Lot {Price = 1.3, DistanceLake = 10,  DistanceHighway = 10,  PoorSoil = false, Steep = SteepType.Flat}},
                { "lot7",  new Lot {Price = 0.9, DistanceLake = 0.1, DistanceHighway = 0.1, PoorSoil = false, Steep = SteepType.Flat}},
                { "lot9",  new Lot {Price = 1.6, DistanceLake = 10,  DistanceHighway = 10,  PoorSoil = false, Steep = SteepType.Flat}},
                { "lot10", new Lot {Price = 1.7, DistanceLake = 0.1, DistanceHighway = 10,  PoorSoil = true,  Steep = SteepType.ModeratelySteep}},
                { "lot11", new Lot {Price = 1.0, DistanceLake = 10,  DistanceHighway = 10,  PoorSoil = false, Steep = SteepType.Steep}},
                { "lot12", new Lot {Price = 1.4, DistanceLake = 0.1, DistanceHighway = 10,  PoorSoil = true,  Steep = SteepType.ModeratelySteep}},
                { "lot17", new Lot {Price = 0.8, DistanceLake = 10,  DistanceHighway = 10,  PoorSoil = false, Steep = SteepType.VerySteep}}
            };

            var landuses = new Dictionary<string, Landuse>
            {
                {"R", new Landuse {Type = LanduseType.Recreational}},
                {"A", new Landuse {Type = LanduseType.Apartments}},
                {"H", new Landuse {Type = LanduseType.HousingComplex}},
                {"C", new Landuse {Type = LanduseType.Cemetery}},
                {"D", new Landuse {Type = LanduseType.Dump}}
            };

            var hardConstraints = new Dictionary<string, IHardConstraint>
            {
                {"C1", new DistanceHardConstraint(new [] {LanduseType.Recreational}, Place.Lake, true)},
                {"C2", new SteepHardConstraint(new[] {LanduseType.Apartments, LanduseType.HousingComplex, LanduseType.Cemetery, LanduseType.Dump}, new[] {SteepType.Flat, SteepType.ModeratelySteep})},
                {"C3", new SoilHardConstraint(new[] {LanduseType.Apartments, LanduseType.HousingComplex}, false)},
                {"C4", new DistanceHardConstraint(new[] {LanduseType.Apartments, LanduseType.HousingComplex, LanduseType.Recreational}, Place.Highway, false)}
            };

            var softConstraints = new Dictionary<string, ISoftConstraint>();

            var problem = new Problem(lots, landuses, hardConstraints, softConstraints);

            problem.WriteJson("../../../example_problems/book_example.txt");
            var newP = Problem.ReadJson("../../../example_problems/book_example.txt");

            var algorithms = new List<SearchAlgorithm>
            {
                new BruteforceSearchAlgorithm(newP),
                new BreadthFirstSearchAlgorithm(newP),
                new DepthFirstSearchAlgorithm(newP),
                new UniformCostAlgorithm(newP),
                new GreedySearchAlgorithm(newP),
                new AStarSearchAlgorithm(newP)
            };

            algorithms.ForEach(algorithm => algorithm.TimedSearch(Console.Out));
            

            Console.WriteLine("Press ENTER to exit.");
            Console.ReadKey(true);
        }
    }
}
