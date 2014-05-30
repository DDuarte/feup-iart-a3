using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LandAllocationsLib.Constraints;
using Newtonsoft.Json;

namespace LandAllocationsLib.StateRepresentation
{
    public class Problem
    {
        public class Result
        {
            public Result(string algorithmName, HashSet<Tuple<string, string>> landuseAllocations,
                long elapsedMilliseconds, long iterations)
            {
                AlgorithmName = algorithmName;
                LanduseAllocations = landuseAllocations;
                ElapsedMilliseconds = elapsedMilliseconds;
                Iterations = iterations;
            }

// ReSharper disable MemberCanBePrivate.Global
            public string AlgorithmName;
            public HashSet<Tuple<string, string>> LanduseAllocations;
            public long ElapsedMilliseconds;
            public long Iterations;
// ReSharper restore MemberCanBePrivate.Global
        }

        public Problem() { }

        public Problem(int size)
        {
            Size = size;
            Lots = new Dictionary<string, Lot>();
            Landuses = new Dictionary<string, Landuse>();
            Lakes = new HashSet<Point>();
            Highways = new HashSet<Point>();
            HardConstraints = new Dictionary<string, IHardConstraint>();
            SoftConstraints = new Dictionary<string, ISoftConstraint>();

            UpdateConstraintsTable();
        }

        public Problem(int size, Dictionary<string, Lot> lots, Dictionary<string, Landuse> landuses,
            HashSet<Point> lakes, HashSet<Point> highways,
            Dictionary<string, IHardConstraint> hardConstraints,
            Dictionary<string, ISoftConstraint> softConstraints)
        {
            Size = size;
            Lots = lots;
            Landuses = landuses;
            Lakes = lakes;
            Highways = highways;
            HardConstraints = hardConstraints;
            SoftConstraints = softConstraints;

            UpdateConstraintsTable();
        }

// ReSharper disable FieldCanBeMadeReadOnly.Global
        public Dictionary<string, Lot> Lots;
        public Dictionary<string, Landuse> Landuses;
        public HashSet<Point> Lakes;
        public HashSet<Point> Highways;
        public Dictionary<string, IHardConstraint> HardConstraints;
        public Dictionary<string, ISoftConstraint> SoftConstraints;
        public Result ProblemResult;
        public int Size;
// ReSharper restore FieldCanBeMadeReadOnly.Global

        [JsonIgnore]
        public Dictionary<string, Dictionary<string, bool>> HardConstraintsTable;
        [JsonIgnore]
        public Dictionary<string, Dictionary<string, double>> SoftConstraintsTable;

        public void UpdateConstraintsTable()
        {
            var hardConstraintsTable = new Dictionary<string, Dictionary<string, bool>>(); // landuse -> lot -> yes/no
            var softConstraintsTable = new Dictionary<string, Dictionary<string, double>>(); // landuse -> lot -> cost

            foreach (var landuse in Landuses)
            {
                foreach (var lot in Lots)
                {
                    // hard
                    var valid = HardConstraints.All(constraint => constraint.Value.Feasible(landuse.Value, lot.Value, this));
                    if (!hardConstraintsTable.ContainsKey(landuse.Key))
                        hardConstraintsTable.Add(landuse.Key, new Dictionary<string, bool>());

                    hardConstraintsTable[landuse.Key][lot.Key] = valid;

                    // soft
                    var cost = SoftConstraints.Sum(constraint => constraint.Value.FeasibleCost(landuse.Value, lot.Value, this));
                    if (!softConstraintsTable.ContainsKey(landuse.Key))
                        softConstraintsTable.Add(landuse.Key, new Dictionary<string, double>());

                    softConstraintsTable[landuse.Key][lot.Key] = cost;
                }
            }

            HardConstraintsTable = hardConstraintsTable;
            SoftConstraintsTable = softConstraintsTable;
        }

        public bool AddConstraint(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;

            var tokens = str.Split(' ');

            string name;
            bool hardConstraint;

            if (tokens[0].StartsWith("H", StringComparison.InvariantCultureIgnoreCase))
            {
                name = "CH" + HardConstraints.Count;
                hardConstraint = true;
            }
            else if (tokens[0].StartsWith("S", StringComparison.InvariantCultureIgnoreCase))
            {
                name = "CS" + HardConstraints.Count;
                hardConstraint = false;
            }
            else
                return false;

            var baseCost = -1;
            if (!hardConstraint)
            {
                var baseCostStr = tokens[0].Substring(2, tokens[0].Length - 3);
                if (!int.TryParse(baseCostStr, out baseCost))
                    return false;
            }

            var failed = false;
            var landuseTypesStr = tokens[1].Trim('[', ']');
            var landuseTypesStrArray = landuseTypesStr.Split(',');
            var landuseTypes = landuseTypesStrArray.Select(s =>
            {
                LanduseType landuseType;
                if (!Enum.TryParse(s, true, out landuseType))
                {
                    failed = true;
                    return default(LanduseType);
                }
                return landuseType;
            }).ToArray();

            if (failed)
                return false;

            if (tokens[2].StartsWith("size", StringComparison.InvariantCultureIgnoreCase))
            {
                bool checkSmaller;
                if (tokens[3] == "<")
                    checkSmaller = true;
                else if (tokens[3] == ">")
                    checkSmaller = false;
                else
                    return false;

                int threshold;
                if (!int.TryParse(tokens[4], out threshold))
                    return false;

                if (hardConstraint)
                    HardConstraints.Add(name, new SizeHardConstraint(landuseTypes, checkSmaller, threshold));
                else
                    SoftConstraints.Add(name, new SizeSoftConstraint(baseCost, landuseTypes, checkSmaller, threshold));

                return true;
            }
            else if (tokens[2].StartsWith("distance", StringComparison.InvariantCultureIgnoreCase))
            {
                var placeStr = tokens[2].Substring(9, tokens[2].Length - 10);
                Place place;
                if (!Enum.TryParse(placeStr, true, out place))
                    return false;

                bool checkSmaller;
                if (tokens[3] == "<")
                    checkSmaller = true;
                else if (tokens[3] == ">")
                    checkSmaller = false;
                else
                    return false;

                int threshold;
                if (!int.TryParse(tokens[4], out threshold))
                    return false;

                if (hardConstraint)
                    HardConstraints.Add(name, new DistanceHardConstraint(landuseTypes, place, checkSmaller, threshold));
                else
                    SoftConstraints.Add(name, new DistanceSoftConstraint(baseCost, landuseTypes, place, checkSmaller, threshold));

                return true;
            }
            else if (tokens[2].StartsWith("steep", StringComparison.InvariantCultureIgnoreCase))
            {
                var steepTypesStr = tokens[3].Trim('[', ']');
                var steepTypesStrArray = steepTypesStr.Split(',');
                var steepTypes = steepTypesStrArray.Select(s =>
                {
                    SteepType steepType;
                    if (!Enum.TryParse(s, true, out steepType))
                    {
                        failed = true;
                        return default(SteepType);
                    }
                    return steepType;
                }).ToArray();

                if (failed)
                    return false;

                if (hardConstraint)
                    HardConstraints.Add(name, new SteepHardConstraint(landuseTypes, steepTypes));
                else
                    SoftConstraints.Add(name, new SteepSoftConstraint(baseCost, landuseTypes, steepTypes));

                return true;
            }
            else if (tokens[2].StartsWith("soil", StringComparison.InvariantCultureIgnoreCase))
            {
                bool poorSoil;
                if (tokens[3].Equals("poor", StringComparison.InvariantCultureIgnoreCase))
                    poorSoil = true;
                else if (tokens[3].Equals("good", StringComparison.InvariantCultureIgnoreCase))
                    poorSoil = false;
                else
                    return false;

                if (hardConstraint)
                    HardConstraints.Add(name, new SoilHardConstraint(landuseTypes, poorSoil));
                else
                    SoftConstraints.Add(name, new SoilSoftConstraint(baseCost, landuseTypes, poorSoil));

                return true;
            }
            else
                return false;
        }

        public void WriteJson(string file)
        {
            var js = new JsonSerializer
            {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            };

            using (var sw = new StreamWriter(file))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    js.Serialize(writer, this);
                }
            }
        }

        public static Problem ReadJson(string file)
        {
            var js = new JsonSerializer { TypeNameHandling = TypeNameHandling.Auto };

            using (var sr = new StreamReader(file))
            {
                using (JsonReader reader = new JsonTextReader(sr))
                {
                    var problem = js.Deserialize<Problem>(reader);
                    problem.UpdateConstraintsTable();
                    return problem;
                }
            }
        }
    }
}
