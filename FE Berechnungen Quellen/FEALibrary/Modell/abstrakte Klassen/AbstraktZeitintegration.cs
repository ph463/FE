using System.Collections.Generic;

namespace FEALibrary.Modell.abstrakte_Klassen
{
    public abstract class AbstraktZeitintegration
    {
        public double Tmax { get; set; }
        public double Dt { get; set; }
        public int Method { get; set; }
        public bool FromStationary { get; set; }
        public double Parameter1 { get; set; }
        public double Parameter2 { get; set; }
        public List<object> Anfangsbedingungen { get; set; }
        public List<object> DämpfungsRaten { get; set; }
    }
}
