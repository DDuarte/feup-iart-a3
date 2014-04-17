using System;
using System.Collections.Generic;
using System.Linq;
using IART_A3.Search;
using IART_A3.StateRepresentation;

namespace IART_A3
{
    public static class Program
    {
        static void Main()
        {
            var lots = new Dictionary<string, Lot>
            {
                { "lot3",  new Lot {Cost = 1.2, DistanceLake = 10,  DistanceHighway = 0.1, PoorSoil = false, Steep = SteepType.Flat}},
                { "lot5",  new Lot {Cost = 1.3, DistanceLake = 10,  DistanceHighway = 10,  PoorSoil = false, Steep = SteepType.Flat}},
                { "lot7",  new Lot {Cost = 0.9, DistanceLake = 0.1, DistanceHighway = 0.1, PoorSoil = false, Steep = SteepType.Flat}},
                { "lot9",  new Lot {Cost = 1.6, DistanceLake = 10,  DistanceHighway = 10,  PoorSoil = false, Steep = SteepType.Flat}},
                { "lot10", new Lot {Cost = 1.7, DistanceLake = 0.1, DistanceHighway = 10,  PoorSoil = true,  Steep = SteepType.ModeratelySteep}},
                { "lot11", new Lot {Cost = 1.0, DistanceLake = 10,  DistanceHighway = 10,  PoorSoil = false, Steep = SteepType.Steep}},
                { "lot12", new Lot {Cost = 1.4, DistanceLake = 0.1, DistanceHighway = 10,  PoorSoil = true,  Steep = SteepType.ModeratelySteep}},
                { "lot17", new Lot {Cost = 0.8, DistanceLake = 10,  DistanceHighway = 10,  PoorSoil = false, Steep = SteepType.VerySteep}}
            };

            var landuses = new Dictionary<string, Landuse>
            {
                {"R", new Landuse {Type = LanduseType.Recreational}},
                {"A", new Landuse {Type = LanduseType.Apartments}},
                {"H", new Landuse {Type = LanduseType.HousingComplex}},
                {"C", new Landuse {Type = LanduseType.Cemetery}},
                {"D", new Landuse {Type = LanduseType.Dump}}
            };

            var constraints = new Dictionary<string, Constraint>
            {
                {"C1", new DistanceConstraint(new [] {LanduseType.Recreational}, Place.Lake, DistanceConstraint.CloserThan)},
                {"C2", new SteepConstraint(new[] {LanduseType.Apartments, LanduseType.HousingComplex, LanduseType.Cemetery, LanduseType.Dump}, new[] {SteepType.Flat, SteepType.ModeratelySteep})},
                {"C3", new SoilConstraint(new[] {LanduseType.Apartments, LanduseType.HousingComplex}, false)},
                {"C4", new DistanceConstraint(new[] {LanduseType.Apartments, LanduseType.HousingComplex, LanduseType.Recreational}, Place.Highway, DistanceConstraint.FartherThan)}
            };


//             ////// Random test data
//             const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
//             var landuseValues = Enum.GetValues(typeof(LanduseType));
//             var steepTypeValues = Enum.GetValues(typeof(SteepType));
// 
//             for (var i = 0; i < 10000; i++)
//             {
//                 var random = new Random();
//                 var lotName = new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
//                 if (lots.ContainsKey(lotName)) continue;
//                 lots.Add(lotName, new Lot { Cost = (random.Next(15) + 5) / 10.0, DistanceLake = random.Next(100) / 10.0, DistanceHighway = random.Next(100) / 10.0, PoorSoil = random.Next(2) != 0, Steep = (SteepType)steepTypeValues.GetValue(random.Next(steepTypeValues.Length)) });
//                 //landuses.Add(lotName, new Landuse { Type = (LanduseType)landuseValues.GetValue(random.Next(landuseValues.Length)) });
//             }
//             for (var i = 0; i < 100; i++)
//             {
//                 var random = new Random();
//                 var lotName = new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
//                 if (landuses.ContainsKey(lotName)) continue;
//                 //lots.Add(lotName, new Lot { Cost = (random.Next(15) + 5) / 10.0, DistanceLake = random.Next(100) / 10.0, DistanceHighway = random.Next(100) / 10.0, PoorSoil = random.Next(2) != 0, Steep = (SteepType)steepTypeValues.GetValue(random.Next(steepTypeValues.Length)) });
//                 landuses.Add(lotName, new Landuse { Type = (LanduseType)landuseValues.GetValue(random.Next(landuseValues.Length)) });
//             }
//             /////////////

            var constraintsTable = SearchUtilities.CreateConstraintsTable(landuses, lots, constraints);
            

            // set of unassigned landuses
            var laui = new List<string>(landuses.Keys.OrderBy(s => constraintsTable[s].Count(r => !r.Value))); // "place the most constrained landuses first"

            // set of unassigned lots
            var loti = new List<string>(lots.Keys.OrderBy(s => lots[s].Cost)); // "set of lots yet to be assigned ordered according to lowest cost first"
            
            
            var root = new TreeNode<LanduseAllocations>(new LanduseAllocations(lots));
            var bruteWatch = System.Diagnostics.Stopwatch.StartNew();
            RecursiveAllocate(laui, loti, constraintsTable, root);
            
            //RecursivePrintTree(root, 0);

            var leaves = new List<LanduseAllocations>();
            RecursiveGetLeaves(root, leaves);
   
            var leavesComplete = leaves.Where(allocations => allocations.Count == landuses.Count).OrderBy(allocations => allocations.CurrentCost());
            //foreach (var l in leavesComplete)
            //{
            //    Console.WriteLine("{0} - €{1}", l, l.CurrentCost());
            //}
            bruteWatch.Stop();
            Console.WriteLine("Bruteforce obtained all solutions in {0} miliseconds:\n", bruteWatch.ElapsedMilliseconds);

            var aStarWatch = System.Diagnostics.Stopwatch.StartNew();
            var astarResult = AStarSearch.Search(landuses, lots, constraints);
            aStarWatch.Stop();
            Console.WriteLine("A* solution:\nTook {0} miliseconds", aStarWatch.ElapsedMilliseconds);

            Console.ReadKey();
        }

        private static void RecursivePrintTree(TreeNode<LanduseAllocations> currentNode, int i)
        {
            Console.WriteLine(currentNode.Data.ToString().Insert(0, new string(' ', i)));
            foreach (var treeNode in currentNode.Children)
            {
                Program.RecursivePrintTree(treeNode, i + 1);
            }
        }

        private static void RecursiveAllocate(List<string> laui, List<string> loti, Dictionary<string, Dictionary<string, bool>> constraintsTable, TreeNode<LanduseAllocations> currentNode)
        {
            foreach (var l in laui)
            {
                foreach (var c in constraintsTable[l].Where(c => c.Value && loti.Contains(c.Key)))
                {
                    Program.RecursiveAllocate(laui.FindAll(s => s != l), loti.FindAll(s => s != c.Key), constraintsTable, currentNode.AddChild(currentNode.Data.Allocate(l, c.Key)));
                }
            }
        }

        private static void RecursiveGetLeaves(TreeNode<LanduseAllocations> currentNode, List<LanduseAllocations> leaves)
        {
            if (currentNode.Children.Count == 0)
                leaves.Add(currentNode.Data);
            else
            {
                foreach (var treeNode in currentNode.Children)
                {
                    Program.RecursiveGetLeaves(treeNode, leaves);
                }
            }
        }

        private static double Cost(IEnumerable<string> loti, List<string> laui, IReadOnlyDictionary<string, Lot> lots, LanduseAllocations n)
        {
            var h = HeuristicCost(loti, laui, lots);
            var g = n.CurrentCost();
            return h + g;
        }

        static double HeuristicCost(IEnumerable<string> loti, IReadOnlyCollection<string> laui, IReadOnlyDictionary<string, Lot> lots)
        {
            // let p be the number of landuses yet to be assigned
            var p = laui.Count;

            // h(n) is the sum of the costs of the first p elements in loti
            return loti.Take(p).Select(s => lots[s].Cost).Sum();
        }
    }
}
