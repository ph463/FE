using FEALibrary.Modell;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace FE_Berechnungen.Tragwerksberechnung.Ergebnisse
{
    public partial class EigenlösungAnzeigen
    {
        private readonly FEModell modell;

        public EigenlösungAnzeigen(FEModell feModell)
        {
            Language = XmlLanguage.GetLanguage("de-DE");
            modell = feModell;
            InitializeComponent();
        }

        private void EigenfrequenzenGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var eigenfrequenzen = new Dictionary<int, double>();
            var nStates = modell.Eigenstate.NumberOfStates;
            for (var k = 0; k < nStates; k++)
            {
                var value = Math.Sqrt(modell.Eigenstate.Eigenvalues[k]) / 2 / Math.PI;
                eigenfrequenzen.Add(k, value);
            }
            EigenfrequenzenGrid = sender as DataGrid;
            if (EigenfrequenzenGrid != null) EigenfrequenzenGrid.ItemsSource = eigenfrequenzen;
        }

        private void EigenvektorenGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var eigenvektoren = new Dictionary<string, string>();
            var dimension = modell.Eigenstate.Eigenvectors[0].Length;
            var i = 0;
            for (var j = 0; j < dimension; j++)
            {
                var line = modell.Eigenstate.Eigenvectors[0][i].ToString("N5");
                for (var k = 1; k < modell.Eigenstate.NumberOfStates; k++)
                {
                    line += "\t" + modell.Eigenstate.Eigenvectors[k][i].ToString("N5");
                }
                eigenvektoren.Add(j.ToString(), line);
                i++;
            }
            EigenvektorenGrid = sender as DataGrid;
            if (EigenvektorenGrid != null) EigenvektorenGrid.ItemsSource = eigenvektoren;
        }
    }
}
