using FEALibrary.Modell;
using FEALibrary.Modell.abstrakte_Klassen;
using FEALibrary.Werkzeuge;
using System;
using System.Windows;

namespace FE_Berechnungen.Tragwerksberechnung.Modelldaten
{
    public class Biegebalken : AbstraktBalken
    {
        protected AbstraktMaterial material;
        private AbstraktElement element;
        protected Querschnitt crossSection;
        private readonly FEModell modell;
        //private Node node;

        private double[,] stiffnessMatrix = new double[6, 6];
        private readonly double[] massMatrix = new double[6];

        private readonly double[] shapeFunction = new double[6];
        private readonly double[] loadVector = new double[6];
        private readonly double gaussPoint = 1.0 / Math.Sqrt(3.0);

        // ... Constructor ........................................................
        public Biegebalken(string[] eNodeIds, string eCrossSectionId, string eMaterialId, FEModell feModel)
        {
            modell = feModel;
            NodeIds = eNodeIds;
            ElementCrossSectionId = eCrossSectionId;
            ElementMaterialId = eMaterialId;
            ElementDof = 3;
            NodesPerElement = 2;
            Nodes = new Knoten[2];
            ElementState = new double[6];
            ElementDeformations = new double[6];
        }

        // ... Compute element matrix ........................................
        public override double[,] ComputeMatrix()
        {
            stiffnessMatrix = ComputeLocalMatrix();
            // ... transform local matrix to compute global stiffness ....
            stiffnessMatrix = TransformMatrix(stiffnessMatrix);
            return stiffnessMatrix;
        }

        // ... compute local stiffness ..................................
        private double[,] ComputeLocalMatrix()
        {
            ComputeGeometry();
            var h2 = ElementMaterial.MaterialWerte[0] * ElementCrossSection.QuerschnittsWerte[1];          // EI
            var c1 = ElementMaterial.MaterialWerte[0] * ElementCrossSection.QuerschnittsWerte[0] / length; // EA/L
            var c2 = (12.0 * h2) / length / length / length;
            var c3 = (6.0 * h2) / length / length;
            var c4 = (4.0 * h2) / length;
            var c5 = 0.5 * c4;

            double[,] localMatrix = {{ c1,  0,  0, -c1,  0,  0},
                                     { 0,  c2,  c3,  0, -c2,  c3},
                                     { 0,  c3,  c4,  0, -c3,  c5},
                                     {-c1,  0,  0,  c1,  0,  0},
                                     { 0, -c2, -c3,  0,  c2, -c3},
                                     { 0,  c3,  c5,  0, -c3,  c4}};
            return localMatrix;
        }

        // ....Compute diagonal Mass Matrix.................................
        public override double[] ComputeDiagonalMatrix()
        {
            if (ElementMaterial.MaterialWerte.Length < 3)
            {
                throw new ModellAusnahme("Biegebalken " + ElementId + ", spezifische Masse noch nicht definiert");
            }
            // Verschiebungen: Me = specific mass * area * 0.5*length
            massMatrix[0] = massMatrix[1] = massMatrix[3] = massMatrix[4] =
                ElementMaterial.MaterialWerte[2] * ElementCrossSection.QuerschnittsWerte[0] * length / 2;
            // Rotationsmassen = 0
            massMatrix[2] = massMatrix[5] = 0.0;
            return massMatrix;
        }

        public double[] ComputeLoadVector(AbstraktLast ael, bool inElementCoordinateSystem)
        {
            ComputeGeometry();
            for (var i = 0; i < loadVector.Length; i++) { loadVector[i] = 0.0; }

            switch (ael)
            {
                case LinienLast ll:
                    {
                        // transform GAUSS point to range [0 -> 1] by using 0.5*(1+z)
                        var gp = new[] { 0.5 * (1 - gaussPoint), 0.5 * (1 + gaussPoint) };
                        foreach (var point in gp)
                        {
                            double xLoad;
                            double yLoad;

                            if (!ll.IsInElementCoordinateSystem())
                            {
                                xLoad = ll.GetXIntensity(point) * cos + ll.GetYIntensity(point) * sin;
                                yLoad = ll.GetXIntensity(point) * -sin + ll.GetYIntensity(point) * cos;
                            }
                            else
                            {
                                xLoad = ll.GetXIntensity(point);
                                yLoad = ll.GetYIntensity(point);
                            }

                            GetShapeFunctionValues(point);
                            loadVector[0] += shapeFunction[0] * (xLoad) * length / 2;
                            loadVector[1] += shapeFunction[1] * (yLoad) * length / 2;
                            loadVector[2] += shapeFunction[2] * (yLoad) * length / 2;
                            loadVector[3] += shapeFunction[3] * (xLoad) * length / 2;
                            loadVector[4] += shapeFunction[4] * (yLoad) * length / 2;
                            loadVector[5] += shapeFunction[5] * (yLoad) * length / 2;
                        }
                        break;
                    }
                case PunktLast pl:
                    {
                        var intensity = pl.Intensity;
                        GetShapeFunctionValues(pl.Offset);

                        double xLoad;
                        double yLoad;

                        if (!pl.IsInElementCoordinateSystem())
                        {
                            xLoad = intensity[0] * cos + intensity[1] * sin;
                            yLoad = intensity[0] * -sin + intensity[1] * cos;
                        }
                        else
                        {
                            xLoad = intensity[0];
                            yLoad = intensity[1];
                        }

                        loadVector[0] = shapeFunction[0] * xLoad;
                        loadVector[1] = shapeFunction[1] * yLoad;
                        loadVector[2] = shapeFunction[2] * yLoad;
                        loadVector[3] = shapeFunction[3] * xLoad;
                        loadVector[4] = shapeFunction[4] * yLoad;
                        loadVector[5] = shapeFunction[5] * yLoad;
                        break;
                    }
                default:
                    throw new ModellAusnahme("Last " + ael + " wird in diesem Elementtyp nicht unterstützt ");
            }

            if (inElementCoordinateSystem) return loadVector;
            var tmpLoadVector = new double[6];
            Array.Copy(loadVector, tmpLoadVector, loadVector.Length);
            // transforms the loadvector to the global coordinate system.
            loadVector[0] = tmpLoadVector[0] * cos + tmpLoadVector[1] * -sin;
            loadVector[1] = tmpLoadVector[0] * sin + tmpLoadVector[1] * cos;
            loadVector[2] = tmpLoadVector[2];
            loadVector[3] = tmpLoadVector[3] * cos + tmpLoadVector[4] * -sin;
            loadVector[4] = tmpLoadVector[3] * sin + tmpLoadVector[4] * cos;
            loadVector[5] = tmpLoadVector[5];
            return loadVector;
        }

        private void GetShapeFunctionValues(double z)
        {
            ComputeGeometry();
            if (z < 0 || z > 1)
                throw new ModellAusnahme("Biegebalken: Formfunktion ungültig : " + z + " liegt außerhalb des Elements");
            // Shape functions. 0 <= z <= 1
            shapeFunction[0] = 1 - z;                           //x translation - low node
            shapeFunction[1] = 2 * z * z * z - 3 * z * z + 1;   //y translation - low node
            shapeFunction[2] = length * z * (z - 1) * (z - 1);  //z rotation - low node
            shapeFunction[3] = z;                               //x translation - high node
            shapeFunction[4] = z * z * (3 - 2 * z);             //y translation - high node
            shapeFunction[5] = length * z * z * (z - 1);        //z rotation - high node
        }

        private double[,] TransformMatrix(double[,] matrix)
        {
            var elementDof = ElementDof;
            for (var i = 0; i < matrix.GetLength(0); i += elementDof)
            {
                for (var k = 0; k < matrix.GetLength(0); k += elementDof)
                {
                    var m11 = matrix[i, k];
                    var m12 = matrix[i, k + 1];
                    var m13 = matrix[i, k + 2];

                    var m21 = matrix[i + 1, k];
                    var m22 = matrix[i + 1, k + 1];
                    var m23 = matrix[i + 1, k + 2];

                    var m31 = matrix[i + 2, k];
                    var m32 = matrix[i + 2, k + 1];

                    var e11 = rotationMatrix[0, 0];
                    var e12 = rotationMatrix[0, 1];
                    var e21 = rotationMatrix[1, 0];
                    var e22 = rotationMatrix[1, 1];

                    var h11 = e11 * m11 + e12 * m21;
                    var h12 = e11 * m12 + e12 * m22;
                    var h21 = e21 * m11 + e22 * m21;
                    var h22 = e21 * m12 + e22 * m22;

                    matrix[i, k] = h11 * e11 + h12 * e12;
                    matrix[i, k + 1] = h11 * e21 + h12 * e22;
                    matrix[i + 1, k] = h21 * e11 + h22 * e12;
                    matrix[i + 1, k + 1] = h21 * e21 + h22 * e22;

                    matrix[i, k + 2] = e11 * m13 + e12 * m23;
                    matrix[i + 1, k + 2] = e21 * m13 + e22 * m23;
                    matrix[i + 2, k] = m31 * e11 + m32 * e12;
                    matrix[i + 2, k + 1] = m31 * e21 + m32 * e22;
                }
            }
            return matrix;
        }

        // ... compute end forces of frame element........................
        public override double[] ComputeElementState()
        {
            var localMatrix = ComputeLocalMatrix();
            var vector = ComputeZustandsvektor();

            // contribution of the node deformations
            ElementState = MatrizenAlgebra.Mult(localMatrix, vector);
            ElementState[2] = -ElementState[2];

            // contribution of the beam loads
            foreach (var item in modell.PunktLasten)
            {
                if (!(item.Value is PunktLast punktLast)) continue;
                if (punktLast.ElementId != ElementId) continue;
                vector = punktLast.ComputeLocalLoadVector();
                vector[2] = -vector[2];
                for (var i = 0; i < vector.Length; i++) ElementState[i] -= vector[i];
            }
            foreach (var item in modell.ElementLasten)
            {
                if (!(item.Value is LinienLast linienLast)) continue;
                if (linienLast.ElementId != ElementId) continue;
                vector = linienLast.ComputeLocalLoadVector();
                vector[2] = -vector[2];
                for (var i = 0; i < vector.Length; i++) ElementState[i] -= vector[i];
            }
            return ElementState;
        }

        // ... compute displacement vector of frame elements .............
        public override double[] ComputeZustandsvektor()
        {
            ComputeGeometry();
            const int ndof = 3;
            for (var i = 0; i < ndof; i++)
            {
                ElementDeformations[i] = Nodes[0].NodalDof[i];
                ElementDeformations[i + ndof] = Nodes[1].NodalDof[i];
            }
            // transform to the local coordinate system
            var temp0 = rotationMatrix[0, 0] * ElementDeformations[0]
                            + rotationMatrix[1, 0] * ElementDeformations[1];

            var temp1 = rotationMatrix[0, 1] * ElementDeformations[0]
                            + rotationMatrix[1, 1] * ElementDeformations[1];
            ElementDeformations[0] = temp0;
            ElementDeformations[1] = temp1;

            temp0 = rotationMatrix[0, 0] * ElementDeformations[3]
                  + rotationMatrix[1, 0] * ElementDeformations[4];
            temp1 = rotationMatrix[0, 1] * ElementDeformations[3]
                  + rotationMatrix[1, 1] * ElementDeformations[4];
            ElementDeformations[3] = temp0;
            ElementDeformations[4] = temp1;

            return ElementDeformations;
        }
        public override double[] ComputeElementState(double z0, double z1)
        {
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
                throw new ModellAusnahme("Biegebalken: " + ElementId + " nicht im Modell gefunden");
            }
            return CenterOfGravity(element);
        }
    }
}