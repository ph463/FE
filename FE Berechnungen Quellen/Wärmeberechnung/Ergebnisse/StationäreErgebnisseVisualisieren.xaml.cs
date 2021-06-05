using FEALibrary.Modell;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Shapes;

namespace FE_Berechnungen.Wärmeberechnung.Ergebnisse
{
    public partial class StationäreErgebnisseVisualisieren
    {
        private readonly FEModell modell;
        private Darstellung darstellung;
        private bool knotenTemperaturAn, elementTemperaturAn, wärmeflussAn;

        public StationäreErgebnisseVisualisieren(FEModell model)
        {
            Language = XmlLanguage.GetLanguage("de-DE");
            modell = model;
            InitializeComponent();
        }
        private void ModelGrid_Loaded(object sender, RoutedEventArgs e)
        {
            darstellung = new Darstellung(modell, VisualErgebnisse);
            darstellung.FestlegungAuflösung();
            darstellung.ElementeZeichnen();
            darstellung.KnotentemperaturZeichnen();
            knotenTemperaturAn = true;
        }

        private void BtnKnotentemperatur_Click(object sender, RoutedEventArgs e)
        {
            if (!knotenTemperaturAn)
            {
                // zeichne den Wert einer jeden Randbedingung als Text an Randknoten
                darstellung.KnotentemperaturZeichnen();
                knotenTemperaturAn = true;
            }
            else
            {
                // entferne ALLE Textdarstellungen der Knotentemperaturen
                foreach (var knotenTemp in darstellung.Knotentemperaturen)
                {
                    VisualErgebnisse.Children.Remove(knotenTemp);
                }
                knotenTemperaturAn = false;
            }
        }

        private void BtnWärmefluss_Click(object sender, RoutedEventArgs e)
        {
            if (!wärmeflussAn)
            {
                // zeichne ALLE resultierenden Wärmeflussvektoren in Elementschwerpunkten
                darstellung.WärmeflussvektorenZeichnen();

                // zeichne den Wert einer jeden Randbedingung als Text an Randknoten
                darstellung.RandbedingungenZeichnen();
                wärmeflussAn = true;
            }
            else
            {
                // entferne ALLE resultierenden Wärmeflussvektoren in Elementschwerpunkten
                foreach (Shape path in darstellung.WärmeVektoren)
                {
                    VisualErgebnisse.Children.Remove(path);
                }

                // entferne ALLE Textdarstellungen der Randbedingungen
                foreach (var rand in darstellung.RandKnoten)
                {
                    VisualErgebnisse.Children.Remove((TextBlock)rand);
                }
                wärmeflussAn = false;
            }
        }

        private void BtnElementTemperaturen_Click(object sender, RoutedEventArgs e)
        {
            if (!elementTemperaturAn)
            {
                darstellung.ElementTemperaturZeichnen();
                elementTemperaturAn = true;
            }
            else
            {
                foreach (var path in darstellung.TemperaturElemente)
                {
                    VisualErgebnisse.Children.Remove(path);
                }
                elementTemperaturAn = false;
            }
        }
    }
}