using FEALibrary.Modell;
using FEALibrary.Modell.abstrakte_Klassen;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace FE_Berechnungen.Tragwerksberechnung.ModelldatenAnzeigen
{
    public partial class TragwerkdatenAnzeigen
    {
        private readonly FEModell modell;
        public TragwerkdatenAnzeigen(FEModell feModell)
        {
            this.Language = XmlLanguage.GetLanguage("de-DE");
            modell = feModell;
            InitializeComponent();
        }
        private void Knoten_Loaded(object sender, RoutedEventArgs e)
        {
            var knoten = modell.Knoten.Select(item => item.Value).ToList();
            KnotenGrid = sender as DataGrid;
            if (KnotenGrid != null) KnotenGrid.ItemsSource = knoten;
        }
        private void ElementeGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var elemente = modell.Elemente.Select(item => item.Value).ToList();
            ElementGrid = sender as DataGrid;
            if (ElementGrid != null) ElementGrid.ItemsSource = elemente;
        }
        private void Material_Loaded(object sender, RoutedEventArgs e)
        {
            var material = modell.Material.Select(item => item.Value).ToList();
            MaterialGrid = sender as DataGrid;
            if (MaterialGrid != null) MaterialGrid.ItemsSource = material;
        }
        private void Querschnitt_Loaded(object sender, RoutedEventArgs e)
        {
            var querschnitt = modell.Querschnitt.Select(item => item.Value).ToList();
            QuerschnittGrid = sender as DataGrid;
            if (QuerschnittGrid != null) QuerschnittGrid.ItemsSource = querschnitt;
        }
        private void Lager_Loaded(object sender, RoutedEventArgs e)
        {
            var lager = new List<AbstraktRandbedingung>();
            foreach (var item in modell.Randbedingungen)
            {
                for (var i = 0; i < item.Value.Prescribed.Length; i++)
                {
                    //if (!item.Value.Restrained[i]) item.Value.Prescribed[i] = Double.NaN;
                    if (!item.Value.Restrained[i]) item.Value.Prescribed[i] = double.PositiveInfinity;
                }
                lager.Add(item.Value);
            }
            LagerGrid = sender as DataGrid;
            if (LagerGrid != null) LagerGrid.ItemsSource = lager;
        }
        private void Knotenlast_Loaded(object sender, RoutedEventArgs e)
        {
            var lasten = modell.Lasten.Select(item => item.Value).ToList();
            KnotenlastGrid = sender as DataGrid;
            if (KnotenlastGrid != null) KnotenlastGrid.ItemsSource = lasten;
        }
        private void Punktlast_Loaded(object sender, RoutedEventArgs e)
        {
            var lasten = modell.PunktLasten.Select(item => item.Value).ToList();
            PunktlastGrid = sender as DataGrid;
            if (PunktlastGrid != null) PunktlastGrid.ItemsSource = lasten;
        }
        private void Linienlast_Loaded(object sender, RoutedEventArgs e)
        {
            var lasten = modell.ElementLasten.Select(item => item.Value).ToList();
            LinienlastGrid = sender as DataGrid;
            if (LinienlastGrid != null) LinienlastGrid.ItemsSource = lasten;
        }

        private void Model_Changed(object sender, DataGridCellEditEndingEventArgs e)
        {
            StartFenster.berechnet = false;
        }
    }
}
