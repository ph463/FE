using FEALibrary.Modell;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using static System.Windows.Controls.Canvas;
using static System.Windows.Media.Brushes;
using static System.Windows.Media.Color;

namespace FE_Berechnungen.Tragwerksberechnung.Ergebnisse
{
    public partial class KnotenzeitverläufeVisualisieren
    {
        private readonly FEModell modell;
        private Knoten knoten;
        private readonly double dt;
        private readonly double tmax;
        private double maxVerformung;
        private double minVerformung;
        private double maxBeschleunigung;
        private double minBeschleunigung;

        private readonly Darstellung darstellung;
        private Darstellungsbereich ausschnitt;
        private bool darstellungsBereichNeu;
        private bool deltaXVerlauf, deltaYVerlauf;
        private bool accXVerlauf, accYVerlauf;
        private TextBlock maximal;

        public KnotenzeitverläufeVisualisieren(FEModell feModell)
        {
            Language = XmlLanguage.GetLanguage("de-DE");
            modell = feModell;
            InitializeComponent();
            Show();

            // Festlegung der Zeitachse
            dt = modell.Zeitintegration.Dt;
            tmax = modell.Zeitintegration.Tmax;

            // Auswahl des Knotens         
            Knotenauswahl.ItemsSource = modell.Knoten.Keys;

            // Initialisierung der Zeichenfläche
            darstellung = new Darstellung(modell, visualErgebnisse);
        }

        private void DropDownKnotenauswahlClosed(object sender, EventArgs e)
        {
            var knotenId = (string)Knotenauswahl.SelectedItem;
            if (modell.Knoten.TryGetValue(knotenId, out knoten)) { }
        }

        private void BtnDeltaX_Click(object sender, RoutedEventArgs e)
        {
            if (knoten == null)
            {
                _ = MessageBox.Show("Knoten muss erst ausgewählt werden", "dynamische Tragwerksberechnung");
            }
            else
            {
                switch (deltaXVerlauf)
                {
                    case true when darstellungsBereichNeu:
                        visualErgebnisse.Children.Clear();
                        maxVerformung = ausschnitt.maxVerformung;
                        minVerformung = -maxVerformung;
                        break;
                    case false:
                        visualErgebnisse.Children.Clear();
                        maxVerformung = knoten.NodalVariables[0].Max();
                        minVerformung = -maxVerformung;
                        break;
                }

                if (maxVerformung < double.Epsilon) return;
                darstellung.Koordinatensystem(tmax, maxVerformung, minVerformung);

                // Textdarstellung des Maximalwertes mit Zeitpunkt
                var zeit = dt * Array.IndexOf(knoten.NodalVariables[0], maxVerformung);
                MaximalwertText("Verformung x", maxVerformung, zeit);

                darstellung.ZeitverlaufZeichnen(dt, tmax, maxVerformung, knoten.NodalVariables[0]);

                deltaXVerlauf = true; deltaYVerlauf = false;
                accXVerlauf = false; accYVerlauf = false;
                darstellungsBereichNeu = false;
            }
        }
        private void BtnDeltaY_Click(object sender, RoutedEventArgs e)
        {
            deltaXVerlauf = false; accXVerlauf = false; accYVerlauf = false;

            if (knoten == null)
            {
                _ = MessageBox.Show("Knoten muss erst ausgewählt werden", "dynamische Tragwerksberechnung");
            }
            else
            {
                switch (deltaYVerlauf)
                {
                    case true when darstellungsBereichNeu:
                        visualErgebnisse.Children.Clear();
                        maxVerformung = ausschnitt.maxVerformung;
                        minVerformung = -maxVerformung;
                        break;
                    case false:
                        visualErgebnisse.Children.Clear();
                        maxVerformung = knoten.NodalVariables[1].Max();
                        minVerformung = -maxVerformung;
                        break;
                }

                if (maxVerformung < double.Epsilon) return;
                darstellung.Koordinatensystem(tmax, maxVerformung, minVerformung);

                // Textdarstellung des Maximalwertes mit Zeitpunkt
                //var maxDeltaY = knoten.NodalVariables[1].Max();
                var zeit = dt * Array.IndexOf(knoten.NodalVariables[1], maxVerformung);
                MaximalwertText("Verformung y", maxVerformung, zeit);

                darstellung.ZeitverlaufZeichnen(dt, tmax, maxVerformung, knoten.NodalVariables[1]);

                deltaYVerlauf = true; deltaXVerlauf = false;
                accXVerlauf = false; accYVerlauf = false;
                darstellungsBereichNeu = false;
            }
        }
        private void BtnAccX_Click(object sender, RoutedEventArgs e)
        {
            deltaXVerlauf = false; deltaYVerlauf = false; accYVerlauf = false;

            if (knoten == null)
            {
                _ = MessageBox.Show("Knoten muss erst ausgewählt werden", "dynamische Tragwerksberechnung");
            }
            else
            {
                switch (accXVerlauf)
                {
                    case true when darstellungsBereichNeu:
                        visualErgebnisse.Children.Clear();
                        maxBeschleunigung = ausschnitt.maxBeschleunigung;
                        minBeschleunigung = -maxBeschleunigung;
                        break;
                    case false:
                        visualErgebnisse.Children.Clear();
                        maxBeschleunigung = knoten.NodalDerivatives[0].Max();
                        minBeschleunigung = -maxBeschleunigung;
                        break;
                }

                if (maxBeschleunigung < double.Epsilon) return;
                darstellung.Koordinatensystem(tmax, maxBeschleunigung, minBeschleunigung);

                // Textdarstellung des Maximalwertes mit Zeitpunkt
                //var maxAccX = knoten.NodalDerivatives[0].Max();
                var zeit = dt * Array.IndexOf(knoten.NodalDerivatives[0], maxBeschleunigung);
                MaximalwertText("Beschleunigung x", maxBeschleunigung, zeit);

                darstellung.ZeitverlaufZeichnen(dt, tmax, maxBeschleunigung, knoten.NodalDerivatives[0]);

                deltaYVerlauf = false; deltaXVerlauf = false;
                accXVerlauf = true; accYVerlauf = false;
                darstellungsBereichNeu = false;
            }
        }
        private void BtnAccY_Click(object sender, RoutedEventArgs e)
        {
            deltaXVerlauf = false; deltaYVerlauf = false; accXVerlauf = false;

            if (knoten == null)
            {
                _ = MessageBox.Show("Knoten muss erst ausgewählt werden", "dynamische Tragwerksberechnung");
            }
            else
            {
                switch (accYVerlauf)
                {
                    case true when darstellungsBereichNeu:
                        visualErgebnisse.Children.Clear();
                        maxBeschleunigung = ausschnitt.maxBeschleunigung;
                        minBeschleunigung = -maxBeschleunigung;
                        break;
                    case false:
                        visualErgebnisse.Children.Clear();
                        maxBeschleunigung = knoten.NodalDerivatives[1].Max();
                        minBeschleunigung = -maxBeschleunigung;
                        break;
                }
                if (maxBeschleunigung < double.Epsilon) return;
                darstellung.Koordinatensystem(tmax, maxBeschleunigung, minBeschleunigung);

                // Textdarstellung des Maximalwertes mit Zeitpunkt
                var maxAccY = knoten.NodalDerivatives[1].Max();
                var zeit = dt * Array.IndexOf(knoten.NodalDerivatives[1], maxAccY);
                MaximalwertText("Beschleunigung y", maxAccY, zeit);

                darstellung.ZeitverlaufZeichnen(dt, tmax, maxBeschleunigung, knoten.NodalDerivatives[1]);

                deltaYVerlauf = false; deltaXVerlauf = false;
                accXVerlauf = false; accYVerlauf = true;
                darstellungsBereichNeu = false;
            }
        }

        private void DarstellungsbereichDialog_Anzeigen(object sender, RoutedEventArgs e)
        {
            if (knoten == null)
            {
                _ = MessageBox.Show("Knoten muss erst ausgewählt werden", "dynamische Tragwerksberechnung");
            }
            else
            {
                visualErgebnisse.Children.Clear();
                ausschnitt = new Darstellungsbereich(tmax, maxVerformung, maxBeschleunigung);
                darstellungsBereichNeu = true;
            }
        }

        private void MaximalwertText(string ordinate, double wert, double zeit)
        {
            var rot = FromArgb(120, 255, 0, 0);
            var myBrush = new SolidColorBrush(rot);
            var maxwert = "Maximalwert für " + ordinate + " = " + wert.ToString("N4") + Environment.NewLine +
                          "an Zeit = " + zeit.ToString("N2");
            maximal = new TextBlock
            {
                FontSize = 12,
                Background = myBrush,
                Foreground = Black,
                FontWeight = FontWeights.Bold,
                Text = maxwert
            };
            SetTop(maximal, 10);
            SetLeft(maximal, 20);
            visualErgebnisse.Children.Add(maximal);
        }
    }
}