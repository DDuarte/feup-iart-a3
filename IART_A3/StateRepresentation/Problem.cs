using System;
using System.Collections.Generic;

using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using IART_A3.Constraints;
using Newtonsoft.Json;


namespace IART_A3.StateRepresentation
{
    public class Problem
    {
        public class Result
        {
            public Result(string algorithmName, LanduseAllocations landuseAllocations,
                long elapsedMilliseconds, long iterations)
            {
                AlgorithmName = algorithmName;
                LanduseAllocations = landuseAllocations;
                ElapsedMilliseconds = elapsedMilliseconds;
                Iterations = iterations;
            }

// ReSharper disable MemberCanBePrivate.Global
            public string AlgorithmName;
            public LanduseAllocations LanduseAllocations;
            public long ElapsedMilliseconds;
            public long Iterations;
// ReSharper restore MemberCanBePrivate.Global
        }

        public Problem(Dictionary<string, Lot> lots, Dictionary<string, Landuse> landuses,
            List<Point> lakes, List<Point> highways,
            Dictionary<string, IHardConstraint> hardConstraints,
            Dictionary<string, ISoftConstraint> softConstraints)
        {
            Lots = lots ?? new Dictionary<string, Lot>();
            Landuses = landuses ?? new Dictionary<string, Landuse>();
            Lakes = lakes ?? new List<Point>();
            Highways = highways ?? new List<Point>();
            HardConstraints = hardConstraints ?? new Dictionary<string, IHardConstraint>();
            SoftConstraints = softConstraints ?? new Dictionary<string, ISoftConstraint>();

            CreateConstraintsTables();
        }

// ReSharper disable FieldCanBeMadeReadOnly.Global
        public Dictionary<string, Lot> Lots;
        public Dictionary<string, Landuse> Landuses;
        public List<Point> Lakes;
        public List<Point> Highways;
        public Dictionary<string, IHardConstraint> HardConstraints;
        public Dictionary<string, ISoftConstraint> SoftConstraints;
        public Result ProblemResult;
// ReSharper restore FieldCanBeMadeReadOnly.Global

        [JsonIgnore]
        public Dictionary<string, Dictionary<string, bool>> HardConstraintsTable;
        [JsonIgnore]
        public Dictionary<string, Dictionary<string, double>> SoftConstraintsTable;

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            CreateConstraintsTables();
        }

        private void CreateConstraintsTables()
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
                    return js.Deserialize<Problem>(reader);
                }
            }
        }
    }
}
