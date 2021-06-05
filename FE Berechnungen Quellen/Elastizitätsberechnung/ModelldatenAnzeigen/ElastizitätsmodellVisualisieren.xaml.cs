using FEALibrary.Modell;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace FE_Berechnungen.Elastizitätsberechnung.ModelldatenAnzeigen
{

    public partial class ElastizitätsmodellVisualisieren
    {
        private readonly Darstellung darstellung;
        private bool lastenAn = true, lagerAn = true, knotenTexteAn = true, elementTexteAn = true;

        public ElastizitätsmodellVisualisieren(FEModell feModel)
        {
            InitializeComponent();
            Show();
            darstellung = new Elastizitätsberechnung.Darstellung(feModel, VisualErgebnisse);
            darstellung.UnverformteGeometrie();

            // mit Element und Knoten Ids
            darstellung.KnotenTexte();
            darstellung.ElementTexte();
            darstellung.LastenZeichnen();
            darstellung.FesthaltungenZeichnen();
        }

        private void BtnKnotenIDs_Click(object sender, RoutedEventArgs e)
        {
            if (!knotenTexteAn)
            {
                darstellung.KnotenTexte();
                knotenTexteAn = true;
            }
            else
            {
                foreach (TextBlock id in darstellung.KnotenIDs) VisualErgebnisse.Children.Remove(id);
                knotenTexteAn = false;
            }
        }
        private void BtnElementIDs_Click(object sender, RoutedEventArgs e)
        {
            if (!elementTexteAn)
            {
                darstellung.ElementTexte();
                elementTexteAn = true;
            }
            else
            {
                foreach (TextBlock id in darstellung.ElementIDs) VisualErgebnisse.Children.Remove(id);
                elementTexteAn = false;
            }
        }

        private void BtnLasten_Click(object sender, RoutedEventArgs e)
        {
            if (!lastenAn)
            {
                darstellung.LastenZeichnen();
                lastenAn = true;
            }
            else
            {
                foreach (Shape lasten in darstellung.LastVektoren)
                {
                    VisualErgebnisse.Children.Remove(lasten);
                }
                lastenAn = false;
            }
        }
        private void BtnFesthaltungen_Click(object sender, RoutedEventArgs e)
        {
            if (!lagerAn)
            {
                darstellung.FesthaltungenZeichnen();
                lagerAn = true;
            }
            else
            {
                foreach (Shape path in darstellung.LagerDarstellung)
                {
                    VisualErgebnisse.Children.Remove(path);
                }
                lagerAn = false;
            }
        }
    }
}
