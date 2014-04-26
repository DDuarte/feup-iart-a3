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
            var stateQueue = new SortedSet<LanduseAllocations>(new AllocationsComparer())
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

        private class AllocationsComparer : IComparer<LanduseAllocations>
        {
            public int Compare(LanduseAllocations x, LanduseAllocations y)
            {
                var costX = x.CurrentCost + x.HeuristicCost;
                var costY = y.CurrentCost + y.HeuristicCost;
                var comparison = costX.CompareTo(costY);
                if (comparison != 0)
                    return comparison;

                comparison = x.LandUsesLeft.CompareTo(y.LandUsesLeft); //for same estimated cost, choose deepest node
                return comparison != 0 ? comparison : x.Id.CompareTo(y.Id); //uses uniqueID for disambiguation
            }
        }
    }
}
