using FE_Berechnungen.Tragwerksberechnung.Modelldaten;
using FEALibrary.Modell;
using FEALibrary.Modell.abstrakte_Klassen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using static System.Windows.Controls.Canvas;
using static System.Windows.Media.Brushes;
using static System.Windows.Media.Color;

namespace FE_Berechnungen.Tragwerksberechnung
{
    public class Darstellung
    {
        private readonly FEModell modell;
        private Knoten knoten;
        private double screenH;
        public double screenV;
        private double minX, maxX, minY;
        public double maxY;
        public double auflösung;
        private double auflösungH, auflösungV, lastAuflösung;
        private int randOben = 60, randLinks = 60;
        private double plazierungV, plazierungH;
        public double überhöhungVerformung = 1;
        public List<object> ElementIDs { get; }
        public List<object> KnotenIDs { get; }
        public List<object> Verformungen { get; }
        public List<object> LastVektoren { get; }
        public List<object> LagerDarstellung { get; }
        public List<object> NormalkraftListe { get; }
        public List<object> QuerkraftListe { get; }
        public List<object> MomenteListe { get; }
        public TextBlock maxMomentText;

        private const int MaxNormalkraftScreen = 30;
        private const int MaxQuerkraftScreen = 30;
        private const int MaxMomentScreen = 50;
        private readonly Canvas visualErgebnisse;

        public Darstellung(FEModell feModell, Canvas visual)
        {
            modell = feModell;
            visualErgebnisse = visual;
            ElementIDs = new List<object>();
            KnotenIDs = new List<object>();
            Verformungen = new List<object>();
            LastVektoren = new List<object>();
            LagerDarstellung = new List<object>();
            NormalkraftListe = new List<object>();
            QuerkraftListe = new List<object>();
            MomenteListe = new List<object>();
            FestlegungAuflösung();
        }
        public void FestlegungAuflösung()
        {
            screenH = visualErgebnisse.ActualWidth;
            screenV = visualErgebnisse.ActualHeight;

            var x = new List<double>();
            var y = new List<double>();
            foreach (var item in modell.Knoten)
            {
                x.Add(item.Value.Coordinates[0]);
                maxX = x.Max(); minX = x.Min();
                y.Add(item.Value.Coordinates[1]);
                maxY = y.Max(); minY = y.Min();
            }

            var delta = Math.Abs(maxX - minX);
            if (delta < 1)
            {
                auflösungH = screenH - 5 * randLinks;
                randLinks = (int)(0.5 * screenH);
            }
            else
            {
                auflösungH = (screenH - 5 * randLinks) / delta;
            }
            plazierungH = randLinks;
            if (maxX < double.Epsilon) plazierungH = screenH / 2;

            delta = Math.Abs(maxY - minY);
            if (delta < 1)
            {
                auflösung = screenV - 5 * randOben;
                randOben = (int)(0.5 * screenV);
            }
            else
            {
                auflösung = (screenV - 5 * randOben) / delta;
            }
            if (auflösungH < auflösung) auflösung = auflösungH;
            plazierungV = randOben;
            if (maxY < double.Epsilon) plazierungV = screenV / 2;
        }

        public void UnverformteGeometrie()
        {
            // pathGeometry enthaelt EIN spezifisches Element
            // alle Elemente werden der GeometryGroup tragwerk hinzugefügt
            var tragwerk = new GeometryGroup();
            foreach (var item in modell.Elemente)
            {
                var pathGeometry = new PathGeometry();
                var element = item.Value;
                var pathFigure = new PathFigure();

                if (modell.Knoten.TryGetValue(element.NodeIds[0], out var node)) { }
                var startPunkt = TransformKnoten(node, auflösung, maxY);
                pathFigure.StartPoint = startPunkt;

                switch (element)
                {
                    // Federelement
                    case FederElement _:
                        FederelementZeichnen(pathGeometry, element);
                        break;

                    // Fachwerkelemente mit Gelenken an beiden Enden zeichnen
                    case Fachwerk _:
                        {
                            if (modell.Knoten.TryGetValue(element.NodeIds[1], out node)) { }
                            var endPunkt = TransformKnoten(node, auflösung, maxY);

                            // Verbindung vom Start- zum Endknoten zeichnen
                            pathFigure.Segments.Add(new LineSegment(endPunkt, true));

                            // Gelenke als Halbkreise an Knoten des Fachwerkelementes zeichnen
                            FachwerkelementZeichnen(pathGeometry, startPunkt, endPunkt);
                            break;
                        }

                    // Gelenk am Startknoten bzw. Endknoten des BiegebalkenGelenk zeichnen
                    case BiegebalkenGelenk _:
                        {
                            if (modell.Knoten.TryGetValue(element.NodeIds[1], out node)) { }
                            var endPunkt = TransformKnoten(node, auflösung, maxY);

                            // Verbindung vom Start- zum Endknoten zeichnen
                            pathFigure.Segments.Add(new LineSegment(endPunkt, true));

                            BiegebalkenGelenkZeichnen(pathGeometry, startPunkt, endPunkt, element);
                            break;
                        }

                    // Elemente mit mehreren Knoten
                    default:
                        {
                            for (var i = 1; i < element.NodeIds.Length; i++)
                            {
                                if (modell.Knoten.TryGetValue(element.NodeIds[i], out node)) { }
                                var endPunkt = TransformKnoten(node, auflösung, maxY);

                                // Verbindung vom Start- zum Endknoten zeichnen
                                pathFigure.Segments.Add(new LineSegment(endPunkt, true));
                            }
                            pathGeometry.Figures.Add(pathFigure);
                            break;
                        }
                }
                if (element.NodeIds.Length > 2) pathFigure.IsClosed = true;
                tragwerk.Children.Add(pathGeometry);
            }

            // Knotengelenke werden als EllipseGeometry der GeometryGroup tragwerk hinzugefügt
            foreach (var gelenk in from item in modell.Knoten select item.Value into knoten where knoten.NumberOfNodalDof == 2 select TransformKnoten(knoten, auflösung, maxY) into gelenkPunkt select new EllipseGeometry(gelenkPunkt, 5, 5))
            {
                tragwerk.Children.Add(gelenk);
            }

            Shape path = new Path()
            {
                Stroke = Black,
                StrokeThickness = 1,
                Data = tragwerk
            };
            // setz oben/links Position zum Zeichnen auf dem Canvas
            SetLeft(path, randLinks);
            SetTop(path, randOben);
            // zeichne Shape
            visualErgebnisse.Children.Add(path);
        }
        public void VerformteGeometrie()
        {
            if (!StartFenster.berechnet)
            {
                var analysis = new Berechnung(modell);
                analysis.ComputeSystemMatrix();
                analysis.ComputeSystemVector();
                analysis.SolveEquations();
                StartFenster.berechnet = true;
            }
            //int überhöhung = 1;
            const int rotationÜberhöhung = 1;
            var pathGeometry = new PathGeometry();

            IEnumerable<AbstraktBalken> Beams()
            {
                foreach (var item in modell.Elemente)
                {
                    if (item.Value is AbstraktBalken element)
                    {
                        yield return element;
                    }
                }
            }
            foreach (var element in Beams())
            {
                //element.ElementState = element.ComputeElementState();
                var pathFigure = new PathFigure();
                Point start;
                Point end;
                double winkel;

                switch (element)
                {
                    case Fachwerk _:
                        {
                            if (modell.Knoten.TryGetValue(element.NodeIds[0], out knoten)) { }
                            start = TransformVerformtenKnoten(knoten, auflösung, maxY);
                            pathFigure.StartPoint = start;

                            for (var i = 1; i < element.NodeIds.Length; i++)
                            {
                                if (modell.Knoten.TryGetValue(element.NodeIds[i], out knoten)) { }
                                end = TransformVerformtenKnoten(knoten, auflösung, maxY);
                                pathFigure.Segments.Add(new LineSegment(end, true));
                            }
                            break;
                        }
                    case Biegebalken _:
                        {
                            if (modell.Knoten.TryGetValue(element.NodeIds[0], out knoten)) { }
                            start = TransformVerformtenKnoten(knoten, auflösung, maxY);
                            pathFigure.StartPoint = start;

                            for (var i = 1; i < element.NodeIds.Length; i++)
                            {
                                if (modell.Knoten.TryGetValue(element.NodeIds[i], out knoten)) { }
                                end = TransformVerformtenKnoten(knoten, auflösung, maxY);
                                var richtung = end - start;
                                richtung.Normalize();
                                winkel = -element.ElementDeformations[2] * 180 / Math.PI * rotationÜberhöhung;
                                richtung = RotateVectorScreen(richtung, winkel);
                                var control1 = start + richtung * element.length / 4 * auflösung;

                                richtung = start - end;
                                richtung.Normalize();
                                winkel = -element.ElementDeformations[5] * 180 / Math.PI * rotationÜberhöhung;
                                richtung = RotateVectorScreen(richtung, winkel);
                                var control2 = end + richtung * element.length / 4 * auflösung;

                                pathFigure.Segments.Add(new BezierSegment(control1, control2, end, true));
                            }
                            break;
                        }
                    case BiegebalkenGelenk _:
                        {
                            if (modell.Knoten.TryGetValue(element.NodeIds[0], out knoten)) { }
                            start = TransformVerformtenKnoten(knoten, auflösung, maxY);
                            pathFigure.StartPoint = start;

                            var control = start;
                            for (var i = 1; i < element.NodeIds.Length; i++)
                            {
                                if (modell.Knoten.TryGetValue(element.NodeIds[i], out knoten)) { }
                                end = TransformVerformtenKnoten(knoten, auflösung, maxY);

                                switch (element.Type)
                                {
                                    case 1:
                                        {
                                            var richtung = start - end;
                                            richtung.Normalize();
                                            winkel = element.ElementDeformations[4] * 180 / Math.PI * rotationÜberhöhung;
                                            richtung = RotateVectorScreen(richtung, winkel);
                                            control = end + richtung * element.length / 4 * auflösung;
                                            break;
                                        }
                                    case 2:
                                        {
                                            var richtung = end - start;
                                            richtung.Normalize();
                                            winkel = element.ElementDeformations[2] * 180 / Math.PI * rotationÜberhöhung;
                                            richtung = RotateVectorScreen(richtung, winkel);
                                            control = start + richtung * element.length / 4 * auflösung;
                                            break;
                                        }
                                }
                                pathFigure.Segments.Add(new QuadraticBezierSegment(control, end, true));
                            }

                            break;
                        }
                }
                if (element.NodeIds.Length > 2) pathFigure.IsClosed = true;
                pathGeometry.Figures.Add(pathFigure);
            }

            Shape path = new Path()
            {
                Stroke = Red,
                StrokeThickness = 2,
                Data = pathGeometry
            };
            // setz oben/links Position zum Zeichnen auf dem Canvas
            SetLeft(path, plazierungH);
            SetTop(path, plazierungV);
            // zeichne Shape
            visualErgebnisse.Children.Add(path);
            Verformungen.Add(path);
        }

        private static void FachwerkelementZeichnen(PathGeometry pathGeometry, Point startPunkt, Point endPunkt)
        {
            var pathFigure = new PathFigure();
            // Gelenk als Halbkreis am Startknoten des Fachwerkelementes zeichnen
            var direction = endPunkt - startPunkt;
            var start = RotateVectorScreen(direction, 90);
            start.Normalize();
            var zielPunkt = startPunkt + (5 * start);
            pathFigure.Segments.Add(new LineSegment(zielPunkt, false));
            var ziel = RotateVectorScreen(direction, -90);
            ziel.Normalize();
            zielPunkt = startPunkt + (5 * ziel);
            // ArcSegment beginnt am letzten Punkt der pathFigure
            // Zielpunkt, Größe in x,y, Öffnungswinkel, isLargeArc, sweepDirection, isStroked
            pathFigure.Segments.Add(new ArcSegment(zielPunkt, new Size(2.5, 2.5), 180, true, 0, true));
            pathFigure.Segments.Add(new LineSegment(startPunkt, false));

            // Verbindung vom Start- zum Endknoten zeichnen
            pathFigure.Segments.Add(new LineSegment(endPunkt, true));

            // Gelenk als Halbkreis am Endknoten des Fachwerkelementes zeichnen
            direction = startPunkt - endPunkt;
            start = RotateVectorScreen(direction, -90);
            start.Normalize();
            zielPunkt = endPunkt + (5 * start);
            pathFigure.Segments.Add(new LineSegment(zielPunkt, false));
            var end = RotateVectorScreen(direction, 90);
            end.Normalize();
            zielPunkt = endPunkt + (5 * end);
            pathFigure.Segments.Add(new ArcSegment(zielPunkt, new Size(2.5, 2.5), 180, true, (SweepDirection)1, true));
            pathFigure.Segments.Add(new LineSegment(endPunkt, false));

            // Verbindung vom Start- zum Endknoten zeichnen
            pathFigure.Segments.Add(new LineSegment(endPunkt, true));

            pathGeometry.Figures.Add(pathFigure);
        }
        private static void BiegebalkenGelenkZeichnen(PathGeometry pathGeometry, Point startPunkt, Point endPunkt, AbstraktElement element)
        {
            Vector direction, start;
            Point zielPunkt;

            var pathFigure = new PathFigure();
            // Gelenk am 1. Knoten des Biegebalken zeichnen
            if (element is BiegebalkenGelenk && element.Type == 1)
            {
                direction = endPunkt - startPunkt;
                start = RotateVectorScreen(direction, 90);
                start.Normalize();
                zielPunkt = startPunkt + (5 * start);
                pathFigure.Segments.Add(new LineSegment(zielPunkt, false));
                var ziel = RotateVectorScreen(direction, -90);
                ziel.Normalize();
                zielPunkt = startPunkt + (5 * ziel);
                // ArcSegment beginnt am letzten Punkt der pathFigure
                // Zielpunkt, Größe in x,y, Öffnungswinkel, isLargeArc, sweepDirection, isStroked
                pathFigure.Segments.Add(new ArcSegment(zielPunkt, new Size(2.5, 2.5), 180, true, 0, true));
                pathFigure.Segments.Add(new LineSegment(startPunkt, false));

                pathGeometry.Figures.Add(pathFigure);
            }

            // Gelenk am 2. Knoten des Biegebalken zeichnen
            if (!(element is BiegebalkenGelenk) && element.Type != 2) return;
            direction = startPunkt - endPunkt;
            start = RotateVectorScreen(direction, -90);
            start.Normalize();
            zielPunkt = endPunkt + (5 * start);
            pathFigure.Segments.Add(new LineSegment(zielPunkt, false));
            var end = RotateVectorScreen(direction, 90);
            end.Normalize();
            zielPunkt = endPunkt + 5 * end;
            pathFigure.Segments.Add(new ArcSegment(zielPunkt, new Size(2.5, 2.5), 180, true, (SweepDirection)1, true));
            pathFigure.Segments.Add(new LineSegment(endPunkt, false));
        }
        private void FederelementZeichnen(PathGeometry pathGeometry, AbstraktElement element)
        {
            var pathFigure = new PathFigure();
            // Plazierungspunkt des Federelementes
            if (modell.Knoten.TryGetValue(element.NodeIds[0], out var node)) { }
            var startPunkt = TransformKnoten(node, auflösung, maxY);

            // setz Referenzen der MaterialWerte
            element.SetReferences(modell);

            // x-Feder
            if (Math.Abs(element.ElementMaterial.MaterialWerte[0]) > 0)
            {
                DehnfederZeichnen(pathFigure, startPunkt);
                pathGeometry.Figures.Add(pathFigure);
                pathGeometry.Transform = new RotateTransform(90, startPunkt.X, startPunkt.Y);
            }

            // y-Feder
            if (Math.Abs(element.ElementMaterial.MaterialWerte[1]) > 0)
            {
                DehnfederZeichnen(pathFigure, startPunkt);
                pathGeometry.Figures.Add(pathFigure);

            }

            // Drehfeder zeichnen
            if (Math.Abs(element.ElementMaterial.MaterialWerte[2]) > 0)
            {
                var b = 10;
                pathFigure.StartPoint = startPunkt;
                var zielPunkt = new Point(startPunkt.X - b, startPunkt.Y - b);
                pathFigure.Segments.Add(
                    new ArcSegment(zielPunkt, new Size(b, b - 3), 200, true, 0, true));
                zielPunkt = new Point(startPunkt.X + b, startPunkt.Y);
                pathFigure.Segments.Add(
                    new ArcSegment(zielPunkt, new Size(b, b + 2), 190, false, 0, true));
                pathGeometry.Figures.Add(pathFigure);
            }
        }
        private static void DehnfederZeichnen(PathFigure pathFigure, Point startPunkt)
        {
            const double b = 6.0; const int h = 3;
            pathFigure.StartPoint = startPunkt;
            pathFigure.Segments.Add(
                new LineSegment(new Point(startPunkt.X, startPunkt.Y + 2 * h), true));
            pathFigure.Segments.Add(
                new LineSegment(new Point(startPunkt.X - b, startPunkt.Y + 3 * h), true));
            pathFigure.Segments.Add(
                new LineSegment(new Point(startPunkt.X + b, startPunkt.Y + 5 * h), true));
            pathFigure.Segments.Add(
                new LineSegment(new Point(startPunkt.X - b, startPunkt.Y + 7 * h), true));
            pathFigure.Segments.Add(
                new LineSegment(new Point(startPunkt.X + b, startPunkt.Y + 9 * h), true));
            pathFigure.Segments.Add(
                new LineSegment(new Point(startPunkt.X, startPunkt.Y + 10 * h), true));
            pathFigure.Segments.Add(
                new LineSegment(new Point(startPunkt.X, startPunkt.Y + 12 * h), true));
            pathFigure.Segments.Add(
                new LineSegment(new Point(startPunkt.X - b, startPunkt.Y + 12 * h), false));
            pathFigure.Segments.Add(
                new LineSegment(new Point(startPunkt.X + b, startPunkt.Y + 12 * h), true));

            pathFigure.Segments.Add(
                new LineSegment(new Point(startPunkt.X + b - h, startPunkt.Y + 13 * h), true));
            pathFigure.Segments.Add(
                new LineSegment(new Point(startPunkt.X + b / 2, startPunkt.Y + 12 * h), false));
            pathFigure.Segments.Add(
                new LineSegment(new Point(startPunkt.X + b / 2 - h, startPunkt.Y + 13 * h), true));
            pathFigure.Segments.Add(
                new LineSegment(new Point(startPunkt.X, startPunkt.Y + 12 * h), false));
            pathFigure.Segments.Add(
                new LineSegment(new Point(startPunkt.X - h, startPunkt.Y + 13 * h), true));
            pathFigure.Segments.Add(
                new LineSegment(new Point(startPunkt.X - b / 2, startPunkt.Y + 12 * h), false));
            pathFigure.Segments.Add(
                new LineSegment(new Point(startPunkt.X - b / 2 - h, startPunkt.Y + 13 * h), true));
            pathFigure.Segments.Add(
                new LineSegment(new Point(startPunkt.X - b, startPunkt.Y + 12 * h), false));
            pathFigure.Segments.Add(
                new LineSegment(new Point(startPunkt.X - b - h, startPunkt.Y + 13 * h), true));
        }

        public void ElementTexte()
        {
            foreach (var item in modell.Elemente)
            {
                //if (!(item.Value is Abstract2D)) continue;
                var element = (Abstrakt2D)item.Value;
                element.SetReferences(modell);
                var cg = element.ComputeCenterOfGravity();
                var id = new TextBlock
                {
                    FontSize = 12,
                    Text = item.Key,
                    Foreground = Blue
                };
                SetTop(id, (-cg.Y + maxY) * auflösung + randOben);
                SetLeft(id, cg.X * auflösung + randLinks);
                visualErgebnisse.Children.Add(id);
                ElementIDs.Add(id);
            }
        }
        public void KnotenTexte()
        {
            foreach (var item in modell.Knoten)
            {
                TextBlock id = new TextBlock
                {
                    FontSize = 12,
                    Text = item.Key,
                    Foreground = Red
                };
                SetTop(id, (-item.Value.Coordinates[1] + maxY) * auflösung + randOben);
                SetLeft(id, item.Value.Coordinates[0] * auflösung + randLinks);
                visualErgebnisse.Children.Add(id);
                KnotenIDs.Add(id);
            }
        }

        public void LastenZeichnen()
        {
            AbstraktLast last;
            Shape path;

            // Knotenlasten
            var maxLastWert = 1.0;
            const int maxLastScreen = 50;
            foreach (var item in modell.Lasten)
            {
                last = item.Value;
                if (Math.Abs(last.Intensity[0]) > maxLastWert) maxLastWert = Math.Abs(last.Intensity[0]);
                if (Math.Abs(last.Intensity[1]) > maxLastWert) maxLastWert = Math.Abs(last.Intensity[1]);
            }
            foreach (var item in modell.PunktLasten)
            {
                last = item.Value;
                if (Math.Abs(last.Intensity[0]) > maxLastWert) maxLastWert = Math.Abs(last.Intensity[0]);
                if (Math.Abs(last.Intensity[1]) > maxLastWert) maxLastWert = Math.Abs(last.Intensity[1]);
            }
            foreach (var linienLast in modell.ElementLasten.Select(item => (AbstraktLinienlast)item.Value))
            {
                if (Math.Abs(linienLast.Intensity[0]) > maxLastWert) maxLastWert = Math.Abs(linienLast.Intensity[0]);
                if (Math.Abs(linienLast.Intensity[1]) > maxLastWert) maxLastWert = Math.Abs(linienLast.Intensity[1]);
            }
            lastAuflösung = maxLastScreen / maxLastWert;

            foreach (var item in modell.Lasten)
            {
                last = item.Value;
                var pathGeometry = KnotenlastZeichnen(last);
                path = new Path()
                {
                    Stroke = Red,
                    StrokeThickness = 3,
                    Data = pathGeometry
                };
                LastVektoren.Add(path);

                SetLeft(path, randLinks);
                SetTop(path, randOben);
                visualErgebnisse.Children.Add(path);
            }
            foreach (var item in modell.PunktLasten)
            {
                var pathGeometry = PunktlastZeichnen(item.Value);
                path = new Path()
                {
                    Stroke = Red,
                    StrokeThickness = 3,
                    Data = pathGeometry
                };
                LastVektoren.Add(path);

                SetLeft(path, randLinks);
                SetTop(path, randOben);
                visualErgebnisse.Children.Add(path);
            }
            foreach (var item in modell.ElementLasten)
            {
                var linienlast = (AbstraktLinienlast)item.Value;
                var pathGeometry = LinienlastZeichnen(linienlast);
                var rot = FromArgb(60, 255, 0, 0);
                var blau = FromArgb(60, 0, 0, 255);
                var myBrush = new SolidColorBrush(rot);
                if (linienlast.Intensity[1] > 0) myBrush = new SolidColorBrush(blau);
                path = new Path()
                {
                    Fill = myBrush,
                    Stroke = Red,
                    StrokeThickness = 1,
                    Data = pathGeometry
                };
                LastVektoren.Add(path);

                SetLeft(path, randLinks);
                SetTop(path, randOben);
                visualErgebnisse.Children.Add(path);
            }
        }
        private PathGeometry KnotenlastZeichnen(AbstraktLast knotenlast)
        {
            var pathGeometry = new PathGeometry();
            var pathFigure = new PathFigure();
            const int lastPfeilGroesse = 10;

            if (modell.Knoten.TryGetValue(knotenlast.NodeId, out knoten)) { }

            if (knoten != null)
            {
                var endPoint = new Point(knoten.Coordinates[0] * auflösung - knotenlast.Intensity[0] * lastAuflösung,
                                         (-knoten.Coordinates[1] + maxY) * auflösung + knotenlast.Intensity[1] * lastAuflösung);
                pathFigure.StartPoint = endPoint;

                var startPoint = TransformKnoten(knoten, auflösung, maxY);
                pathFigure.Segments.Add(new LineSegment(startPoint, true));

                var vector = startPoint - endPoint;
                vector.Normalize();
                vector *= lastPfeilGroesse;
                vector = RotateVectorScreen(vector, 30);
                endPoint = new Point(startPoint.X - vector.X, startPoint.Y - vector.Y);
                pathFigure.Segments.Add(new LineSegment(endPoint, true));

                vector = RotateVectorScreen(vector, -60);
                endPoint = new Point(startPoint.X - vector.X, startPoint.Y - vector.Y);
                pathFigure.Segments.Add(new LineSegment(endPoint, false));
                pathFigure.Segments.Add(new LineSegment(startPoint, true));

                if (knotenlast.Intensity.Length > 2 && Math.Abs(knotenlast.Intensity[2]) > double.Epsilon)
                {
                    startPoint.X += 30;
                    pathFigure.Segments.Add(new LineSegment(startPoint, false));
                    startPoint.X -= 30;
                    startPoint.Y += 30;
                    pathFigure.Segments.Add(new ArcSegment
                        (startPoint, new Size(30, 30), 270, true, new SweepDirection(), true));

                    vector = new Vector(1, 0);
                    vector *= lastPfeilGroesse;
                    vector = RotateVectorScreen(vector, 45);
                    endPoint = new Point(startPoint.X - vector.X, startPoint.Y - vector.Y);
                    pathFigure.Segments.Add(new LineSegment(endPoint, true));

                    vector = RotateVectorScreen(vector, -60);
                    endPoint = new Point(startPoint.X - vector.X, startPoint.Y - vector.Y);
                    pathFigure.Segments.Add(new LineSegment(endPoint, false));
                    pathFigure.Segments.Add(new LineSegment(startPoint, true));
                }
            }

            pathGeometry.Figures.Add(pathFigure);
            return pathGeometry;
        }
        private PathGeometry PunktlastZeichnen(AbstraktElementLast last)
        {
            var punktlast = (PunktLast)last;
            var pathGeometry = new PathGeometry();
            var pathFigure = new PathFigure();
            const int lastPfeilGroesse = 10;

            punktlast.SetReferences(modell);
            if (modell.Elemente.TryGetValue(punktlast.ElementId, out var element)) { }

            if (element == null) return pathGeometry;
            if (modell.Knoten.TryGetValue(element.NodeIds[0], out var startKnoten)) { }
            var startPunkt = TransformKnoten(startKnoten, auflösung, maxY);

            // zweiter Elementknoten 
            if (modell.Knoten.TryGetValue(element.NodeIds[1], out var endKnoten)) { }
            var endPunkt = TransformKnoten(endKnoten, auflösung, maxY);

            var vector = new Vector(endPunkt.X, endPunkt.Y) - new Vector(startPunkt.X, startPunkt.Y);
            var lastPunkt = (Point)(punktlast.Offset * vector);

            lastPunkt.X = startPunkt.X + lastPunkt.X;
            lastPunkt.Y = startPunkt.Y + lastPunkt.Y;

            endPunkt = new Point(lastPunkt.X - punktlast.Intensity[0] * lastAuflösung,
                                -lastPunkt.Y + punktlast.Intensity[1] * lastAuflösung);
            pathFigure.StartPoint = endPunkt;

            pathFigure.Segments.Add(new LineSegment(lastPunkt, true));

            vector = lastPunkt - endPunkt;
            vector.Normalize();
            vector *= lastPfeilGroesse;
            vector = RotateVectorScreen(vector, 30);
            endPunkt = new Point(lastPunkt.X - vector.X, lastPunkt.Y - vector.Y);
            pathFigure.Segments.Add(new LineSegment(endPunkt, true));

            vector = RotateVectorScreen(vector, -60);
            endPunkt = new Point(lastPunkt.X - vector.X, lastPunkt.Y - vector.Y);
            pathFigure.Segments.Add(new LineSegment(endPunkt, false));
            pathFigure.Segments.Add(new LineSegment(lastPunkt, true));

            pathGeometry.Figures.Add(pathFigure);
            return pathGeometry;
        }
        private PathGeometry LinienlastZeichnen(AbstraktElementLast last)
        {
            var linienlast = (LinienLast)last;
            var pathGeometry = new PathGeometry();
            var pathFigure = new PathFigure();
            const int lastPfeilGroesse = 8;
            const int linienkraftÜberhöhung = 1;
            var linienLastAuflösung = linienkraftÜberhöhung * lastAuflösung;

            last.SetReferences(modell);
            if (modell.Elemente.TryGetValue(linienlast.ElementId, out var element)) { }
            if (element == null) return pathGeometry;

            if (modell.Knoten.TryGetValue(element.NodeIds[0], out var startKnoten)) { }
            var startPunkt = TransformKnoten(startKnoten, auflösung, maxY);

            // zweiter Elementknoten 
            if (modell.Knoten.TryGetValue(element.NodeIds[1], out var endKnoten)) { }
            var endPunkt = TransformKnoten(endKnoten, auflösung, maxY);
            var vector = endPunkt - startPunkt;

            pathFigure.StartPoint = startPunkt;

            var lastVektor = RotateVectorScreen(vector, -90);
            lastVektor.Normalize();
            var vec = lastVektor * linienLastAuflösung * linienlast.Intensity[1];
            var nextPunkt = new Point(startPunkt.X - vec.X, startPunkt.Y - vec.Y);

            lastVektor *= lastPfeilGroesse;
            lastVektor = RotateVectorScreen(lastVektor, -150);
            var punkt = new Point(startPunkt.X - lastVektor.X, startPunkt.Y - lastVektor.Y);
            pathFigure.Segments.Add(new LineSegment(punkt, true));

            lastVektor = RotateVectorScreen(lastVektor, -60);
            punkt = new Point(startPunkt.X - lastVektor.X, startPunkt.Y - lastVektor.Y);
            pathFigure.Segments.Add(new LineSegment(punkt, false));
            pathFigure.Segments.Add(new LineSegment(startPunkt, true));
            pathFigure.Segments.Add(new LineSegment(nextPunkt, true));

            lastVektor = RotateVectorScreen(vector, 90);
            lastVektor.Normalize();
            vec = lastVektor * linienLastAuflösung * linienlast.Intensity[1];
            nextPunkt = new Point(endPunkt.X + vec.X, endPunkt.Y + vec.Y);
            pathFigure.Segments.Add(new LineSegment(nextPunkt, true));
            pathFigure.Segments.Add(new LineSegment(endPunkt, true));

            lastVektor *= lastPfeilGroesse;
            lastVektor = RotateVectorScreen(lastVektor, 30);
            nextPunkt = new Point(endPunkt.X - lastVektor.X, endPunkt.Y - lastVektor.Y);
            pathFigure.Segments.Add(new LineSegment(nextPunkt, true));

            lastVektor = RotateVectorScreen(lastVektor, -60);
            nextPunkt = new Point(endPunkt.X - lastVektor.X, endPunkt.Y - lastVektor.Y);
            pathFigure.Segments.Add(new LineSegment(nextPunkt, false));
            pathFigure.Segments.Add(new LineSegment(endPunkt, true));
            pathFigure.Segments.Add(new LineSegment(startPunkt, false));

            pathGeometry.Figures.Add(pathFigure);

            return pathGeometry;
        }

        public void FesthaltungenZeichnen()
        {
            foreach (var item in modell.Randbedingungen)
            {
                var lager = item.Value;
                var pathGeometry = new PathGeometry();

                if (modell.Knoten.TryGetValue(lager.NodeId, out var lagerKnoten)) { }
                var drehPunkt = TransformKnoten(lagerKnoten, auflösung, maxY);
                double drehWinkel = 0;

                switch (lager.Type)
                {
                    // X_FIXED = 1, Y_FIXED = 2, R_FIXED = 4, XY_FIXED = 3, 
                    // XR_FIXED = 5, YR_FIXED = 6, XYR_FIXED = 7
                    case 1:
                        {
                            pathGeometry = EineFesthaltungZeichnen(lagerKnoten);
                            drehWinkel = 45;
                            if (lagerKnoten != null && (lagerKnoten.Coordinates[0] - minX) < (maxX - lagerKnoten.Coordinates[0])) drehWinkel = -45;
                            pathGeometry.Transform = new RotateTransform(drehWinkel, drehPunkt.X, drehPunkt.Y);
                            break;
                        }
                    case 2:
                        pathGeometry = EineFesthaltungZeichnen(lagerKnoten);
                        break;
                    case 3:
                        pathGeometry = ZweiFesthaltungenZeichnen(lagerKnoten);
                        break;
                    case 7:
                        {
                            pathGeometry = DreiFesthaltungenZeichnen(lagerKnoten);
                            if (lagerKnoten != null && (int)(lagerKnoten.Coordinates[1] - minY) == 0) drehWinkel = 0;
                            else if (lagerKnoten != null && (lagerKnoten.Coordinates[0] - minX) < (maxX - lagerKnoten.Coordinates[0])) drehWinkel = -45;
                            else if (lagerKnoten != null && (lagerKnoten.Coordinates[0] - minX) > (maxX - lagerKnoten.Coordinates[0])) drehWinkel = 45;
                            pathGeometry.Transform = new RotateTransform(drehWinkel, drehPunkt.X, drehPunkt.Y);
                            break;
                        }
                }

                Shape path = new Path()
                {
                    Stroke = Green,
                    StrokeThickness = 2,
                    Data = pathGeometry
                };
                LagerDarstellung.Add(path);

                // setz oben/links Position zum Zeichnen auf dem Canvas
                SetLeft(path, randLinks);
                SetTop(path, randOben);
                // zeichne Shape
                visualErgebnisse.Children.Add(path);
            }
        }
        private PathGeometry EineFesthaltungZeichnen(Knoten lagerKnoten)
        {
            var pathGeometry = new PathGeometry();
            var pathFigure = new PathFigure();
            const int lagerSymbol = 20;

            var startPoint = TransformKnoten(lagerKnoten, auflösung, maxY);
            pathFigure.StartPoint = startPoint;

            var endPoint = new Point(startPoint.X - lagerSymbol, startPoint.Y + lagerSymbol);
            pathFigure.Segments.Add(new LineSegment(endPoint, true));
            endPoint = new Point(endPoint.X + 2 * lagerSymbol, startPoint.Y + lagerSymbol);
            pathFigure.Segments.Add(new LineSegment(endPoint, true));
            pathFigure.Segments.Add(new LineSegment(startPoint, true));

            startPoint = new Point(endPoint.X + 5, endPoint.Y + 5);
            pathFigure.Segments.Add(new LineSegment(startPoint, false));
            endPoint = new Point(startPoint.X - 50, startPoint.Y);
            pathFigure.Segments.Add(new LineSegment(endPoint, true));

            pathGeometry.Figures.Add(pathFigure);
            return pathGeometry;
        }
        private PathGeometry ZweiFesthaltungenZeichnen(Knoten lagerKnoten)
        {
            var pathGeometry = new PathGeometry();
            var pathFigure = new PathFigure();
            const int lagerSymbol = 20;

            var startPoint = TransformKnoten(lagerKnoten, auflösung, maxY);
            pathFigure.StartPoint = startPoint;

            var endPoint = new Point(startPoint.X - lagerSymbol, startPoint.Y + lagerSymbol);
            pathFigure.Segments.Add(new LineSegment(endPoint, true));
            endPoint = new Point(endPoint.X + 2 * lagerSymbol, startPoint.Y + lagerSymbol);
            pathFigure.Segments.Add(new LineSegment(endPoint, true));
            pathFigure.Segments.Add(new LineSegment(startPoint, true));

            startPoint = endPoint;
            pathFigure.Segments.Add(new LineSegment(startPoint, false));
            endPoint = new Point(startPoint.X - 5, startPoint.Y + 5);
            pathFigure.Segments.Add(new LineSegment(endPoint, true));

            pathFigure.Segments.Add(new LineSegment(new Point(startPoint.X - 10, startPoint.Y), false));
            pathFigure.Segments.Add(new LineSegment(new Point(endPoint.X - 10, endPoint.Y), true));

            pathFigure.Segments.Add(new LineSegment(new Point(startPoint.X - 20, startPoint.Y), false));
            pathFigure.Segments.Add(new LineSegment(new Point(endPoint.X - 20, endPoint.Y), true));

            pathFigure.Segments.Add(new LineSegment(new Point(startPoint.X - 30, startPoint.Y), false));
            pathFigure.Segments.Add(new LineSegment(new Point(endPoint.X - 30, endPoint.Y), true));

            pathFigure.Segments.Add(new LineSegment(new Point(startPoint.X - 40, startPoint.Y), false));
            pathFigure.Segments.Add(new LineSegment(new Point(endPoint.X - 40, endPoint.Y), true));

            pathGeometry.Figures.Add(pathFigure);
            return pathGeometry;
        }
        private PathGeometry DreiFesthaltungenZeichnen(Knoten klagerKnoten)
        {
            var pathGeometry = new PathGeometry();
            var pathFigure = new PathFigure();
            const int lagerSymbol = 20;

            var startPoint = TransformKnoten(klagerKnoten, auflösung, maxY);

            startPoint = new Point(startPoint.X - lagerSymbol, startPoint.Y);
            pathFigure.StartPoint = startPoint;
            var endPoint = new Point(startPoint.X + 2 * lagerSymbol, startPoint.Y);
            pathFigure.Segments.Add(new LineSegment(endPoint, true));
            pathGeometry.Figures.Add(pathFigure);
            pathFigure = new PathFigure
            {
                StartPoint = startPoint
            };
            endPoint = new Point(startPoint.X - 10, startPoint.Y + 10);
            pathFigure.Segments.Add(new LineSegment(endPoint, true));
            pathGeometry.Figures.Add(pathFigure);
            for (var i = 0; i < 4; i++)
            {
                pathFigure = new PathFigure();
                startPoint = new Point(startPoint.X + 10, startPoint.Y);
                pathFigure.StartPoint = startPoint;
                endPoint = new Point(startPoint.X - 10, startPoint.Y + 10);
                pathFigure.Segments.Add(new LineSegment(endPoint, true));
                pathGeometry.Figures.Add(pathFigure);
            }
            return pathGeometry;
        }

        //public void Beschleunigungen_Zeichnen()
        //{
        //    var fensterPunkt = new int[2];
        //    var beschleunigungAuflösung = 0.5;
        //    foreach (var item in modell.Knoten)
        //    {
        //        knoten = item.Value;
        //        var pathGeometry = new PathGeometry();
        //        var pathFigure = new PathFigure();
        //        var verformt = TransformVerformtenKnoten(knoten, auflösung, maxY);
        //        pathFigure.StartPoint = verformt;

        //        fensterPunkt[0] = (int)(verformt.X - item.Value.NodalDerivatives[0][zeitschritt] * beschleunigungAuflösung);
        //        fensterPunkt[1] = (int)(verformt.Y + item.Value.NodalDerivatives[1][zeitschritt] * beschleunigungAuflösung);

        //        var beschleunigung = new Point(fensterPunkt[0], fensterPunkt[1]);
        //        pathFigure.Segments.Add(new LineSegment(beschleunigung, true));

        //        pathGeometry.Figures.Add(pathFigure);
        //        Shape path = new Path()
        //        {
        //            Stroke = Blue,
        //            StrokeThickness = 2,
        //            Data = pathGeometry
        //        };
        //        SetLeft(path, randLinks);
        //        SetTop(path, randOben);
        //        visualErgebnisse.Children.Add(path);
        //        Beschleunigungen.Add(path);
        //    }
        //}

        public void Normalkraft_Zeichnen(AbstraktBalken element, double maxNormalkraft, bool elementlast)
        {
            var barEndForces = element.ElementState;
            var normalkraft1Skaliert = barEndForces[0] / maxNormalkraft * MaxNormalkraftScreen;
            double normalkraft2Skaliert;
            if (barEndForces.Length == 2)
            {
                normalkraft2Skaliert = barEndForces[1] / maxNormalkraft * MaxNormalkraftScreen;
            }
            else
            {
                normalkraft2Skaliert = barEndForces[3] / maxNormalkraft * MaxNormalkraftScreen;
            }

            Point nextPoint;
            Vector vec, vec2;
            var rot = FromArgb(120, 255, 0, 0);
            var blau = FromArgb(120, 0, 0, 255);

            if (modell.Knoten.TryGetValue(element.NodeIds[0], out knoten)) { }
            var startPoint = TransformKnoten(knoten, auflösung, maxY);

            if (modell.Knoten.TryGetValue(element.NodeIds[1], out knoten)) { }
            var endPoint = TransformKnoten(knoten, auflösung, maxY);

            if (!elementlast)
            {
                var pathGeometry = new PathGeometry();
                var pathFigure = new PathFigure();

                var myBrush = new SolidColorBrush(blau);
                if (normalkraft1Skaliert < 0) myBrush = new SolidColorBrush(rot);

                pathFigure.StartPoint = startPoint;
                vec = endPoint - startPoint;
                vec.Normalize();
                vec2 = RotateVectorScreen(vec, -90);
                nextPoint = startPoint + vec2 * normalkraft1Skaliert;
                pathFigure.Segments.Add(new LineSegment(nextPoint, true));
                nextPoint = endPoint - vec2 * normalkraft2Skaliert;
                pathFigure.Segments.Add(new LineSegment(nextPoint, true));
                pathFigure.Segments.Add(new LineSegment(endPoint, true));
                pathFigure.IsClosed = true;
                pathGeometry.Figures.Add(pathFigure);

                Shape path = new Path()
                {
                    Fill = myBrush,
                    Stroke = Black,
                    StrokeThickness = 1,
                    Data = pathGeometry
                };
                SetLeft(path, randLinks);
                SetTop(path, plazierungV);
                visualErgebnisse.Children.Add(path);
                NormalkraftListe.Add(path);
            }
            else
            {
                // Anteil einer Punktlast
                double punktLastN = 0, punktLastO = 0;
                IEnumerable<PunktLast> PunktLasten()
                {
                    foreach (var last in modell.PunktLasten.Select(item => (PunktLast)item.Value)
                        .Where(last => last.ElementId == element.ElementId))
                    {
                        yield return last;
                    }
                }
                foreach (var punktLast in PunktLasten())
                {
                    punktLastN = punktLast.Intensity[0];
                    punktLastO = punktLast.Offset;
                }

                // Anteil einer Linienlast
                IEnumerable<LinienLast> LinienLasten()
                {
                    foreach (var item in modell.ElementLasten)
                    {
                        if (item.Value is LinienLast linienLast && item.Value.ElementId == element.ElementId)
                        {
                            yield return linienLast;
                        }
                    }
                }
                foreach (var linienLast in LinienLasten())
                {
                    var pathGeometry = new PathGeometry();
                    var pathFigure = new PathFigure();

                    var myBrush = new SolidColorBrush(blau);
                    if (normalkraft1Skaliert < 0) myBrush = new SolidColorBrush(rot);

                    pathFigure.StartPoint = startPoint;
                    vec = endPoint - startPoint;
                    vec.Normalize();
                    vec2 = RotateVectorScreen(vec, -90);
                    nextPoint = startPoint + vec2 * normalkraft1Skaliert;
                    pathFigure.Segments.Add(new LineSegment(nextPoint, true));

                    if (punktLastO > double.Epsilon)
                    {
                        nextPoint += punktLastO * (endPoint - startPoint);

                        var na = linienLast.Intensity[0];
                        var nb = linienLast.Intensity[2];
                        var konstant = na * punktLastO * element.length;
                        var linear = (nb - na) * punktLastO / 2 * element.length;
                        if (nb < na)
                        {
                            konstant = nb * punktLastO * element.length;
                            linear = (na - nb) * (1 - punktLastO) / 2 * element.length;
                        }
                        nextPoint += vec2 * (konstant + linear) / maxNormalkraft * MaxNormalkraftScreen;
                        pathFigure.Segments.Add(new LineSegment(nextPoint, true));
                        nextPoint += vec2 * punktLastN / maxNormalkraft * MaxNormalkraftScreen;
                        pathFigure.Segments.Add(new LineSegment(nextPoint, true));
                    }
                    nextPoint = endPoint - vec2 * normalkraft2Skaliert;
                    pathFigure.Segments.Add(new LineSegment(nextPoint, true));
                    pathFigure.Segments.Add(new LineSegment(endPoint, true));
                    pathFigure.IsClosed = true;
                    pathGeometry.Figures.Add(pathFigure);

                    Shape path = new Path()
                    {
                        Fill = myBrush,
                        Stroke = Black,
                        StrokeThickness = 1,
                        Data = pathGeometry
                    };
                    SetLeft(path, randLinks);
                    SetTop(path, plazierungV);
                    visualErgebnisse.Children.Add(path);
                    NormalkraftListe.Add(path);
                }
            }
        }
        public void Querkraft_Zeichnen(AbstraktBalken element, double maxQuerkraft, bool elementlast)
        {
            var barEndForces = element.ElementState;
            var querkraft1Skaliert = barEndForces[1] / maxQuerkraft * MaxQuerkraftScreen;
            var querkraft2Skaliert = barEndForces[4] / maxQuerkraft * MaxQuerkraftScreen;

            Point nextPoint;
            Vector vec, vec2;
            var rot = FromArgb(120, 255, 0, 0);
            var blau = FromArgb(120, 0, 0, 255);
            SolidColorBrush myBrush;

            if (modell.Knoten.TryGetValue(element.NodeIds[0], out Knoten startKnoten)) { }
            var startPoint = TransformKnoten(startKnoten, auflösung, maxY);

            if (modell.Knoten.TryGetValue(element.NodeIds[1], out Knoten endKnoten)) { }
            var endPoint = TransformKnoten(endKnoten, auflösung, maxY);

            if (!elementlast)
            {
                var pathGeometry = new PathGeometry();
                var pathFigure = new PathFigure();

                myBrush = new SolidColorBrush(blau);
                if (querkraft1Skaliert < 0) myBrush = new SolidColorBrush(rot);

                pathFigure.StartPoint = startPoint;
                vec = endPoint - startPoint;
                vec.Normalize();
                vec2 = RotateVectorScreen(vec, -90);
                nextPoint = startPoint + vec2 * querkraft1Skaliert;
                pathFigure.Segments.Add(new LineSegment(nextPoint, true));
                nextPoint = endPoint + vec2 * querkraft1Skaliert;
                pathFigure.Segments.Add(new LineSegment(nextPoint, true));
                pathFigure.Segments.Add(new LineSegment(endPoint, true));
                pathFigure.IsClosed = true;
                pathGeometry.Figures.Add(pathFigure);

                Shape path = new Path()
                {
                    Fill = myBrush,
                    Stroke = Black,
                    StrokeThickness = 1,
                    Data = pathGeometry
                };
                SetLeft(path, randLinks);
                SetTop(path, plazierungV);
                visualErgebnisse.Children.Add(path);
                QuerkraftListe.Add(path);
            }
            // Element hat 1 Punkt- und/oder 1 Linienlast
            else
            {
                // test, ob element 1 Punktlast hat
                bool balkenPunktlast = false, balkenGleichlast = false;
                double punktLastQ = 0, punktLastO = 0;
                AbstraktElementLast linienLast = null;

                foreach (var item in modell.PunktLasten)
                {
                    if (item.Value is PunktLast last && item.Value.ElementId == element.ElementId)
                    {
                        balkenPunktlast = true;
                        punktLastQ = last.Intensity[1];
                        punktLastO = last.Offset;
                        break;
                    }
                }

                // test, ob element 1 Linienlast hat
                foreach (var item in modell.ElementLasten)
                {
                    if (item.Value is LinienLast last && item.Value.ElementId == element.ElementId)
                    {
                        balkenGleichlast = true;
                        linienLast = last;
                        break;
                    }
                }
                // nur 1 Punktlast auf dem Balken und keine Gleichlast
                if (balkenPunktlast && !balkenGleichlast)
                {
                    var pathGeometry = new PathGeometry();
                    var pathFigure = new PathFigure();

                    myBrush = new SolidColorBrush(blau);
                    if (querkraft1Skaliert < 0) myBrush = new SolidColorBrush(rot);

                    pathFigure.StartPoint = startPoint;
                    vec = endPoint - startPoint;
                    vec.Normalize();
                    vec2 = RotateVectorScreen(vec, -90);
                    nextPoint = startPoint + vec2 * querkraft1Skaliert;
                    pathFigure.Segments.Add(new LineSegment(nextPoint, true));

                    nextPoint += punktLastO * (endPoint - startPoint);
                    pathFigure.Segments.Add(new LineSegment(nextPoint, true));

                    startPoint += punktLastO * (endPoint - startPoint);
                    pathFigure.Segments.Add(new LineSegment(startPoint, true));
                    pathFigure.IsClosed = true;
                    pathGeometry.Figures.Add(pathFigure);
                    Shape path = new Path()
                    {
                        Fill = myBrush,
                        Stroke = Black,
                        StrokeThickness = 1,
                        Data = pathGeometry
                    };
                    SetLeft(path, randLinks);
                    SetTop(path, plazierungV);
                    visualErgebnisse.Children.Add(path);
                    QuerkraftListe.Add(path);

                    pathGeometry = new PathGeometry();
                    pathFigure = new PathFigure();
                    myBrush = new SolidColorBrush(blau);
                    if (querkraft1Skaliert + punktLastQ / maxQuerkraft * MaxQuerkraftScreen < 0)
                    {
                        myBrush = new SolidColorBrush(rot);
                    }
                    pathFigure.StartPoint = startPoint;
                    nextPoint += vec2 * punktLastQ / maxQuerkraft * MaxQuerkraftScreen;
                    pathFigure.Segments.Add(new LineSegment(nextPoint, true));

                    nextPoint = endPoint - vec2 * querkraft2Skaliert;
                    pathFigure.Segments.Add(new LineSegment(nextPoint, true));

                    pathFigure.Segments.Add(new LineSegment(endPoint, true));
                    pathFigure.IsClosed = true;
                    pathGeometry.Figures.Add(pathFigure);

                    path = new Path()
                    {
                        Fill = myBrush,
                        Stroke = Black,
                        StrokeThickness = 1,
                        Data = pathGeometry
                    };
                    SetLeft(path, randLinks);
                    SetTop(path, plazierungV);
                    visualErgebnisse.Children.Add(path);
                    QuerkraftListe.Add(path);
                }
                // 1 Gleichlast auf dem Balken und ggf. 1 Punktlast zusätzlich
                else if (balkenGleichlast)
                {
                    var pathGeometry = new PathGeometry();
                    var pathFigure = new PathFigure();
                    Shape path;

                    myBrush = new SolidColorBrush(blau);
                    if (querkraft1Skaliert < 0) myBrush = new SolidColorBrush(rot);

                    pathFigure.StartPoint = startPoint;
                    vec = endPoint - startPoint;
                    vec.Normalize();
                    vec2 = RotateVectorScreen(vec, -90);
                    nextPoint = startPoint + vec2 * querkraft1Skaliert;
                    pathFigure.Segments.Add(new LineSegment(nextPoint, true));

                    if (punktLastO > double.Epsilon)
                    {
                        nextPoint += punktLastO * (endPoint - startPoint);
                        double lastAbstand = punktLastO * element.length;
                        double qa = linienLast.Intensity[1];
                        double qb = linienLast.Intensity[3];
                        double konstant = qa * lastAbstand;
                        double linear = (qb - qa) * lastAbstand / 2;
                        if (qb < qa)
                        {
                            konstant = qb * lastAbstand;
                            linear = (qa - qb) * (1 - punktLastO) * element.length / 2;
                        }
                        nextPoint += vec2 * (konstant + linear) / maxQuerkraft * MaxQuerkraftScreen;
                        pathFigure.Segments.Add(new LineSegment(nextPoint, true));
                        startPoint += punktLastO * (endPoint - startPoint);
                        //nextPoint += vec2 * punktLastQ / maxQuerkraft * maxQuerkraftScreen;
                        pathFigure.Segments.Add(new LineSegment(startPoint, true));
                        pathFigure.IsClosed = true;
                        pathGeometry.Figures.Add(pathFigure);

                        path = new Path()
                        {
                            Fill = myBrush,
                            Stroke = Black,
                            StrokeThickness = 1,
                            Data = pathGeometry
                        };
                        SetLeft(path, randLinks);
                        SetTop(path, plazierungV);
                        visualErgebnisse.Children.Add(path);
                        QuerkraftListe.Add(path);

                        pathGeometry = new PathGeometry();
                        pathFigure = new PathFigure();
                        if (querkraft1Skaliert + punktLastQ / maxQuerkraft * MaxQuerkraftScreen > 0)
                        {
                            myBrush = new SolidColorBrush(rot);
                        }
                        pathFigure.StartPoint = startPoint;
                        nextPoint += vec2 * punktLastQ / maxQuerkraft * MaxQuerkraftScreen;
                        pathFigure.Segments.Add(new LineSegment(nextPoint, true));
                    }
                    nextPoint = endPoint - vec2 * querkraft2Skaliert;
                    pathFigure.Segments.Add(new LineSegment(nextPoint, true));
                    pathFigure.Segments.Add(new LineSegment(endPoint, true));
                    pathFigure.IsClosed = true;
                    pathGeometry.Figures.Add(pathFigure);

                    path = new Path()
                    {
                        Fill = myBrush,
                        Stroke = Black,
                        StrokeThickness = 1,
                        Data = pathGeometry
                    };
                    SetLeft(path, randLinks);
                    SetTop(path, plazierungV);
                    visualErgebnisse.Children.Add(path);
                    QuerkraftListe.Add(path);
                }
            }
        }
        public void Momente_Zeichnen(AbstraktBalken element, double maxMoment, bool elementlast)
        {
            var stabEndKräfte = element.ElementState;
            var moment1Skaliert = stabEndKräfte[2] / maxMoment * MaxMomentScreen;
            var moment2Skaliert = stabEndKräfte[5] / maxMoment * MaxMomentScreen;

            var rot = FromArgb(120, 255, 0, 0);
            var blau = FromArgb(120, 0, 0, 255);

            if (modell.Knoten.TryGetValue(element.NodeIds[0], out knoten)) { }
            var startPunkt = TransformKnoten(knoten, auflösung, maxY);

            if (modell.Knoten.TryGetValue(element.NodeIds[1], out knoten)) { }
            var endPunkt = TransformKnoten(knoten, auflösung, maxY);

            double punktLastO = 0;
            var pathGeometry = new PathGeometry();
            var pathFigure = new PathFigure();

            var myBrush = new SolidColorBrush(blau);
            if (moment1Skaliert < 0) myBrush = new SolidColorBrush(rot);

            pathFigure.StartPoint = startPunkt;
            var vec = endPunkt - startPunkt;
            vec.Normalize();

            // Linie von start nach Moment1 skaliert
            var vec2 = RotateVectorScreen(vec, 90);
            var nächsterPunkt = startPunkt + vec2 * moment1Skaliert;
            pathFigure.Segments.Add(new LineSegment(nächsterPunkt, true));

            // nur Knotenlasten, keine Punkt-/Linienlasten, d.h. nur Stabendkräfte
            if (!elementlast)
            {
                //Linie von Moment1 skaliert nach Moment2 skaliert
                nächsterPunkt = endPunkt + vec2 * moment2Skaliert;
                pathFigure.Segments.Add(new LineSegment(nächsterPunkt, true));

                // Linie nach end und anschliessend pathFigure schliessen
                pathFigure.Segments.Add(new LineSegment(endPunkt, true));
                pathFigure.IsClosed = true;
                pathGeometry.Figures.Add(pathFigure);

                Shape path = new Path()
                {
                    Fill = myBrush,
                    Stroke = Black,
                    StrokeThickness = 1,
                    Data = pathGeometry
                };
                SetLeft(path, randLinks);
                SetTop(path, plazierungV);
                visualErgebnisse.Children.Add(path);
                MomenteListe.Add(path);
            }

            // Elementlasten (Linienlast, Punktlast) vorhanden
            // Element hat Punkt- und/oder Linienlast
            else
            {
                bool elementHatPunktLast = false, elementHatLinienLast = false;
                LinienLast linienLast = null;

                // finde Punktlast auf Balkenelement
                foreach (var item in modell.PunktLasten)
                {
                    if (!(item.Value is PunktLast last) || item.Value.ElementId != element.ElementId) continue;
                    punktLastO = last.Offset;
                    elementHatPunktLast = true;
                    break;
                }

                var maxPunkt = new Point(0, 0);
                double mmax = 0;

                // finde Linienlast auf Balkenelement
                foreach (var item in modell.ElementLasten)
                {
                    if (!(item.Value is LinienLast last) || item.Value.ElementId != element.ElementId) continue;
                    linienLast = last;
                    elementHatLinienLast = true;
                    break;
                }

                // zeichne Momentenlinie, nur Punkt-, keine Linienlast
                if (elementHatPunktLast && !elementHatLinienLast)
                {
                    // Linie von Moment1 skaliert nach Mmax skaliert
                    mmax = stabEndKräfte[2] + stabEndKräfte[1] * element.length / 2;
                    var mmaxSkaliert = mmax / maxMoment * MaxMomentScreen;
                    maxPunkt = startPunkt + vec * punktLastO * element.length;
                    var maxPunktSkaliert = maxPunkt;
                    maxPunktSkaliert.X *= auflösung;
                    maxPunktSkaliert.Y = (maxPunkt.Y + maxY) * auflösung;
                    nächsterPunkt = maxPunktSkaliert + vec2 * mmaxSkaliert;
                    pathFigure.Segments.Add(new LineSegment(nächsterPunkt, true));

                    //Linie von Mmax skaliert nach Moment2 skaliert
                    nächsterPunkt = endPunkt + vec2 * moment2Skaliert;
                    pathFigure.Segments.Add(new LineSegment(nächsterPunkt, true));

                    // Linie nach end und anschliessend pathFigure schliessen
                    pathFigure.Segments.Add(new LineSegment(endPunkt, true));
                }

                // zeichne Momentenlinie unter Gleich- oder Dreieckslast ggf. mit Punktlast
                else if (elementHatLinienLast)  // Element hat Linienlast
                {
                    double qa = linienLast.Intensity[1];
                    double qb = linienLast.Intensity[3];
                    double l = element.length;
                    const double controlOffset = 2;
                    double abstandMmax = Math.Abs(stabEndKräfte[1] / (qa * 2 / 3 + qb * 1 / 3));
                    var konstant = qa * abstandMmax;
                    var linear = (qb - qa) * abstandMmax / 2;
                    mmax = stabEndKräfte[2] + stabEndKräfte[1] * abstandMmax + konstant * abstandMmax / 2 +
                           linear * abstandMmax / 6;

                    // nur Linien-, keine Punktlast, Momentenlinie als 1 quadratisches Bezier-Segment
                    if (!elementHatPunktLast)
                    {
                        // maxPunkt als maximales Moment, Kontrollpunkt durch Überhöhung (controlOffset) des max. Momentes
                        maxPunkt = startPunkt + abstandMmax/element.length * (endPunkt - startPunkt)
                                   + vec2 * controlOffset * mmax / maxMoment * MaxMomentScreen;
                        nächsterPunkt = endPunkt + vec2 * moment2Skaliert;
                        pathFigure.Segments.Add(new QuadraticBezierSegment(maxPunkt, nächsterPunkt, true));
                        pathFigure.Segments.Add(new LineSegment(endPunkt, true));
                    }

                    // Element hat Linien- und Punktlast
                    else
                    {
                        double max1, max2, abstand;
                        var abstandPunktlast = punktLastO * element.length;
                        abstandMmax = Math.Abs(stabEndKräfte[1] / (qa * 2 / 3 + qb * 1 / 3));

                        // Unstetigkeit an Punktlast, Momentenlinie durch 2 quadratrische Bezier-Segmente
                        // qa <= qb   Gleichlast oder Dreieckslast linear steigend
                        if ((Math.Abs(qa) - Math.Abs(qb)) < double.Epsilon)
                        {
                            if (abstandMmax > abstandPunktlast) abstandMmax = abstandPunktlast;

                            // M(x) = Ma + Qa * x + qa * x * x/2 + (qb - qa) * x/2 * x/3
                            mmax = stabEndKräfte[2] + stabEndKräfte[1] * abstandMmax + konstant * abstandMmax / 2 +
                                   linear * abstandMmax / 6;

                            abstand = abstandMmax / 2;
                            max1 = stabEndKräfte[2] + stabEndKräfte[1] * abstand
                                                    + qa * abstand * abstand / 4
                                                    +(qb - qa) * abstand * abstand / 24;
                            abstand = abstandMmax + (l - abstandMmax) / 2;
                            max2 = stabEndKräfte[5] + stabEndKräfte[4] * abstand / 2
                                                    + qa * abstand * abstand / 4
                                                    +(qb - qa) * abstand * abstand / 24;
                        }

                        // Dreieckslast linear fallend
                        else
                        {
                            if (abstandMmax > abstandPunktlast) abstandMmax = abstandPunktlast;

                            // M(x) = Ma + Qa * x + qa * x * x/2 + (qb - qa) * x/2 * x*2/3
                            mmax = stabEndKräfte[2] + stabEndKräfte[1] * abstandMmax + konstant * abstandMmax / 2 +
                                   linear * abstandMmax / 3;
                            abstand = (l - abstandMmax) / 2;
 
                            max2 = stabEndKräfte[5] + stabEndKräfte[4] * abstand
                                                    + qb * abstand * abstand / 4
                                                    +(qa - qb) * abstand * abstand / 24;
                            abstand = (l - abstandMmax) + abstandMmax / 2;
                            max1 = stabEndKräfte[2] + stabEndKräfte[1] * abstand / 2
                                                    + qb * abstand * abstand / 4
                                                    +(qa - qb) * abstand * abstand / 24;
                        }

                        var controlPoint = startPunkt + punktLastO * (endPunkt - startPunkt) / 2
                                           + vec2 * controlOffset * max1 / maxMoment * MaxMomentScreen;

                        maxPunkt = startPunkt + punktLastO * (endPunkt - startPunkt) +
                                   vec2 * mmax / maxMoment * MaxMomentScreen;

                        var controlPoint2 = maxPunkt + punktLastO * (endPunkt - maxPunkt)
                                            + vec2 * controlOffset * max2 / maxMoment * MaxMomentScreen;

                        nächsterPunkt = endPunkt + vec2 * moment2Skaliert;
                        // Startpunkt ist Endpunkt in PathFigure
                        // Kontrollpunkt1 für Auslenkung im 1. Segment, maxPunkt am Ende des 1. Segments,
                        // Kontrollpunkt2 für Auslenkung im 2. Segment, Endpunkt der Kurve
                        var bezierPoints = new PointCollection(4)
                        {
                            controlPoint,
                            maxPunkt,
                            controlPoint2,
                            nächsterPunkt
                        };
                        pathFigure.Segments.Add(new PolyQuadraticBezierSegment(bezierPoints, true));
                        pathFigure.Segments.Add(new LineSegment(endPunkt, true));
                    }
                }

                pathFigure.IsClosed = true;
                pathGeometry.Figures.Add(pathFigure);

                Shape path = new Path()
                {
                    Fill = myBrush,
                    Stroke = Black,
                    StrokeThickness = 1,
                    Data = pathGeometry
                };
                SetLeft(path, randLinks);
                SetTop(path, plazierungV);
                visualErgebnisse.Children.Add(path);
                MomenteListe.Add(path);

                maxMomentText = new TextBlock
                {
                    FontSize = 12,
                    Text = "max Moment = " + mmax.ToString("N2"),
                    Foreground = Blue
                };
                SetTop(maxMomentText, maxPunkt.Y + plazierungV);
                SetLeft(maxMomentText, maxPunkt.X);
                visualErgebnisse.Children.Add(maxMomentText);
            }
        }

        public void Koordinatensystem(double tmax, double max, double min)
        {
            const int rand = 20;
            screenH = visualErgebnisse.ActualWidth;
            screenV = visualErgebnisse.ActualHeight;
            auflösungV = (screenV - rand) / (max - min);
            auflösungH = (screenH - rand) / tmax;
            var xAchse = new Line
            {
                Stroke = Black,
                X1 = 0,
                Y1 = max * auflösungV + randOben,
                X2 = tmax * auflösungH,
                Y2 = max * auflösungV + randOben,
                StrokeThickness = 2
            };
            visualErgebnisse.Children.Add(xAchse);
            var yAchse = new Line
            {
                Stroke = Black,
                X1 = randLinks,
                Y1 = max * auflösungV - min * auflösungV + 2 * randOben,
                X2 = randLinks,
                Y2 = randOben,
                StrokeThickness = 2
            };
            visualErgebnisse.Children.Add(yAchse);
        }
        public void ZeitverlaufZeichnen(double dt, double tmax, double mY, double[] ordinaten)
        {
            var nSteps = (int)(tmax / dt) + 1;
            var zeitverlauf = new Polyline
            {
                Stroke = Red,
                StrokeThickness = 2
            };
            var stützpunkte = new PointCollection();
            for (var i = 0; i < nSteps; i++)
            {
                var point = new Point(dt * i * auflösungH, -ordinaten[i] * auflösungV);
                stützpunkte.Add(point);
            }
            zeitverlauf.Points = stützpunkte;

            // setz oben/links Position zum Zeichnen auf dem Canvas
            SetLeft(zeitverlauf, randLinks);
            SetTop(zeitverlauf, mY * auflösungV + randOben);
            // zeichne Shape
            visualErgebnisse.Children.Add(zeitverlauf);
        }

        private static Vector RotateVectorScreen(Vector vec, double winkel)  // clockwise in degree
        {
            var vector = vec;
            var angle = winkel * Math.PI / 180.0;
            return new Vector(vector.X * Math.Cos(angle) - vector.Y * Math.Sin(angle), vector.X * Math.Sin(angle) + vector.Y * Math.Cos(angle));
        }
        private static Point TransformKnoten(Knoten knoten, double auflösung, double maxY)
        {
            return new Point(knoten.Coordinates[0] * auflösung, (-knoten.Coordinates[1] + maxY) * auflösung);
        }
        private Point TransformVerformtenKnoten(Knoten verformt, double resolution, double max)
        {
            // eingabeEinheit z.B. in m, verformungsEinheit z.B. cm --> Überhöhung
            return new Point((verformt.Coordinates[0] + verformt.NodalDof[0] * überhöhungVerformung) * resolution,
                             (-verformt.Coordinates[1] - verformt.NodalDof[1] * überhöhungVerformung + max) * resolution);
        }
    }
}