namespace FE_Bibliothek.Modell.abstrakte_Klassen
{
    public abstract class AbstraktLinienlast : AbstraktElementLast
    {
        public string StartNodeId { get; set; }
        public Knoten StartNode { get; set; }
        public string EndNodeId { get; set; }
        public Knoten EndNode { get; set; }
    }
}
