using System.Collections.Generic;
using System.Linq;
using IART_A3.StateRepresentation;

namespace IART_A3.SearchAlgorithms
{
    public class BruteforceSearchAlgorithm : SearchAlgorithm
    {
        public BruteforceSearchAlgorithm(Problem problem) : base(problem) { }

        public override string Name { get { return "Bruteforce"; } }

        protected override LanduseAllocations SearchImpl()
        {
            if (Problem.Landuses.Count > Problem.Lots.Count) return null;
            var firstState = new LanduseAllocations(Problem);
            var stateQueue = new List<LanduseAllocations> { firstState };
            var visitedStates = new List<LanduseAllocations>();

            var states = new List<LanduseAllocations>();

            while (stateQueue.Count > 0)
            {
                ++Iterations;
                var curState = stateQueue.First();
                stateQueue.Remove(curState);

                if (curState.IsFinalState)
                    states.Add(curState);

                foreach (var state in curState.GetSuccessors().Where(st => !visitedStates.Contains(st)))
                {
                    visitedStates.Add(state);
                    stateQueue.Add(state);
                }

            }

            if (!states.Any(allocations => allocations.IsFinalState))
                return null;

            return states
                .Where(allocations => allocations.IsFinalState)
                .OrderBy(allocations => allocations.CurrentCost)
                .First();
        }
    }
}
