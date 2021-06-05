using FEALibrary.Modell.abstrakte_Klassen;

namespace FE_Berechnungen.Tragwerksberechnung.Modelldaten
{
    public class KnotenLast : AbstraktKnotenlast
    {
        // ... Constructor ........................................................
        public KnotenLast(string nodeId, double px, double py, double moment)
        {
            NodeId = nodeId;
            Intensity = new double[3];
            Intensity[0] = px;
            Intensity[1] = py;
            Intensity[2] = moment;
        }

        public KnotenLast(string nodeId, double px, double py)
        {
            NodeId = nodeId;
            Intensity = new double[2];
            Intensity[0] = px;
            Intensity[1] = py;
        }
        public override double[] ComputeLoadVector()
        {
            return Intensity;
        }
    }
}
