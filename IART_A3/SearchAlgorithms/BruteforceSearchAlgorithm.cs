using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using IART_A3.StateRepresentation;

namespace IART_A3.SearchAlgorithms
{
    class BruteforceSearchAlgorithm : SearchAlgorithm
    {
        public BruteforceSearchAlgorithm(Problem problem) : base(problem) { }

        public override string Name { get { return "Bruteforce"; } }

        protected override LanduseAllocations SearchImpl()
        {
            // set of unassigned landuses
            var laui = new List<string>(Problem.Landuses.Keys.OrderBy(s => Problem.HardConstraintsTable[s].Count(r => !r.Value))); // "place the most constrained landuses first"

            // set of unassigned lots
            var loti = new List<string>(Problem.Lots.Keys.OrderBy(s => Problem.Lots[s].Price)); // "set of lots yet to be assigned ordered according to lowest cost first"

            // TODO: perhaps get rid of the TreeNode
            var root = new TreeNode<LanduseAllocations>(new LanduseAllocations(Problem.Lots, Problem.HardConstraintsTable));
            RecursiveAllocate(laui, loti, Problem.HardConstraintsTable, root);

            var leaves = new List<LanduseAllocations>();
            RecursiveGetLeaves(root, leaves);

            return leaves
                .Where(allocations => allocations.Count == Problem.Landuses.Count)
                .OrderBy(allocations => allocations.CurrentCost())
                .First();
        }

        private static void RecursiveAllocate(List<string> laui, List<string> loti, ReadOnlyDictionary<string, Dictionary<string, bool>> constraintsTable, TreeNode<LanduseAllocations> currentNode)
        {
            foreach (var l in laui)
            {
                foreach (var c in constraintsTable[l].Where(c => c.Value && loti.Contains(c.Key)))
                {
                    RecursiveAllocate(laui.FindAll(s => s != l), loti.FindAll(s => s != c.Key), constraintsTable, currentNode.AddChild(currentNode.Data.Allocate(l, c.Key, constraintsTable)));
                }
            }
        }

        private static void RecursiveGetLeaves(TreeNode<LanduseAllocations> currentNode, ICollection<LanduseAllocations> leaves)
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
    }
}
