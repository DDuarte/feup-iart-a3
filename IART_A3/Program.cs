using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            var constraints = new Dictionary<string, Constraint>
            {
                {"C1", new DistanceConstraint(new [] {LanduseType.Recreational}, Place.Lake, DistanceConstraint.CloserThan)},
                {"C2", new SteepConstraint(new[] {LanduseType.Apartments, LanduseType.HousingComplex, LanduseType.Cemetery, LanduseType.Dump}, new[] {SteepType.Flat, SteepType.ModeratelySteep})},
                {"C3", new SoilConstraint(new[] {LanduseType.Apartments, LanduseType.HousingComplex}, false)},
                {"C4", new DistanceConstraint(new[] {LanduseType.Apartments, LanduseType.HousingComplex, LanduseType.Recreational}, Place.Highway, DistanceConstraint.FartherThan)}
            };

            var constraintsTable = SearchUtilities.CreateConstraintsTable(landuses, lots, constraints);
            

            // set of unassigned landuses
            var laui = new List<string>(landuses.Keys.OrderBy(s => constraintsTable[s].Count(r => !r.Value))); // "place the most constrained landuses first"

            // set of unassigned lots
            var loti = new List<string>(lots.Keys.OrderBy(s => lots[s].Price)); // "set of lots yet to be assigned ordered according to lowest cost first"

            var root = new TreeNode<LanduseAllocations>(new LanduseAllocations(lots, constraintsTable));
            var bruteWatch = Stopwatch.StartNew();
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
            Console.WriteLine("Bruteforce obtained all solutions in {0} miliseconds:\n{2}\nCost: {1}", bruteWatch.ElapsedMilliseconds,leavesComplete.ElementAt(0).CurrentCost(), leavesComplete.ElementAt(0));

            var aStarWatch = Stopwatch.StartNew();
            var astarResult = AStarSearch.Search(landuses, lots, constraints);
            aStarWatch.Stop();
            Console.WriteLine("A* solution:\nTook {0} miliseconds\n{2}\nCost: {1}", aStarWatch.ElapsedMilliseconds, astarResult.CurrentCost(), astarResult);

            Console.ReadKey();
        }

        private static void RecursivePrintTree(TreeNode<LanduseAllocations> currentNode, int i)
        {
            Console.WriteLine(currentNode.Data.ToString().Insert(0, new string(' ', i)));
            foreach (var treeNode in currentNode.Children)
            {
                RecursivePrintTree(treeNode, i + 1);
            }
        }

        private static void RecursiveAllocate(List<string> laui, List<string> loti, Dictionary<string, Dictionary<string, bool>> constraintsTable, TreeNode<LanduseAllocations> currentNode)
        {
            foreach (var l in laui)
            {
                foreach (var c in constraintsTable[l].Where(c => c.Value && loti.Contains(c.Key)))
                {
                    RecursiveAllocate(laui.FindAll(s => s != l), loti.FindAll(s => s != c.Key), constraintsTable, currentNode.AddChild(currentNode.Data.Allocate(l, c.Key, constraintsTable)));
                }
            }
        }

        private static void RecursiveGetLeaves(TreeNode<LanduseAllocations> currentNode, List<LanduseAllocations> leaves)
        {
            if (currentNode.Children.Count == 0 && currentNode.Data.IsFinalState())
                leaves.Add(currentNode.Data);
            else
            {
                foreach (var treeNode in currentNode.Children)
                {
                    RecursiveGetLeaves(treeNode, leaves);
                }
            }
        }

        private static double Cost(IEnumerable<string> loti, IReadOnlyCollection<string> laui, IReadOnlyDictionary<string, Lot> lots, LanduseAllocations n)
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
            return loti.Take(p).Select(s => lots[s].Price).Sum();
        }
    }
}
