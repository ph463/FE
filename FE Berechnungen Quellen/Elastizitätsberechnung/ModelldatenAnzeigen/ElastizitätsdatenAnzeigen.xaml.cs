using FEALibrary.Modell;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace FE_Berechnungen.Elastizitätsberechnung.ModelldatenAnzeigen
{
    public partial class ElastizitätsdatenAnzeigen
    {
        private readonly FEModell modell;
        public ElastizitätsdatenAnzeigen(FEModell modell)
        {
            this.Language = XmlLanguage.GetLanguage("de-DE");
            this.modell = modell;
            InitializeComponent();
        }
        private void Knoten_Loaded(object sender, RoutedEventArgs e)
        {
            KnotenGrid = sender as DataGrid;
            if (KnotenGrid != null) KnotenGrid.ItemsSource = modell.Knoten;
        }
        private void ElementeGrid_Loaded(object sender, RoutedEventArgs e)
        {
            ElementGrid = sender as DataGrid;
            if (ElementGrid != null) ElementGrid.ItemsSource = modell.Elemente;
        }
        private void Material_Loaded(object sender, RoutedEventArgs e)
        {
            MaterialGrid = sender as DataGrid;
            if (MaterialGrid != null) MaterialGrid.ItemsSource = modell.Material;
        }
        private void Knotenlast_Loaded(object sender, RoutedEventArgs e)
        {
            KnotenlastGrid = sender as DataGrid;
            if (KnotenlastGrid != null) KnotenlastGrid.ItemsSource = modell.Lasten;
        }
        private void Rand_Loaded(object sender, RoutedEventArgs e)
        {
            var rand = new Dictionary<string, Lagerbedingung>();
            foreach (var item in modell.Randbedingungen)
            {
                var nodeId = item.Value.NodeId;
                var supportName = item.Value.SupportId;
                string[] vordefiniert = { "frei", "frei", "frei" };

                switch (item.Value.Type)
                {
                    case 1:
                        {
                            if (item.Value.Restrained[0]) vordefiniert[0] = item.Value.Prescribed[0].ToString("F4");
                            if (modell.SpatialDimension == 2) vordefiniert[2] = "";
                            break;
                        }
                    case 2:
                        {
                            if (item.Value.Restrained[1]) vordefiniert[1] = item.Value.Prescribed[1].ToString("F4");
                            if (modell.SpatialDimension == 2) vordefiniert[2] = "";
                            break;
                        }
                    case 3:
                        {
                            if (item.Value.Restrained[0]) vordefiniert[0] = item.Value.Prescribed[0].ToString("F4");
                            if (item.Value.Restrained[1]) vordefiniert[1] = item.Value.Prescribed[1].ToString("F4");
                            if (modell.SpatialDimension == 2) vordefiniert[2] = "";
                            break;
                        }
                    case 4:
                        {
                            if (item.Value.Restrained[2]) vordefiniert[2] = item.Value.Prescribed[2].ToString("F4");
                            break;
                        }
                    case 5:
                        {
                            if (item.Value.Restrained[0]) vordefiniert[0] = item.Value.Prescribed[0].ToString("F4");
                            if (item.Value.Restrained[2]) vordefiniert[2] = item.Value.Prescribed[2].ToString("F4");
                            break;
                        }
                    case 6:
                        {
                            if (item.Value.Restrained[1]) vordefiniert[1] = item.Value.Prescribed[1].ToString("F4");
                            if (item.Value.Restrained[2]) vordefiniert[2] = item.Value.Prescribed[2].ToString("F4");
                            break;
                        }
                    case 7:
                        {
                            if (item.Value.Restrained[0]) vordefiniert[0] = item.Value.Prescribed[0].ToString("F4");
                            if (item.Value.Restrained[1]) vordefiniert[1] = item.Value.Prescribed[1].ToString("F4");
                            if (item.Value.Restrained[2]) vordefiniert[2] = item.Value.Prescribed[2].ToString("F4");
                            break;
                        }
                    default:
                        throw new ModellAusnahme("Lagerbedingung für Lager " + supportName + " falsch definiert");
                }

                var lager = new Lagerbedingung(nodeId, vordefiniert);
                rand.Add(item.Key, lager);
            }
            RandGrid = sender as DataGrid;
            if (RandGrid != null) RandGrid.ItemsSource = rand;
        }
    }
}