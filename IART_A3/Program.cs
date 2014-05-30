using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using IART_A3.SearchAlgorithms;
using IART_A3.StateRepresentation;

namespace IART_A3
{
    public static class Program
    {
        enum ExitCodes
        {
            Success = 0,
            WrongUsage,
            UnknownAlgorithm,
            BadInputFile,
            BadOutputFile
        }

        static int Main(string[] args)
        {
            var algorithms = new Dictionary<string, Type>
            {
                {"A*", typeof (AStarSearchAlgorithm)},
                {"Greedy", typeof (GreedySearchAlgorithm)},
                {"UniformCost", typeof (UniformCostAlgorithm)},
                {"BreadthFirst", typeof (BreadthFirstSearchAlgorithm)},
                {"Bruteforce", typeof (BruteforceSearchAlgorithm)},
                {"DepthFirst", typeof (DepthFirstSearchAlgorithm)}
            };

            var allAlgorithms = algorithms.Keys.Aggregate((s, s1) => s + "|" + s1);

            if (args.Length != 3 && args.Length != 4)
            {
                Console.Error.WriteLine("Usage: {0} {1} <in.json> <out.json> [--verbose|-v]",
                    AppDomain.CurrentDomain.FriendlyName, allAlgorithms);
                return (int) ExitCodes.WrongUsage;
            }

            var algorithmName = args[0];
            var inFileName = args[1];
            var outFileName = args[2];
            TextWriter textWriter = null;
            if (args.Length == 4)
                if (args[3] == "-v" || args[3] == "--verbose")
                    textWriter = Console.Out;

            if (!algorithms.Keys.Contains(algorithmName))
            {
                Console.Error.WriteLine("Algorithm {0} not supported. Available algorithms: {1}",
                    algorithmName, allAlgorithms);
                return (int) ExitCodes.UnknownAlgorithm;
            }

            Problem problem;

            try
            {
                problem = Problem.ReadJson(inFileName);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Could not read problem data from {0}. Details: {1}",
                    inFileName, ex);
                return (int) ExitCodes.BadInputFile;
            }

            var algorithmType = algorithms[algorithmName];
            var algorithm = (SearchAlgorithm)Activator.CreateInstance(algorithmType, problem);

            problem.ProblemResult = algorithm.Search(textWriter);

            try
            {
                problem.WriteJson(outFileName);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Could not write solved problem data to {0}. Details: {1}",
                    outFileName, ex);

                return (int) ExitCodes.BadOutputFile;
            }

            return (int) ExitCodes.Success;
        }
    }
}
