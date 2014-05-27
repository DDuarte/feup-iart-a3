using System;
using System.Collections.Generic;

using System.IO;
using System.Linq;
using IART_A3.Constraints;
using IART_A3.SearchAlgorithms;
using Newtonsoft.Json;


namespace IART_A3.StateRepresentation
{
    public class Problem
    {
        public Problem(Dictionary<string, Lot> lots, Dictionary<string, Landuse> landuses,
            Dictionary<string, IHardConstraint> hardConstraints,
            Dictionary<string, ISoftConstraint> softConstraints)
        {
            Lots = lots ?? new Dictionary<string, Lot>();
            Landuses = landuses ?? new Dictionary<string, Landuse>();
            HardConstraints = hardConstraints ?? new Dictionary<string, IHardConstraint>();
            SoftConstraints = softConstraints ?? new Dictionary<string, ISoftConstraint>();

            CreateConstraintsTables();
        }

        public Dictionary<string, Lot> Lots;
        public Dictionary<string, Landuse> Landuses;
        public Dictionary<string, IHardConstraint> HardConstraints;
        public Dictionary<string, ISoftConstraint> SoftConstraints;

        public Dictionary<string, Dictionary<string, bool>> HardConstraintsTable;
        public Dictionary<string, Dictionary<string, double>> SoftConstraintsTable;

        private void CreateConstraintsTables()
        {
            var hardConstraintsTable = new Dictionary<string, Dictionary<string, bool>>(); // landuse -> lot -> yes/no
            var softConstraintsTable = new Dictionary<string, Dictionary<string, double>>(); // landuse -> lot -> cost

            foreach (var landuse in Landuses)
            {
                foreach (var lot in Lots)
                {
                    // hard
                    var valid = HardConstraints.All(constraint => constraint.Value.Feasible(landuse.Value, lot.Value));
                    if (!hardConstraintsTable.ContainsKey(landuse.Key))
                        hardConstraintsTable.Add(landuse.Key, new Dictionary<string, bool>());

                    hardConstraintsTable[landuse.Key][lot.Key] = valid;

                    // soft
                    var cost = SoftConstraints.Sum(constraint => constraint.Value.FeasibleCost(landuse.Value, lot.Value));
                    if (!softConstraintsTable.ContainsKey(landuse.Key))
                        softConstraintsTable.Add(landuse.Key, new Dictionary<string, double>());

                    softConstraintsTable[landuse.Key][lot.Key] = cost;
                }
            }

            HardConstraintsTable = hardConstraintsTable;
            SoftConstraintsTable = softConstraintsTable;
        }

        public void WriteJson(String filepath)
        {
            var js = new JsonSerializer { TypeNameHandling = TypeNameHandling.Auto };
            
            using (var sw = new StreamWriter(filepath))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                js.Serialize(writer, this);
            }
        }

        public static Problem ReadJson(String filepath)
        {
            var js = new JsonSerializer { TypeNameHandling = TypeNameHandling.Auto };

            using (var sr = new StreamReader(filepath))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                return js.Deserialize<Problem>(reader);  
            }
        }
    }
}
