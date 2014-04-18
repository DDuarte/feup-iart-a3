using System.Collections.Generic;
using System.Linq;
using IART_A3.Constraints;
using IART_A3.StateRepresentation;

namespace IART_A3
{
    public class Problem
    {
        public Dictionary<string, Lot> Lots { get; set; }
        public Dictionary<string, Landuse> Landuses { get; set; }
        public Dictionary<string, IHardConstraint> HardConstraints { private get; set; }
        public Dictionary<string, ISoftConstraint> SoftConstraints { private get; set; }

        public Dictionary<string, Dictionary<string, bool>> HardConstraintsTable { get; private set; }
        public Dictionary<string, Dictionary<string, double>> SoftConstraintsTable { get; private set; }

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

            HardConstraintsTable = new Dictionary<string, Dictionary<string, bool>>(); // landuse -> lot -> yes/no
            SoftConstraintsTable = new Dictionary<string, Dictionary<string, double>>(); // landuse -> lot -> cost

            foreach (var landuse in Landuses)
            {
                foreach (var lot in Lots)
                {
                    // hard
                    var valid = HardConstraints.All(constraint => constraint.Value.Feasible(landuse.Value, lot.Value));
                    if (!HardConstraintsTable.ContainsKey(landuse.Key))
                        HardConstraintsTable.Add(landuse.Key, new Dictionary<string, bool>());

                    HardConstraintsTable[landuse.Key][lot.Key] = valid;

                    // soft
                    //var cost = SoftConstraints.Aggregate(0.0, (d, constraint) => d + constraint.Value.FeasibleCost(landuse.Value, lot.Value));
                    var cost = SoftConstraints.Sum(constraint => constraint.Value.FeasibleCost(landuse.Value, lot.Value));
                    if (!SoftConstraintsTable.ContainsKey(landuse.Key))
                        SoftConstraintsTable.Add(landuse.Key, new Dictionary<string, double>());

                    SoftConstraintsTable[landuse.Key][lot.Key] = cost;
                }
            }
        }
    }
}
