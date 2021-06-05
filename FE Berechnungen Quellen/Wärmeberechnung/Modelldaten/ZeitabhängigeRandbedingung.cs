using FEALibrary.Modell.abstrakte_Klassen;

namespace FE_Berechnungen.Wärmeberechnung.Modelldaten
{
    public class ZeitabhängigeRandbedingung : AbstraktZeitabhängigeRandbedingung
    {
        public ZeitabhängigeRandbedingung(string nodeId, bool datei)
        {
            NodeId = nodeId;
            Restrained = new bool[1];
            Restrained[0] = true;
            Datei = datei;
            VariationType = 0;
        }
        public ZeitabhängigeRandbedingung(string nodeId, double konstanteTemperatur)
        {
            NodeId = nodeId;
            Restrained = new bool[1];
            Restrained[0] = true;
            KonstanteTemperatur = konstanteTemperatur;
            VariationType = 3;
        }
        public ZeitabhängigeRandbedingung(string nodeId, double[] interval)
        {
            NodeId = nodeId;
            Restrained = new bool[1];
            Restrained[0] = true;
            Interval = interval;
            VariationType = 1;
        }
        public ZeitabhängigeRandbedingung(string nodeId,
                                double amplitude, double frequency, double phaseAngle)
        {
            NodeId = nodeId;
            Restrained = new bool[1];
            Restrained[0] = true;
            Amplitude = amplitude;
            Frequency = frequency;
            PhaseAngle = phaseAngle;
            VariationType = 2;
        }
    }
}
