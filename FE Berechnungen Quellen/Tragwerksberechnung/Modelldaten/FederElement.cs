using FEALibrary.Modell;
using FEALibrary.Modell.abstrakte_Klassen;
using System.Windows;

namespace FE_Berechnungen.Tragwerksberechnung.Modelldaten
{
    public class FederElement : Abstrakt2D
    {
        private readonly FEModell modell;
        private Knoten node;

        private readonly double[,] stiffnessMatrix = new double[3, 3];
        private readonly double[] elementDeformations = new double[3];
        private readonly double[] springForces = new double[3];

        // ... Constructor ........................................................
        public FederElement(string[] springNode, string eMaterialId, FEModell feModel)
        {
            modell = feModel;
            NodeIds = springNode;
            ElementMaterialId = eMaterialId;
            ElementDof = 3;
            NodesPerElement = 1;
            Nodes = new Knoten[1];
        }

        // ... compute element matrix ..................................
        public override double[,] ComputeMatrix()
        {
            stiffnessMatrix[0, 0] = ElementMaterial.MaterialWerte[0];
            stiffnessMatrix[1, 1] = ElementMaterial.MaterialWerte[1];
            stiffnessMatrix[2, 2] = ElementMaterial.MaterialWerte[2];
            return stiffnessMatrix;
        }

        // ....Compute diagonal Spring Matrix.................................
        public override double[] ComputeDiagonalMatrix()
        {
            throw new ModellAusnahme("*** Massenmatrix nicht implementiert für Federlager");
        }

        // ... compute forces of spring element........................
        public override double[] ComputeZustandsvektor()
        {
            elementDeformations[0] = Nodes[0].NodalDof[0];
            elementDeformations[1] = Nodes[0].NodalDof[1];
            elementDeformations[2] = Nodes[0].NodalDof[2];

            // contribution of the node displacements
            for (var i = 0; i < stiffnessMatrix.Length; i++)
            {
                var sum = 0.0;
                int m;
                for (m = 0; m < elementDeformations.Length; m++)
                    sum += stiffnessMatrix[i, m] * elementDeformations[m];
                springForces[i] = sum;
            }
            return springForces;
        }

        public double[] ComputeSpringForces()
        {
            int i;
            elementDeformations[0] = Nodes[0].NodalDof[0];
            elementDeformations[1] = Nodes[0].NodalDof[1];
            elementDeformations[2] = Nodes[0].NodalDof[2];

            // contribution of the node displacements
            for (i = 0; i < stiffnessMatrix.Length; i++)
            {
                var sum = 0.0;
                int m;
                for (m = 0; m < elementDeformations.Length; m++)
                    sum += stiffnessMatrix[i, m] * elementDeformations[m];
                springForces[i] = sum;
            }
            return springForces;
        }

        public override double[] ComputeElementState(double z0, double z1)
        {
            var springForces = new double[3];
            return springForces;
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
            var cg = new Point();

            if (!modell.Knoten.TryGetValue(NodeIds[0], out node))
            {
                throw new ModellAusnahme("FederElement: " + ElementId + " nicht im Modell gefunden");
            }

            cg.X = node.Coordinates[0];
            cg.Y = node.Coordinates[1];
            return cg;
        }
    }
}
