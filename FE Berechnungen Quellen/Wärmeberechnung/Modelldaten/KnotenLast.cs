using FEALibrary.Modell.abstrakte_Klassen;

namespace FE_Berechnungen.Wärmeberechnung.Modelldaten
{
    public class KnotenLast : AbstraktKnotenlast
    {
        private int[] systemIndices;
        // ....Constructor....................................................
        public KnotenLast() { }
        public KnotenLast(string nodeId)
        {
            NodeId = nodeId;
        }
        public KnotenLast(string nodeId, double[] stream)
        {
            NodeId = nodeId;
            Intensity = new double[1];
            Intensity = stream;
        }
        public KnotenLast(string id, string nodeId)
        {
            LoadId = id;
            NodeId = nodeId;
        }
        public KnotenLast(string id, string nodeId, double[] stream)
        {
            LoadId = id;
            NodeId = nodeId;
            Intensity = new double[1];
            Intensity = stream;
        }
        // ....Compute the system indices of a node ..............................
        public int[] ComputeSystemIndices()
        {
            systemIndices = Node.SystemIndices;
            return systemIndices;
        }
        public override double[] ComputeLoadVector()
        {
            return Intensity;
        }
    }
}
