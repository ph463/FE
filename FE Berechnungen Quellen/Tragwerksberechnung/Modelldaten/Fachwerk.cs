using FEALibrary.Modell;
using FEALibrary.Modell.abstrakte_Klassen;
using FEALibrary.Werkzeuge;
using System;
using System.Windows;

namespace FE_Berechnungen.Tragwerksberechnung.Modelldaten
{
    public class Fachwerk : AbstraktBalken
    {
        private readonly FEModell modell;
        private AbstraktElement element;

        private static double[,] _stiffnessMatrix = new double[4, 4];

        private static readonly double[] MassMatrix = new double[4];

        // ... Constructor ........................................................
        public Fachwerk(string[] eNodes, string crossSectionId, string materialId, FEModell feModel)
        {
            modell = feModel;
            NodeIds = eNodes;
            ElementMaterialId = materialId;
            ElementCrossSectionId = crossSectionId;
            ElementDof = 2;
            NodesPerElement = 2;
            Nodes = new Knoten[2];
            ElementState = new double[2];
            ElementDeformations = new double[2];
        }

        // ... compute element matrix ..................................
        public override double[,] ComputeMatrix()
        {
            ComputeGeometry();
            var factor = ElementMaterial.MaterialWerte[0] * ElementCrossSection.QuerschnittsWerte[0] / length;
            var sx = ComputeSx();
            _stiffnessMatrix = MatrizenAlgebra.MultTransposedRect(factor, sx);
            return _stiffnessMatrix;
        }

        // ....Compute diagonal Mass Matrix.................................
        public override double[] ComputeDiagonalMatrix() //throws AlgebraicException
        {
            if (ElementMaterial.MaterialWerte.Length < 3)
            {
                throw new ModellAusnahme("Fachwerk " + ElementId + ", spezifische Masse noch nicht definiert");
            }
            // Me = specific mass * area * 0.5*length
            MassMatrix[0] = MassMatrix[1] = MassMatrix[2] = MassMatrix[3] =
                ElementMaterial.MaterialWerte[2] * ElementCrossSection.QuerschnittsWerte[0] * length / 2;
            return MassMatrix;
        }

        public static double[] ComputeLoadVector(AbstraktElementLast ael, bool inElementCoordinateSystem)
        {
            if (ael == null) throw new ArgumentNullException(nameof(ael));
            throw new ModellAusnahme("Fachwerkelement kann keine interne Last aufnehmen! Benutze Biegebalken mit Gelenk");
        }

        // ... compute end forces of frame element........................
        public override double[] ComputeElementState()
        {
            ComputeGeometry();
            ComputeZustandsvektor();
            var c1 = ElementMaterial.MaterialWerte[0] * ElementCrossSection.QuerschnittsWerte[0] / length;
            ElementState[0] = c1 * (ElementDeformations[0] - ElementDeformations[1]);
            ElementState[1] = -ElementState[0];
            return ElementState;
        }

        // ... compute displacement vector of frame elements .............
        public override double[] ComputeZustandsvektor()
        {
            // transform to the local coordinate system
            ElementDeformations[0] = rotationMatrix[0, 0] * Nodes[0].NodalDof[0]
                                    + rotationMatrix[1, 0] * Nodes[0].NodalDof[1];
            ElementDeformations[1] = rotationMatrix[0, 0] * Nodes[1].NodalDof[0]
                                    + rotationMatrix[1, 0] * Nodes[1].NodalDof[1];
            return ElementDeformations;
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
            if (!modell.Elemente.TryGetValue(ElementId, out element))
            {
                throw new ModellAusnahme("Fachwerk: " + ElementId + " nicht im Modell gefunden");
            }
            return CenterOfGravity(element);
        }

        public override double[] ComputeElementState(double z0, double z1)
        {
            throw new NotImplementedException();
        }
    }
}
