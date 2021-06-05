namespace FEALibrary.Modell
{
    public class Knotenwerte
    {
        public string NodeId { get; set; }
        public double[] Values { get; set; }

        public Knotenwerte(string nodeId, double[] values)
        {
            NodeId = nodeId;
            Values = values;
        }
    }
}
