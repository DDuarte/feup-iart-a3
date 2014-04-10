using System;
using System.Collections.Generic;
using System.Linq;

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

            var contraints = new Dictionary<string, Constraint>
            {
                {"C1", new DistanceConstraint(new [] {LanduseType.Recreational}, Place.Lake, DistanceConstraint.CloserThan)},
                {"C2", new SteepConstraint(new[] {LanduseType.Apartments, LanduseType.HousingComplex, LanduseType.Cemetery, LanduseType.Dump}, new[] {SteepType.Flat, SteepType.ModeratelySteep})},
                {"C3", new SoilConstraint(new[] {LanduseType.Apartments, LanduseType.HousingComplex}, false)},
                {"C4", new DistanceConstraint(new[] {LanduseType.Apartments, LanduseType.HousingComplex, LanduseType.Recreational}, Place.Highway, DistanceConstraint.FartherThan)}
            };

            var constraintsTable = new Dictionary<string, Dictionary<string, bool>>(); // landuse, -> lot -> yes/no

            foreach (var landuse in landuses)
            {
                foreach (var lot in lots)
                {
                    var valid = contraints.All(constraint => constraint.Value.Feasible(landuse.Value, lot.Value));
                    if (!constraintsTable.ContainsKey(landuse.Key))
                        constraintsTable.Add(landuse.Key, new Dictionary<string, bool>());

                    constraintsTable[landuse.Key].Add(lot.Key, valid);
                }
            }

            Console.WriteLine("Contraints table:");
            foreach (var c in constraintsTable)
            {
                foreach (var v in c.Value)
                {
                    Console.WriteLine("\t{0} - {1} = {2}", c.Key, v.Key, v.Value ? "OK" : "NOTOK");
                }
            }

            // set of unassigned landuses
            var laui = new List<string>(landuses.Keys.OrderBy(s => constraintsTable[s].Count(r => !r.Value))); // "place the most constrained landuses first"

            // set of unassigned lots
            var loti = new List<string>(lots.Keys.OrderBy(s => lots[s].Cost)); // "set of lots yet to be assigned ordered according to lowest cost first"

            var root = new TreeNode<LanduseAllocations>(new LanduseAllocations());
            RecursiveAllocate(laui, loti, constraintsTable, root);

            //RecursivePrintTree(root, 0);

            var leaves = new List<LanduseAllocations>();
            RecursiveGetLeaves(root, leaves);

            var leavesComplete = leaves.Where(allocations => allocations.Count == landuses.Count).OrderBy(allocations => allocations.Cost(lots));
            foreach (var l in leavesComplete)
            {
                Console.WriteLine("{0} - €{1}", l, l.Cost(lots));
            }

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
                foreach (var c in constraintsTable[l].Where(c => c.Value /*&& loti.Contains(c.Key)*/))
                {
                    RecursiveAllocate(laui.FindAll(s => s != l), loti.FindAll(s => s != c.Key), constraintsTable, currentNode.AddChild(currentNode.Data.Allocate(l, c.Key)));
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
                    RecursiveGetLeaves(treeNode, leaves);
                }
            }
        }

        private static double Cost(IEnumerable<string> loti, List<string> laui, IReadOnlyDictionary<string, Lot> lots, LanduseAllocations n)
        {
            var h = HeuristicCost(loti, laui, lots);
            var g = n.Cost(lots);
            return h + g;
        }

        static double HeuristicCost(IEnumerable<string> loti, IReadOnlyCollection<string> laui, IReadOnlyDictionary<string, Lot> lots)
        {
            // let p be the number of landuses yet to be assigned
            var p = laui.Count;

            // h(n) is the sum of the costs of the first p elements in Loti'
            return loti.Take(p).Select(s => lots[s].Cost).Sum();
        }
    }
}
