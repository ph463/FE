using System.Windows;

namespace FEALibrary.Modell.abstrakte_Klassen
{
    public abstract class AbstraktElement
    {
        public string ElementId { get; set; }
        public string[] NodeIds { get; protected set; }
        public Knoten[] Nodes { get; protected set; }
        protected int ElementDof { get; set; }
        protected int NodesPerElement { get; set; }
        public int[] SystemIndicesOfElement { get; protected set; }
        protected string ElementMaterialId { get; set; }
        protected string ElementCrossSectionId { get; set; }
        public AbstraktMaterial ElementMaterial { get; set; }
        public int Type { get; protected set; }
        public double[] ElementState { get; set; }
        public double[] ElementDeformations { get; protected set; }
        public double Determinant { get; protected set; }
        public abstract double[,] ComputeMatrix();
        public abstract double[] ComputeDiagonalMatrix();
        public abstract void SetSystemIndicesOfElement();
        public abstract double[] ComputeZustandsvektor();

        public void SetReferences(FEModell modell)
        {
            for (int i = 0; i < NodesPerElement; i++)
            {
                if (modell.Knoten.TryGetValue(NodeIds[i], out Knoten node)) { Nodes[i] = node; }

                if (node != null) continue;
                var message = "Element mit ID = " + NodeIds[i] + " ist nicht im Modell enthalten";
                _ = MessageBox.Show(message, "AbstraktElement");
            }
            if (modell.Material.TryGetValue(ElementMaterialId, out AbstraktMaterial material)) { ElementMaterial = material; }
            if (material == null)
            {
                var message = "Material mit ID=" + ElementMaterialId + " ist nicht im Modell enthalten";
                _ = MessageBox.Show(message, "AbstraktElement");
            }
        }
    }
}
