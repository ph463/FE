﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using FE_Bibliothek.Modell;

namespace FE_Bibliothek.Werkzeuge
{
    public static class FEGeometrie
    {
        public static List<Knoten> innenKnoten = new List<Knoten>();

        public static List<Knoten> ConvexHull(List<Knoten> knoten, bool eng, double formFaktor)
        // ***** The convex hull or convex envelope or convex closure of a shape is the smallest convex set that contains it. 
        // ***** For a bounded subset of the plane, the convex hull may be visualized as the shape enclosed by a rubber band stretched around the subset. 
        //
        // The current impementation assumes the first element to be in the upper left corner with a 
        // sucessive sequence of Nodes in clockwise direction.
        // Input: A list containing node definitions of a Finite Element Model, 
        //        the FormFactor for neighbouring elements, i.e. the maximum length to find neighbours, e.g. 1.2
        //        _eng = true  > Koordinatensystem links-unten, y nach oben,  Ingenieurkoordinaten
        //        _eng = false > Koordinatensystem links-oben,  y nach unten, Bildschirmkoordinaten
        //
        // Output: A list of successive FE nodes as basis for the hull geometry
        //         node geometries can be added to a PointCollection
        //         as base for a Polygon which is closed by definition
        //        "Geometry.innerNodes" available as a list of nodes
        // ***** 
        {
            int factor = 1;
            if (eng) factor = -1;
            double startWinkel = factor * 100;
            Knoten found = null;
            var hullKnotenList = new List<Knoten>();
            var next = new Point(knoten[0].Coordinates[0], knoten[0].Coordinates[1]);
            var start = next;
            hullKnotenList.Add(knoten[0]);
            var basisVektor = new Vector(1, 0);
            basisVektor = RotateVector(basisVektor, startWinkel);

            innenKnoten = knoten.ToList();
            innenKnoten.Remove(knoten[0]);
            foreach (var unused in knoten)
            {
                Point end;
                Vector vec;
                foreach (var rest in innenKnoten)
                {
                    end = new Point(rest.Coordinates[0], rest.Coordinates[1]);
                    vec = (Vector)end - (Vector)start;
                    if (vec.Length < formFaktor)
                    {
                        startWinkel = Vector.AngleBetween(basisVektor, vec);
                        next = end; found = rest;
                        break;
                    }
                    innenKnoten.Remove(found);
                }
                foreach (var rest in innenKnoten)
                {
                    end = new Point(rest.Coordinates[0], rest.Coordinates[1]);
                    vec = (Vector)end - (Vector)start;
                    if (vec.Length < formFaktor)
                    {
                        var winkel = -Math.Abs(Vector.AngleBetween(basisVektor, vec));
                        if (!(winkel < startWinkel)) continue;
                        next = end; found = rest; startWinkel = winkel;
                    }
                }
                innenKnoten.Remove(found);
                hullKnotenList.Add(found);
                basisVektor = RotateVector((Vector)next - (Vector)start, factor * 100);
                start = next;
                if (found != null && (hullKnotenList.Count > 2) &&
                    (Math.Sqrt(Math.Pow(knoten[0].Coordinates[0] - found.Coordinates[0], 2) +
                               Math.Pow((knoten[0].Coordinates[1] - found.Coordinates[1]), 2))) <= 1)
                { break; }
            }
            return hullKnotenList;
        }
        public static Vector RotateVector(Vector vec, double angle)  // clockwise in degree
        {
            var winkel = angle * Math.PI / 180;
            var rotated = new Vector(vec.X * Math.Cos(winkel) - vec.Y * Math.Sin(winkel),
                                     vec.X * Math.Sin(winkel) + vec.Y * Math.Cos(winkel));
            return rotated;
        }
    }
}
