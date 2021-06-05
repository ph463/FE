using FEALibrary.Modell;
using FEALibrary.Modell.abstrakte_Klassen;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Shapes;
using static System.Windows.Controls.Canvas;
using static System.Windows.Media.Brushes;

namespace FE_Berechnungen.Tragwerksberechnung.Ergebnisse
{
    public partial class DynamischeModellzuständeVisualisieren
    {
        private readonly FEModell modell;
        private readonly double dt;
        private int index, indexN, indexQ, indexM;

        private readonly Darstellung darstellung;
        public List<object> ElementTexte { get; }
        public List<object> KnotenTexte { get; }
        public List<object> Geometrie { get; }
        public List<object> Verformungen { get; }
        private bool geometrieAn = true, verformungenAn, normalkräfteAn, querkräfteAn, momenteAn;
        private double maxNormalkraft, maxQuerkraft, maxMoment;
        private double maxNormalkraftZeit, maxQuerkraftZeit, maxMomentZeit;
        private TextBlock maximalWerte, maximalWertZeitschritt;

        public DynamischeModellzuständeVisualisieren(FEModell feModel)
        {
            Language = XmlLanguage.GetLanguage("de-DE");
            modell = feModel;
            ElementTexte = new List<object>();
            KnotenTexte = new List<object>();
            Geometrie = new List<object>();
            Verformungen = new List<object>();

            InitializeComponent();
            Show();

            // Auswahl des Zeitschritts
            dt = modell.Zeitintegration.Dt;
            var tmax = modell.Zeitintegration.Tmax;

            // Auswahl des Zeitschritts aus Zeitraster, z.B. jeder 10.
            var nSteps = (int)(tmax / dt);
            const int zeitraster = 1;
            //if (nSteps > 1000) zeitraster = 10;
            nSteps = (nSteps / zeitraster) + 1;
            var zeit = new double[nSteps];
            for (var i = 0; i < nSteps; i++) { zeit[i] = (i * dt * zeitraster); }

            darstellung = new Darstellung(modell, visualErgebnisse);
            //var auflösung = darstellung.auflösung;
            //var maxY = darstellung.maxY;
            //var screenV = darstellung.screenV;

            ZeichneGeometrie();
            MaximalwerteGesamterZeitverlauf();
            Zeitschrittauswahl.ItemsSource = zeit;
        }

        private void ZeichneGeometrie()
        {
            darstellung.UnverformteGeometrie();

            // mit Knoten und Element Ids
            darstellung.KnotenTexte();
            darstellung.ElementTexte();
        }
        private void ZeichneVerformung()
        {
            darstellung.VerformteGeometrie();
        }
        private void DropDownZeitschrittauswahlClosed(object sender, EventArgs e)
        {
            index = Zeitschrittauswahl.SelectedIndex;
            foreach (var item in modell.Knoten)
            {
                for (var i = 0; i < item.Value.NumberOfNodalDof; i++)
                {
                    item.Value.NodalDof[i] = item.Value.NodalVariables[i][index];
                }
            }

            momenteAn = false;
        }

        // Button event
        private void BtnGeometrie_Click(object sender, RoutedEventArgs e)
        {
            if (geometrieAn)
            {
                foreach (Shape geometrie in Geometrie)
                {
                    visualErgebnisse.Children.Remove(geometrie);
                }
                foreach (TextBlock knotenText in KnotenTexte)
                {
                    visualErgebnisse.Children.Remove(knotenText);
                }
                foreach (TextBlock elementText in ElementTexte)
                {
                    visualErgebnisse.Children.Remove(elementText);
                }
                geometrieAn = false;
            }
            else
            {
                foreach (Shape geometrie in Geometrie)
                {
                    visualErgebnisse.Children.Add(geometrie);
                }
                foreach (TextBlock knotenText in KnotenTexte)
                {
                    visualErgebnisse.Children.Add(knotenText);
                }
                foreach (TextBlock elementText in ElementTexte)
                {
                    visualErgebnisse.Children.Add(elementText);
                }
                geometrieAn = true;
            }

        }
        private void BtnVerformung_Click(object sender, RoutedEventArgs e)
        {
            if (!StartFenster.berechnet)
            {
                _ = MessageBox.Show("Zeitschrittverlaufsberechnung muss erst durchgeführt werden", "Tragwerksberechnung");
            }
            else if (index == 0)
            {
                _ = MessageBox.Show("Zeitschritt muss erst ausgewählt werden", "Tragwerksberechnung");
            }
            else
            {
                if (!verformungenAn)
                {
                    ZeichneVerformung();
                    verformungenAn = true;
                }
                else
                {
                    foreach (Shape path in Verformungen)
                    {
                        visualErgebnisse.Children.Remove(path);
                    }
                    verformungenAn = false;
                }
            }
        }

        private void BtnNormalkraft_Click(object sender, RoutedEventArgs e)
        {
            if (!StartFenster.berechnet)
            {
                _ = MessageBox.Show("Zeitschrittverlaufsberechnung muss erst durchgeführt werden", "Tragwerksberechnung");
                return;
            }
            if (index == 0)
            {
                _ = MessageBox.Show("Zeitschritt muss erst ausgewählt werden", "Tragwerksberechnung");
                return;
            }

            if (!querkräfteAn)
            {
                if (momenteAn)
                {
                    foreach (Shape path in darstellung.MomenteListe)
                    {
                        visualErgebnisse.Children.Remove(path);
                    }

                    visualErgebnisse.Children.Remove(maximalWerte);
                    momenteAn = false;
                }
                else if (normalkräfteAn)
                {
                    // Darstellung der Normalkräfte am folgenden Zeitschritt
                    index++;
                    foreach (var item in modell.Knoten)
                    {
                        for (var i = 0; i < item.Value.NumberOfNodalDof; i++)
                        {
                            item.Value.NodalDof[i] = item.Value.NodalVariables[i][index];
                        }
                    }
                    normalkräfteAn = false;
                }
            }
            else
            {
                foreach (Shape path in darstellung.QuerkraftListe)
                {
                    visualErgebnisse.Children.Remove(path);
                }
                if (maximalWerte != null) visualErgebnisse.Children.Remove(maximalWerte);
                querkräfteAn = false;
            }

            if (normalkräfteAn) return;
            {
                // ggf. vorhandene Normalkraftdarstellung entfernen
                foreach (Shape path in darstellung.NormalkraftListe)
                {
                    visualErgebnisse.Children.Remove(path);
                }
                visualErgebnisse.Children.Remove(maximalWertZeitschritt);

                // Darstellung der Normalkräfte
                DarstellungNormalkräfte(index);
                normalkräfteAn = true;
            }
        }
        private void BtnQuerkraft_Click(object sender, RoutedEventArgs e)
        {
            if (!StartFenster.berechnet)
            {
                _ = MessageBox.Show("Zeitschrittverlaufsberechnung muss erst durchgeführt werden", "Tragwerksberechnung");
                return;
            }
            if (index == 0)
            {
                _ = MessageBox.Show("Zeitschritt muss erst ausgewählt werden", "Tragwerksberechnung");
                return;
            }

            if (normalkräfteAn)
            {
                foreach (Shape path in darstellung.NormalkraftListe)
                {
                    visualErgebnisse.Children.Remove(path);
                }
                visualErgebnisse.Children.Remove(maximalWerte);
                normalkräfteAn = false;
            }
            else if (momenteAn)
            {
                foreach (Shape path in darstellung.MomenteListe)
                {
                    visualErgebnisse.Children.Remove(path);
                }
                visualErgebnisse.Children.Remove(maximalWerte);
                momenteAn = false;
            }
            else if (querkräfteAn)
            {
                // Darstellung der Querkräfte am folgenden Zeitschritt
                index++;
                foreach (var item in modell.Knoten)
                {
                    for (var i = 0; i < item.Value.NumberOfNodalDof; i++)
                    {
                        item.Value.NodalDof[i] = item.Value.NodalVariables[i][index];
                    }
                }
                querkräfteAn = false;
            }

            if (querkräfteAn) return;
            {
                // ggf. vorhandene Querkraftdarstellung entfernen
                foreach (Shape path in darstellung.QuerkraftListe)
                {
                    visualErgebnisse.Children.Remove(path);
                }
                visualErgebnisse.Children.Remove(maximalWertZeitschritt);

                DarstellungQuerkräfte(index);
                querkräfteAn = true;
            }
        }
        private void BtnBiegemoment_Click(object sender, RoutedEventArgs e)
        {

            if (!StartFenster.berechnet)
            {
                _ = MessageBox.Show("Zeitschrittverlaufsberechnung muss erst durchgeführt werden", "Tragwerksberechnung");
                return;
            }
            if (index == 0)
            {
                _ = MessageBox.Show("Zeitschritt muss erst ausgewählt werden", "Tragwerksberechnung");
                return;
            }

            if (normalkräfteAn)
            {
                foreach (Shape path in darstellung.NormalkraftListe)
                {
                    visualErgebnisse.Children.Remove(path);
                }
                visualErgebnisse.Children.Remove(maximalWerte);
                normalkräfteAn = false;
            }
            else if (querkräfteAn)
            {
                foreach (Shape path in darstellung.QuerkraftListe)
                {
                    visualErgebnisse.Children.Remove(path);
                }
                visualErgebnisse.Children.Remove(maximalWerte);
                querkräfteAn = false;
            }
            else if (momenteAn)
            {
                // Darstellung der Momente am folgenden Zeitschritt
                index++;
                foreach (var item in modell.Knoten)
                {
                    for (var i = 0; i < item.Value.NumberOfNodalDof; i++)
                    {
                        item.Value.NodalDof[i] = item.Value.NodalVariables[i][index];
                    }
                }
                momenteAn = false;
            }

            if (momenteAn) return;
            {
                // ggf. vorhandene Momentendarstellung entfernen
                foreach (Shape path in darstellung.MomenteListe)
                {
                    visualErgebnisse.Children.Remove(path);
                }
                visualErgebnisse.Children.Remove(maximalWertZeitschritt);

                DarstellungMomente(index);
                momenteAn = true;
            }
        }

        private void DarstellungNormalkräfte(int zeitIndex)
        {
            // Bestimmung der maximalen Normalkraft
            IEnumerable<AbstraktBalken> Beams()
            {
                foreach (var item in modell.Elemente)
                {
                    if (!(item.Value is AbstraktBalken element)) continue;
                    yield return element;
                }
            }
            maxNormalkraftZeit = 0;
            // Maximalwert der Normalkraft ermitteln
            foreach (var element in Beams())
            {
                element.ElementState = element.ComputeElementState();
                if (Math.Abs(element.ElementState[0]) > maxNormalkraftZeit) maxNormalkraftZeit = Math.Abs(element.ElementState[0]);
                if (element.ElementState.Length == 2)
                {
                    if (Math.Abs(element.ElementState[1]) > maxNormalkraftZeit) maxNormalkraftZeit = Math.Abs(element.ElementState[1]);
                }
                else
                {
                    if (Math.Abs(element.ElementState[3]) > maxNormalkraftZeit) maxNormalkraftZeit = Math.Abs(element.ElementState[3]);
                }
            }

            // Normalkraftdarstellung für alle Biegebalken zeichnen
            foreach (var element in Beams())
            {
                if (maxNormalkraft == 0) continue;
                darstellung.Normalkraft_Zeichnen(element, maxNormalkraft, false);
            }
            var zeit = zeitIndex * dt;
            var maxText = "maximale absolute Normalkraft = " + maxNormalkraftZeit.ToString("N0") + " nach Zeit = " + zeit.ToString("N2");
            MaximalwertAnZeitschritt(maxText);
        }
        private void DarstellungQuerkräfte(int zeitIndex)
        {
            // Bestimmung der maximalen Querkraft
            IEnumerable<AbstraktBalken> Beams()
            {
                foreach (var item in modell.Elemente)
                {
                    if (!(item.Value is AbstraktBalken element)) continue;
                    yield return element;
                }
            }
            maxQuerkraftZeit = 0;
            foreach (var element in Beams())
            {
                element.ElementState = element.ComputeElementState();
                if (element.ElementState.Length <= 2) continue;
                if (Math.Abs(element.ElementState[1]) > maxQuerkraft) maxQuerkraft = Math.Abs(element.ElementState[1]);
                if (Math.Abs(element.ElementState[4]) > maxQuerkraft) maxQuerkraft = Math.Abs(element.ElementState[4]);
            }

            // Querkraftdarstellung für alle Biegebalken zeichnen
            foreach (var element in Beams())
            {
                if (element.ElementState.Length <= 2 || maxQuerkraft == 0) continue;
                darstellung.Querkraft_Zeichnen(element, maxQuerkraft, false);
            }
            var zeit = zeitIndex * dt;
            var maxText = "maximale absolute Querkraft = " + maxQuerkraftZeit.ToString("N0") + " nach Zeit = " + zeit.ToString("N2");
            MaximalwertAnZeitschritt(maxText);
        }
        private void DarstellungMomente(int zeitIndex)
        {
            // Bestimmung des maximalen Biegemoments
            IEnumerable<AbstraktBalken> Beams()
            {
                foreach (var item in modell.Elemente)
                    if (item.Value is AbstraktBalken element)
                    {
                        yield return element;
                    }
            }
            maxMomentZeit = 0;
            foreach (var element in Beams())
            {
                element.ElementState = element.ComputeElementState();
                if (element.ElementState.Length <= 2) continue;
                if (Math.Abs(element.ElementState[2]) > maxMomentZeit) maxMomentZeit = Math.Abs(element.ElementState[2]);
                if (Math.Abs(element.ElementState[5]) > maxMomentZeit) maxMomentZeit = Math.Abs(element.ElementState[5]);
            }
            // Momentendarstellung für alle Biegebalken zeichnen
            foreach (var element in Beams())
            {
                if (element.ElementState.Length <= 2 || maxMoment == 0) continue;
                darstellung.Momente_Zeichnen(element, maxMoment, false);
            }
            var zeit = zeitIndex * dt;
            var maxText = "maximales absolutes Moment = " + maxMomentZeit.ToString("N0") + " nach Zeit = " + zeit.ToString("N2");
            MaximalwertAnZeitschritt(maxText);
        }

        private void MaximalwertAnZeitschritt(string maxText)
        {
            maximalWertZeitschritt = new TextBlock
            {
                FontSize = 12,
                Text = maxText,
                Foreground = Blue
            };
            SetTop(maximalWertZeitschritt, 20);
            SetLeft(maximalWertZeitschritt, 5);
            visualErgebnisse.Children.Add(maximalWertZeitschritt);
        }
        private void MaximalwerteGesamterZeitverlauf()
        {

            // Schleife über alle Zeitschritte
            var nSteps = (int)(modell.Zeitintegration.Tmax / modell.Zeitintegration.Dt);
            for (var i = 0; i < nSteps; i++)
            {
                foreach (var item in modell.Knoten)
                {
                    for (var k = 0; k < item.Value.NumberOfNodalDof; k++)
                    {
                        item.Value.NodalDof[k] = item.Value.NodalVariables[k][i];
                    }
                }

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

                // Zustand aller Fachwerk- und Biegebalkenelemente an einem Zeitschritt
                foreach (var element in Beams())
                {
                    element.ElementState = element.ComputeElementState();

                    // Fachwerkstäbe
                    if (element.ElementState.Length == 2)
                    {
                        if (Math.Abs(element.ElementState[0]) > maxNormalkraft) { indexN = i; maxNormalkraft = Math.Abs(element.ElementState[0]); }
                        if (Math.Abs(element.ElementState[1]) > maxNormalkraft) { indexN = i; maxNormalkraft = Math.Abs(element.ElementState[1]); }
                    }

                    // Biegebalken
                    else
                    {
                        if (Math.Abs(element.ElementState[0]) > maxNormalkraft) { indexN = i; maxNormalkraft = Math.Abs(element.ElementState[0]); }
                        if (Math.Abs(element.ElementState[3]) > maxNormalkraft) { indexN = i; maxNormalkraft = Math.Abs(element.ElementState[3]); }
                        if (Math.Abs(element.ElementState[1]) > maxQuerkraft) { indexQ = i; maxQuerkraft = Math.Abs(element.ElementState[1]); }
                        if (Math.Abs(element.ElementState[4]) > maxQuerkraft) { indexQ = i; maxQuerkraft = Math.Abs(element.ElementState[4]); }
                        if (Math.Abs(element.ElementState[2]) > maxMoment) { indexM = i; maxMoment = Math.Abs(element.ElementState[2]); }
                        if (Math.Abs(element.ElementState[5]) > maxMoment) { indexM = i; maxMoment = Math.Abs(element.ElementState[5]); }
                    }
                }
            }

            maximalWerte = new TextBlock
            {
                FontSize = 12,
                Text = "maximale Normalkraft = " + maxNormalkraft.ToString("N0") + " nach Zeit = " + (indexN * dt).ToString("N2") +
                     ", maximaleQuerkraft = " + maxQuerkraft.ToString("N0") + " nach Zeit = " + (indexQ * dt).ToString("N2") +
                     " und maximales Moment = " + maxMoment.ToString("N0") + " nach Zeit = " + (indexM * dt).ToString("N2"),
                Foreground = Blue
            };
            SetTop(maximalWerte, 0);
            SetLeft(maximalWerte, 5);
            visualErgebnisse.Children.Add(maximalWerte);
        }
    }
}