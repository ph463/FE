using FEALibrary.Modell;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace FE_Berechnungen.Eigenlösung
{
    public partial class Eigenlösung
    {
        private readonly FEModell modell;
        public Eigenlösung(FEModell modell)
        {
            this.modell = modell;
            InitializeComponent();
        }
        private void EigenwerteGrid_Loaded(object sender, RoutedEventArgs e)
        {
            Dictionary<string, double> eigenwerteGridItemsSource = new Dictionary<string, double>();
            int nStates = modell.Eigenstate.NumberOfStates;
            for (int k = 0; k < nStates; k++)
            {
                eigenwerteGridItemsSource.Add(k.ToString(), modell.Eigenstate.Eigenvalues[k]);
            }
            EigenwerteGrid = sender as DataGrid;
            if (EigenwerteGrid != null) EigenwerteGrid.ItemsSource = eigenwerteGridItemsSource;
        }
        private void EigenvektorenGrid_Loaded(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> eigenvektorenGridItemsSource = new Dictionary<string, string>();
            var dimension = modell.Eigenstate.Eigenvectors[0].Length;
            var i = 0;
            for (var j = 0; j < dimension; j++)
            {
                var line = modell.Eigenstate.Eigenvectors[0][i].ToString("N5");
                for (var k = 1; k < modell.Eigenstate.NumberOfStates; k++)
                {
                    line += "\t" + modell.Eigenstate.Eigenvectors[k][i].ToString("N5");
                }
                eigenvektorenGridItemsSource.Add(j.ToString(), line);
                i++;
            }
            EigenvektorenGrid = sender as DataGrid;
            if (EigenvektorenGrid != null) EigenvektorenGrid.ItemsSource = eigenvektorenGridItemsSource;
        }
        private void TitelLoaded(object sender, RoutedEventArgs e)
        {
            titel.Content = "Eigenlösung Identifikator = " + modell.Eigenstate.Id + ", Anzahl = "
                                                           + modell.Eigenstate.NumberOfStates;
        }
    }
}
