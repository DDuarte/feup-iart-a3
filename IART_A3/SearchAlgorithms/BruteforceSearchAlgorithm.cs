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

            if (!states.Any(allocations => allocations.IsFinalState))
                return null;

            return states
                .Where(allocations => allocations.IsFinalState)
                .OrderBy(allocations => allocations.CurrentCost)
                .First();
        }

        private void RecursiveAllocate(LanduseAllocations state, ICollection<LanduseAllocations> states)
        {
            ++ItCounter;
            var successors = state.GetSuccessors();
            foreach (var successor in successors)
            {
                states.Add(successor);
                RecursiveAllocate(successor, states);
            }
        }
    }
}
