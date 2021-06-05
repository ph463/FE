using FEALibrary.Modell.abstrakte_Klassen;
using System;
using System.Collections.Generic;

namespace FE_Berechnungen.Wärmeberechnung.Modelldaten
{
    public class ElementLast4 : AbstraktElementLast
    {
        private int[] systemIndices;
        // ....Constructors...................................................
        public ElementLast4(string elementId, IReadOnlyList<double> p)
        {
            ElementId = elementId;
            Intensity = new double[4];
            Intensity[0] = p[0]; Intensity[1] = p[1]; Intensity[2] = p[2]; Intensity[3] = p[3];
        }
        public ElementLast4(string id, string elementId, IReadOnlyList<double> p)
        {
            LoadId = id;
            ElementId = elementId;
            Intensity = new double[4];
            Intensity[0] = p[0]; Intensity[1] = p[1]; Intensity[2] = p[2]; Intensity[3] = p[3];
        }
        // ....Compute the element load vector.................................
        public override double[] ComputeLoadVector()
        {
            var element4 = (Element2D4)Element;
            int row, col;
            var ssT = new double[4, 4];
            _ = new double[4];
            double[] gaussCoord = { -1 / Math.Sqrt(3), 1 / Math.Sqrt(3) };
            var vector = new double[4];

            foreach (var coor1 in gaussCoord)
            {
                foreach (var coor2 in gaussCoord)
                {
                    var z0 = coor1;
                    var z1 = coor2;
                    element4.ComputeGeometry(z0, z1);
                    var s = AbstraktLinear2D4.ComputeS(z0, z1);

                    for (row = 0; row < 4; row++)
                        for (col = 0; col < 4; col++)
                            ssT[col, row] = ssT[col, row] + element4.Determinant * s[row] * s[col];
                }
            }
            for (row = 0; row < 4; row++)
            {
                vector[row] = 0;
                for (col = 0; col < 4; col++)
                    vector[row] = vector[row] + ssT[row, col] * Intensity[row];
            }
            return vector;
        }
        // ....Compute the element system indices .................................
        public int[] ComputeSystemIndices()
        {
            systemIndices = Element.SystemIndicesOfElement;
            return systemIndices;
        }
    }
}
