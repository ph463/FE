﻿using FEALibrary.Modell;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace FE_Berechnungen.Elastizitätsberechnung.Ergebnisse
{

    public partial class StatikErgebnisseAnzeigen
    {
        private readonly FEModell modell;

        public StatikErgebnisseAnzeigen(FEModell feModell)
        {
            Language = XmlLanguage.GetLanguage("de-DE");
            modell = feModell;
            InitializeComponent();
            DataContext = this;
        }

        private void Knotenverformungen_Loaded(object sender, RoutedEventArgs e)
        {
            KnotenverformungenGrid = sender as DataGrid;
            if (KnotenverformungenGrid != null) KnotenverformungenGrid.ItemsSource = modell.Knoten;
        }

        private void ElementspannungenGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var elementSpannungen = new Dictionary<string, ElementSpannung>();
            foreach (var item in modell.Elemente)
            {
                var elementSpannung = new ElementSpannung(item.Value.ComputeZustandsvektor());
                elementSpannungen.Add(item.Key, elementSpannung);
            }
            ElementspannungenGrid = sender as DataGrid;
            if (ElementspannungenGrid != null) ElementspannungenGrid.ItemsSource = elementSpannungen;
        }

        private void ReaktionenGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var knotenReaktionen = new Dictionary<string, KnotenReaktion>();
            foreach (var item in modell.Randbedingungen)
            {
                var knotenId = item.Value.NodeId;
                if (knotenReaktionen.ContainsKey(knotenId)) break;
                if (!modell.Knoten.TryGetValue(knotenId, out var knoten)) break;
                var knotenReaktion = new KnotenReaktion(knoten.Reactions);
                knotenReaktionen.Add(knotenId, knotenReaktion);
            }
            ReaktionenGrid = sender as DataGrid;
            if (ReaktionenGrid != null) ReaktionenGrid.ItemsSource = knotenReaktionen;
        }

        internal class ElementSpannung
        {
            public double[] Spannungen { get; }

            public ElementSpannung(double[] spannungen)
            {
                Spannungen = spannungen;
            }
        }
        internal class KnotenReaktion
        {
            public double[] Reaktionen { get; }

            public KnotenReaktion(double[] reaktionen)
            {
                Reaktionen = reaktionen;
            }
        }
    }
}