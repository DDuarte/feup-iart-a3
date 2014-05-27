using System;
using System.Diagnostics;
using System.IO;
using IART_A3.StateRepresentation;

namespace IART_A3.SearchAlgorithms
{
    public abstract class SearchAlgorithm
    {
        protected readonly Problem Problem;

        protected SearchAlgorithm(Problem problem)
        {
            Problem = problem;
        }

        /// <summary>
        /// Algorithm name, for debugging purposes
        /// </summary>
        public abstract string Name { get; }

        public LanduseAllocations Search(TextWriter output = null)
        {
            var res = Problem.Landuses.Count > Problem.Lots.Count ? SearchImpl() : null;

            if (output != null)
            {
                output.WriteLine("{0} solution:\n\t\t{1}\n\tCost: {2}\n",
                    Name,  res, res.CurrentCost);
            }

            return SearchImpl();
        }

        public Tuple<LanduseAllocations, long> TimedSearch(TextWriter output = null)
        {
            var watch = Stopwatch.StartNew();
            var res = SearchImpl();
            watch.Stop();
            var time = watch.ElapsedMilliseconds;

            if (output == null) return Tuple.Create(res, time);

            if (res != null)
            {
                output.WriteLine("{0} solution:\n\tTook {1} milliseconds\n\t{2}\n\tCost: {3}\n",
                    Name, time, res, res.CurrentCost);
            }
            else
            {
                output.WriteLine("{0} solution:\n\tTook {1} milliseconds\n\tNo solution found.\n",
                    Name, time);
            }

            return Tuple.Create(res, time);
        }

        protected abstract LanduseAllocations SearchImpl();
    }
}
