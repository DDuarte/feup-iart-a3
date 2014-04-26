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
        private readonly HashSet<Tuple<string, string>> _allocations; // [landuse, lot]
        private readonly HashSet<string> _unattributedLanduses;
        private readonly HashSet<string> _unattributedLots;
        private readonly double _currentCost;
        private readonly double _heuristicCost;
        private readonly Problem _problem;

        public int Id { get { return _id; } }

        // "the g(n) function is the cost of the partial solution"
        public double CurrentCost { get { return _currentCost; } }

        public double HeuristicCost { get { return _heuristicCost; } }

        public int Count { get { return _allocations.Count; } }

        public bool IsFinalState { get { return LandUsesLeft == 0; } }

        public int LandUsesLeft { get { return _unattributedLanduses.Count; } }

        public LanduseAllocations(Problem problem)
        {
            _id = _curId++;
            _problem = problem;
            _allocations = new HashSet<Tuple<string, string>>();
            _unattributedLanduses = new HashSet<string>(problem.Landuses.Keys);
            _unattributedLots = new HashSet<string>(problem.Lots.Keys);

            _currentCost = 0;
            _heuristicCost = CalculateHeuristicCost();
        }

        private LanduseAllocations(LanduseAllocations landuseAllocations, string landuse, string lot)
        {
            _id = _curId++;
            _problem = landuseAllocations._problem;

            var al = new HashSet<Tuple<string, string>>(landuseAllocations._allocations);
            var lu = new HashSet<string>(landuseAllocations._unattributedLanduses);
            var lo = new HashSet<string>(landuseAllocations._unattributedLots);

            al.Add(Tuple.Create(landuse, lot));
            lu.Remove(landuse);
            lo.Remove(lot);

            _allocations = al;
            _unattributedLanduses = lu;
            _unattributedLots = lo;

            _currentCost = landuseAllocations.CurrentCost +
                _problem.Lots[lot].Price +
                _problem.SoftConstraintsTable[landuse][lot];
            _heuristicCost = CalculateHeuristicCost();
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

        private double CalculateHeuristicCost() // TODO Improve/Optimize heuristic and add softconstraints costs
        {
            // let p be the number of land uses yet to be assigned
            var p = _unattributedLanduses.Count;

            // h(n) is the sum of the costs of the first p elements in the list of free lots
            var costs = _unattributedLots.Where(lot => _problem.HardConstraintsTable.Any(s => s.Value[lot]))
                .Select(lot => _problem.Lots[lot].Price).ToList();

            return costs.Count >= p ? costs.OrderBy(s => s).Take(p).Sum() : double.MaxValue;
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
    }
}
