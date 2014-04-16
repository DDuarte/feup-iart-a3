using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using IART_A3.StateRepresentation;

namespace IART_A3.Search
{
    class AStarSearch
    {
        public static LanduseAllocations Search(IReadOnlyDictionary<string, Landuse> landuses, IReadOnlyDictionary<string, Lot> lots, IReadOnlyDictionary<string, Constraint> constraints)
        {
            var constraintsTable = SearchUtilities.CreateConstraintsTable(landuses, lots, constraints);
            var lotNames = new HashSet<string>(lots.Keys);
            var landuseNames = new HashSet<string>(landuses.Keys);

            var stateComparer = new AllocationsComparer(lots);
            var visitedStates = new List<LanduseAllocations>();
            var stateQueue = new List<LanduseAllocations> { new LanduseAllocations(landuseNames, lotNames) };

            

            while (stateQueue.Any()) //while not empty
            {
                var currentState = stateQueue.First();
                stateQueue.RemoveAt(0);
                if (currentState.IsFinalState())
                    return currentState;

                foreach (var state in currentState.GetSuccessors().Where(state => !visitedStates.Contains(state)))
                {
                    visitedStates.Add(state);
                    stateQueue.Add(state);
                }

                stateQueue.Sort(stateComparer);
            }

            return null;
        } 

        private class AllocationsComparer : IComparer<LanduseAllocations>
        {
            private readonly IReadOnlyDictionary<string, Lot> _lots;

            public AllocationsComparer(IReadOnlyDictionary<string, Lot> lots)
            {
                _lots = lots;
            }

            public int Compare(LanduseAllocations x, LanduseAllocations y)
            {
                var costX = x.CurrentCost(_lots) + x.HeuristicCost(_lots);
                var costY = y.CurrentCost(_lots) + y.HeuristicCost(_lots);
                var comparison = costX.CompareTo(costY);
                return comparison == 0 ? x.LandUsesLeft().CompareTo(y.LandUsesLeft()) : comparison; //for same estimated cost, choose deepest node
            }
        }
    }
}
