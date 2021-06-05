﻿using FEALibrary.Modell.abstrakte_Klassen;

namespace FE_Berechnungen.Wärmeberechnung.Modelldaten
{
    public class ZeitabhängigeElementLast : AbstraktZeitabhängigeElementLast
    {
        public ZeitabhängigeElementLast(string elementId, double[] p)
        {
            ElementId = elementId;
            P = p;
            VariationType = 0;
        }
        public override double[] ComputeLoadVector()
        {
            var element = (AbstraktLinear2D3)Element;
            element.ComputeGeometry();
            var area = 0.5 * Element.Determinant;
            const double gaussWeight = 1.0 / 3.0;
            const double gc0 = 2.0 / 3.0;
            const double gc12 = 1.0 / 6.0;
            var vector = new double[3];
            var qp0 = (gc0 * P[0] + gc12 * P[1] + gc12 * P[2]);
            var qp1 = (gc12 * P[0] + gc0 * P[1] + gc12 * P[2]);
            var qp2 = (gc12 * P[0] + gc12 * P[1] + gc0 * P[2]);
            vector[0] = (gc0 * qp0 + gc12 * qp1 + gc12 * qp2) * gaussWeight * area;
            vector[1] = (gc12 * qp0 + gc0 * qp1 + gc12 * qp2) * gaussWeight * area;
            vector[2] = (gc12 * qp0 + gc12 * qp1 + gc0 * qp2) * gaussWeight * area;
            return vector;
        }
    }
}
