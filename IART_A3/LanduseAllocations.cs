using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IART_A3
{
    public class LanduseAllocations
    {
        private readonly List<Tuple<string, string>> _allocations; // [landuse, lot]

        public LanduseAllocations()
        {
            _allocations = new List<Tuple<string, string>>();
        }

        public LanduseAllocations(List<Tuple<string, string>> allocations)
        {
            _allocations = allocations; // lol.
        }

        public LanduseAllocations Allocate(string landuse, string lot)
        {
            var al = _allocations.ConvertAll(input => input);
            al.Add(Tuple.Create(landuse, lot));
            return new LanduseAllocations(al);
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
            return _allocations.Sum(allocation => lots[allocation.Item2].Cost);
        }

        public int Count
        {
            get { return _allocations.Count; }
        }
    }
}
