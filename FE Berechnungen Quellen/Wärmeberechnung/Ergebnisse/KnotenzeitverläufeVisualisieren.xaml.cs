using FEALibrary.Modell;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using static System.Windows.Media.Brushes;
using static System.Windows.Media.Color;

namespace FE_Berechnungen.Wärmeberechnung.Ergebnisse
{
    public partial class KnotenzeitverläufeVisualisieren : Window
    {
        private readonly FEModell modell;
        private Knoten knoten;
        private readonly double dt;
        public double tmax;
        public double maxTemperatur, minTemperatur;
        public double maxWärmefluss, minWärmefluss;

        private readonly Darstellung darstellung;
        private Darstellungsbereich ausschnitt;

        private bool darstellungsBereichNeu = false;
        private bool temperaturVerlauf = false, wärmeflussVerlauf = false;
        private TextBlock maximal;

        public KnotenzeitverläufeVisualisieren(FEModell modell)
        {
            Language = XmlLanguage.GetLanguage("de-DE");
            this.modell = modell;
            InitializeComponent();
            Show();

            // Festlegung der Zeitachse
            dt = modell.Zeitintegration.Dt;
            tmax = modell.Zeitintegration.Tmax;

            // Auswahl des Knotens
            Knotenauswahl.ItemsSource = modell.Knoten.Keys;

            // Initialisierung der Zeichenfläche
            darstellung = new Darstellung(modell, VisualErgebnisse);
        }

        private void DropDownKnotenauswahlClosed(object sender, System.EventArgs e)
        {
            var knotenId = (string)Knotenauswahl.SelectedItem;
            if (modell.Knoten.TryGetValue(knotenId, out knoten)) { }
        }

        private void KnotentemperaturVerlauf_Anzeigen(object sender, RoutedEventArgs e)
        {
            if (knoten == null)
            {
                _ = MessageBox.Show("Knoten muss erst ausgewählt werden", "instationäre Wärmeberechnung");
            }
            else
            {
                switch (temperaturVerlauf)
                {
                    case true when darstellungsBereichNeu:
                        VisualErgebnisse.Children.Clear();
                        maxTemperatur = ausschnitt.maxTemperatur;
                        minTemperatur = 0;
                        break;
                    case false:
                        VisualErgebnisse.Children.Clear();
                        maxTemperatur = knoten.NodalVariables[0].Max();
                        minTemperatur = 0;
                        break;
                }
                darstellung.Koordinatensystem(tmax, maxTemperatur, minTemperatur);

                // Textdarstellung des Maximalwertes mit Zeitpunkt
                VisualErgebnisse.Children.Remove(maximal);
                var maxTemperaturText = knoten.NodalVariables[0].Max();
                var zeit = dt * Array.IndexOf(knoten.NodalVariables[0], maxTemperaturText);
                MaximalwertText("Temperatur", maxTemperaturText, zeit);

                darstellung.ZeitverlaufZeichnen(dt, tmax, maxTemperatur, knoten.NodalVariables[0]);

                temperaturVerlauf = true;
                wärmeflussVerlauf = false;
                darstellungsBereichNeu = false;
            }
        }

        private void KnotenWärmeflussVerlauf_Anzeigen(object sender, RoutedEventArgs e)
        {
            if (knoten == null)
            {
                _ = MessageBox.Show("Knoten muss erst ausgewählt werden", "instationäre Wärmeberechnung");
            }
            else
            {
                switch (wärmeflussVerlauf)
                {
                    case true when darstellungsBereichNeu:
                        VisualErgebnisse.Children.Clear();
                        maxWärmefluss = ausschnitt.maxWärmefluss;
                        minWärmefluss = -maxWärmefluss;
                        break;
                    case false:
                        VisualErgebnisse.Children.Clear();
                        maxWärmefluss = knoten.NodalDerivatives[0].Max();
                        minWärmefluss = -maxWärmefluss;
                        break;
                }
                darstellung.Koordinatensystem(tmax, maxWärmefluss, minWärmefluss);

                // Textdarstellung des Maximalwertes mit Zeitpunkt
                VisualErgebnisse.Children.Remove(maximal);
                var maxWärmeflussText = knoten.NodalDerivatives[0].Max();
                var zeit = dt * Array.IndexOf(knoten.NodalDerivatives[0], maxWärmeflussText);
                MaximalwertText("Wärmefluss", maxWärmeflussText, zeit);

                darstellung.ZeitverlaufZeichnen(dt, tmax, maxWärmefluss, knoten.NodalDerivatives[0]);

                temperaturVerlauf = false; ;
                wärmeflussVerlauf = true;
                darstellungsBereichNeu = false;
            }
        }

        private void DarstellungsbereichDialog_Anzeigen(object sender, RoutedEventArgs e)
        {
            if (knoten == null)
            {
                _ = MessageBox.Show("Knoten muss erst ausgewählt werden", "instationäre Wärmeberechnung");
            }
            else
            {
                VisualErgebnisse.Children.Clear();
                ausschnitt = new Darstellungsbereich(tmax, maxTemperatur, maxWärmefluss);
                darstellungsBereichNeu = true;
            }
        }

        private void MaximalwertText(string ordinate, double wert, double zeit)
        {
            var rot = FromArgb(120, 255, 0, 0);
            var myBrush = new SolidColorBrush(rot);
            var maxwert = "Maximalwert für " + ordinate + " = " + wert.ToString("N2") + Environment.NewLine +
                          "an Zeit = " + zeit.ToString("N2");
            maximal = new TextBlock
            {
                FontSize = 12,
                Background = myBrush,
                Foreground = Black,
                FontWeight = FontWeights.Bold,
                Text = maxwert
            };
            Canvas.SetTop(maximal, 10);
            Canvas.SetLeft(maximal, 20);
            VisualErgebnisse.Children.Add(maximal);
        }
    }
}
