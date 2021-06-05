using FEALibrary.Modell.abstrakte_Klassen;
using System;

namespace FE_Berechnungen.Wärmeberechnung.Modelldaten
{
    public class ZeitabhängigeKnotenLast : AbstraktZeitabhängigeKnotenlast
    {
        public ZeitabhängigeKnotenLast(String nodeId, bool datei)
        {
            NodeId = nodeId;
            Datei = datei;
            VariationType = 0;      // lies Lastvektor aus Datei
        }
        public ZeitabhängigeKnotenLast(String nodeId, double[] interval)
        {
            NodeId = nodeId;
            Interval = interval;
            VariationType = 1;      // lies Zeit-/Wert-Intervalle der Anregung mit linearer Interpolation
        }
        public ZeitabhängigeKnotenLast(string nodeId,
                                double amplitude, double frequency, double phaseAngle)
        {
            NodeId = nodeId;
            Amplitude = amplitude;
            Frequency = frequency;
            PhaseAngle = phaseAngle;
            VariationType = 2;      // lies harmonische Anregung
        }
        public ZeitabhängigeKnotenLast(string nodeId, double konstanteTemperatur)
        {
            NodeId = nodeId;
            KonstanteTemperatur = konstanteTemperatur;
            VariationType = 3;      // lies konstanten Temperaturwert
        }
    }
}
