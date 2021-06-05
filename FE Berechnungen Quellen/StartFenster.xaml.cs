using FEALibrary.Modell;
using Microsoft.Win32;
using System;
using System.IO;
using System.Text;
using System.Windows;
using FeParser = FEALibrary.Modell.FeParser;

namespace FE_Berechnungen
{
    public partial class StartFenster
    {
        private FeParser parse;
        private FEModell modell;
        private Berechnung analysis;
        private string path;
        private string[] lines;
        private readonly StringBuilder sb = new StringBuilder();
        private bool wärmeDaten, tragwerksDaten, zeitintegrationDaten;
        public static bool berechnet, zeitintegrationBerechnet;

        public StartFenster()
        {
            InitializeComponent();
        }
        //********************************************************************
        // Wärmeberechnung
        private void WärmedatenEinlesen(object sender, RoutedEventArgs e)
        {
            //var fileDialog = new OpenFileDialog
            //{
            //    Filter = "inp files (*.inp)|*.inp|All files (*.*)|*.*",
            //    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
            //};
            //fileDialog.InitialDirectory += "\\FE Programm\\input\\Wärmeberechnung";
            //if (fileDialog.ShowDialog() != true)
            //    return;
            //path = fileDialog.FileName;
            var fileDialog = new OpenFileDialog
            {
                Filter = "inp files (*.inp)|*.inp|All files (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
            };
            fileDialog.InitialDirectory += "\\FE Programm\\input\\Wärmeberechnung";
            if (fileDialog.ShowDialog() != true)
                return;
            path = fileDialog.FileName;

            try
            {
                lines = File.ReadAllLines(path);
            }
            catch (ParseAusnahme)
            {
                throw new ParseAusnahme("Abbruch: Fehler beim Lesen aus Eingabedatei ");
            }

            parse = new FeParser();
            parse.ParseModel(lines);
            modell = parse.FeModell;
            parse.ParseNodes(lines);

            var wärmeElemente = new Wärmeberechnung.ModelldatenLesen.ElementParser();
            wärmeElemente.ParseWärmeElements(lines, modell);

            var wärmeMaterial = new Wärmeberechnung.ModelldatenLesen.MaterialParser();
            wärmeMaterial.ParseMaterials(lines, modell);

            var wärmeLasten = new Wärmeberechnung.ModelldatenLesen.LastParser();
            wärmeLasten.ParseLasten(lines, modell);

            var wärmeRandbedingungen = new Wärmeberechnung.ModelldatenLesen.RandbedingungParser();
            wärmeRandbedingungen.ParseRandbedingungen(lines, modell);

            var wärmeTransient = new Wärmeberechnung.ModelldatenLesen.TransientParser();
            wärmeTransient.ParseZeitintegration(lines, modell);

            zeitintegrationDaten = wärmeTransient.zeitintegrationDaten;
            wärmeDaten = true;
            berechnet = false;
            zeitintegrationBerechnet = false;

            sb.Append(FeParser.InputFound + "\n\nWärmemodelldaten erfolgreich eingelesen");
            _ = MessageBox.Show(sb.ToString(), "Wärmeberechnung");
            sb.Clear();
        }
        private void WärmedatenEditieren(object sender, RoutedEventArgs e)
        {
            if (path == null)
            {
                var wärmedaten = new Dateieingabe.ModelldatenEditieren();
                wärmedaten.Show();
            }
            else
            {
                var wärmedaten = new Dateieingabe.ModelldatenEditieren(path);
                wärmedaten.Show();
            }
        }
        private void WärmedatenAnzeigen(object sender, RoutedEventArgs e)
        {
            if (modell == null) modell = new FEModell(2);
            var wärme = new Wärmeberechnung.ModelldatenAnzeigen.WärmedatenAnzeigen(modell);
            wärme.Show();
        }
        private void WärmedatenVisualisieren(object sender, RoutedEventArgs e)
        {
            var wärmeModell = new Wärmeberechnung.ModelldatenAnzeigen.WärmemodellVisualisieren(modell);
            wärmeModell.Show();
        }
        private void WärmedatenBerechnen(object sender, EventArgs e)
        {
            if (wärmeDaten)
            {
                analysis = new Berechnung(modell);
                analysis.ComputeSystemMatrix();
                analysis.ComputeSystemVector();
                analysis.SolveEquations();
                berechnet = true;
                _ = MessageBox.Show("Systemgleichungen erfolgreich gelöst", "Wärmeberechnung");
            }
            else
            {
                _ = MessageBox.Show("Modelldaten müssen zuerst eingelesen werden", "Wärmeberechnung");
            }
        }
        private void WärmeberechnungErgebnisseAnzeigen(object sender, EventArgs e)
        {
            if (wärmeDaten)
            {
                if (!berechnet)
                {
                    analysis = new Berechnung(modell);
                    analysis.ComputeSystemMatrix();
                    analysis.ComputeSystemVector();
                    analysis.SolveEquations();
                    berechnet = true;
                }
                var ergebnisse = new Wärmeberechnung.Ergebnisse.StationäreErgebnisseAnzeigen(modell);
                ergebnisse.Show();
            }
            else
            {
                _ = MessageBox.Show("Modelldaten für Wärmeberechnung sind noch nicht spezifiziert", "Wärmeberechnung");
            }
        }
        private void WärmeberechnungErgebnisseVisualisieren(object sender, RoutedEventArgs e)
        {
            if (wärmeDaten)
            {
                if (!berechnet)
                {
                    analysis = new Berechnung(modell);
                    analysis.ComputeSystemMatrix();
                    analysis.ComputeSystemVector();
                    analysis.SolveEquations();
                    berechnet = true;
                }
                var wärmeModell = new Wärmeberechnung.Ergebnisse.StationäreErgebnisseVisualisieren(modell);
                wärmeModell.Show();
            }
            else
            {
                _ = MessageBox.Show("Modelldaten für Wärmeberechnung sind noch nicht spezifiziert", "Wärmeberechnung");
            }
        }
        private void InstationäreDaten(object sender, RoutedEventArgs e)
        {
            if (modell == null)
            {
                _ = MessageBox.Show("Modelldaten für Wärmeberechnung sind noch nicht spezifiziert", "Wärmeberechnung");
            }
            else
            {
                var wärme = new Wärmeberechnung.ModelldatenAnzeigen.InstationäreDatenAnzeigen(modell);
                wärme.Show();
                zeitintegrationBerechnet = false;
            }
        }
        private void EigenlösungWärmeBerechnen(object sender, RoutedEventArgs e)
        {
            if (modell != null)
            {
                if (!berechnet)
                {
                    analysis = new Berechnung(modell);
                    analysis.ComputeSystemMatrix();
                }
                // default = 2 Eigenstates, falls nicht anders spezifiziert
                if (modell.Eigenstate == null) { modell.Eigenstate = new Eigenzustand("default", 2); }
                analysis.Eigenstates();
                _ = MessageBox.Show("Eigenlösung erfolgreich ermittelt", "Wärmeberechnung");
            }
            else
            {
                _ = MessageBox.Show("Modelldaten sind noch nicht spezifiziert", "Wärmeberechnung");
            }
        }
        private void EigenlösungWärmeAnzeigen(object sender, RoutedEventArgs e)
        {
            if (modell != null)
            {
                if (!berechnet)
                {
                    analysis = new Berechnung(modell);
                    analysis.ComputeSystemMatrix();
                    // default = 2 Eigenstates, falls nicht anders spezifiziert
                    if (modell.Eigenstate == null) { modell.Eigenstate = new Eigenzustand("default", 2); }
                }
                analysis.Eigenstates();
                var eigen = new Eigenlösung.Eigenlösung(modell);
                eigen.Show();
            }
            else
            {
                _ = MessageBox.Show("Modelldaten sind noch nicht spezifiziert", "Wärmeberechnung");
            }
        }
        private void EigenlösungWärmeVisualisieren(object sender, RoutedEventArgs e)
        {
            if (modell != null)
            {
                if (!zeitintegrationBerechnet)
                {
                    analysis = new Berechnung(modell);
                    analysis.ComputeSystemMatrix();
                    // default = 2 Eigenstates, falls nicht anders spezifiziert
                    if (modell.Eigenstate == null) { modell.Eigenstate = new Eigenzustand("default", 2); }
                }
                analysis.Eigenstates();
                var visual = new Wärmeberechnung.Ergebnisse.EigenlösungVisualisieren(modell);
                visual.Show();
            }
            else
            {
                _ = MessageBox.Show("Modelldaten sind noch nicht spezifiziert", "Tragwerksberechnung");
            }
        }
        private void InstationäreBerechnung(object sender, RoutedEventArgs e)
        {
            if (zeitintegrationDaten)
            {
                if (!berechnet)
                {
                    analysis = new Berechnung(modell);
                    analysis.ComputeSystemMatrix();
                    analysis.ComputeSystemVector();
                    analysis.SolveEquations();
                    berechnet = true;
                }
                analysis.TimeIntegration1StOrder();
                zeitintegrationBerechnet = true;
                _ = MessageBox.Show("Zeitintegration erfolgreich durchgeführt", "instationäre Wärmeberechnung");
            }
            else
            {
                _ = MessageBox.Show("Daten für Zeitintegration sind noch nicht spezifiziert", "Wärmeberechnung");
            }

        }
        private void InstationäreErgebnisseAnzeigen(object sender, RoutedEventArgs e)
        {
            if (zeitintegrationBerechnet)
            {
                _ = new Wärmeberechnung.Ergebnisse.InstationäreErgebnisseAnzeigen(modell);
            }
            else
            {
                _ = MessageBox.Show("Zeitintegration noch nicht ausgeführt!!", "Wärmeberechnung");
            }
        }
        private void InstationäreModellzuständeVisualisieren(object sender, RoutedEventArgs e)
        {
            if (zeitintegrationBerechnet)
            {
                var wärmeModell = new Wärmeberechnung.Ergebnisse.InstationäreModellzuständeVisualisieren(modell);
                wärmeModell.Show();
            }
            else
            {
                _ = MessageBox.Show("Zeitintegration noch nicht ausgeführt!!", "Wärmeberechnung");
            }
        }
        private void KnotenzeitverläufeWärmeVisualisieren(object sender, RoutedEventArgs e)
        {
            if (zeitintegrationBerechnet)
            {
                var wärmeModell = new Wärmeberechnung.Ergebnisse.KnotenzeitverläufeVisualisieren(modell);
                wärmeModell.Show();
            }
            else
            {
                _ = MessageBox.Show("Zeitintegration noch nicht ausgeführt!!", "Wärmeberechnung");
            }
        }

        //********************************************************************
        // Tragwerksberechnung
        private void TragwerksdatenEinlesen(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Filter = "inp files (*.inp)|*.inp|All files (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
            };
            fileDialog.InitialDirectory += "\\FE Programm\\input\\Tragwerksberechnung";
            if (fileDialog.ShowDialog() != true)
                return;
            path = fileDialog.FileName;

            try
            {
                lines = File.ReadAllLines(path);
            }
            catch (ParseAusnahme)
            {
                throw new ParseAusnahme("Abbruch: Fehler bei Lesen aus Eingabedatei");
            }

            parse = new FeParser();
            parse.ParseModel(lines);
            modell = parse.FeModell;
            parse.ParseNodes(lines);

            var tragwerksElemente = new Tragwerksberechnung.ModelldatenLesen.ElementParser();
            tragwerksElemente.ParseElements(lines, modell);

            var tragwerksMaterial = new Tragwerksberechnung.ModelldatenLesen.MaterialParser();
            tragwerksMaterial.ParseMaterials(lines, modell);

            var tragwerksLasten = new Tragwerksberechnung.ModelldatenLesen.LastParser();
            tragwerksLasten.ParseLasten(lines, modell);

            var tragwerksRandbedingungen = new Tragwerksberechnung.ModelldatenLesen.RandbedingungParser();
            tragwerksRandbedingungen.ParseRandbedingungen(lines, modell);

            var tragwerksTransient = new Tragwerksberechnung.ModelldatenLesen.TransientParser();
            tragwerksTransient.ParseZeitintegration(lines, modell);

            zeitintegrationDaten = tragwerksTransient.zeitintegrationDaten;
            tragwerksDaten = true;
            berechnet = false;
            zeitintegrationBerechnet = false;

            sb.Append(FeParser.InputFound + "\n\nTragwerksdaten erfolgreich eingelesen");
            _ = MessageBox.Show(sb.ToString(), "Tragwerksberechnung");
            sb.Clear();
        }
        private void TragwerksdatenEditieren(object sender, RoutedEventArgs e)
        {
            if (path == null)
            {
                var tragwerksdaten = new Dateieingabe.ModelldatenEditieren();
                tragwerksdaten.Show();
            }
            else
            {
                var tragwerksdaten = new Dateieingabe.ModelldatenEditieren(path);
                tragwerksdaten.Show();
            }
        }
        private void TragwerksdatenAnzeigen(object sender, EventArgs e)
        {
            if (modell == null) modell = new FEModell(2);
            var tragwerk = new Tragwerksberechnung.ModelldatenAnzeigen.TragwerkdatenAnzeigen(modell);
            tragwerk.Show();
        }
        private void TragwerksdatenVisualisieren(object sender, RoutedEventArgs e)
        {
            var tragwerksmodell = new Tragwerksberechnung.ModelldatenAnzeigen.TragwerkmodellVisualisieren(modell);
            tragwerksmodell.Show();
        }
        private void TragwerksdatenBerechnen(object sender, EventArgs e)
        {
            if (tragwerksDaten)
            {
                analysis = new Berechnung(modell);
                analysis.ComputeSystemMatrix();
                analysis.ComputeSystemVector();
                analysis.SolveEquations();
                berechnet = true;
                _ = MessageBox.Show("Systemgleichungen erfolgreich gelöst", "statische Tragwerksberechnung");
            }
            else
            {
                _ = MessageBox.Show("Tragwerksdaten müssen zuerst eingelesen werden", "statische Tragwerksberechnung");
            }

        }
        private void StatikErgebnisseAnzeigen(object sender, EventArgs e)
        {
            if (!berechnet)
            {
                analysis = new Berechnung(modell);
                analysis.ComputeSystemMatrix();
                analysis.ComputeSystemVector();
                analysis.SolveEquations();
                berechnet = true;
            }
            var ergebnisse = new Tragwerksberechnung.Ergebnisse.StatikErgebnisseAnzeigen(modell);
            ergebnisse.Show();
        }
        private void StatikErgebnisseVisualisieren(object sender, RoutedEventArgs e)
        {
            if (!berechnet)
            {
                analysis = new Berechnung(modell);
                analysis.ComputeSystemMatrix();
                analysis.ComputeSystemVector();
                analysis.SolveEquations();
                berechnet = true;
            }
            _ = new Tragwerksberechnung.Ergebnisse.StatikErgebnisseVisualisieren(modell);
        }
        private void EigenlösungTragwerkBerechnen(object sender, RoutedEventArgs e)
        {
            if (modell != null)
            {
                if (!berechnet)
                {
                    analysis = new Berechnung(modell);
                    analysis.ComputeSystemMatrix();
                }
                // default = 2 Eigenstates, falls nicht anders spezifiziert
                if (modell.Eigenstate == null) { modell.Eigenstate = new Eigenzustand("default", 2); }
                analysis.Eigenstates();
                _ = MessageBox.Show("Eigenfrequenzen erfolgreich ermittelt", "Tragwerksberechnung");
            }
            else
            {
                _ = MessageBox.Show("Modelldaten sind noch nicht spezifiziert", "Tragwerksberechnung");
            }
        }
        private void EigenlösungTragwerkAnzeigen(object sender, RoutedEventArgs e)
        {
            if (modell != null)
            {
                if (!berechnet)
                {
                    analysis = new Berechnung(modell);
                    analysis.ComputeSystemMatrix();
                    // default = 2 Eigenstates, falls nicht anders spezifiziert
                    if (modell.Eigenstate == null) { modell.Eigenstate = new Eigenzustand("default", 2); }
                }
                analysis.Eigenstates();
                var eigen = new Tragwerksberechnung.Ergebnisse.EigenlösungAnzeigen(modell);
                eigen.Show();
            }
            else
            {
                _ = MessageBox.Show("Modelldaten sind noch nicht spezifiziert", "Tragwerksberechnung");
            }
        }
        private void EigenlösungTragwerkVisualisieren(object sender, RoutedEventArgs e)
        {
            if (modell != null)
            {
                if (!berechnet)
                {
                    analysis = new Berechnung(modell);
                    analysis.ComputeSystemMatrix();
                    // default = 2 Eigenstates, falls nicht anders spezifiziert
                    if (modell.Eigenstate == null) { modell.Eigenstate = new Eigenzustand("default", 2); }
                }
                analysis.Eigenstates();
                var visual = new Tragwerksberechnung.Ergebnisse.EigenlösungVisualisieren(modell);
                visual.Show();
            }
            else
            {
                _ = MessageBox.Show("Modelldaten sind noch nicht spezifiziert", "Tragwerksberechnung");
            }
        }
        private void DynamischeDaten(object sender, EventArgs e)
        {
            if (zeitintegrationDaten)
            {
                var tragwerk = new Tragwerksberechnung.ModelldatenAnzeigen.DynamikDatenAnzeigen(modell);
                tragwerk.Show();
            }
            else
            {
                _ = MessageBox.Show("Daten für Zeitintegration sind noch nicht spezifiziert", "Tragwerksberechnung");
            }
        }
        private void DynamischeBerechnung(object sender, EventArgs e)
        {
            if (zeitintegrationDaten)
            {
                if (!berechnet)
                {
                    analysis = new Berechnung(modell);
                    analysis.ComputeSystemMatrix();
                    analysis.ComputeSystemVector();
                    analysis.SolveEquations();
                    berechnet = true;
                }
                analysis.TimeIntegration2NdOrder();
                zeitintegrationBerechnet = true;
            }
            else
            {
                _ = MessageBox.Show("Daten für Zeitintegration sind noch nicht spezifiziert", "Tragwerksberechnung");
            }
        }
        private void DynamischeErgebnisseAnzeigen(object sender, RoutedEventArgs e)
        {
            if (zeitintegrationBerechnet)
            {
                _ = new Tragwerksberechnung.Ergebnisse.DynamischeErgebnisseAnzeigen(modell);
            }
            else
            {
                _ = MessageBox.Show("Zeitintegration noch nicht ausgeführt!!", "dynamische Tragwerksberechnung");
            }
        }
        private void DynamischeModellzuständeVisualisieren(object sender, RoutedEventArgs e)
        {
            if (zeitintegrationBerechnet)
            {
                var dynamikErgebnisse = new Tragwerksberechnung.Ergebnisse.DynamischeModellzuständeVisualisieren(modell);
                dynamikErgebnisse.Show();
            }
            else
            {
                _ = MessageBox.Show("Zeitintegration noch nicht ausgeführt!!", "dynamische Tragwerksberechnung");
            }
        }
        private void KnotenzeitverläufeTragwerkVisualisieren(object sender, RoutedEventArgs e)
        {
            if (zeitintegrationBerechnet)
            {
                var knotenzeitverläufe = new Tragwerksberechnung.Ergebnisse.KnotenzeitverläufeVisualisieren(modell);
                knotenzeitverläufe.Show();
            }
            else
            {
                _ = MessageBox.Show("Zeitintegration noch nicht ausgeführt!!", "dynamische Tragwerksberechnung");
            }
        }

        //********************************************************************
        // Elastizitätsberechnung
        private void ElastizitätsdatenEinlesen(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                Filter = "inp files (*.inp)|*.inp|All files (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal)
            };
            fileDialog.InitialDirectory += "\\FE Programm\\input\\Elastizitätsberechnung";
            if (fileDialog.ShowDialog() != true)
                return;
            path = fileDialog.FileName;

            try
            {
                lines = File.ReadAllLines(path);
            }
            catch (ParseAusnahme)
            {
                throw new ParseAusnahme("Abbruch: Fehler bei Lesen aus Eingabedatei");
            }

            parse = new FeParser();
            parse.ParseModel(lines);
            modell = parse.FeModell;
            parse.ParseNodes(lines);

            var parseElastizität = new Elastizitätsberechnung.ModelldatenLesen.ElastizitätsParser();
            parseElastizität.ParseElastizität(lines, modell);

            berechnet = false;

            sb.Clear();
            sb.Append(FeParser.InputFound + "\n\nModelldaten für Elastizitätsberechnung erfolgreich eingelesen");
            _ = MessageBox.Show(sb.ToString(), "Elastizitätsberechnung");
            sb.Clear();
        }
        private void ElastizitätsdatenEditieren(object sender, RoutedEventArgs e)
        {
            if (path == null)
            {
                var elastizitätsdaten = new Dateieingabe.ModelldatenEditieren();
                elastizitätsdaten.Show();
            }
            else
            {
                var elastizitätsdaten = new Dateieingabe.ModelldatenEditieren(path);
                elastizitätsdaten.Show();
            }
        }
        private void ElastizitätsdatenAnzeigen(object sender, EventArgs e)
        {
            if (modell == null)
            {
                _ = MessageBox.Show("Modelldaten sind noch nicht spezifiziert", "Elastizitätsberechnung");
                return;
            }
            var tragwerk = new Elastizitätsberechnung.ModelldatenAnzeigen.ElastizitätsdatenAnzeigen(modell);
            tragwerk.Show();
        }
        private void ElastizitätsdatenVisualisieren(object sender, RoutedEventArgs e)
        {
            if (modell == null)
            {
                _ = MessageBox.Show("Modelldaten sind noch nicht spezifiziert", "Elastizitätsberechnung");
                return;
            }
            switch (modell.SpatialDimension)
            {
                case 2:
                    {
                        var tragwerk = new Elastizitätsberechnung.ModelldatenAnzeigen.ElastizitätsmodellVisualisieren(modell);
                        tragwerk.Show();
                        break;
                    }
                case 3:
                    {
                        var tragwerk = new Elastizitätsberechnung.ModelldatenAnzeigen.Elastizitätsmodell3DVisualisieren(modell);
                        tragwerk.Show();
                        break;
                    }
            }
        }
        private void ElastizitätsdatenBerechnen(object sender, EventArgs e)
        {
            if (modell == null)
            {
                _ = MessageBox.Show("Modelldaten für Elastizitätsberechnung sind noch nicht spezifiziert", "Elastizitätsberechnung");
                return;
            }
            try
            {
                analysis = new Berechnung(modell);
                analysis.ComputeSystemMatrix();
                analysis.ComputeSystemVector();
                analysis.SolveEquations();
                berechnet = true;

                _ = MessageBox.Show("Systemgleichungen erfolgreich gelöst", "Elastizitätsberechnung");
            }

            catch (BerechnungAusnahme)
            {
                throw new BerechnungAusnahme("Abbruch: Fehler bei Lösung der Systemgleichungen");
            }
        }
        private void ElastizitätsberechnungErgebnisse(object sender, EventArgs e)
        {
            if (!berechnet)
            {
                if (modell == null)
                {
                    _ = MessageBox.Show("Modelldaten für Elastizitätsberechnung sind noch nicht spezifiziert", "Elastizitätsberechnung");
                    return;
                }
                analysis = new Berechnung(modell);
                analysis.ComputeSystemMatrix();
                analysis.ComputeSystemVector();
                analysis.SolveEquations();
                berechnet = true;
            }
            var ergebnisse = new Elastizitätsberechnung.Ergebnisse.StatikErgebnisseAnzeigen(modell);
            ergebnisse.Show();
        }
        private void ElastizitätsErgebnisseVisualisieren(object sender, RoutedEventArgs e)
        {
            if (!berechnet)
            {
                if (modell == null)
                {
                    _ = MessageBox.Show("Modelldaten für Elastizitätsberechnung sind noch nicht spezifiziert", "Elastizitätsberechnung");
                    return;
                }
                analysis = new Berechnung(modell);
                analysis.ComputeSystemMatrix();
                analysis.ComputeSystemVector();
                analysis.SolveEquations();
                berechnet = true;
            }

            if (modell.SpatialDimension == 2)
            {
                var tragwerk = new Elastizitätsberechnung.Ergebnisse.StatikErgebnisseVisualisieren(modell);
                tragwerk.Show();
            }
            else if (modell.SpatialDimension == 3)
            {
                var tragwerk = new Elastizitätsberechnung.Ergebnisse.StatikErgebnisse3DVisualisieren(modell);
                tragwerk.Show();
            }
            else _ = MessageBox.Show(sb.ToString(), "falsche Raumdimension, muss 2 oder 3 sein");
        }
    }
}