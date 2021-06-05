using FEALibrary.Modell.abstrakte_Klassen;
using System.Collections.Generic;

namespace FE_Berechnungen.Wärmeberechnung.Modelldaten
{
    public class Material : AbstraktMaterial
    {
        public double DichteLeitfähigkeit { get; set; }
        public double[] Leitfähigkeit { get; set; }

        public Material(string id, IReadOnlyList<double> conduct)
        {
            MaterialId = id;
            Leitfähigkeit = new double[conduct.Count];
            for (var i = 0; i < conduct.Count; i++)
            {
                Leitfähigkeit[i] = conduct[i];
            }
        }
        public Material(string id, IReadOnlyList<double> conduct, double rhoC)
        {
            MaterialId = id;
            Leitfähigkeit = new double[conduct.Count];
            for (var i = 0; i < conduct.Count; i++)
            {
                Leitfähigkeit[i] = conduct[i];
            }
            DichteLeitfähigkeit = rhoC;
        }
    }
}
