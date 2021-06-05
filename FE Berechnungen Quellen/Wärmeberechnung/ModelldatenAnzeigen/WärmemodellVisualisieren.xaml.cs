using FEALibrary.Modell;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Shapes;

namespace FE_Berechnungen.Wärmeberechnung.ModelldatenAnzeigen
{
    public partial class WärmemodellVisualisieren
    {
        private bool knotenAn = true, elementeAn = true;
        private bool knotenLastAn, elementLastAn, randbedingungAn;
        private readonly Darstellung darstellung;

        public WärmemodellVisualisieren(FEModell feModell)
        {
            Language = XmlLanguage.GetLanguage("de-DE");
            InitializeComponent();
            Show();

            darstellung = new Darstellung(feModell, VisualModel);
            darstellung.ElementeZeichnen();

            // mit Knoten und Element Ids
            darstellung.KnotenTexte();
            darstellung.ElementTexte();
        }

        private void BtnKnoten_Click(object sender, RoutedEventArgs e)
        {
            if (!knotenAn)
            {
                darstellung.KnotenTexte();
                knotenAn = true;
            }
            else
            {
                foreach (TextBlock id in darstellung.KnotenIDs) VisualModel.Children.Remove(id);
                knotenAn = false;
            }
        }
        private void BtnElemente_Click(object sender, RoutedEventArgs e)
        {
            if (!elementeAn)
            {
                darstellung.ElementTexte();
                elementeAn = true;
            }
            else
            {
                foreach (TextBlock id in darstellung.ElementIDs) VisualModel.Children.Remove(id);
                elementeAn = false;
            }
        }

        private void BtnKnotenlasten_Click(object sender, RoutedEventArgs e)
        {
            if (!knotenLastAn)
            {
                darstellung.KnotenlastenZeichnen();
                knotenLastAn = true;
            }
            else
            {
                foreach (TextBlock id in darstellung.LastKnoten) VisualModel.Children.Remove(id);
                knotenLastAn = false;
            }
        }
        private void BtnElementlasten_Click(object sender, RoutedEventArgs e)
        {
            if (!elementLastAn)
            {
                darstellung.ElementlastenZeichnen();
                elementLastAn = true;
            }
            else
            {
                foreach (Shape lastElement in darstellung.LastElemente) VisualModel.Children.Remove(lastElement);
                elementLastAn = false;
            }
        }
        private void BtnRandbedingungen_Click(object sender, RoutedEventArgs e)
        {
            if (!randbedingungAn)
            {
                darstellung.RandbedingungenZeichnen();
                randbedingungAn = true;
            }
            else
            {
                foreach (TextBlock randbedingung in darstellung.RandKnoten)
                {
                    VisualModel.Children.Remove(randbedingung);
                }
                randbedingungAn = false;
            }
        }
    }
}