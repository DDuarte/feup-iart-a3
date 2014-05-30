using System.Collections.Generic;
using System.Linq;
using LandAllocationsLib.StateRepresentation;

namespace LandAllocationsLib.SearchAlgorithms
{
    public abstract class BestFirstSearchAlgorithm : SearchAlgorithm
    {
        protected BestFirstSearchAlgorithm(Problem problem) : base(problem) { }

        protected abstract bool UseCurrentCost { get; }
        protected abstract bool UseHeuristicCost { get; }

        protected override LanduseAllocations SearchImpl()
        {
            if (Problem.Landuses.Count > Problem.Lots.Count) return null;

            var visitedStates = new List<LanduseAllocations>();
            var stateQueue =
                new SortedSet<LanduseAllocations>(new LanduseAllocations.Comparer
                {
                    UseCurrentCost = UseCurrentCost,
                    UseHeuristicCost = UseHeuristicCost
                })
            {
                new LanduseAllocations(Problem)
            };

            
            while (stateQueue.Count > 0 && !ShouldStop) // while not empty
            {
                ++Iterations;
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

    public class AStarSearchAlgorithm : BestFirstSearchAlgorithm
    {
        public AStarSearchAlgorithm(Problem problem) : base(problem) { }

        public override string Name { get { return "A*"; } }

        protected override bool UseCurrentCost { get { return true; } }

        protected override bool UseHeuristicCost { get { return true; } }
    }

    public class GreedySearchAlgorithm : BestFirstSearchAlgorithm
    {
        public GreedySearchAlgorithm(Problem problem) : base(problem) { }

        public override string Name { get { return "Greedy"; } }

        protected override bool UseCurrentCost { get { return false; } }

        protected override bool UseHeuristicCost { get { return true; } }
    }

    public class UniformCostAlgorithm : BestFirstSearchAlgorithm
    {
        public UniformCostAlgorithm(Problem problem) : base(problem) { }

        public override string Name { get { return "UniformCost"; } }

        protected override bool UseCurrentCost { get { return true; } }

        protected override bool UseHeuristicCost { get { return false; } }
    }
}
