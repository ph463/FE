using FEALibrary.Modell.abstrakte_Klassen;

namespace FE_Berechnungen.Wärmeberechnung.Modelldaten
{
    public class Randbedingung : AbstraktRandbedingung
    {
        // ....Constructor....................................................
        public Randbedingung(string nodeId, double pre)
        {
            NodeId = nodeId;
            Prescribed = new double[1];
            Prescribed[0] = pre;
            Restrained = new bool[1];
            Restrained[0] = true;
        }
    }
}
