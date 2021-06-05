using FEALibrary.Modell;
using FEALibrary.Modell.abstrakte_Klassen;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace FE_Berechnungen.Tragwerksberechnung.ModelldatenAnzeigen
{
    public partial class DynamikDatenAnzeigen
    {
        public FEModell modell;
        private List<TimeInterval> intervals;

        public DynamikDatenAnzeigen(FEModell feModell)
        {
            this.Language = XmlLanguage.GetLanguage("de-DE");
            modell = feModell;
            InitializeComponent();
            //DataContext für Integrationsparameter
            DataContext = modell;
        }

        private void Daten_Loaded(object sender, RoutedEventArgs e)
        {
            // ************************* Anfangsbedingungen *********************************
            if (modell.Zeitintegration.Anfangsbedingungen.Count > 0)
            {
                var anfangsverformungen = modell.Zeitintegration.Anfangsbedingungen.Cast<Knotenwerte>().ToList();
                AnfangsbedingungenGrid.ItemsSource = anfangsverformungen;
            }

            // ************************* Zeitabhängige KnotenLasten ***********************
            if (modell.ZeitabhängigeKnotenLasten.Count > 0)
            {
                foreach (var item in modell.ZeitabhängigeKnotenLasten)
                {
                    var variationTyp = item.Value.VariationType;
                    var knotenVerformungen = new List<AbstraktZeitabhängigeKnotenlast>();
                    switch (variationTyp)
                    {
                        // lies Lastvektor aus Datei
                        case 0:
                            Knoten.Text = item.Value.NodeId + "    " + item.Value.NodalDof;
                            KnotenVerformung.Text = "Bodenanregung\nAnregungsverlauf aus festzulegender Datei einlesen";
                            break;
                        // lies Zeit-/Wert-Intervalle der Anregung mit linearer Interpolation
                        case 1:
                            {
                                intervals = new List<TimeInterval>();
                                foreach (var item1 in modell.ZeitabhängigeKnotenLasten)
                                {
                                    knotenVerformungen.Add(item1.Value);
                                    for (var i = 0; i < item1.Value.Interval.Length; i += 2)
                                    {
                                        intervals.Add(new TimeInterval(item1.Value.NodeId, item1.Value.Interval[i], item1.Value.Interval[i + 1]));
                                    }
                                }
                                KnotenlastenGrid.ItemsSource = intervals;
                                KnotenVerformung.Text = "Bodenanregung";
                                break;
                            }
                        // lies harmonische Anregung
                        case 2:
                            {
                                knotenVerformungen = modell.ZeitabhängigeKnotenLasten.Select(item1 => item1.Value).ToList();
                                KnotenHarmonischGrid.ItemsSource = knotenVerformungen;
                                break;
                            }
                    }
                }
            }

            // ************************* Knotendämpfungsraten ***********************
            if (modell.Zeitintegration.DämpfungsRaten.Count <= 0) return;
            var dämpfungsRaten = modell.Zeitintegration.DämpfungsRaten.Cast<Knotenwerte>().ToList();
            DämpfungGrid.ItemsSource = dämpfungsRaten;
        }

        private void KnotenlastenGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            var k = 0;
            foreach (var item1 in modell.ZeitabhängigeKnotenLasten)
            {
                for (var i = 0; i < item1.Value.Interval.Length; i += 2)
                {
                    item1.Value.Interval[i] = intervals[k].Time;
                    item1.Value.Interval[i + 1] = intervals[k].Force;
                    k++;
                }
            }
        }
    }
}