namespace FE_Bibliothek.Modell.abstrakte_Klassen
{
    public abstract class AbstraktZeitabhängigeRandbedingung : AbstraktRandbedingung
    {
        public bool Datei { get; set; }
        public int VariationType { get; set; }
        public double KonstanteTemperatur { get; set; }
        public double Amplitude { get; set; }
        public double Frequency { get; set; }
        public double PhaseAngle { get; set; }
        public double[] Interval { get; set; }
    }
}
