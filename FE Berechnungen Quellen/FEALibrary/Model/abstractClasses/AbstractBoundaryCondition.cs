namespace FEALibrary.Model.abstractClasses
{
    public abstract class AbstractBoundaryCondition
    {
        public string SupportId { get; set; }
        public string NodeId { get; set; }
        public Node Node { get; set; }
        public int Type { get; set; }
        public double[] Prescribed { get; set; }
        public bool[] Restrained { get; set; }
        //public double[] Reactions { get; set; }

        public void SetReferences(FeModel modell)
        {
            if (NodeId != null)
            {
                if (modell.Knoten.TryGetValue(NodeId, out Node node))
                {
                    Node = node;
                }

                if (node == null)
                {
                    throw new ModelException("Knoten mit ID = " + NodeId + " ist nicht im Modell enthalten");
                }
            }
            else
            {
                throw new ModelException("Knotenidentifikator für Randbedingung " + SupportId +
                                         " ist nicht definiert");
            }
        }
    }
}