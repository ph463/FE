using FEALibrary.Modell;
using FEALibrary.Modell.abstrakte_Klassen;

namespace FE_Berechnungen.Tragwerksberechnung.Modelldaten
{
    public class LinienLast : AbstraktLinienlast
    {
        // ... Constructors ........................................................
        public LinienLast(string elementId, double p1X, double p2X, double p1Y, double p2Y)
        {
            ElementId = elementId;
            Intensity = new double[4];                           // 2 nodes, 2 dimensions
            Intensity[0] = p1X; Intensity[1] = p2X; Intensity[2] = p1Y; Intensity[3] = p2Y;
        }
        public LinienLast(string elementId, double p1X, double p2X, double p1Y, double p2Y, bool inElementCoordinateSystem)
        {
            ElementId = elementId;
            Intensity = new double[4];                           // 2 nodes, 2 dimensions
            Intensity[0] = p1X; Intensity[1] = p2X; Intensity[2] = p1Y; Intensity[3] = p2Y;
            InElementCoordinateSystem = inElementCoordinateSystem;
        }

        public override double[] ComputeLoadVector()
        {
            var balken = (Biegebalken)Element;
            return balken.ComputeLoadVector(this, false);
        }

        public double[] ComputeLocalLoadVector()
        {
            var balken = (Biegebalken)Element;
            return balken.ComputeLoadVector(this, true);
        }

        // useful for GAUSS integration
        public double GetXIntensity(double z)
        {
            if (z < 0 || z > 1) throw new ModellAusnahme("LinienLast auf element:" + ElementId + "ausserhalb Koordinaten 0 <= z <= 1");
            return Intensity[0] * (1 - z) + Intensity[2] * z;
        }
        public double GetYIntensity(double z)
        {
            if (z < 0 || z > 1) throw new ModellAusnahme("LinienLast auf element:" + ElementId + "ausserhalb Koordinaten 0 <= z <= 1");
            return Intensity[1] * (1 - z) + Intensity[3] * z;
        }
    }
}
