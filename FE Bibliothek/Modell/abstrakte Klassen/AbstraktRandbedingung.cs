namespace FE_Bibliothek.Modell.abstrakte_Klassen
{
    public abstract class AbstraktRandbedingung
    {
        public string SupportId { get; set; }
        public string NodeId { get; set; }
        public Knoten Node { get; set; }
        public int Type { get; set; }
        public double[] Prescribed { get; set; }
        public bool[] Restrained { get; set; }
        //public double[] Reactions { get; set; }

        public void SetReferences(FEModell modell)
        {
            if (NodeId != null)
            {
                if (modell.Knoten.TryGetValue(NodeId, out Knoten node))
                {
                    Node = node;
                }

                if (node == null)
                {
                    throw new ModellAusnahme("Knoten mit ID = " + NodeId + " ist nicht im Modell enthalten");
                }
            }
            else
            {
                throw new ModellAusnahme("Knotenidentifikator für Randbedingung " + SupportId +
                                         " ist nicht definiert");
            }
        }
    }
}
