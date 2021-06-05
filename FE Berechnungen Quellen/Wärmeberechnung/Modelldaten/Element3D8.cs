﻿using FEALibrary.Modell;
using FEALibrary.Modell.abstrakte_Klassen;
using FEALibrary.Werkzeuge;
using System;
using System.Windows;
using System.Windows.Media.Media3D;

namespace FE_Berechnungen.Wärmeberechnung.Modelldaten
{
    public class Element3D8 : AbstraktLinear3D8
    {
        private AbstraktElement element;
        private readonly double[,] elementMatrix = new double[8, 8];
        private readonly double[] elementTemperatures = new double[8]; // at element nodes
        private readonly double[,] e = new double[3, 3];               // material matrix
        public double[] WärmeStatus { get; set; }
        private AbstraktMaterial Material { get; set; }
        private FEModell Modell { get; }

        // constructor
        public Element3D8(string[] eNodes, string materialId)
        {
            WärmeStatus = new double[3];
            if (Modell.SpatialDimension != 3)
                _ = MessageBox.Show("Das Modell ist nicht 3D", "Wärmeberechnung");

            ElementDof = 1;
            NodesPerElement = 8;
            NodeIds = eNodes;
            Nodes = new Knoten[NodesPerElement];
            for (var i = 0; i < NodesPerElement; i++)
            {
                if (Modell.Knoten.TryGetValue(NodeIds[i], out var node)) { }
                Nodes[i] = node;
            }
            ElementMaterialId = materialId;
        }
        public Element3D8(string id, string[] eNodes, string materialId, FEModell feModell)
        {
            Modell = feModell;
            WärmeStatus = new double[3];
            if (Modell.SpatialDimension != 3)
                _ = MessageBox.Show("Das Modell ist nicht 3D", "Wärmeberechnung");

            ElementId = id;
            ElementDof = 1;
            NodesPerElement = 8;
            NodeIds = eNodes;
            Nodes = new Knoten[NodesPerElement];
            for (var i = 0; i < NodesPerElement; i++)
            {
                if (Modell.Knoten.TryGetValue(NodeIds[i], out var node)) { }
                Nodes[i] = node;
            }
            ElementMaterialId = materialId;
        }
        // ....Compute element matrix.....................................
        public override double[,] ComputeMatrix()
        {
            if (Modell.Material.TryGetValue(ElementMaterialId, out var abstractMaterial)) { }
            Material = (Material)abstractMaterial;
            double[] gCoord = { -1 / Math.Sqrt(5.0 / 3), 0, 1 / (Math.Sqrt(5.0 / 3)) };
            double[] gWeight = { (5.0 / 9), (8.0 / 9), (5.0 / 9) };                               // gaussian coordinates, weights
            _ = new double[8, 3];
            MatrizenAlgebra.Clear(elementMatrix);

            // material matrix für ebene Verzerrung (plane strain)
            var conduct = ((Material)Material)?.Leitfähigkeit;
            if (conduct != null)
            {
                e[0, 0] = conduct[0];
                e[1, 1] = conduct[1];
                e[2, 2] = conduct[2];
            }

            for (var i = 0; i < gCoord.Length; i++)
            {
                var z0 = gCoord[i]; var g0 = gWeight[i];
                for (var j = 0; j < gCoord.Length; j++)
                {
                    var z1 = gCoord[j]; var g1 = gWeight[j];
                    for (var k = 0; k < gCoord.Length; k++)
                    {
                        var z2 = gCoord[k]; var g2 = gWeight[k];
                        ComputeGeometry(z0, z1, z2);
                        Sx = ComputeSx(z0, z1, z2);
                        // Ke = determinant*g0*g1*g2*Sx*E*SxT
                        var temp = MatrizenAlgebra.Mult(Sx, e);
                        MatrizenAlgebra.MultAddMatrixTransposed(elementMatrix, Determinant * g0 * g1 * g2, temp, Sx);
                    }
                }
            }
            return elementMatrix;
        }
        // ....Compute diagonal Specific Heat Matrix.................................
        public override double[] ComputeDiagonalMatrix()
        {
            throw new ModellAusnahme("*** specific heat matrix not implemented yet in Heat3D8");
        }

        // --- Behaviour of the Element ----------------------------------
        // ....Compute the heat state at the (z0,z1,z2) of the element......
        public override double[] ComputeZustandsvektor()
        {
            var elementWärmeStatus = new double[3];             // in element
            return elementWärmeStatus;
        }

        public override double[] ComputeElementState(double z0, double z1, double z2)
        {
            for (var i = 0; i < 8; i++)
            {
                elementTemperatures[i] = Nodes[i].NodalDof[0];
            }
            // midPointHeatState = E * Sx(transponiert) * Temperatures
            var midpointHeatState = MatrizenAlgebra.MultTransposed(Sx, elementTemperatures);
            midpointHeatState = MatrizenAlgebra.Mult(e, midpointHeatState);
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
        public override Point3D ComputeCenterOfGravity3D()
        {
            if (!Modell.Elemente.TryGetValue(ElementId, out element))
            {
                throw new ModellAusnahme("Element3D8: " + ElementId + " nicht im Modell gefunden");
            }
            element.SetReferences(Modell);
            return CenterOfGravity(element);
        }
    }
}
