using System;
using System.Collections.Generic;
using System.Linq;

namespace IART_A3
{
    public static class Program
    {
        static void Main()
        {
            var lots = new Dictionary<string, Lot>
            {
                { "lot3",  new Lot {Cost = 1.2, DistanceLake = 10,  DistanceHighway = 0.1, PoorSoil = false, Steep = SteepType.Flat}},
                { "lot5",  new Lot {Cost = 1.3, DistanceLake = 10,  DistanceHighway = 10,  PoorSoil = false, Steep = SteepType.Flat}},
                { "lot7",  new Lot {Cost = 0.9, DistanceLake = 0.1, DistanceHighway = 0.1, PoorSoil = false, Steep = SteepType.Flat}},
                { "lot9",  new Lot {Cost = 1.6, DistanceLake = 10,  DistanceHighway = 10,  PoorSoil = false, Steep = SteepType.Flat}},
                { "lot10", new Lot {Cost = 1.7, DistanceLake = 0.1, DistanceHighway = 10,  PoorSoil = true,  Steep = SteepType.ModeratelySteep}},
                { "lot11", new Lot {Cost = 1.0, DistanceLake = 10,  DistanceHighway = 10,  PoorSoil = false, Steep = SteepType.Steep}},
                { "lot12", new Lot {Cost = 1.4, DistanceLake = 0.1, DistanceHighway = 10,  PoorSoil = true,  Steep = SteepType.ModeratelySteep}},
                { "lot17", new Lot {Cost = 0.8, DistanceLake = 10,  DistanceHighway = 10,  PoorSoil = false, Steep = SteepType.VerySteep}}
            };

            var landuses = new Dictionary<string, Landuse>
            {
                {"R", new Landuse {Type = LanduseType.Recreational}},
                {"A", new Landuse {Type = LanduseType.Apartments}},
                {"H", new Landuse {Type = LanduseType.HousingComplex}},
                {"C", new Landuse {Type = LanduseType.Cemetery}},
                {"D", new Landuse {Type = LanduseType.Dump}}
            };

            var contraints = new Dictionary<string, Constraint>
            {
                {"C1", new DistanceConstraint(new [] {LanduseType.Recreational}, Place.Lake, DistanceConstraint.CloserThan)},
                {"C2", new SteepConstraint(new[] {LanduseType.Apartments, LanduseType.HousingComplex, LanduseType.Cemetery, LanduseType.Dump}, new[] {SteepType.Flat, SteepType.ModeratelySteep})},
                {"C3", new SoilConstraint(new[] {LanduseType.Apartments, LanduseType.HousingComplex}, false)},
                {"C4", new DistanceConstraint(new[] {LanduseType.Apartments, LanduseType.HousingComplex, LanduseType.Recreational}, Place.Highway, DistanceConstraint.FartherThan)}
            };

            var constraintsTable = new Dictionary<Tuple<string, string>, bool>();

            foreach (var landuse in landuses)
            {
                foreach (var lot in lots)
                {
                    var valid = contraints.All(constraint => constraint.Value.Feasible(landuse.Value, lot.Value));
                    constraintsTable.Add(Tuple.Create(landuse.Key, lot.Key), valid);
                }
            }

            Console.WriteLine("Contraints table:");
            foreach (var b in constraintsTable)
            {
                Console.WriteLine("\t{0} - {1} = {2}", b.Key.Item1, b.Key.Item2, b.Value ? "OK" : "NOTOK");
            }

            Console.ReadKey();
        }
    }
}
