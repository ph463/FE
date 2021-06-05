using System;

namespace FE_Bibliothek.Modell.abstrakte_Klassen
{
    public abstract class AbstraktZeitabhängigeKnotenlast : AbstraktKnotenlast
    {
        public bool Datei { get; set; }
        public bool Bodenanregung { get; set; }
        public int VariationType { get; set; }
        public double KonstanteTemperatur { get; set; }
        public double Amplitude { get; set; }
        public double Frequency { get; set; }
        public double PhaseAngle { get; set; }
        public double[] Interval { get; set; }
        public override double[] ComputeLoadVector()
        {
            throw new NotImplementedException();
        }
    }
}
