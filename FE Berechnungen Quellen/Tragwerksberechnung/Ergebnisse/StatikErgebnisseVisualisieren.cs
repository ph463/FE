using FE_Berechnungen.Tragwerksberechnung.Modelldaten;
using FEALibrary.Modell;
using FEALibrary.Modell.abstrakte_Klassen;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Shapes;

namespace FE_Berechnungen.Tragwerksberechnung.Ergebnisse
{
    public partial class StatikErgebnisseVisualisieren
    {
        private readonly FEModell modell;
        private AbstraktElement element;
        //private const double Eps = 1.0E-10;
        private bool elementTexteAn = true, knotenTexteAn = true,
                     verformungenAn, normalkräfteAn, querkräfteAn, momenteAn;
        private readonly Darstellung darstellung;

        public StatikErgebnisseVisualisieren(FEModell feModell)
        {
            Language = XmlLanguage.GetLanguage("de-DE");
            modell = feModell;
            InitializeComponent();
            Show();

            darstellung = new Darstellung(modell, VisualErgebnisse);

            // unverformte Geometrie
            darstellung.UnverformteGeometrie();

            // mit Element Ids
            darstellung.ElementTexte();

            // mit Knoten Ids
            darstellung.KnotenTexte();

            // Faktor für Überhöhung des Verformungszustands
            darstellung.überhöhungVerformung = double.Parse(Überhöhung.Text);
        }

        private void BtnVerformung_Click(object sender, RoutedEventArgs e)
        {
            if (!verformungenAn)
            {
                darstellung.VerformteGeometrie();
                verformungenAn = true;
            }
            else
            {
                foreach (Shape path in darstellung.Verformungen)
                {
                    VisualErgebnisse.Children.Remove(path);
                }
                verformungenAn = false;
            }
        }
        private void BtnÜberhöhung_Click(object sender, RoutedEventArgs e)
        {
            darstellung.überhöhungVerformung = double.Parse(Überhöhung.Text);
            foreach (Shape path in darstellung.Verformungen)
            {
                VisualErgebnisse.Children.Remove(path);
            }
            verformungenAn = false;
            darstellung.VerformteGeometrie();
            verformungenAn = true;
        }

        private void BtnNormalkraft_Click(object sender, RoutedEventArgs e)
        {
            double maxNormalkraft = 0;

            if (querkräfteAn)
            {
                foreach (Shape path in darstellung.QuerkraftListe)
                {
                    VisualErgebnisse.Children.Remove(path);
                }
                querkräfteAn = false;
            }
            if (momenteAn)
            {
                foreach (Shape path in darstellung.MomenteListe)
                {
                    VisualErgebnisse.Children.Remove(path);
                }
                VisualErgebnisse.Children.Remove(darstellung.maxMomentText);
                momenteAn = false;
            }
            if (!normalkräfteAn)
            {
                // Bestimmung der maximalen Normalkraft
                IEnumerable<AbstraktBalken> Beams()
                {
                    foreach (var item in modell.Elemente)
                    {
                        if (item.Value is AbstraktBalken beam)
                        {
                            yield return beam;
                        }
                    }
                }
                foreach (var beam in Beams())
                {
                    var barEndForces = beam.ComputeElementState();
                    if (Math.Abs(barEndForces[0]) > maxNormalkraft) maxNormalkraft = Math.Abs(barEndForces[0]);
                    if (barEndForces.Length > 2)
                    {
                        if (Math.Abs(barEndForces[3]) > maxNormalkraft) maxNormalkraft = Math.Abs(barEndForces[3]);
                    }
                    else
                    {
                        if (Math.Abs(barEndForces[1]) > maxNormalkraft) maxNormalkraft = Math.Abs(barEndForces[1]);
                    }
                }

                // Skalierung der Normalkraftdarstellung und Darstellung aller Normalkraftverteilungen
                foreach (var beam in Beams())
                {
                    beam.ComputeElementState();
                    darstellung.Normalkraft_Zeichnen(beam, maxNormalkraft, false);
                }
                normalkräfteAn = true;
            }
            else
            {
                foreach (Shape path in darstellung.NormalkraftListe)
                {
                    VisualErgebnisse.Children.Remove(path);
                }
                normalkräfteAn = false;
            }
        }
        private void BtnQuerkraft_Click(object sender, RoutedEventArgs e)
        {
            double maxQuerkraft = 0;
            if (normalkräfteAn)
            {
                foreach (Shape path in darstellung.NormalkraftListe)
                {
                    VisualErgebnisse.Children.Remove(path);
                }
                normalkräfteAn = false;
            }
            if (momenteAn)
            {
                foreach (Shape path in darstellung.MomenteListe)
                {
                    VisualErgebnisse.Children.Remove(path);
                }
                VisualErgebnisse.Children.Remove(darstellung.maxMomentText);
                momenteAn = false;
            }
            if (!querkräfteAn)
            {
                bool elementlast;
                // Bestimmung der maximalen Querkraft
                IEnumerable<AbstraktBalken> Beams()
                {
                    foreach (KeyValuePair<string, AbstraktElement> item in modell.Elemente)
                    {
                        if (item.Value is AbstraktBalken beam)
                        {
                            yield return beam;
                        }
                    }
                }
                foreach (var beam in Beams())
                {
                    beam.ElementState = beam.ComputeElementState();
                    if (beam.ElementState.Length <= 2) continue;
                    if (Math.Abs(beam.ElementState[1]) > maxQuerkraft) maxQuerkraft = Math.Abs(beam.ElementState[1]);
                    if (Math.Abs(beam.ElementState[4]) > maxQuerkraft) maxQuerkraft = Math.Abs(beam.ElementState[4]);
                }

                // skalierte Querkraftverläufe zeichnen
                foreach (var beam in Beams())
                {
                    elementlast = false;
                    beam.ElementState = beam.ComputeElementState();
                    if (beam.ElementState.Length <= 2) continue;
                    if (Math.Abs(beam.ElementState[1] + beam.ElementState[4]) > double.Epsilon) elementlast = true;
                    darstellung.Querkraft_Zeichnen(beam, maxQuerkraft, elementlast);
                }
                querkräfteAn = true;
            }
            else
            {
                foreach (Shape path in darstellung.QuerkraftListe)
                {
                    VisualErgebnisse.Children.Remove(path);
                }
                querkräfteAn = false;
            }
        }
        private void BtnMomente_Click(object sender, RoutedEventArgs e)
        {
            double maxMoment = 0;

            if (normalkräfteAn)
            {
                foreach (Shape path in darstellung.NormalkraftListe)
                {
                    VisualErgebnisse.Children.Remove(path);
                }
                normalkräfteAn = false;
            }
            if (querkräfteAn)
            {
                foreach (Shape path in darstellung.QuerkraftListe)
                {
                    VisualErgebnisse.Children.Remove(path);
                }
                querkräfteAn = false;
            }
            if (!momenteAn)
            {
                // Bestimmung des maximalen Biegemoments

                IEnumerable<AbstraktBalken> Beams()
                {
                    foreach (var item in modell.Elemente)
                    {
                        if (item.Value is AbstraktBalken beam)
                        {
                            yield return beam;
                        }
                    }
                }
                foreach (var beam in Beams())
                {
                    beam.ElementState = beam.ComputeElementState();
                    if (beam.ElementState.Length <= 2) continue;
                    if (Math.Abs(beam.ElementState[2]) > maxMoment) maxMoment = Math.Abs(beam.ElementState[2]);
                    if (Math.Abs(beam.ElementState[5]) > maxMoment) maxMoment = Math.Abs(beam.ElementState[5]);
                }

                // Anteil lokaler Momentenverteilung infolge Punkt-/Linienlast
                AbstraktBalken lastBalken;
                double lokalesMoment;
                IEnumerable<PunktLast> PunktLasten()
                {
                    foreach (var item in modell.PunktLasten)
                    {
                        var last = (PunktLast)item.Value;
                        if (modell.Elemente.TryGetValue(last.ElementId, out element))
                        {
                            yield return last;
                        }
                    }
                }
                foreach (var last in PunktLasten())
                {
                    lastBalken = (AbstraktBalken)element;
                    lokalesMoment = lastBalken.ElementState[2] + lastBalken.ElementState[1] * last.Offset * lastBalken.length;
                    if (Math.Abs(lokalesMoment) > maxMoment) maxMoment = Math.Abs(lokalesMoment);
                }

                IEnumerable<LinienLast> LinienLasten()
                {
                    foreach (var item in modell.ElementLasten)
                    {
                        //if (item.Value is LinienLast linienLast && item.Value.ElementId == element.ElementId)
                        var last = (LinienLast)item.Value;
                        if (modell.Elemente.TryGetValue(last.ElementId, out element))
                        {
                            yield return last;
                        }
                    }
                }
                foreach (var linienLast in LinienLasten())
                {
                    //if (modell.Elemente.TryGetValue(linienLast.ElementId, out element)) { }
                    lastBalken = (AbstraktBalken)element;
                    var stabEndkräfte = lastBalken.ComputeElementState();
                    lokalesMoment = stabEndkräfte[2] + stabEndkräfte[1] * lastBalken.length / 2 + linienLast.Intensity[1] * lastBalken.length / 2 * lastBalken.length / 4;
                    if (Math.Abs(lokalesMoment) > maxMoment) maxMoment = Math.Abs(lokalesMoment);
                }

                // Skalierung der Momentendarstellung und Momentenverteilung für alle Biegebalken zeichnen
                foreach (var beam in Beams())
                {
                    var elementlast = false;
                    beam.ElementState = beam.ComputeElementState();
                    if (beam.ElementState.Length <= 2) continue;
                    if (Math.Abs(beam.ElementState[1] + beam.ElementState[4]) > double.Epsilon) elementlast = true;
                    darstellung.Momente_Zeichnen(beam, maxMoment, elementlast);
                }
                momenteAn = true;
            }
            else
            {
                foreach (Shape path in darstellung.MomenteListe)
                {
                    VisualErgebnisse.Children.Remove(path);
                }
                momenteAn = false;
            }
        }

        private void BtnElementIDs_Click(object sender, RoutedEventArgs e)
        {
            if (!elementTexteAn)
            {
                darstellung.ElementTexte();
                elementTexteAn = true;
            }
            else
            {
                foreach (TextBlock id in darstellung.ElementIDs) VisualErgebnisse.Children.Remove(id);
                elementTexteAn = false;
            }
        }
        private void BtnKnotenIDs_Click(object sender, RoutedEventArgs e)
        {
            if (!knotenTexteAn)
            {
                darstellung.KnotenTexte();
                knotenTexteAn = true;
            }
            else
            {
                foreach (TextBlock id in darstellung.KnotenIDs) VisualErgebnisse.Children.Remove(id);
                knotenTexteAn = false;
            }
        }
    }
}