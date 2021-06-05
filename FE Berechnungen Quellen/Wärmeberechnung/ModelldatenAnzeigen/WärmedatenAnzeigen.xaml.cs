using FEALibrary.Modell;
using FEALibrary.Modell.abstrakte_Klassen;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace FE_Berechnungen.Wärmeberechnung.ModelldatenAnzeigen
{
    public partial class WärmedatenAnzeigen
    {
        public readonly FEModell modell;
        public WärmedatenAnzeigen(FEModell modell)
        {
            Language = XmlLanguage.GetLanguage("de-DE");
            this.modell = modell;
            InitializeComponent();
            DataContext = this.modell;
        }
        private void Knoten_Loaded(object sender, RoutedEventArgs e)
        {
            var knoten = modell.Knoten.Select(item => item.Value).ToList();
            KnotenGrid = sender as DataGrid;
            if (KnotenGrid != null) KnotenGrid.ItemsSource = knoten;
        }
        private void Elemente_Loaded(object sender, RoutedEventArgs e)
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
        private void Randbedingung_Loaded(object sender, RoutedEventArgs e)
        {
            var rand = modell.Randbedingungen.Select(item => item.Value).ToList();
            RandbedingungGrid = sender as DataGrid;
            if (RandbedingungGrid != null) RandbedingungGrid.ItemsSource = rand;
        }
        private void KnotenEinwirkungen_Loaded(object sender, RoutedEventArgs e)
        {
            var lasten = modell.PunktLasten.Select(item => item.Value).Cast<AbstraktLast>().ToList();
            KnotenEinwirkungenGrid = sender as DataGrid;
            if (KnotenEinwirkungenGrid != null) KnotenEinwirkungenGrid.ItemsSource = lasten;
        }
        private void LinienEinwirkungen_Loaded(object sender, RoutedEventArgs e)
        {
            var lasten = modell.LinienLasten.Select(item => item.Value).Cast<AbstraktLast>().ToList();
            LinienEinwirkungenGrid = sender as DataGrid;
            if (LinienEinwirkungenGrid != null) LinienEinwirkungenGrid.ItemsSource = lasten;
        }
        private void ElementEinwirkungen_Loaded(object sender, RoutedEventArgs e)
        {
            var lasten = modell.ElementLasten.Select(item => item.Value).Cast<AbstraktLast>().ToList();
            ElementEinwirkungenGrid = sender as DataGrid;
            if (ElementEinwirkungenGrid != null) ElementEinwirkungenGrid.ItemsSource = lasten;
        }
        //private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        //{
        //    // ... hol die TextBox, die editiert wurde
        //    var element = e.EditingElement as TextBox;
        //    var text = element.Text;

        //    // ... pruef, ob die Textveraenderung abgelehnt werden soll
        //    // ... Ablehnung, falls der Nutzer ein ? eingibt
        //    if (text == "?")
        //    {
        //        Title = "Invalid";
        //        e.Cancel = true;
        //    }
        //    else
        //    {
        //        // ... zeige den Zellenwert im Titel
        //        Title = "Eingabe: " + text;
        //    }
        //}
    }
}