using FEALibrary.Modell;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Markup;

namespace FE_Berechnungen.Tragwerksberechnung.Ergebnisse
{
    public partial class DynamischeErgebnisseAnzeigen
    {
        private readonly FEModell modell;
        private Knoten knoten;

        private double Dt { get; }
        private int NSteps { get; }
        private int Index { get; set; }
        private string KnotenId { get; set; }

        public DynamischeErgebnisseAnzeigen(FEModell feModell)
        {
            this.Language = XmlLanguage.GetLanguage("de-DE");
            modell = feModell;
            InitializeComponent();
            Show();

            Knotenauswahl.ItemsSource = modell.Knoten.Keys;

            // Auswahl des Zeitschritts aus Zeitraster, z.B. jeder 10.
            Dt = modell.Zeitintegration.Dt;
            var tmax = modell.Zeitintegration.Tmax;
            NSteps = (int)(tmax / Dt);
            const int zeitraster = 1;
            //if (NSteps > 1000) zeitraster = 10;
            NSteps = (NSteps / zeitraster) + 1;
            var zeit = new double[NSteps];
            for (var i = 0; i < NSteps; i++) { zeit[i] = (i * Dt * zeitraster); }

            Zeitschrittauswahl.ItemsSource = zeit;
        }

        private void DropDownKnotenauswahlClosed(object sender, System.EventArgs e)
        {
            KnotenId = (string)Knotenauswahl.SelectedItem;
            if (modell.Knoten.TryGetValue(KnotenId, out knoten)) { }
        }
        private void KnotenverformungenGrid_Anzeigen(object sender, RoutedEventArgs e)
        {
            if (knoten == null) return;
            var knotenverformungen = new List<Knotenverformungen>();
            var dt = modell.Zeitintegration.Dt;
            var nSteps = knoten.NodalVariables[0].Length;
            var zeit = new double[nSteps + 1];
            zeit[0] = 0;

            Knotenverformungen knotenverformung = null;
            for (var i = 0; i < nSteps; i++)
            {
                switch (knoten.NodalVariables.Length)
                {
                    case 2:
                        knotenverformung = new Knotenverformungen(zeit[i], knoten.NodalVariables[0][i], knoten.NodalVariables[1][i],
                            knoten.NodalDerivatives[0][i], knoten.NodalDerivatives[1][i]);
                        break;
                    case 3:
                        knotenverformung = new Knotenverformungen(zeit[i], knoten.NodalVariables[0][i], knoten.NodalVariables[1][i], knoten.NodalVariables[2][i],
                            knoten.NodalDerivatives[0][i], knoten.NodalDerivatives[1][i], knoten.NodalDerivatives[2][i]);
                        break;
                }
                knotenverformungen.Add(knotenverformung);
                zeit[i + 1] = zeit[i] + dt;
            }
            KnotenverformungenGrid.ItemsSource = knotenverformungen;
        }

        private void DropDownZeitschrittauswahlClosed(object sender, System.EventArgs e)
        {
            Index = Zeitschrittauswahl.SelectedIndex;
        }
        private void ZeitschrittGrid_Anzeigen(object sender, RoutedEventArgs e)
        {
            if (Index == 0) return;
            var zeitschritt = new List<Knotenverformungen>();
            var dt = modell.Zeitintegration.Dt;
            var tmax = modell.Zeitintegration.Tmax;
            var nSteps = (int)(tmax / dt) + 1;
            var zeit = new double[nSteps + 1];
            zeit[0] = 0;

            Knotenverformungen knotenverformung = null;
            foreach (var item in modell.Knoten)
            {
                // eingabeEinheit z.B. in m, verformungsEinheit z.B. cm, beschleunigungsEinheit z.B. cm/s/s
                const int verformungsEinheit = 1;
                knoten = item.Value;
                switch (knoten.NodalVariables.Length)
                {
                    case 2:
                        knotenverformung = new Knotenverformungen(item.Value.Id,
                            knoten.NodalVariables[0][Index] * verformungsEinheit, knoten.NodalVariables[1][Index] * verformungsEinheit,
                            knoten.NodalDerivatives[0][Index] * verformungsEinheit, knoten.NodalDerivatives[1][Index] * verformungsEinheit);
                        break;
                    case 3:
                        knotenverformung = new Knotenverformungen(item.Value.Id,
                            knoten.NodalVariables[0][Index] * verformungsEinheit, knoten.NodalVariables[1][Index] * verformungsEinheit,
                            knoten.NodalVariables[2][Index] * verformungsEinheit,
                            knoten.NodalDerivatives[0][Index] * verformungsEinheit, knoten.NodalDerivatives[1][Index] * verformungsEinheit,
                            knoten.NodalDerivatives[2][Index] * verformungsEinheit);
                        break;
                }
                zeitschritt.Add(knotenverformung);
            }
            ZeitschrittGrid.ItemsSource = zeitschritt;
        }
    }
}
