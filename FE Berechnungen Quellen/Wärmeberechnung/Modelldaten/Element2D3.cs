using FEALibrary.Modell;
using FEALibrary.Modell.abstrakte_Klassen;
using FEALibrary.Werkzeuge;
using System.Windows;

namespace FE_Berechnungen.Wärmeberechnung.Modelldaten
{
    public class Element2D3 : AbstraktLinear2D3
    {
        private AbstraktElement element;
        private Material material;
        private Knoten knoten;
        private double[,] elementMatrix = new double[3, 3];
        public double[] SpecificHeatMatrix { get; }
        private readonly double[] elementTemperatures = new double[3];   // at element nodes
        public FEModell Modell { get; }
        public Element2D3(string[] eNodes, string eMaterialId, FEModell feModell)
        {
            Modell = feModell;
            ElementDof = 1;
            NodesPerElement = 3;
            NodeIds = eNodes;
            Nodes = new Knoten[NodesPerElement];
            ElementMaterialId = eMaterialId;
            SpecificHeatMatrix = new double[3];
        }
        public Element2D3(string id, string[] eNodes, string eMaterialId, FEModell feModell)
        {
            Modell = feModell;
            ElementId = id;
            ElementDof = 1;
            NodesPerElement = 3;
            NodeIds = eNodes;
            Nodes = new Knoten[NodesPerElement];
            ElementMaterialId = eMaterialId;
            SpecificHeatMatrix = new double[3];
        }
        // ....Compute element Matrix.....................................
        public override double[,] ComputeMatrix()
        {
            ComputeGeometry();
            if (Modell.Material.TryGetValue(ElementMaterialId, out var abstractMaterial)) { }
            material = (Material)abstractMaterial;
            ElementMaterial = material;
            if (material == null) return elementMatrix;
            var conductivity = material.Leitfähigkeit[0];
            // Ke = area*c*Sx*SxT
            elementMatrix = MatrizenAlgebra.RectMultMatrixTransposed(0.5 * Determinant * conductivity, Sx, Sx);

            return elementMatrix;
        }
        // ....Compute diagonal Specific Heat Matrix.................................
        public override double[] ComputeDiagonalMatrix()
        {
            ComputeGeometry();
            // Me = density * conductivity * 0.5*determinant / 3    (area/3)
            SpecificHeatMatrix[0] = material.DichteLeitfähigkeit * Determinant / 6;
            SpecificHeatMatrix[1] = SpecificHeatMatrix[0];
            if (SpecificHeatMatrix.Length > 2) SpecificHeatMatrix[2] = SpecificHeatMatrix[0];
            return SpecificHeatMatrix;
        }
        // ....Compute the heat state at the midpoint of the element......
        public override double[] ComputeZustandsvektor()
        {
            var elementState = new double[2];
            return elementState;
        }

        public override double[] ComputeElementState(double z0, double z1)
        {
            var elementWärmeStatus = new double[2];             // in element
            ComputeGeometry();
            if (Modell.Material.TryGetValue(ElementMaterialId, out var abstractMaterial)) { }
            material = (Material)abstractMaterial;
            ElementMaterial = material;
            if (Modell.Elemente.TryGetValue(ElementId, out element))
            {
                for (var i = 0; i < element.Nodes.Length; i++)
                {
                    if (Modell.Knoten.TryGetValue(element.NodeIds[i], out knoten)) { }

                    //Debug.Assert(node != null, nameof(node) + " != null");
                    if (knoten != null) elementTemperatures[i] = knoten.NodalDof[0];
                }

                if (material == null) return elementWärmeStatus;
                var conductivity = material.Leitfähigkeit[0];
                elementWärmeStatus = MatrizenAlgebra.MultTransposed(-conductivity, Sx, elementTemperatures);
            }
            else
            {
                throw new ModellAusnahme("Element2D3: " + ElementId + " nicht im Modell gefunden");
            }
            return elementWärmeStatus;
        }

        public override void SetSystemIndicesOfElement()
        {
            SystemIndicesOfElement = new int[NodesPerElement * ElementDof];
            var counter = 0;
            for (var i = 0; i < NodesPerElement; i++)
            {
                for (var j = 0; j < ElementDof; j++)
                    SystemIndicesOfElement[counter++] = Nodes[i].SystemIndices[j];
            }
        }
        public override Point ComputeCenterOfGravity()
        {
            if (!Modell.Elemente.TryGetValue(ElementId, out element))
            {
                throw new ModellAusnahme("Element2D3: " + ElementId + " nicht im Modell gefunden");
            }
            element.SetReferences(Modell);
            return CenterOfGravity(element);
        }
    }
}