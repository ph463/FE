using System.Windows;

namespace FE_Bibliothek.Modell.abstrakte_Klassen
{
    public abstract class AbstraktKnotenlast : AbstraktLast
    {
        public Knoten Node { get; set; }
        public int NodalDof { get; set; }
        //public bool[] Restrained { get; set; }
        //public double[] Reactions { get; set; }

        public void SetReferences(FEModell modell)
        {
            if (modell.Knoten.TryGetValue(NodeId, out Knoten node)) { }

            if (node != null) return;
            var message = "Knoten mit ID=" + NodeId + " ist nicht im Modell enthalten";
            _ = MessageBox.Show(message, "AbstractNodeLoad");
        }
    }
}
