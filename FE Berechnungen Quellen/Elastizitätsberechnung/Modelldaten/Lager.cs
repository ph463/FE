using FEALibrary.Modell;
using FEALibrary.Modell.abstrakte_Klassen;

namespace FE_Berechnungen.Elastizitätsberechnung.Modelldaten
{
    public class Lager : AbstraktRandbedingung
    {
        //private int supportType;
        public string face;
        protected bool timeDependent = false;
        protected double[] deflection;
        public static readonly int X_FIXED = 1, Y_FIXED = 2, Z_FIXED = 4,
                                   XY_FIXED = 3, XZ_FIXED = 5, YZ_FIXED = 6,
                                   XYZ_FIXED = 7;

        public Lager(string nodeId, string _face, int supportType, double[] pre, FEModell modell)
        {
            face = _face;
            int ndof;
            //switch (supportType)
            //{
            //    case 1:
            //        nodeId = "N00" + nodeId.Substring(3, 4);
            //        break;
            //    case 2:
            //        nodeId = nodeId.Substring(0, 3) + "00" + nodeId.Substring(5,2);
            //        break;
            //    case 3:
            //        nodeId = nodeId.Substring(0, 3) + nodeId.Substring(5, 2) + "00";
            //        break;
            //}
            if (modell.Knoten.TryGetValue(nodeId, out var node))
            {
                ndof = node.NumberOfNodalDof;
            }
            else
            {
                throw new ModellAusnahme("Lagerknoten nicht definiert");
                //Console.WriteLine(@"Lagerknoten nicht definiert"); return;
            }
            Type = supportType;
            //Reactions = new double[ndof];
            Prescribed = new double[ndof];
            Restrained = new bool[ndof];
            for (int i = 0; i < ndof; i++) Restrained[i] = false;
            NodeId = nodeId;

            if (supportType == X_FIXED) { Prescribed[0] = pre[0]; Restrained[0] = true; }
            if (supportType == Y_FIXED) { Prescribed[1] = pre[1]; Restrained[1] = true; }
            if (supportType == Z_FIXED) { Prescribed[2] = pre[2]; Restrained[2] = true; }
            if (supportType == XY_FIXED)
            {
                Prescribed[0] = pre[0]; Restrained[0] = true;
                Prescribed[1] = pre[1]; Restrained[1] = true;
            }
            if ((supportType) == XZ_FIXED)
            {
                Prescribed[0] = pre[0]; Restrained[0] = true;
                Prescribed[2] = pre[2]; Restrained[2] = true;
            }
            if ((supportType) == YZ_FIXED)
            {
                Prescribed[1] = pre[1]; Restrained[1] = true;
                Prescribed[2] = pre[2]; Restrained[2] = true;
            }
            if ((supportType) == XYZ_FIXED)
            {
                Prescribed[0] = pre[0]; Restrained[0] = true;
                Prescribed[1] = pre[1]; Restrained[1] = true;
                Prescribed[2] = pre[2]; Restrained[2] = true;
            }
        }

        //public bool XFixed() { return ((X_FIXED & type) == X_FIXED); }
        //public bool YFixed() { return ((Y_FIXED & type) == Y_FIXED); }
    }
}
