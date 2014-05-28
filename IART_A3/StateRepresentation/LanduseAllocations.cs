using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IART_A3.StateRepresentation
{
    public class LanduseAllocations : IEquatable<LanduseAllocations>
    {
        private readonly HashSet<Tuple<string, string>> _allocations; // [landuse, lot]
        private readonly HashSet<string> _unattributedLanduses;
        private readonly HashSet<string> _unattributedLots;
        private readonly Problem _problem;
        private string _string;
        private readonly int _id;
        private static int _lastId;

        // "the g(n) function is the cost of the partial solution"
        public double CurrentCost { get; private set; }

        public double HeuristicCost { get; private set; }

        public bool IsFinalState { get { return LandUsesLeft == 0; } }

        public int LandUsesLeft { get { return _unattributedLanduses.Count; } }

        public LanduseAllocations(Problem problem)
        {
            _id = _lastId++;
            _string = null;
            _problem = problem;
            _allocations = new HashSet<Tuple<string, string>>();
            _unattributedLanduses = new HashSet<string>(problem.Landuses.Keys);
            _unattributedLots = new HashSet<string>(problem.Lots.Keys);

            CurrentCost = 0;
            HeuristicCost = CalculateHeuristicCost();
        }

        private LanduseAllocations(LanduseAllocations landuseAllocations, string landuse, string lot)
        {
            _id = _lastId++;
            _problem = landuseAllocations._problem;

            _allocations = new HashSet<Tuple<string, string>>(landuseAllocations._allocations);
            _unattributedLanduses = new HashSet<string>(landuseAllocations._unattributedLanduses);
            _unattributedLots = new HashSet<string>(landuseAllocations._unattributedLots);

            _allocations.Add(Tuple.Create(landuse, lot));
            _unattributedLanduses.Remove(landuse);
            _unattributedLots.Remove(lot);

            CurrentCost = landuseAllocations.CurrentCost +
                _problem.Lots[lot].Price +
                _problem.SoftConstraintsTable[landuse][lot];
            HeuristicCost = CalculateHeuristicCost();
        }

        public bool Equals(LanduseAllocations la)
        {
            return _allocations.SetEquals(la._allocations) &&
                _unattributedLanduses.SetEquals(la._unattributedLanduses) &&
                _unattributedLots.SetEquals(la._unattributedLots);
        }

        public override string ToString()
        {
            if (_string != null) return _string;
            var alls = _allocations.ToList();
            var b = new StringBuilder("{");
            for (var i = 0; i < alls.Count; i++)
            {
                b.AppendFormat("{0}-{1}", alls[i].Item1, alls[i].Item2);
                if (i != _allocations.Count - 1)
                    b.Append(", ");
            }
            _string = b.Append('}').ToString();
            return _string;
        }

        private double CalculateHeuristicCost()
        {
            // let p be the number of land uses yet to be assigned
            var p = _unattributedLanduses.Count;

            // h(n) is the sum of the costs of the first p elements in the list of free lots
            var costs = _unattributedLots.Where(lot => _problem.HardConstraintsTable.Any(s => s.Value[lot]))
                .Select(lot => _problem.Lots[lot].Price).ToList();


            var heurCost = costs.Count >= p ? costs.OrderBy(s => s).Take(p).Sum() : double.MaxValue;

            if (heurCost >= double.MaxValue)
                return heurCost;

            foreach (var lu in _unattributedLanduses)
            {
                var minCost = Double.MaxValue;
                foreach (var lot in _unattributedLots)
                {
                    if (_problem.HardConstraintsTable[lu][lot] && _problem.SoftConstraintsTable[lu][lot] < minCost)
                        minCost = _problem.SoftConstraintsTable[lu][lot];
                }
                heurCost += minCost;
            }


            return heurCost;
        }

        public IEnumerable<LanduseAllocations> GetSuccessors()
        {
            var successors = new List<LanduseAllocations>();

            if (_unattributedLanduses.Count == 0 || _unattributedLots.Count == 0)
                return successors;

            foreach (var landuse in _unattributedLanduses)
                successors.AddRange(
                    _unattributedLots.Where(lot => _problem.HardConstraintsTable[landuse][lot])
                        .Select(lot => new LanduseAllocations(this, landuse, lot)));

            return successors;
        }

        public class Comparer : IComparer<LanduseAllocations>
        {
            public bool UseCurrentCost { get; set; }
            public bool UseHeuristicCost { get; set; }

            public int Compare(LanduseAllocations x, LanduseAllocations y)
            {
                var costX = (UseCurrentCost ? x.CurrentCost : 0) + (UseHeuristicCost ? x.HeuristicCost : 0);
                var costY = (UseCurrentCost ? y.CurrentCost : 0) + (UseHeuristicCost ? y.HeuristicCost : 0);
                var comparison = costX.CompareTo(costY);
                var res = comparison != 0 ? comparison : x.LandUsesLeft.CompareTo(y.LandUsesLeft); // for same estimated cost, choose deepest node
                return res != 0 ? res : (x.Equals(y) ? 0 : x._id.CompareTo(y._id));
            }
        }
    }
}
