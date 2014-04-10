using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IART_A3
{
    class SearchUtilities
    {
        /// <summary>
        /// All successors for a given current state and constraints table. Does not need to check for loops since the
        /// search domain is a tree
        /// </summary>
        /// <typeparam name="T">State representation</typeparam>
        /// <param name="currentState">Current state</param>
        /// <param name="constraintsTable">Table containing information on whether the lots satisfy the strong constraints for a landuse</param>
        /// <returns></returns>
        List<T> GetSuccessors<T>(T currentState, IReadOnlyDictionary<Tuple<string, string>, bool> constraintsTable)
        {
            var successors = new List<T>();
            foreach (var landuse in T.UnattributedLanduses)
            {
                foreach (var lot in T.FreeLot)
                {
                    if (constraintsTable[Tuple.Create(landuse,lot)])
                        successors.Add(T.add(landuse, lot)); //add updates UnattributedLanduses and FreeLots for new state
                }
            }
            return successors;
        } 


    }
}
