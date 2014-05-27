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
            if (Problem.Landuses.Count > Problem.Lots.Count) return null;
            var firstState = new LanduseAllocations(Problem);
            var states = new List<LanduseAllocations>();
            RecursiveAllocate(firstState, states);

            return states
                .Where(allocations => allocations.IsFinalState)
                .OrderBy(allocations => allocations.CurrentCost)
                .First();
        }

        private static void RecursiveAllocate(LanduseAllocations state, ICollection<LanduseAllocations> states)
        {
            var successors = state.GetSuccessors();
            foreach (var successor in successors)
            {
                states.Add(successor);
                RecursiveAllocate(successor, states);
            }
        }
    }
}
