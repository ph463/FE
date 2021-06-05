using FEALibrary.Modell;
using FEALibrary.Modell.abstrakte_Klassen;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Markup;

namespace FE_Berechnungen.Wärmeberechnung.ModelldatenAnzeigen
{
    public partial class InstationäreDatenAnzeigen
    {
        private readonly FEModell modell;
        private List<TimeInterval> intervals;

        public InstationäreDatenAnzeigen(FEModell modell)
        {
            Language = XmlLanguage.GetLanguage("de-DE");
            this.modell = modell;
            InitializeComponent();
            DataContext = this.modell;
        }

        private void Daten_Loaded(object sender, RoutedEventArgs e)
        {
            // ************************* Anfangsbedingungen *********************************
            if (modell.Zeitintegration.FromStationary == true)
            {
                Alle.Text = "alle";
                Temperatur.Text = "stationäre Loesung";
            }
            if (modell.Zeitintegration.Anfangsbedingungen.Count > 0)
            {
                var anfangstemperaturen = modell.Zeitintegration.Anfangsbedingungen.Cast<Knotenwerte>().ToList();
                AnfangstemperaturenGrid.ItemsSource = anfangstemperaturen;
            }

            // ************************* Zeitabhängige Randbedingungen ***********************
            if (modell.ZeitabhängigeRandbedingung.Count > 0)
            {
                foreach (var item in modell.ZeitabhängigeRandbedingung)
                {
                    var variationTyp = item.Value.VariationType;
                    switch (variationTyp)
                    {
                        case 0:
                            RandKnoten.Text = item.Value.NodeId;
                            RandTemperatur.Text = "aus Datei";
                            break;
                        case 1:
                            {
                                var randbedingungen = new List<AbstraktZeitabhängigeRandbedingung>();
                                intervals = new List<TimeInterval>();
                                foreach (var item1 in modell.ZeitabhängigeRandbedingung)
                                {
                                    randbedingungen.Add(item1.Value);
                                    for (var i = 0; i < item1.Value.Interval.Length; i += 2)
                                    {
                                        intervals.Add(new TimeInterval(item1.Value.NodeId, item1.Value.Interval[i], item1.Value.Interval[i + 1]));
                                    }
                                }
                                RandtemperaturenGrid.ItemsSource = intervals;
                                break;
                            }
                        case 2:
                            {
                                var randbedingungen = modell.ZeitabhängigeRandbedingung.Select(item1 => item1.Value).ToList();
                                RandHarmonischGrid.ItemsSource = randbedingungen;
                                break;
                            }
                    }
                }
            }

            // ************************* ZeitabhängigeKnotenTemperaturen ********************************
            if (modell.ZeitabhängigeKnotenLasten.Count > 0)
            {
                foreach (var item in modell.ZeitabhängigeKnotenLasten)
                {
                    var variationTyp = item.Value.VariationType;
                    switch (variationTyp)
                    {
                        case 0:
                            Knoten.Text = item.Value.NodeId;
                            knotenTemperatur.Text = "aus Datei";
                            break;
                        case 1:
                            {
                                var knotenTemperaturen = new List<AbstraktZeitabhängigeKnotenlast>();
                                intervals = new List<TimeInterval>();
                                foreach (var item1 in modell.ZeitabhängigeKnotenLasten)
                                {
                                    knotenTemperaturen.Add(item1.Value);
                                    for (var i = 0; i < item1.Value.Interval.Length; i += 2)
                                    {
                                        intervals.Add(new TimeInterval(item1.Value.NodeId, item1.Value.Interval[i], item1.Value.Interval[i + 1]));
                                    }
                                }
                                KnotentemperaturGrid.ItemsSource = intervals;
                                break;
                            }
                        case 2:
                            {
                                var knotenTemperaturen = modell.ZeitabhängigeKnotenLasten.Select(item1 => item1.Value).ToList();
                                KnotenHarmonischGrid.ItemsSource = knotenTemperaturen;
                                break;
                            }
                    }
                }
            }

            // ************************* ZeitabhängigeElementLasten *********************************
            if (modell.ZeitabhängigeElementLasten.Count <= 0) return;
            {
                var elementLasten = modell.ZeitabhängigeElementLasten.Select(item => item.Value).ToList();
                ElementtemperaturenGrid.ItemsSource = elementLasten;
            }
        }

        private void RandtemperaturenGrid_RowEditEnding(object sender, System.Windows.Controls.DataGridRowEditEndingEventArgs e)
        {
            var k = 0;
            foreach (var item1 in modell.ZeitabhängigeKnotenLasten)
            {
                for (var i = 0; i < item1.Value.Interval.Length; i += 2)
                {
                    item1.Value.Interval[i] = intervals[k].Time;
                    item1.Value.Interval[i + 1] = intervals[k].Temperatur;
                    k++;
                }
            }
        }

        private void KnotentemperaturGrid_RowEditEnding(object sender, System.Windows.Controls.DataGridRowEditEndingEventArgs e)
        {
            var k = 0;
            foreach (var item1 in modell.ZeitabhängigeKnotenLasten)
            {
                for (var i = 0; i < item1.Value.Interval.Length; i += 2)
                {
                    item1.Value.Interval[i] = intervals[k].Time;
                    item1.Value.Interval[i + 1] = intervals[k].Temperatur;
                    k++;
                }
            }
        }
    }
    public class TimeInterval
    {
        public string KnotenId { get; set; }
        public double Time { get; set; }
        public double Temperatur { get; set; }

        public TimeInterval(string knotenId, double time, double temperatur)
        {
            KnotenId = knotenId;
            Time = time;
            Temperatur = temperatur;
        }
    }
}