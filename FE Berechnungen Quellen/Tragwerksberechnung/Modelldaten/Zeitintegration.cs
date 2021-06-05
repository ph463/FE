using FEALibrary.Modell.abstrakte_Klassen;
using System.Collections.Generic;

namespace FE_Berechnungen.Tragwerksberechnung.Modelldaten
{
    public class Zeitintegration : AbstraktZeitintegration
    {
        public Zeitintegration(double tmax, double dt, int method, double parameter1)
        {
            Tmax = tmax;
            Dt = dt;
            Method = method;
            Parameter1 = parameter1;
            Anfangsbedingungen = new List<object>();
            DämpfungsRaten = new List<object>();
        }
        public Zeitintegration(double tmax, double dt, int method, double parameter1, double parameter2)
        {
            Tmax = tmax;
            Dt = dt;
            Method = method;
            Parameter1 = parameter1;
            Parameter2 = parameter2;
            Anfangsbedingungen = new List<object>();
            DämpfungsRaten = new List<object>();
        }
    }
}
