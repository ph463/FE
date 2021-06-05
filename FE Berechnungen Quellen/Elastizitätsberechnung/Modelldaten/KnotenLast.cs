using FEALibrary.Modell.abstrakte_Klassen;
using System;

namespace FE_Berechnungen.Elastizitätsberechnung.Modelldaten
{
    public class KnotenLast : AbstraktLast
    {
        // ... Constructor ........................................................
        public KnotenLast(String nodeId, double px, double py)
        {
            NodeId = nodeId;
            Intensity = new double[2];
            Intensity[0] = px;
            Intensity[1] = py;
        }
        public KnotenLast(String nodeId, double px, double py, double pz)
        {
            NodeId = nodeId;
            Intensity = new double[3];
            Intensity[0] = px;
            Intensity[1] = py;
            Intensity[2] = pz;
        }
        public override double[] ComputeLoadVector()
        {
            return Intensity;
        }
    }
}
