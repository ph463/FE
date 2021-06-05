using FEALibrary.Modell;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Shapes;

namespace FE_Berechnungen.Tragwerksberechnung.ModelldatenAnzeigen
{
    public partial class TragwerkmodellVisualisieren
    {
        private readonly Darstellung darstellung;
        private bool lastenAn = true, lagerAn = true, knotenTexteAn = true, elementTexteAn = true;

        public TragwerkmodellVisualisieren(FEModell feModell)
        {
            Language = XmlLanguage.GetLanguage("de-DE");
            InitializeComponent();
            Show();

            darstellung = new Darstellung(feModell, VisualModel);
            darstellung.UnverformteGeometrie();

            // mit Knoten und Element Ids
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
                foreach (TextBlock id in darstellung.KnotenIDs) VisualModel.Children.Remove(id);
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
                foreach (TextBlock id in darstellung.ElementIDs) VisualModel.Children.Remove(id);
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
                    VisualModel.Children.Remove(lasten);
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
                    VisualModel.Children.Remove(path);
                }
                lagerAn = false;
            }
        }
    }
}