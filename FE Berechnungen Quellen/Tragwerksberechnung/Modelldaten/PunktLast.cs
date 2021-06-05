using FEALibrary.Modell.abstrakte_Klassen;

namespace FE_Berechnungen.Tragwerksberechnung.Modelldaten
{
    public class PunktLast : AbstraktElementLast
    {
        public double Offset { get; set; }

        // constructor for point load .....
        public PunktLast(string elementId, double fx, double fy, double o)
        {
            ElementId = elementId;
            Intensity = new double[2];
            Intensity[0] = fx;
            Intensity[1] = fy;
            Offset = o;
        }

        // --- get global load vector ---------------------------------------------
        public override double[] ComputeLoadVector()
        {
            var balken = (Biegebalken)Element;
            return balken.ComputeLoadVector(this, false);
        }

        // ... get load vector ....................................................
        public double[] ComputeLocalLoadVector()
        {
            var balken = (Biegebalken)Element;
            return balken.ComputeLoadVector(this, true);
        }
    }
}
