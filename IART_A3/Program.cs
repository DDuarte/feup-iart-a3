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
                { "Porto",  new Lot {Price = 230, DistanceLake = 4,  DistanceHighway = 0.1, PoorSoil = false, Steep = SteepType.Steep}},
                { "Lisboa",  new Lot {Price = 250, DistanceLake = 10,  DistanceHighway = 0.1, PoorSoil = true, Steep = SteepType.Flat}},
                { "Guimaraes",  new Lot {Price = 150, DistanceLake = 2,  DistanceHighway = 0.5, PoorSoil = false, Steep = SteepType.ModeratelySteep}},
                { "Santarem",  new Lot {Price = 100, DistanceLake = 1,  DistanceHighway = 0.4, PoorSoil = false, Steep = SteepType.ModeratelySteep}},
                { "Braga",  new Lot {Price = 100, DistanceLake = 3,  DistanceHighway = 0.9, PoorSoil = true, Steep = SteepType.Steep}},
                { "Braganca",  new Lot {Price = 127, DistanceLake = 1,  DistanceHighway = 1.5, PoorSoil = false, Steep = SteepType.VerySteep}},
                { "Setubal",  new Lot {Price = 183, DistanceLake = 5,  DistanceHighway = 2, PoorSoil = true, Steep = SteepType.Flat}},
                { "Faro",  new Lot {Price = 82, DistanceLake = 4,  DistanceHighway = 3, PoorSoil = false, Steep = SteepType.Flat}},
                { "Viseu",  new Lot {Price = 200, DistanceLake = 0.2,  DistanceHighway = 1.7, PoorSoil = false, Steep = SteepType.ModeratelySteep}},
                { "Aveiro",  new Lot {Price = 140, DistanceLake = 0.1,  DistanceHighway = 2.3, PoorSoil = false, Steep = SteepType.Steep}}
            };

            var landuses = new Dictionary<string, Landuse>
            {
                {"Parques", new Landuse {Type = LanduseType.Recreational}},
                {"Predios", new Landuse {Type = LanduseType.Apartments}},
                {"Casas", new Landuse {Type = LanduseType.HousingComplex}},
                {"Cemiterio", new Landuse {Type = LanduseType.Cemetery}},
                {"Lixeira", new Landuse {Type = LanduseType.Dump}},
                {"Zona de guerra", new Landuse {Type = LanduseType.Cemetery}},
                {"Favela", new Landuse {Type = LanduseType.Dump}}
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

            problem.WriteJson("../../../example_problems/portugal_randomized.txt");
            var newP = Problem.ReadJson("../../../example_problems/portugal_randomized.txt");

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
