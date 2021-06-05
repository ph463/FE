using FEALibrary.Modell;
using FEALibrary.Modell.abstrakte_Klassen;
using FEALibrary.Werkzeuge;
using System.Windows;

namespace FE_Berechnungen.Elastizitätsberechnung.Modelldaten
{
    public class Element2D3 : AbstraktLinear2D3
    {
        private AbstraktElement element;
        private double[,] matrix = new double[6, 6];
        private readonly double[] elementVerformungen = new double[6];// at element nodes

        //private AbstractMaterial Material { get; }
        private FEModell Modell { get; }

        private readonly double[,] b = new double[3, 6];               // strain-displacement transformation
        private readonly double[,] e = new double[3, 3];               // Materialmatrix

        // ....Constructor................................................
        public Element2D3(string[] eNodes, string crossSectionId, string eMaterialId, FEModell feModell)
        {
            Modell = feModell;
            ElementDof = 2;
            NodesPerElement = 3;
            NodeIds = eNodes;
            Nodes = new Knoten[NodesPerElement];
            ElementCrossSectionId = crossSectionId;
            ElementMaterialId = eMaterialId;
        }

        // ....Compute element matrix.....................................
        public override double[,] ComputeMatrix()
        {
            ComputeGeometry();
            ComputeStrainDisplacementTransformation();
            ComputeMaterial();
            // Ke = 0.5*thickness*determinant*BT*E*B
            var temp = MatrizenAlgebra.MultTransposedMatrix(0.5 * ElementCrossSection.QuerschnittsWerte[0] * Determinant, b, e);
            matrix = MatrizenAlgebra.Mult(temp, b);
            return matrix;
        }

        // ....Compute mass Matrix.................................
        public override double[] ComputeDiagonalMatrix()
        {
            throw new ModellAusnahme("*** Mass Matrix noch nicht implementiert in Elastizität2D3");
        }

        // compute strain-displacement transformation matrix eps = B * u
        private void ComputeStrainDisplacementTransformation()
        {
            b[0, 0] = xzu[1, 1]; b[0, 1] = 0; b[0, 2] = -xzu[1, 0]; b[0, 3] = 0; b[0, 4] = xzu[1, 0] - xzu[1, 1]; b[0, 5] = 0;
            b[1, 0] = 0; b[1, 1] = -xzu[0, 1]; b[1, 2] = 0; b[1, 3] = xzu[0, 0]; b[1, 4] = 0; b[1, 5] = xzu[0, 1] - xzu[0, 0];
            b[2, 0] = -xzu[0, 1]; b[2, 1] = xzu[1, 1]; b[2, 2] = xzu[0, 0]; b[2, 3] = -xzu[1, 0]; b[2, 4] = xzu[0, 1] - xzu[0, 0]; b[2, 5] = xzu[1, 0] - xzu[1, 1];
        }

        // compute material matrix for plane strain
        private void ComputeMaterial()
        {
            var emod = ElementMaterial.MaterialWerte[0];
            var ratio = ElementMaterial.MaterialWerte[1];
            var factor = emod * (1.0 - ratio) / ((1.0 + ratio) * (1.0 - 2.0 * ratio));
            var coeff = ratio / (1.0 - ratio);

            e[0, 0] = factor; e[0, 1] = coeff * factor;
            e[1, 0] = coeff * factor; e[1, 1] = factor;
            e[2, 2] = (1.0 - 2.0 * ratio) / 2.0 / (1.0 - ratio) * factor;
        }

        // --- Elementverhalten ----------------------------------

        // ....Berechne Elementspannungen: sigma = E * B * Ue (Element Verformungen) ......
        public override double[] ComputeZustandsvektor()
        {
            for (var i = 0; i < NodesPerElement; i++)
            {
                var nodalDof = i * 2;
                elementVerformungen[nodalDof] = Nodes[i].NodalDof[0];
                elementVerformungen[nodalDof + 1] = Nodes[i].NodalDof[1];
            }
            var temp = MatrizenAlgebra.Mult(e, b);
            var elementSpannungen = MatrizenAlgebra.Mult(temp, elementVerformungen);
            return elementSpannungen;
        }

        public override double[] ComputeElementState(double z0, double z1)
        {
            for (var i = 0; i < NodesPerElement; i++)
            {
                var nodalDof = i * 2;
                elementVerformungen[nodalDof] = Nodes[i].NodalDof[0];
                elementVerformungen[nodalDof + 1] = Nodes[i].NodalDof[1];
            }
            return elementVerformungen;
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