using System.Collections.Generic;
using System.Linq;
using IART_A3.StateRepresentation;

namespace IART_A3.Search
{
    public static class SearchUtilities
    {
        public static Dictionary<string, Dictionary<string, bool>> CreateConstraintsTable(IEnumerable<KeyValuePair<string, Landuse>> landuses, IReadOnlyDictionary<string, Lot> lots, IReadOnlyDictionary<string, Constraint> constraints)
        {
            var constraintsTable = new Dictionary<string, Dictionary<string, bool>>(); // landuse, -> lot -> yes/no

            foreach (var landuse in landuses)
            {
                foreach (var lot in lots)
                {
                    var valid = constraints.All(constraint => constraint.Value.Feasible(landuse.Value, lot.Value));
                    if (!constraintsTable.ContainsKey(landuse.Key))
                        constraintsTable.Add(landuse.Key, new Dictionary<string, bool>());

                    constraintsTable[landuse.Key].Add(lot.Key, valid);
                }
            }
            return constraintsTable;
        }
    }
}