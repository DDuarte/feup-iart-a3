using System;
using System.Collections.Generic;
using System.Linq;
using IART_A3.StateRepresentation;

namespace IART_A3.SearchAlgorithms
{
    class DepthFirstSearchAlgorithm : SearchAlgorithm
    {
        public DepthFirstSearchAlgorithm(Problem problem) : base(problem) { }

        public override string Name { get { return "DepthFirst"; } }

        private static LanduseAllocations SearchRecursive(LanduseAllocations curState, List<LanduseAllocations> visitedStates)
        {
            if (curState.IsFinalState)
                return curState;

            visitedStates.Add(curState);

            return curState.GetSuccessors().Where(st => !visitedStates.Contains(st)).Select(state => SearchRecursive(state, visitedStates)).FirstOrDefault(branchResult => branchResult != null);
        }

        protected override LanduseAllocations SearchImpl()
        {
            var firstState = new LanduseAllocations(Problem);
            var visitedStates = new List<LanduseAllocations>();

            return SearchRecursive(firstState, visitedStates);
        }
    }
}
