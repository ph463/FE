using FEALibrary.Modell;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Markup;

namespace FE_Berechnungen.Wärmeberechnung.Ergebnisse
{
    public partial class InstationäreErgebnisseAnzeigen : Window
    {
        public FEModell modell;
        private Knoten knoten;
        private readonly double[] zeit;

        public double Dt { get; }
        public int NSteps { get; }
        public int Index { get; set; }
        public string KnotenId { get; set; }

        public InstationäreErgebnisseAnzeigen(FEModell modell)
        {
            this.Language = XmlLanguage.GetLanguage("de-DE");
            this.modell = modell;
            InitializeComponent();
            Show();

            Knotenauswahl.ItemsSource = this.modell.Knoten.Keys;

            Dt = this.modell.Zeitintegration.Dt;
            var tmax = this.modell.Zeitintegration.Tmax;
            NSteps = (int)(tmax / Dt) + 1;
            zeit = new double[NSteps];
            for (var i = 0; i < NSteps; i++) { zeit[i] = (i * Dt); }
            Zeitschrittauswahl.ItemsSource = zeit;
        }

        private void DropDownKnotenauswahlClosed(object sender, System.EventArgs e)
        {
            KnotenId = (string)Knotenauswahl.SelectedItem;
            if (modell != null && modell.Knoten.TryGetValue(KnotenId, out knoten)) { };
        }
        private void KnotentemperaturGrid_Anzeigen(object sender, RoutedEventArgs e)
        {
            if (knoten == null) return;
            var knotentemperaturen = new Dictionary<string, string>();
            var line = "Zustand des Knotens " + KnotenId;
            line += "\nZeit" + "\tTemperatur" + "\tGradient";
            knotentemperaturen.Add("Schritt", line);
            for (var i = 0; i < NSteps; i++)
            {
                line = zeit[i].ToString("N2");
                line += "\t" + knoten.NodalVariables[0][i].ToString("N4");
                line += "\t\t" + knoten.NodalDerivatives[0][i].ToString("N4");
                knotentemperaturen.Add(i.ToString(), line);
            }
            KnotentemperaturGrid.ItemsSource = knotentemperaturen;
        }

        private void DropDownZeitschrittauswahlClosed(object sender, System.EventArgs e)
        {
            Index = Zeitschrittauswahl.SelectedIndex;
        }
        private void ZeitschrittGrid_Anzeigen(object sender, RoutedEventArgs e)
        {
            var zeitschritt = new Dictionary<string, string>();
            var line = "Modellzustand  an Zeitschritt  " + Index;
            line += "\nTemperatur" + "\tGradient";
            zeitschritt.Add("Knoten", line);
            foreach (KeyValuePair<string, Knoten> item in modell.Knoten)
            {
                line = item.Value.NodalVariables[0][Index].ToString("N4");
                line += "\t\t" + item.Value.NodalDerivatives[0][Index].ToString("N4");
                zeitschritt.Add(item.Key, line);
            }
            ZeitschrittGrid.ItemsSource = zeitschritt;
        }
    }
}
