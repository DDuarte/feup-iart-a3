using System;
using System.Diagnostics;
using System.IO;
using IART_A3.StateRepresentation;

namespace IART_A3.SearchAlgorithms
{
    public abstract class SearchAlgorithm
    {
        protected readonly Problem Problem;
        protected long ItCounter;

        protected SearchAlgorithm(Problem problem)
        {
            Problem = problem;
            ItCounter = 0;
        }

        /// <summary>
        /// Algorithm name, for debugging purposes
        /// </summary>
        public abstract string Name { get; }

        public LanduseAllocations Search(TextWriter output = null)
        {
            ItCounter = 0;
            var res = Problem.Landuses.Count > Problem.Lots.Count ? SearchImpl() : null;

            if (output == null) return res;

            if (res != null)
            {
                output.WriteLine("{0} solution:\n\t\t{1}\n\tCost: {2}\n\tTook {3} iterations",
                    Name, res, res.CurrentCost, ItCounter);
            }
            else
            {
                output.WriteLine("{0} solution:\n\tNo solution found.\n\t Took {1} iterations",
                    Name, ItCounter);
            }

            return res;
        }

        /// <summary>
        /// In addition to finding a solution, also measures the time taken to complete the search and the number of iterations necessary.
        /// </summary>
        /// <param name="output">TextWriter where to print the result. If null, no result will be printed</param>
        /// <returns>Returns a tuple with the solution found, the time taken to find it (miliseconds) and the number of iterations needed</returns>
        public Tuple<LanduseAllocations, long, long> TimedSearch(TextWriter output = null)
        {
            ItCounter = 0;
            var watch = Stopwatch.StartNew();
            var res = SearchImpl();
            watch.Stop();
            var time = watch.ElapsedMilliseconds;

            if (output == null) return Tuple.Create(res, time, ItCounter);

            if (res != null)
            {
                output.WriteLine("{0} solution:\n\tTook {1} milliseconds\n\tTook {2} iterations\n\t{3}\n\tCost: {4}\n",
                    Name, time, ItCounter, res, res.CurrentCost);
            }
            else
            {
                output.WriteLine("{0} solution:\n\tTook {1} milliseconds\n\tTook {2} iterations\n\tNo solution found.\n",
                    Name, time, ItCounter);
            }

            return Tuple.Create(res, time, ItCounter);
        }

        protected abstract LanduseAllocations SearchImpl();
    }
}
