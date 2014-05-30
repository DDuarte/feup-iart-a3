using System.Collections.Generic;
using System.Linq;
using LandAllocationsLib.StateRepresentation;

namespace LandAllocationsLib.SearchAlgorithms
{
    public class BreadthFirstSearchAlgorithm : SearchAlgorithm
    {
        public BreadthFirstSearchAlgorithm(Problem problem) : base(problem) { }

        public override string Name { get { return "BreadthFirst"; } }

        protected override LanduseAllocations SearchImpl()
        {
            if (Problem.Landuses.Count > Problem.Lots.Count) return null;
            var firstState = new LanduseAllocations(Problem);
            var stateQueue = new List<LanduseAllocations> { firstState };
            var visitedStates = new List<LanduseAllocations>();

            while (stateQueue.Count > 0)
            {
                ++Iterations;
                var curState = stateQueue.First();
                stateQueue.Remove(curState);

                if (curState.IsFinalState)
                    return curState;

                foreach (var state in curState.GetSuccessors().Where(st => !visitedStates.Contains(st)))
                {
                    visitedStates.Add(state);
                    stateQueue.Add(state);
                }

            }

            return null;
        }
    }
}
