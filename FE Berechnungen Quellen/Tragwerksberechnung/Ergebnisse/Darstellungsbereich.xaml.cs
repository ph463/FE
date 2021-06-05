using System.Globalization;
using System.Windows;

namespace FE_Berechnungen.Tragwerksberechnung.Ergebnisse
{
    public partial class Darstellungsbereich : Window
    {
        public double tmax;
        public double maxVerformung, minVerformung;
        public double maxBeschleunigung, minBeschleunigung;

        public Darstellungsbereich(double tmax, double maxVerformung, double maxBeschleunigung)
        {
            InitializeComponent();
            this.tmax = tmax;
            this.maxVerformung = maxVerformung;
            this.maxBeschleunigung = maxBeschleunigung;
            TxtMaxZeit.Text = this.tmax.ToString(CultureInfo.CurrentCulture);
            TxtMaxVerformung.Text = this.maxVerformung.ToString("N2");
            TxtMaxBeschleunigung.Text = this.maxBeschleunigung.ToString("N2");
            Show();
        }

        private void BtnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            tmax = double.Parse(TxtMaxZeit.Text);
            maxVerformung = double.Parse(TxtMaxVerformung.Text);
            maxBeschleunigung = double.Parse(TxtMaxBeschleunigung.Text);
            Close();
        }

        private void BtnDialogCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
