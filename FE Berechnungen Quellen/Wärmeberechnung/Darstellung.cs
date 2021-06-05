using FEALibrary.Modell;
using FEALibrary.Modell.abstrakte_Klassen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using static System.Globalization.CultureInfo;
using static System.Windows.Controls.Canvas;
using static System.Windows.Media.Brushes;
using static System.Windows.Media.Color;

namespace FE_Berechnungen.Wärmeberechnung
{
    public class Darstellung
    {
        private readonly FEModell modell;
        private AbstraktElement element;
        private Knoten knoten;
        private readonly Canvas visualErgebnisse;
        private double maxX;
        private double screenH, screenV;
        private readonly double maxScreenLength = 40;
        public double auflösung;
        private double auflösungH, auflösungV;
        public double maxY;
        private double temp;
        private double minTemp = 100;
        private double maxTemp;
        private const int RandOben = 10;
        private const int RandLinks = 10;
        public List<object> ElementIDs { get; }
        public List<object> KnotenIDs { get; }
        public List<object> LastKnoten { get; }
        public List<object> LastElemente { get; }
        public List<TextBlock> Knotentemperaturen { get; }
        public List<TextBlock> Knotengradienten { get; }
        public List<Shape> TemperaturElemente { get; }
        public List<object> WärmeVektoren { get; }
        public List<object> RandKnoten { get; }
        private double vektorskalierung, vektorLänge, vektorWinkel;
        public int zeitschritt;

        public Darstellung(FEModell feModell, Canvas visual)
        {
            modell = feModell;
            visualErgebnisse = visual;
            KnotenIDs = new List<object>();
            ElementIDs = new List<object>();
            LastKnoten = new List<object>();
            LastElemente = new List<object>();
            Knotentemperaturen = new List<TextBlock>();
            Knotengradienten = new List<TextBlock>();
            TemperaturElemente = new List<Shape>();
            WärmeVektoren = new List<object>();
            RandKnoten = new List<object>();
            FestlegungAuflösung();
        }

        public void FestlegungAuflösung()
        {
            const int rand = 100;
            screenH = visualErgebnisse.ActualWidth;
            screenV = visualErgebnisse.ActualHeight;

            foreach (var item in modell.Knoten)
            {
                knoten = item.Value;
                if (knoten.Coordinates[0] > maxX) maxX = knoten.Coordinates[0];
                if (knoten.Coordinates[1] > maxY) maxY = knoten.Coordinates[1];
            }
            if (screenH / maxX < screenV / maxY) auflösung = (screenH - rand) / maxX;
            else auflösung = (screenV - rand) / maxY;
        }
        public void KnotenTexte()
        {
            foreach (var item in modell.Knoten)
            {
                var id = new TextBlock
                {
                    FontSize = 12,
                    Text = item.Key,
                    Foreground = Red
                };
                SetTop(id, (-item.Value.Coordinates[1] + maxY) * auflösung + RandOben);
                SetLeft(id, item.Value.Coordinates[0] * auflösung + RandLinks);
                visualErgebnisse.Children.Add(id);
                KnotenIDs.Add(id);
            }
        }
        public void ElementTexte()
        {
            foreach (var item in modell.Elemente)
            {
                var abstract2D = (Abstrakt2D)item.Value;
                var cg = abstract2D.ComputeCenterOfGravity();
                var id = new TextBlock
                {
                    FontSize = 12,
                    Text = item.Key,
                    Foreground = Blue
                };
                SetTop(id, (-cg.Y + maxY) * auflösung + RandOben);
                SetLeft(id, cg.X * auflösung + RandLinks);
                visualErgebnisse.Children.Add(id);
                ElementIDs.Add(id);
            }
        }

        public void ElementeZeichnen()
        {
            foreach (var item in modell.Elemente)
            {
                element = item.Value;
                var pathGeometry = AktElementZeichnen(element);

                Shape path = new Path()
                {
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                    Data = pathGeometry
                };
                // setz oben/links Position zum Zeichnen auf dem Canvas
                SetLeft(path, RandLinks);
                SetTop(path, RandOben);
                // zeichne Shape
                visualErgebnisse.Children.Add(path);
            }
        }
        private PathGeometry AktElementZeichnen(AbstraktElement aktElement)
        {
            var pathFigure = new PathFigure();
            var pathGeometry = new PathGeometry();

            if (modell.Knoten.TryGetValue(aktElement.NodeIds[0], out knoten)) { }
            //fensterKnoten = TransformKnoten(knoten, auflösung, maxY);
            var startPoint = TransformKnoten(knoten, auflösung, maxY);
            pathFigure.StartPoint = startPoint;
            for (var i = 1; i < aktElement.NodeIds.Length; i++)
            {
                if (modell.Knoten.TryGetValue(aktElement.NodeIds[i], out knoten)) { }
                var nextPoint = TransformKnoten(knoten, auflösung, maxY);
                pathFigure.Segments.Add(new LineSegment(nextPoint, true));
            }
            pathFigure.IsClosed = true;
            pathGeometry.Figures.Add(pathFigure);
            return pathGeometry;
        }

        public void KnotenlastenZeichnen()
        {
            foreach (var item in modell.Lasten)
            {
                var knotenId = item.Value.NodeId;
                var lastWert = item.Value.Intensity[0];
                if (modell.Knoten.TryGetValue(knotenId, out knoten)) { }
                var lastPunkt = TransformKnoten(knoten, auflösung, maxY);
                var knotenLast = new TextBlock
                {
                    FontSize = 12,
                    Text = lastWert.ToString(CurrentCulture),
                    Foreground = Red
                };
                SetTop(knotenLast, lastPunkt.Y + RandOben + 10);
                SetLeft(knotenLast, lastPunkt.X + RandLinks);

                LastKnoten.Add(knotenLast);
                visualErgebnisse.Children.Add(knotenLast);
            }
        }
        public void ElementlastenZeichnen()
        {
            foreach (var item in modell.ElementLasten)
            {
                if (modell.Elemente.TryGetValue(item.Value.ElementId, out element)) { }
                var pathGeometry = AktElementZeichnen((Abstrakt2D)element);

                var mySolidColorBrush = new SolidColorBrush(Colors.Red) { Opacity = .2 };
                var lastElement = new Path()
                {
                    Stroke = Black,
                    StrokeThickness = 1,
                    Fill = mySolidColorBrush,
                    Data = pathGeometry
                };
                LastElemente.Add(lastElement);
                // setz oben/links Position zum Zeichnen auf dem Canvas
                SetLeft(lastElement, RandLinks);
                SetTop(lastElement, RandOben);
                // zeichne Shape
                visualErgebnisse.Children.Add(lastElement);
            }
        }
        public void RandbedingungenZeichnen()
        {
            // zeichne den Wert einer jeden Randbedingung als Text an Randknoten
            foreach (var item in modell.Randbedingungen)
            {
                var knotenId = item.Value.NodeId;
                if (modell.Knoten.TryGetValue(knotenId, out knoten)) { }
                var fensterKnoten = TransformKnoten(knoten, auflösung, maxY);

                var randWert = item.Value.Prescribed[0];
                var randbedingung = new TextBlock
                {
                    FontSize = 12,
                    Text = randWert.ToString("N2"),
                    //Foreground = Brushes.DarkOliveGreen
                    Background = LightBlue
                };
                RandKnoten.Add(randbedingung);
                SetTop(randbedingung, fensterKnoten.Y + RandOben + 15);
                SetLeft(randbedingung, fensterKnoten.X + RandLinks);
                visualErgebnisse.Children.Add(randbedingung);
            }
        }

        public void KnotentemperaturZeichnen()
        {
            foreach (var item in modell.Knoten)
            {
                knoten = item.Value;
                var temperatur = knoten.NodalDof[0].ToString("N2");
                temp = knoten.NodalDof[0];
                if (temp > maxTemp) maxTemp = temp;
                if (temp < minTemp) minTemp = temp;
                var fensterKnoten = TransformKnoten(knoten, auflösung, maxY);

                var id = new TextBlock
                {
                    FontSize = 12,
                    Background = LightGray,
                    FontWeight = FontWeights.Bold,
                    Text = temperatur
                };
                Knotentemperaturen.Add(id);
                SetTop(id, fensterKnoten.Y + RandOben);
                SetLeft(id, fensterKnoten.X + RandLinks);
                visualErgebnisse.Children.Add(id);
            }
        }
        public void KnotentemperaturZeichnen(int index)
        {
            foreach (var item in modell.Knoten)
            {
                knoten = item.Value;
                var temperatur = knoten.NodalVariables[0][index].ToString("N2");
                temp = knoten.NodalDof[0];
                if (temp > maxTemp) maxTemp = temp;
                if (temp < minTemp) minTemp = temp;
                var fensterKnoten = TransformKnoten(knoten, auflösung, maxY);

                var id = new TextBlock
                {
                    FontSize = 12,
                    Background = LightGray,
                    FontWeight = FontWeights.Bold,
                    Text = temperatur
                };
                Knotentemperaturen.Add(id);
                SetTop(id, fensterKnoten.Y + RandOben);
                SetLeft(id, fensterKnoten.X + RandLinks);
                visualErgebnisse.Children.Add(id);
            }
        }

        public void ElementTemperaturZeichnen()
        {
            foreach (var item in modell.Knoten)
            {
                knoten = item.Value;
                temp = knoten.NodalDof[0];
                if (temp > maxTemp) maxTemp = temp;
                if (temp < minTemp) minTemp = temp;
            }

            foreach (var item in modell.Elemente)
            {
                element = item.Value;
                var pathGeometry = AktElementZeichnen((Abstrakt2D)element);
                var elementTemperature = element.NodeIds.Where(knotenId
                    => modell.Knoten.TryGetValue(knotenId, out knoten)).Sum(knotenId => knoten.NodalDof[0]);
                elementTemperature /= element.NodeIds.Length;
                var intens = (byte)(255 * (elementTemperature - minTemp) / (maxTemp - minTemp));
                var rot = FromArgb(intens, 255, 0, 0);
                var myBrush = new SolidColorBrush(rot);

                Shape path = new Path()
                {
                    Stroke = Blue,
                    StrokeThickness = 1,
                    Fill = myBrush,
                    Data = pathGeometry
                };
                TemperaturElemente.Add(path);
                // setz oben/links Position zum Zeichnen auf dem Canvas
                SetLeft(path, RandLinks);
                SetTop(path, RandOben);
                // zeichne Shape
                visualErgebnisse.Children.Add(path);
            }
        }

        public void KnotengradientenZeichnen(int index)
        {
            foreach (var item in modell.Knoten)
            {
                knoten = item.Value;
                var gradient = knoten.NodalDerivatives[0][index].ToString("N2");
                temp = knoten.NodalDof[0];
                if (temp > maxTemp) maxTemp = temp;
                if (temp < minTemp) minTemp = temp;
                var fensterKnoten = TransformKnoten(knoten, auflösung, maxY);

                var id = new TextBlock
                {
                    FontSize = 12,
                    Background = LightBlue,
                    FontWeight = FontWeights.Bold,
                    Text = gradient
                };
                Knotengradienten.Add(id);
                SetTop(id, fensterKnoten.Y + RandOben + 15);
                SetLeft(id, fensterKnoten.X + RandLinks);
                visualErgebnisse.Children.Add(id);
            }
        }

        public void WärmeflussvektorenZeichnen()
        {
            double maxVektor = 0;
            foreach (var abstract2D in modell.Elemente.Select(item => (Abstrakt2D)item.Value))
            {
                abstract2D.ElementState = abstract2D.ComputeElementState(0, 0);
                var vektor = Math.Sqrt(abstract2D.ElementState[0] * abstract2D.ElementState[0] +
                                             abstract2D.ElementState[1] * abstract2D.ElementState[1]);
                if (maxVektor < vektor) maxVektor = vektor;
            }
            vektorskalierung = maxScreenLength / maxVektor;

            foreach (var abstrakt2D in modell.Elemente.Select(item => (Abstrakt2D)item.Value))
            {
                abstrakt2D.ElementState = abstrakt2D.ComputeElementState(0, 0);
                vektorLänge = (Math.Sqrt(abstrakt2D.ElementState[0] * abstrakt2D.ElementState[0] +
                                         abstrakt2D.ElementState[1] * abstrakt2D.ElementState[1])) * vektorskalierung;
                vektorWinkel = Math.Atan2(abstrakt2D.ElementState[1], abstrakt2D.ElementState[0]) * 180 / Math.PI;
                // zeichne den resultierenden Vektor mit seinem Mittelpunkt im Elementschwerpunkt
                // füge am Endpunkt Pfeilspitzen an und füge Wärmeflusspfeil als pathFigure zur pathGeometry hinzu
                var pathGeometry = WärmeflussElementmitte(abstrakt2D, vektorLänge);

                Shape path = new Path()
                {
                    Stroke = Black,
                    StrokeThickness = 2,
                    Data = pathGeometry
                };
                // rotiere Wärmeflusspfeil im Schwerpunkt um den Vektorwinkel
                var cg = abstrakt2D.ComputeCenterOfGravity();
                var rotateTransform = new RotateTransform(-vektorWinkel)
                {
                    CenterX = (int)(cg.X * auflösung),
                    CenterY = (int)((-cg.Y + maxY) * auflösung)
                };
                path.RenderTransform = rotateTransform;
                // sammle alle Wärmeflusspfeile in der Liste Wärmevektoren, um deren Darstellung löschen zu können
                WärmeVektoren.Add(path);

                // setz oben/links Position zum Zeichnen auf dem Canvas
                SetLeft(path, RandLinks);
                SetTop(path, RandOben);
                // zeichne Shape
                visualErgebnisse.Children.Add(path);
            }
        }
        private PathGeometry WärmeflussElementmitte(AbstraktElement abstraktElement, double length)
        {
            Abstrakt2D abstrakt2D = (Abstrakt2D)abstraktElement;
            var pathFigure = new PathFigure();
            var pathGeometry = new PathGeometry();
            var cg = abstrakt2D.ComputeCenterOfGravity();
            int[] fensterKnoten = { (int)(cg.X * auflösung), (int)((-cg.Y + maxY) * auflösung) };
            pathFigure.StartPoint = new Point(fensterKnoten[0] - length / 2, fensterKnoten[1]);
            var endPoint = new Point(fensterKnoten[0] + length / 2, fensterKnoten[1]);
            pathFigure.Segments.Add(new LineSegment(endPoint, true));
            pathFigure.Segments.Add(new LineSegment(new Point(endPoint.X - 3, endPoint.Y - 2), true));
            pathFigure.Segments.Add(new LineSegment(new Point(endPoint.X - 3, endPoint.Y + 2), true));
            pathFigure.Segments.Add(new LineSegment(new Point(endPoint.X, endPoint.Y), true));
            pathGeometry.Figures.Add(pathFigure);
            return pathGeometry;
        }

        public void ZeitverlaufZeichnen(double dt, double tmax, double mY, double[] ordinaten)
        {
            maxY = mY;
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
            SetLeft(zeitverlauf, RandLinks);
            SetTop(zeitverlauf, mY * auflösungV + RandOben);
            // zeichne Shape
            visualErgebnisse.Children.Add(zeitverlauf);
        }
        public void Koordinatensystem(double tmax, double mY, double minY)
        {
            maxY = mY;
            const int rand = 100;
            screenH = visualErgebnisse.ActualWidth;
            screenV = visualErgebnisse.ActualHeight;
            auflösungV = (screenV - rand) / (maxY - minY);
            auflösungH = (screenH - rand) / tmax;
            var xAchse = new Line
            {
                Stroke = Black,
                X1 = 0,
                Y1 = maxY * auflösungV + RandOben,
                X2 = tmax * auflösungH,
                Y2 = maxY * auflösungV + RandOben,
                StrokeThickness = 2
            };
            visualErgebnisse.Children.Add(xAchse);
            var yAchse = new Line
            {
                Stroke = Black,
                X1 = RandLinks,
                Y1 = (maxY - minY) * auflösungV + 2 * RandOben,
                X2 = RandLinks,
                Y2 = RandOben,
                StrokeThickness = 2
            };
            visualErgebnisse.Children.Add(yAchse);
        }

        private static Point TransformKnoten(Knoten knoten, double auflösung, double maxY)
        {
            return new Point(knoten.Coordinates[0] * auflösung, (-knoten.Coordinates[1] + maxY) * auflösung);
        }
    }
}