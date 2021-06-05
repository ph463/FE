using System.Globalization;
using System.Windows;

namespace FE_Berechnungen.Wärmeberechnung.Ergebnisse
{
    public partial class Darstellungsbereich : Window
    {
        public double tmax;
        public double maxTemperatur, minTemperatur;
        public double maxWärmefluss, minWärmefluss;

        public Darstellungsbereich(double tmax, double maxTemperatur, double maxWärmefluss)
        {
            InitializeComponent();
            this.tmax = tmax;
            this.maxTemperatur = maxTemperatur;
            this.maxWärmefluss = maxWärmefluss;
            TxtMaxZeit.Text = this.tmax.ToString(CultureInfo.CurrentCulture);
            TxtMaxTemperatur.Text = this.maxTemperatur.ToString("N2");
            TxtMaxWärmefluss.Text = this.maxWärmefluss.ToString("N2");
            Show();
        }

        private void BtnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            tmax = double.Parse(TxtMaxZeit.Text);
            maxTemperatur = double.Parse(TxtMaxTemperatur.Text);
            maxWärmefluss = double.Parse(TxtMaxWärmefluss.Text);
            Close();
        }
        private void BtnDialogCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
