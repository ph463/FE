using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FE_Bibliothek.Modell
{
    public class Knotenwerte
    {
        public string NodeId { get; set; }
        public double[] Values { get; set; }

        public Knotenwerte(string nodeId, double[] values)
        {
            NodeId = nodeId;
            Values = values;
        }
    }
}
