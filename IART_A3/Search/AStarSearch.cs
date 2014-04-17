using System.Collections.Generic;
using System.Linq;
using IART_A3.StateRepresentation;

namespace IART_A3.Search
{
    class AStarSearch
    {
        public static LanduseAllocations Search(IReadOnlyDictionary<string, Landuse> landuses, IReadOnlyDictionary<string, Lot> lots, IReadOnlyDictionary<string, Constraint> constraints)
        {
            var constraintsTable = SearchUtilities.CreateConstraintsTable(landuses, lots, constraints);
            var landuseNames = new HashSet<string>(landuses.Keys);

            var stateComparer = new AllocationsComparer();
            var visitedStates = new List<LanduseAllocations>();
            var stateQueue = new SortedSet<LanduseAllocations> (stateComparer) { new LanduseAllocations(landuseNames, lots) };


            while (stateQueue.Any()) //while not empty
            {
                var currentState = stateQueue.First();
                stateQueue.Remove(currentState);

                if (currentState.IsFinalState())
                    return currentState;

                
                foreach (var state in currentState.GetSuccessors(constraintsTable).Where(state => !visitedStates.Contains(state)))
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
                var costX = x.CurrentCost() + x.HeuristicCost();
                var costY = y.CurrentCost() + y.HeuristicCost();
                var comparison = costX.CompareTo(costY);
                if (comparison != 0)
                    return comparison;

                comparison = x.LandUsesLeft().CompareTo(y.LandUsesLeft()); //for same estimated cost, choose deepest node
                return comparison != 0 ? comparison : x.Id.CompareTo(y.Id); //uses uniqueID for disambiguation
            }
        }
    }
}
