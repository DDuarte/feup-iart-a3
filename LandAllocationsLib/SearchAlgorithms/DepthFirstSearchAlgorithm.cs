using System.Collections.Generic;
using System.Linq;
using LandAllocationsLib.StateRepresentation;

namespace LandAllocationsLib.SearchAlgorithms
{
    public class DepthFirstSearchAlgorithm : SearchAlgorithm
    {
        public DepthFirstSearchAlgorithm(Problem problem) : base(problem) { }

        public override string Name { get { return "DepthFirst"; } }

        private LanduseAllocations SearchRecursive(LanduseAllocations curState, List<LanduseAllocations> visitedStates)
        {
            if (ShouldStop)
                return null;

            ++Iterations;
            if (curState.IsFinalState)
                return curState;

            visitedStates.Add(curState);

            return curState.GetSuccessors().Where(st => !visitedStates.Contains(st)).Select(state => SearchRecursive(state, visitedStates)).FirstOrDefault(branchResult => branchResult != null);
        }

        protected override LanduseAllocations SearchImpl()
        {
            if (Problem.Landuses.Count > Problem.Lots.Count) return null;
            var firstState = new LanduseAllocations(Problem);
            var visitedStates = new List<LanduseAllocations>();

            return SearchRecursive(firstState, visitedStates);
        }
    }
}
