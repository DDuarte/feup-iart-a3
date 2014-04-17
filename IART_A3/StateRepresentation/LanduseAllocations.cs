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
        public int Id { get { return _id; } }
        private readonly HashSet<Tuple<string, string>> _allocations; // [landuse, lot]
        private readonly HashSet<string> _unattributedLanduses;
        private readonly HashSet<string> _unattributedLots;
        private readonly double _currentCost;
        private readonly double _heuristicCost;
        private readonly IReadOnlyDictionary<string, Lot> _lots;

        public LanduseAllocations(IReadOnlyDictionary<string, Lot> lots, Dictionary<string, Dictionary<string, bool>> constraintsTable)
        {
            
            _id = _curId++;
            _allocations = new HashSet<Tuple<string, string>>();
            _unattributedLanduses = new HashSet<string>();
            _unattributedLots = new HashSet<string>();
            _lots = lots;
            _currentCost = 0;
            _heuristicCost = CalculateHeuristicCost(lots, constraintsTable);
        }

        public LanduseAllocations(HashSet<string> landuses, IReadOnlyDictionary<string, Lot> lots, Dictionary<string, Dictionary<string, bool>> constraintsTable)
        {
            
            _id = _curId++;
            _allocations = new HashSet<Tuple<string, string>>();
            _unattributedLanduses = landuses;
            _unattributedLots = new HashSet<string>(lots.Keys);
            _lots = lots;
            _currentCost = 0;
            _heuristicCost = CalculateHeuristicCost(lots, constraintsTable);
        }

        public LanduseAllocations(double newCost, HashSet<Tuple<string, string>> allocations, HashSet<string> unattributedLanduses, HashSet<string> freeLots, IReadOnlyDictionary<string, Lot> lots, Dictionary<string, Dictionary<string, bool>> constraintsTable)
        {
            
            _id = _curId++;
            _allocations = allocations;
            _unattributedLanduses = unattributedLanduses;
            _unattributedLots = freeLots;
            _lots = lots;
            _currentCost = newCost;
            _heuristicCost = CalculateHeuristicCost(lots, constraintsTable);
        }

        public LanduseAllocations Allocate(string landuse, string lot, Dictionary<string, Dictionary<string, bool>> constraintsTable)
        {
            var al = new HashSet<Tuple<string, string>>(_allocations);
            var lu = new HashSet<string>(_unattributedLanduses);
            var lo = new HashSet<string>(_unattributedLots);

            al.Add(Tuple.Create(landuse, lot));
            lu.Remove(landuse);
            lo.Remove(lot);

            return new LanduseAllocations(CurrentCost()+_lots[lot].Price,al, lu, lo, _lots, constraintsTable);
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

        private double CalculateHeuristicCost(IReadOnlyDictionary<string, Lot> lots, Dictionary<string, Dictionary<string, bool>> constraintsTable) // TODO Improve/Optimize heuristic
        {
            // let p be the number of land uses yet to be assigned
            var p = _unattributedLanduses.Count;

            // h(n) is the sum of the costs of the first p elements in the list of free lots
            var costs = new SortedSet<double>();
            foreach (var lot in _unattributedLots)
            {
                costs.Add(constraintsTable.Any(s => s.Value[lot]) ? lots[lot].Price : 9999999);
            }

            return costs.Take(p).Sum();
        }

        public double CurrentCost() // "the g(n) function is the cost of the partial solution"
        {
            return _currentCost; // TODO elaborate this further with weak constraints raising cost
        }

        public double HeuristicCost()
        {
            return _heuristicCost;
        }

        public int Count
        {
            get { return _allocations.Count; }
        }

        public IEnumerable<LanduseAllocations> GetSuccessors(Dictionary<string, Dictionary<string, bool>> constraintsTable)
        {
            var successors = new List<LanduseAllocations>();

            if (_unattributedLanduses.Count == 0 || _unattributedLots.Count == 0)
                return successors;

            foreach (var landuse in _unattributedLanduses)
                successors.AddRange(from lot in _unattributedLots where constraintsTable[landuse][lot] select Allocate(landuse, lot, constraintsTable));

            return successors;
        }

        public bool IsFinalState()
        {
            return _unattributedLanduses.Count == 0;
        }

        public int LandUsesLeft()
        {
            return _unattributedLanduses.Count;
        }

    }
}
