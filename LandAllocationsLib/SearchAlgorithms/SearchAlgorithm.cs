using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using LandAllocationsLib.StateRepresentation;

namespace LandAllocationsLib.SearchAlgorithms
{
    public abstract class SearchAlgorithm
    {
        protected readonly Problem Problem;
        protected long Iterations;

        protected SearchAlgorithm(Problem problem)
        {
            Problem = problem;
            Iterations = 0;
        }

        /// <summary>
        /// Algorithm name, for debugging purposes
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// In addition to finding a solution, also measures the time taken to complete the search and the number of iterations necessary.
        /// </summary>
        /// <param name="output">TextWriter where to print the result. If null, no result will be printed</param>
        /// <returns>Returns a Problem.Result with the solution found, the time taken to find it (milliseconds) and the number of iterations needed</returns>
        public Problem.Result Search(TextWriter output = null)
        {
            Iterations = 0;
            var watch = Stopwatch.StartNew();
            var res = SearchImpl();
            watch.Stop();
            var time = watch.ElapsedMilliseconds;

            if (output != null)
            {
                if (res != null)
                {
                    output.WriteLine("{0} solution:\n\tTook {1} milliseconds\n\tTook {2} iterations\n\t{3}\n\tCost: {4}\n",
                        Name, time, Iterations, res, res.CurrentCost);
                }
                else
                {
                    output.WriteLine("{0} solution:\n\tTook {1} milliseconds\n\tTook {2} iterations\n\tNo solution found.\n",
                        Name, time, Iterations);
                }
            }

            return new Problem.Result(Name, res != null ? res.Allocations : new HashSet<Tuple<string, string>>(),
                res.CurrentCost, time, Iterations);
        }

        protected abstract LanduseAllocations SearchImpl();
    }
}
