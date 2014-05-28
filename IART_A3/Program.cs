using System;
using System.Collections.Generic;
using System.Linq;
//using IART_A3.Constraints;
using IART_A3.SearchAlgorithms;
using IART_A3.StateRepresentation;

namespace IART_A3
{
    public static class Program
    {
        static void Main()
        {
            /*
            var lots = new Dictionary<string, Lot>
            {
                { "Porto",  new Lot {Price = 230, Size = 400, DistanceLake = 4,  DistanceHighway = 0.1, PoorSoil = false, Steep = SteepType.Steep}},
                { "Lisboa",  new Lot {Price = 250, Size = 500, DistanceLake = 10,  DistanceHighway = 0.1, PoorSoil = true, Steep = SteepType.Flat}},
                { "Guimaraes",  new Lot {Price = 150, Size = 200, DistanceLake = 2,  DistanceHighway = 0.5, PoorSoil = false, Steep = SteepType.ModeratelySteep}},
                { "Santarem",  new Lot {Price = 100, Size = 100, DistanceLake = 1,  DistanceHighway = 0.4, PoorSoil = false, Steep = SteepType.ModeratelySteep}},
                { "Braga",  new Lot {Price = 100, Size = 120, DistanceLake = 3,  DistanceHighway = 0.9, PoorSoil = true, Steep = SteepType.Steep}},
                { "Braganca",  new Lot {Price = 127, Size = 80, DistanceLake = 1,  DistanceHighway = 1.5, PoorSoil = false, Steep = SteepType.VerySteep}},
                { "Setubal",  new Lot {Price = 183, Size = 230, DistanceLake = 5,  DistanceHighway = 2, PoorSoil = true, Steep = SteepType.Flat}},
                { "Faro",  new Lot {Price = 82, Size = 220, DistanceLake = 4,  DistanceHighway = 3, PoorSoil = false, Steep = SteepType.Flat}},
                { "Viseu",  new Lot {Price = 200, Size = 140, DistanceLake = 0.2,  DistanceHighway = 1.7, PoorSoil = false, Steep = SteepType.ModeratelySteep}},
                { "Aveiro",  new Lot {Price = 140, Size = 90, DistanceLake = 0.1,  DistanceHighway = 2.3, PoorSoil = false, Steep = SteepType.Steep}},
                { "Alentejo",  new Lot {Price = 120, Size = 140, DistanceLake = 0.1,  DistanceHighway = 2.3, PoorSoil = false, Steep = SteepType.Flat}}
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
                {"C4", new DistanceHardConstraint(new[] {LanduseType.Apartments, LanduseType.HousingComplex, LanduseType.Recreational}, Place.Highway, false)},
                {"C5", new SizeHardConstraint(new [] {LanduseType.Recreational}, false, 3)}
            };

            var softConstraints = new Dictionary<string, ISoftConstraint>
            {
                {"S1", new SizeSoftConstraint(200, new [] {LanduseType.Dump}, false, 4)}
            };

            var problem = new Problem(lots, landuses, hardConstraints, softConstraints);

            problem.WriteJson("../../../example_problems/portugal_larger.txt");*/

            var problemPaths = new List<String>
            {
                "../../../example_problems/book_example.txt",
                "../../../example_problems/portugal_small.txt",
                "../../../example_problems/portugal_medium.txt",
                "../../../example_problems/portugal_large.txt",
                "../../../example_problems/portugal_larger.txt"
            };
            ComputeStatistics("../../../example_problems/statistics.txt", problemPaths);

            Console.WriteLine("Press ENTER to exit.");
            Console.ReadKey(true);
        }

        public static void ComputeStatistics(string filepath, List<String> problemPaths)
        {
            var problemList = problemPaths.Select(Problem.ReadJson).ToList();

            var stream = new System.IO.StreamWriter(filepath, false);
            for (var i = 0; i < problemPaths.Count; i++)
            {
                var probName = problemPaths[i];
                stream.WriteLine("-----------------------" + probName + "-----------------------\n");
                var prob = problemList[i];
                var algorithms = new List<SearchAlgorithm>
                {
                    new BruteforceSearchAlgorithm(prob),
                    new BreadthFirstSearchAlgorithm(prob),
                    new DepthFirstSearchAlgorithm(prob),
                    new UniformCostAlgorithm(prob),
                    new GreedySearchAlgorithm(prob),
                    new AStarSearchAlgorithm(prob)
                };

                algorithms.ForEach(algorithm => algorithm.TimedSearch(stream));
                stream.Flush();
            }
            stream.Close();
        }
    }
}
