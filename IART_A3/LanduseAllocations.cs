using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IART_A3
{
    public class LanduseAllocations
    {
        private readonly HashSet<Tuple<string, string>> _allocations; // [landuse, lot]
        private readonly HashSet<string> _unattributedLanduses;
        private readonly HashSet<string> _unattributedLots;
        

        public static Dictionary<string, Dictionary<string, bool>> ConstraintsTable { get; set; }

        public LanduseAllocations()
        {
            _allocations = new HashSet<Tuple<string, string>>();
            _unattributedLanduses = new HashSet<string>();
            _unattributedLots = new HashSet<string>();
        }

        public LanduseAllocations(HashSet<Tuple<string, string>> allocations, HashSet<string> unattributedLanduses, HashSet<string> unattributedLots)
        {
            _allocations = allocations;
            _unattributedLanduses = unattributedLanduses;
            _unattributedLots = unattributedLots;
        }

        public LanduseAllocations Allocate(string landuse, string lot)
        {
            var al = new HashSet<Tuple<string, string>>(_allocations);
            var lu = new HashSet<string>(_unattributedLanduses);
            var lo = new HashSet<string>(_unattributedLots);

            al.Add(Tuple.Create(landuse, lot));
            lu.Remove(landuse);
            lu.Remove(lot);

            return new LanduseAllocations(al, lu, lo);
        }

        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
                return false;

            // If parameter cannot be cast to LanduseAllocations return false.
            var la = obj as LanduseAllocations;
            if (la == null)
            {
                return false;
            }

            
            return _allocations.SetEquals(la._allocations) &&
                _unattributedLanduses.SetEquals(la._unattributedLanduses) &&
                _unattributedLots.SetEquals(la._unattributedLots);
        }

        public override int GetHashCode()
        {
            var hash = _allocations.Aggregate(0, (current, alloc) => current*31 + (alloc == null ? 0 : alloc.GetHashCode()));
            
            hash = _unattributedLanduses.Aggregate(hash, (current, la) => current*31 + (la == null ? 0 : la.GetHashCode()));

            return _unattributedLots.Aggregate(hash, (current, lot) => current*31 + (lot == null ? 0 : lot.GetHashCode()));
        }

        public override string ToString()
        {
            var alls = _allocations.ToList();
            var b = new StringBuilder("{");
            for (var i = 0; i < alls.Count; i++)
            {
                b.AppendFormat("{0}-{1}", alls[i].Item1, alls[i].Item2);
                if (i != _allocations.Count - 1)
                    b.Append(", ");
            }
            return b.Append('}').ToString();
        }

        public double Cost(IReadOnlyDictionary<string, Lot> lots) // "the g(n) function is the cost of the partial solution"
        {
            return _allocations.Sum(allocation => lots[allocation.Item2].Cost); //TODO elaborate this further with weak constraints raising cost
        }

        public int Count
        {
            get { return _allocations.Count; }
        }


        public List<LanduseAllocations> GetSuccessors()
        {
            var successors = new List<LanduseAllocations>();

            foreach (var landuse in _unattributedLanduses)
                successors.AddRange(from lot in _unattributedLots where ConstraintsTable[landuse][lot] select Allocate(landuse, lot));
            
            return successors;
        }  

    }
}
