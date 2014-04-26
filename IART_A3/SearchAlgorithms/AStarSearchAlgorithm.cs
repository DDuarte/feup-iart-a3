using System.Collections.Generic;
using System.Linq;
using IART_A3.StateRepresentation;

namespace IART_A3.SearchAlgorithms
{
    public class AStarSearchAlgorithm : SearchAlgorithm
    {
        public AStarSearchAlgorithm(Problem problem) : base(problem) { }

        public override string Name { get { return "A*"; } }

        protected override LanduseAllocations SearchImpl()
        {
            var visitedStates = new List<LanduseAllocations>();
            var stateQueue =
                new SortedSet<LanduseAllocations>(new LanduseAllocations.Comparer
                {
                    UseCurrentCost = true,
                    UseHeuristicCost = true
                })
            {
                new LanduseAllocations(Problem)
            };

            while (stateQueue.Count > 0) // while not empty
            {
                var currentState = stateQueue.Min;
                stateQueue.Remove(currentState);

                if (currentState.IsFinalState)
                    return currentState;

                foreach (var state in currentState.GetSuccessors().Where(state => !visitedStates.Contains(state)))
                {
                    visitedStates.Add(state);
                    stateQueue.Add(state);
                }
            }

            return null;
        }
    }
}
