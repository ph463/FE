﻿using System;

namespace FEALibrary.Modell
{
    public class Knoten
    {
        private double[] coordinates;

        // Properties
        public string Id { get; }
        public int SpatialDimension { get; }
        public int NumberOfNodalDof { get; }
        public double[] NodalDof { get; set; }
        public double[][] NodalVariables { get; set; }
        public double[][] NodalDerivatives { get; set; }
        public double[] Reactions { get; set; }
        public double[] Coordinates
        {
            get => coordinates;
            set
            {
                coordinates = value ?? throw new ArgumentNullException(nameof(value));

                if (coordinates.Length == SpatialDimension)
                {
                    coordinates = new double[SpatialDimension];
                }
                else
                {
                    throw new ModellAusnahme("Knoten " + Id + ": Anzahl Koordinaten nicht gleich Raumdimension");
                }
            }
        }
        public int[] SystemIndices { get; set; }

        // ... Constructor ........................................................
        public Knoten(double[] crds, int ndof, int dimension)
        {
            SpatialDimension = dimension;
            coordinates = crds;
            NumberOfNodalDof = ndof;
        }
        public Knoten(string id, double[] crds, int ndof, int dimension)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            SpatialDimension = dimension;
            coordinates = crds;
            NumberOfNodalDof = ndof;
        }
        public int SetSystemIndices(int k)
        {
            SystemIndices = new int[NumberOfNodalDof];
            for (var i = 0; i < NumberOfNodalDof; i++)
                SystemIndices[i] = k++;
            // returning the incremented system index of a node
            return k;
        }
    }
}
