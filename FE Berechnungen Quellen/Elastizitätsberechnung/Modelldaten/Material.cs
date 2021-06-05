using FEALibrary.Modell.abstrakte_Klassen;

namespace FE_Berechnungen.Elastizitätsberechnung.Modelldaten
{
    public class Material : AbstraktMaterial
    {
        public Material(double _emodulus, double _poisson)
        {
            MaterialWerte = new double[2];
            MaterialWerte[0] = _emodulus;
            MaterialWerte[1] = _poisson;
        }
        public Material(double _emodulus, double _poisson, double _mass)
        {
            MaterialWerte = new double[3];
            MaterialWerte[0] = _emodulus;
            MaterialWerte[1] = _poisson;
            MaterialWerte[2] = _mass;
        }
    }
}
