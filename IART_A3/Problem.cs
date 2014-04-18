using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using IART_A3.Constraints;
using IART_A3.StateRepresentation;

namespace IART_A3
{
    public class Problem
    {
        public ReadOnlyDictionary<string, Lot> Lots { get; set; }
        public ReadOnlyDictionary<string, Landuse> Landuses { get; set; }
        public ReadOnlyDictionary<string, IHardConstraint> HardConstraints { private get; set; }
        public ReadOnlyDictionary<string, ISoftConstraint> SoftConstraints { private get; set; }

        public ReadOnlyDictionary<string, Dictionary<string, bool>> HardConstraintsTable { get; private set; }
        public ReadOnlyDictionary<string, Dictionary<string, double>> SoftConstraintsTable { get; private set; }

        public void Init()
        {
            CreateConstraintsTables();
            // Perhaps create a constructor that accepts the above properties and calls the create method?
        }

        private void CreateConstraintsTables()
        {
            /*Contract.Requires(Landuses != null);
            Contract.Requires(Lots != null);
            Contract.Requires(HardConstraints != null);
            Contract.Requires(SoftConstraints != null);
            Contract.Ensures(HardConstraintsTable != null);
            Contract.Ensures(SoftConstraintsTable != null);*/

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
                    //var cost = SoftConstraints.Aggregate(0.0, (d, constraint) => d + constraint.Value.FeasibleCost(landuse.Value, lot.Value));
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
