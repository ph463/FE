using FEALibrary.Modell;
using FEALibrary.Modell.abstrakte_Klassen;
using FEALibrary.Werkzeuge;
using System;
using System.Windows;

namespace FE_Berechnungen.Wärmeberechnung.Modelldaten
{
    public class Element2D4 : AbstraktLinear2D4
    {
        private Material material;
        private readonly double[,] elementMatrix = new double[4, 4];
        public double[] SpecificHeatMatrix { get; }
        private readonly double[] elementTemperatures = new double[4];   // at element nodes
        public FEModell Modell { get; set; }

        // ....Constructor................................................
        public Element2D4(string[] eNodes, string materialId, FEModell feModell)
        {
            // The null-coalescing operator ?? returns the value of its left-hand operand
            // if it isn't null; otherwise, it evaluates the right-hand operand and returns
            // its result. The ?? operator doesn't evaluate its right-hand operand if the
            // left-hand operand evaluates to non-null.
            Modell = feModell ?? throw new ArgumentNullException(nameof(feModell));
            ElementDof = 1;
            NodesPerElement = 4;
            NodeIds = eNodes ?? throw new ArgumentNullException(nameof(eNodes));
            Nodes = new Knoten[NodesPerElement];
            for (var i = 0; i < NodesPerElement; i++)
            {
                if (Modell.Knoten.TryGetValue(NodeIds[i], out var node)) { }
                Nodes[i] = node ?? throw new ArgumentNullException(nameof(node));
            }
            ElementMaterialId = materialId ?? throw new ArgumentNullException(nameof(materialId));
            SpecificHeatMatrix = new double[4];
        }
        public Element2D4(string id, string[] eNodes, string materialId, FEModell feModell)
        {
            if (eNodes == null) throw new ArgumentNullException(nameof(eNodes));
            Modell = feModell ?? throw new ArgumentNullException(nameof(feModell));
            ElementId = id ?? throw new ArgumentNullException(nameof(id));
            ElementDof = 1;
            NodesPerElement = 4;
            NodeIds = eNodes ?? throw new ArgumentNullException(nameof(eNodes));
            Nodes = new Knoten[NodesPerElement];
            for (var i = 0; i < NodesPerElement; i++)
            {
                if (Modell.Knoten.TryGetValue(NodeIds[i], out var node)) { }

                if (node != null) Nodes[i] = node ?? throw new ArgumentNullException(nameof(node));
            }
            ElementMaterialId = materialId ?? throw new ArgumentNullException(nameof(materialId));
            SpecificHeatMatrix = new double[4];
        }
        // ....Compute element Matrix.....................................
        public override double[,] ComputeMatrix()
        {
            double[] gaussCoord = { -1 / Math.Sqrt(3), 1 / Math.Sqrt(3) };
            if (Modell.Material.TryGetValue(ElementMaterialId, out var abstractMaterial)) { }
            material = (Material)abstractMaterial;
            ElementMaterial = material ?? throw new ArgumentNullException(nameof(material));
            var conductivity = material.Leitfähigkeit[0];

            MatrizenAlgebra.Clear(elementMatrix);
            foreach (var coor1 in gaussCoord)
            {
                foreach (var coor2 in gaussCoord)
                {
                    var z0 = coor1;
                    var z1 = coor2;
                    ComputeGeometry(z0, z1);
                    Sx = ComputeSx(z0, z1);
                    // Ke = C*Sx*SxT*determinant
                    MatrizenAlgebra.MultAddMatrixTransposed(elementMatrix, Determinant * conductivity, Sx, Sx);
                }
            }
            return elementMatrix;
        }
        // ....Compute diagonal Specific Heat Matrix.................................
        public override double[] ComputeDiagonalMatrix()
        {
            throw new ModellAusnahme("*** specifische Wärmematrix noch nicht implementiert in Heat2D4");
        }
        // ....Compute the heat state at the (z0,z1) of the element......
        public override double[] ComputeZustandsvektor()
        {
            var elementWärmeStatus = new double[2];             // in element
            return elementWärmeStatus;
        }

        public override double[] ComputeElementState(double z0, double z1)
        {
            _ = new double[2];                 // in element
            ComputeGeometry(z0, z1);
            Sx = ComputeSx(z0, z1);
            for (var i = 0; i < NodesPerElement; i++)
                elementTemperatures[i] = Nodes[i].NodalDof[0];
            var conductivity = material.Leitfähigkeit[0];
            var midpointHeatState = MatrizenAlgebra.MultTransposed(-conductivity, Sx, elementTemperatures);
            return midpointHeatState;
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
            throw new NotImplementedException();
        }
    }
}
