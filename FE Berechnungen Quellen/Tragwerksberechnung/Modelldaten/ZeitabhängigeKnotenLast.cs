using FEALibrary.Modell.abstrakte_Klassen;
using System;

namespace FE_Berechnungen.Tragwerksberechnung.Modelldaten
{
    public class ZeitabhängigeKnotenLast : AbstraktZeitabhängigeKnotenlast
    {
        public ZeitabhängigeKnotenLast(string nodeId, int nodalDof, bool datei)
        {
            NodeId = nodeId;
            NodalDof = nodalDof;
            Datei = datei;
            VariationType = 0;
        }
        public ZeitabhängigeKnotenLast(string nodeId, int nodalDof, double[] interval)
        {
            NodeId = nodeId;
            NodalDof = nodalDof;
            Interval = interval;
            VariationType = 1;
        }
        public ZeitabhängigeKnotenLast(string nodeId, int nodalDof,
                                double amplitude, double frequency, double phaseAngle)
        {
            NodeId = nodeId;
            NodalDof = nodalDof;
            Amplitude = amplitude;
            Frequency = frequency;
            PhaseAngle = phaseAngle;
            VariationType = 2;
        }

        public override double[] ComputeLoadVector()
        {
            throw new NotImplementedException();
        }
    }
}
