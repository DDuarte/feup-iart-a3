using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IART_A3
{
    public class LanduseAllocations
    {
        private readonly List<Tuple<string, string>> _allocations; // [landuse, lot]
        private readonly List<string> _unattributedLanduses;
        private readonly List<string> _unattributedLots;
        

        public static Dictionary<string, Dictionary<string, bool>> ConstraintsTable { get; set; }

        public LanduseAllocations()
        {
            _allocations = new List<Tuple<string, string>>();
            _unattributedLanduses = new List<string>();
            _unattributedLots = new List<string>();
        }

        public LanduseAllocations(List<Tuple<string, string>> allocations, List<string> unattributedLanduses, List<string> unattributedLots)
        {
            _allocations = allocations;
            _unattributedLanduses = unattributedLanduses;
            _unattributedLots = unattributedLots;
        }

        public LanduseAllocations Allocate(string landuse, string lot)
        {
            var al = _allocations.ConvertAll(input => input);
            var lu = _unattributedLanduses.Where(input => input != landuse).ToList();
            var lo = _unattributedLots.Where(input => input != lot).ToList();
            al.Add(Tuple.Create(landuse, lot));

            return new LanduseAllocations(al, lu, lo);
        }

        public override string ToString()
        {
            var b = new StringBuilder("{");
            for (var i = 0; i < _allocations.Count; i++)
            {
                b.AppendFormat("{0}-{1}", _allocations[i].Item1, _allocations[i].Item2);
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
