using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using IART_A3.Constraints;

namespace IART_A3.StateRepresentation
{
    public class Problem
    {
        public Problem(ReadOnlyDictionary<string, Lot> lots, ReadOnlyDictionary<string, Landuse> landuses,
            ReadOnlyDictionary<string, IHardConstraint> hardConstraints,
            ReadOnlyDictionary<string, ISoftConstraint> softContraints)
        {
            Lots = lots;
            Landuses = landuses;
            HardConstraints = hardConstraints;
            SoftConstraints = softContraints;

            CreateConstraintsTables();
        }

        public ReadOnlyDictionary<string, Lot> Lots { get; private set; }
        public ReadOnlyDictionary<string, Landuse> Landuses { get; private set; }
        public ReadOnlyDictionary<string, IHardConstraint> HardConstraints { get; private set; }
        public ReadOnlyDictionary<string, ISoftConstraint> SoftConstraints { get; private set; }

        public ReadOnlyDictionary<string, Dictionary<string, bool>> HardConstraintsTable { get; private set; }
        public ReadOnlyDictionary<string, Dictionary<string, double>> SoftConstraintsTable { get; private set; }

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

            HardConstraintsTable = new ReadOnlyDictionary<string, Dictionary<string, bool>>(hardConstraintsTable);
            SoftConstraintsTable = new ReadOnlyDictionary<string, Dictionary<string, double>>(softConstraintsTable);
        }
    }
}
