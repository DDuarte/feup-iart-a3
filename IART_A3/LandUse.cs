using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IART_A3
{
    public enum LanduseType
    {
        Recreational,
        Apartments,
        HousingComplex,
        Dump,
        Cemetery
    }

    /// <summary>
    /// Land allocation
    /// </summary>
    public class Landuse
    {
        public LanduseType Type { get; set; }
    }
}
