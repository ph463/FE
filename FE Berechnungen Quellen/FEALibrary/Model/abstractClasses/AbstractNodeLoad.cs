using System.Windows;

namespace FEALibrary.Model.abstractClasses
{
    public abstract class AbstractNodeLoad : AbstractLoad
    {
        public Node Node { get; set; }
        public int NodalDof { get; set; }
        //public bool[] Restrained { get; set; }
        //public double[] Reactions { get; set; }

        public void SetReferences(FeModel modell)
        {
            if (modell.Knoten.TryGetValue(NodeId, out Node node)) { }

            if (node != null) return;
            var message = "Knoten mit ID=" + NodeId + " ist nicht im Modell enthalten";
            _ = MessageBox.Show(message, "AbstractNodeLoad");
        }
    }
}