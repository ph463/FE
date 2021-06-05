using FEALibrary.Modell.abstrakte_Klassen;
using System;
using System.Runtime.Serialization;

namespace FE_Berechnungen.Elastizitätsberechnung.Modelldaten
{
    public class LinienLast : AbstraktLinienlast
    {
        private int startNDOF, endNDOF;           // number of degrees of freedom for start and end node
        public int StartNDOF { get { return startNDOF; } set { startNDOF = value; } }
        public int EndNDOF { get { return endNDOF; } set { endNDOF = value; } }

        // ... Constructors ........................................................
        public LinienLast(string _startNodeId, double p1x, double p1y, string _endNodeId, double p2x, double p2y)
        {
            StartNodeId = _startNodeId;
            EndNodeId = _endNodeId;
            Intensity = new double[4];                           // 2 nodes, 2 dimensions
            Intensity[0] = p1x; Intensity[1] = p2x; Intensity[2] = p1y; Intensity[3] = p2y;
        }

        public override double[] ComputeLoadVector()
        {
            double[] load = new double[4];
            double c1, c2, l;
            double[] nStart = StartNode.Coordinates;
            double[] nEnd = EndNode.Coordinates;
            c1 = nEnd[0] - nStart[0];
            c2 = nEnd[1] - nStart[1];
            l = Math.Sqrt(c1 * c1 + c2 * c2) / 6.0;
            load[0] = l * (2.0 * Intensity[0] + Intensity[2]);
            load[2] = l * (2.0 * Intensity[2] + Intensity[0]);
            load[1] = l * (2.0 * Intensity[1] + Intensity[3]);
            load[3] = l * (2.0 * Intensity[3] + Intensity[1]);
            return load;
        }

        [Serializable]
        private class RuntimeException : Exception
        {
            public RuntimeException() { }
            public RuntimeException(string message) : base(message) { }
            public RuntimeException(string message, Exception innerException) : base(message, innerException) { }
            protected RuntimeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        }
    }
}
