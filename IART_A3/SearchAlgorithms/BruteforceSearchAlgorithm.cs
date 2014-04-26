using System.Collections.Generic;
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
            // TODO: perhaps get rid of the TreeNode
            var root = new TreeNode<LanduseAllocations>(new LanduseAllocations(Problem));
            RecursiveAllocate(root);

            var leaves = new List<LanduseAllocations>();
            RecursiveGetLeaves(root, leaves);

            return leaves
                .Where(allocations => allocations.Count == Problem.Landuses.Count)
                .OrderBy(allocations => allocations.CurrentCost)
                .First();
        }

        private static void RecursiveAllocate(TreeNode<LanduseAllocations> currentNode)
        {
            var successors = currentNode.Data.GetSuccessors();
            foreach (var successor in successors)
            {
                RecursiveAllocate(currentNode.AddChild(successor));
            }
        }

        private static void RecursiveGetLeaves(TreeNode<LanduseAllocations> currentNode, ICollection<LanduseAllocations> leaves)
        {
            if (currentNode.Children.Count == 0 && currentNode.Data.IsFinalState)
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
