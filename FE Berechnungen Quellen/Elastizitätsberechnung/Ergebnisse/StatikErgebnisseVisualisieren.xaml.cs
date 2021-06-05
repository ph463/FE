using FEALibrary.Modell;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Shapes;

namespace FE_Berechnungen.Elastizitätsberechnung.Ergebnisse
{

    public partial class StatikErgebnisseVisualisieren
    {
        private readonly Darstellung darstellung;
        private bool elementTexteAn = true, knotenTexteAn = true, verformungenAn, spannungenAn, reaktionenAn;

        public StatikErgebnisseVisualisieren(FEModell feModell)
        {
            Language = XmlLanguage.GetLanguage("de-DE");
            InitializeComponent();
            Show();

            darstellung = new Darstellung(feModell, VisualErgebnisse);

            // unverformte Geometrie
            darstellung.UnverformteGeometrie();

            // mit Element Ids
            darstellung.ElementTexte();

            // mit Knoten Ids
            darstellung.KnotenTexte();
        }

        private void BtnVerformung_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!verformungenAn)
            {
                darstellung.VerformteGeometrie();
                verformungenAn = true;
            }
            else
            {
                foreach (Shape path in darstellung.Verformungen)
                {
                    VisualErgebnisse.Children.Remove(path);
                }
                verformungenAn = false;
            }
        }
        private void BtnSpannungen_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!spannungenAn)
            {
                // zeichne Spannungsvektoren in Elementmitte
                darstellung.SpannungenZeichnen();
                spannungenAn = true;
            }
            else
            {
                // entferne Spannungsvektoren
                foreach (Shape path in darstellung.Spannungen)
                {
                    VisualErgebnisse.Children.Remove(path);
                }
                spannungenAn = false;
            }
        }
        private void Reaktionen_Click(object sender, RoutedEventArgs e)
        {
            if (!reaktionenAn)
            {
                // zeichne Reaktionen an Festhaltungen
                darstellung.ReaktionenZeichnen();
                reaktionenAn = true;
            }
            else
            {
                // entferne Spannungsvektoren
                foreach (Shape path in darstellung.Reaktionen)
                {
                    VisualErgebnisse.Children.Remove(path);
                }
                reaktionenAn = false;
            }
        }

        private void BtnElementIDs_Click(object sender, System.Windows.RoutedEventArgs e)
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
        private void BtnKnotenIDs_Click(object sender, System.Windows.RoutedEventArgs e)
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

        private void BtnÜberhöhung_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            darstellung.überhöhungVerformung = double.Parse(Überhöhung.Text);
            foreach (Shape path in darstellung.Verformungen)
            {
                VisualErgebnisse.Children.Remove(path);
            }
            verformungenAn = false;
            darstellung.VerformteGeometrie();
            verformungenAn = true;
        }
        //private void OnKeyDownHandler(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Return)
        //    {
        //        überhöhung = Überhöhung.Text;
        //    }
        //}
    }
}
