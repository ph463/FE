﻿using FEALibrary.Modell.abstrakte_Klassen;
using System;

namespace FE_Berechnungen.Wärmeberechnung.Modelldaten
{
    public class LinienLast : AbstraktLinienlast
    {
        public LinienLast(string startNodeId, string endNodeId, double[] p)
        {
            StartNodeId = startNodeId;
            EndNodeId = endNodeId;
            Intensity = new double[2];
            Intensity = p;
        }
        public LinienLast(string id, string startNodeId, string endNodeId, double[] p)
        {
            LoadId = id;
            StartNodeId = startNodeId;
            EndNodeId = endNodeId;
            Intensity = new double[2];
            Intensity = p;
        }
        // ....Compute concentrated node forces in local coordinate system....
        public override double[] ComputeLoadVector()
        {
            //Lastwerte = new double[2];
            var nStart = StartNode.Coordinates;
            var nEnd = EndNode.Coordinates;
            var vector = new double[2];
            var c1 = nEnd[0] - nStart[0];
            var c2 = nEnd[1] - nStart[1];
            var l = Math.Sqrt(c1 * c1 + c2 * c2) / 6.0;
            vector[0] = l * (2.0 * Intensity[0] + Intensity[1]);
            vector[1] = l * (2.0 * Intensity[1] + Intensity[0]);
            return vector;
        }
    }
}
