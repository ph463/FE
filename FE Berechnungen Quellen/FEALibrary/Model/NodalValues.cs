namespace FEALibrary.Model
{
    public class NodalValues
    {
        public string NodeId { get; set; }
        public double[] Values { get; set; }

        public NodalValues(string nodeId, double[] values)
        {
            NodeId = nodeId;
            Values = values;
        }
    }
}