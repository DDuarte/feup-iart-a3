using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IART_A3.StateRepresentation
{
    public class LanduseAllocations : IEquatable<LanduseAllocations>
    {
        private static int _curId;

        private readonly int _id;
        public int Id { get { return _id; } } //TODO think of a better way to implement CompareTo so that 0 is never returned if Equal is false
        private readonly HashSet<Tuple<string, string>> _allocations; // [landuse, lot]
        private readonly HashSet<string> _unattributedLanduses;
        private readonly HashSet<string> _unattributedLots;

        public LanduseAllocations()
        {
            _id = _curId++;
            _allocations = new HashSet<Tuple<string, string>>();
            _unattributedLanduses = new HashSet<string>();
            _unattributedLots = new HashSet<string>();
        }

        public LanduseAllocations(HashSet<string> unattributedLanduses, HashSet<string> unattributedLots)
        {
            _id = _curId++;
            _allocations = new HashSet<Tuple<string, string>>();
            _unattributedLanduses = unattributedLanduses;
            _unattributedLots = unattributedLots;
        }

        public LanduseAllocations(HashSet<Tuple<string, string>> allocations, HashSet<string> unattributedLanduses, HashSet<string> unattributedLots)
        {
            _id = _curId++;
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
            lo.Remove(lot);

            return new LanduseAllocations(al, lu, lo);
        }

        public IReadOnlyList<Tuple<string, string>> GetAllocations()
        {
            return _allocations.ToList();
        }

        public bool Equals(LanduseAllocations la)
        {
            return _allocations.SetEquals(la._allocations) &&
                _unattributedLanduses.SetEquals(la._unattributedLanduses) &&
                _unattributedLots.SetEquals(la._unattributedLots);
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

        public double CurrentCost(IReadOnlyDictionary<string, Lot> lots) // "the g(n) function is the cost of the partial solution"
        {
            return _allocations.Sum(allocation => lots[allocation.Item2].Cost); //TODO elaborate this further with weak constraints raising cost
        }

        public double HeuristicCost(IReadOnlyDictionary<string, Lot> lots) //TODO Improve/Optimize heuristic
        {
            // let p be the number of land uses yet to be assigned
            var p = _unattributedLanduses.Count;

            // h(n) is the sum of the costs of the first p elements in the list of free lots
            return lots.Values.OrderBy(s => s.Cost).Take(p).Select(s => s.Cost).Sum();
        }

        public int Count
        {
            get { return _allocations.Count; }
        }


        public List<LanduseAllocations> GetSuccessors(Dictionary<string, Dictionary<string, bool>> constraintsTable)
        {
            var successors = new List<LanduseAllocations>();

            if (!_unattributedLanduses.Any() || !_unattributedLots.Any())
                return successors;

            foreach (var landuse in _unattributedLanduses)
                successors.AddRange(from lot in _unattributedLots where constraintsTable[landuse][lot] select Allocate(landuse, lot));

            return successors;
        }

        public bool IsFinalState()
        {
            return !_unattributedLanduses.Any(); //_unattributedLanduses.Count() == 0;
        }

        public int LandUsesLeft()
        {
            return _unattributedLanduses.Count();
        }

    }
}
