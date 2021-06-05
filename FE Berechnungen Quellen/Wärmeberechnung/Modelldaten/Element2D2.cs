using FEALibrary.Modell;
using FEALibrary.Modell.abstrakte_Klassen;
using System;
using System.Windows;

namespace FE_Berechnungen.Wärmeberechnung.Modelldaten
{
    public class Element2D2 : AbstraktLinear2D2
    {
        private readonly FEModell modell;
        private AbstraktElement element;
        private Material material;
        private readonly double[,] elementMatrix;
        private readonly double[] specificHeatMatrix;
        public Element2D2(string[] eNodes, string eMaterialId, FEModell feModell)
        {
            if (feModell != null) modell = feModell ?? throw new ArgumentNullException(nameof(feModell));
            NodeIds = eNodes ?? throw new ArgumentNullException(nameof(eNodes));
            ElementMaterialId = eMaterialId;
            ElementDof = 1;
            NodesPerElement = 2;
            elementMatrix = new double[NodesPerElement, NodesPerElement];
            specificHeatMatrix = new double[NodesPerElement];
            Nodes = new Knoten[NodesPerElement];
        }
        public Element2D2(string id, string[] eNodes, string eMaterialId, FEModell feModell)
        {
            modell = feModell ?? throw new ArgumentNullException(nameof(feModell));
            ElementId = id ?? throw new ArgumentNullException(nameof(id));
            NodeIds = eNodes ?? throw new ArgumentNullException(nameof(eNodes));
            ElementMaterialId = eMaterialId ?? throw new ArgumentNullException(nameof(eMaterialId));
            ElementDof = 1;
            NodesPerElement = 2;
            elementMatrix = new double[NodesPerElement, NodesPerElement];
            specificHeatMatrix = new double[NodesPerElement];
            Nodes = new Knoten[NodesPerElement];
        }
        // ... compute element matrix ..................................
        public override double[,] ComputeMatrix()
        {
            if (modell.Material.TryGetValue(ElementMaterialId, out var abstractMaterial)) { }
            material = (Material)abstractMaterial;
            ElementMaterial = material ?? throw new ArgumentNullException(nameof(material));
            length = Math.Abs(Nodes[1].Coordinates[0] - Nodes[0].Coordinates[0]);
            if (material == null) return elementMatrix;
            var factor = material.Leitfähigkeit[0] / length;
            elementMatrix[0, 0] = elementMatrix[1, 1] = factor;
            elementMatrix[0, 1] = elementMatrix[1, 0] = -factor;
            return elementMatrix;
        }
        // ....Compute diagonal Specific Heat Matrix.................................
        public override double[] ComputeDiagonalMatrix()
        {
            length = Math.Abs(Nodes[1].Coordinates[0] - Nodes[0].Coordinates[0]);
            // Me = specific heat * density * 0.5*length
            specificHeatMatrix[0] = specificHeatMatrix[1] = material.DichteLeitfähigkeit * length / 2;
            return specificHeatMatrix;
        }
        public override double[] ComputeZustandsvektor()
        {
            var elementWärmeStatus = new double[2];             // in element
            return elementWärmeStatus;
        }
        public override double[] ComputeElementState(double z0, double z1)
        {
            var elementWärmeStatus = new double[2];             // in element
            return elementWärmeStatus;
        }
        public override Point ComputeCenterOfGravity()
        {
            if (!modell.Elemente.TryGetValue(ElementId, out element))
            {
                throw new ModellAusnahme("Element2D2: " + ElementId + " nicht im Modell gefunden");
            }
            element.SetReferences(modell);
            return CenterOfGravity(element);
        }
    }
}