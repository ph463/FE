using FEALibrary.Modell;
using FEALibrary.Modell.abstrakte_Klassen;
using System;
using System.Collections.Generic;

namespace FE_Berechnungen.Tragwerksberechnung.Modelldaten
{
    public class Lager : AbstraktRandbedingung
    {
        protected bool timeDependent = false;
        protected double[] deflection;
        public static readonly int X_FIXED = 1, Y_FIXED = 2, R_FIXED = 4,
                                   XY_FIXED = 3, XR_FIXED = 5, YR_FIXED = 6,
                                   XYR_FIXED = 7;

        public Lager(string nodeId, int conditions, IReadOnlyList<double> pre, FEModell modell)
        {
            if (modell.Knoten.TryGetValue(nodeId, out _))
            {
            }
            else
            {
                Console.WriteLine(@"Lagerknoten nicht definiert"); return;
            }
            //Reactions = new double[pre.Count];
            Prescribed = new double[pre.Count];
            Restrained = new bool[pre.Count];
            for (var i = 0; i < pre.Count; i++) Restrained[i] = false;
            NodeId = nodeId;
            Type = conditions;

            if (conditions == X_FIXED) { Prescribed[0] = pre[0]; Restrained[0] = true; }
            if (conditions == Y_FIXED) { Prescribed[1] = pre[1]; Restrained[1] = true; }
            if (conditions == R_FIXED) { Prescribed[2] = pre[2]; Restrained[2] = true; }
            if (conditions == XY_FIXED)
            {
                Prescribed[0] = pre[0]; Restrained[0] = true;
                Prescribed[1] = pre[1]; Restrained[1] = true;
            }
            if ((conditions) == XR_FIXED)
            {
                Prescribed[0] = pre[0]; Restrained[0] = true;
                Prescribed[2] = pre[2]; Restrained[2] = true;
            }
            if ((conditions) == YR_FIXED)
            {
                Prescribed[1] = pre[1]; Restrained[1] = true;
                Prescribed[2] = pre[2]; Restrained[2] = true;
            }
            if ((conditions) == XYR_FIXED)
            {
                Prescribed[0] = pre[0]; Restrained[0] = true;
                Prescribed[1] = pre[1]; Restrained[1] = true;
                Prescribed[2] = pre[2]; Restrained[2] = true;
            }
        }

        public bool XFixed() { return ((X_FIXED & Type) == X_FIXED); }
        public bool YFixed() { return ((Y_FIXED & Type) == Y_FIXED); }
        public bool RotationFixed() { return ((R_FIXED & Type) == R_FIXED); }

        // ... get() ..............................................................
        public bool GetTimeDependent() { return timeDependent; }
        public double[] GetTimeVariation() { return deflection; }
    }
}
