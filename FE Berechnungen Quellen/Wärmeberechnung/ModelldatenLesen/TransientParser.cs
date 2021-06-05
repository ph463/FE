using FE_Berechnungen.Wärmeberechnung.Modelldaten;
using FEALibrary.Modell;
using System;
using System.Runtime.Serialization;

namespace FE_Berechnungen.Wärmeberechnung.ModelldatenLesen
{
    public class TransientParser
    {
        private string[] substrings;
        public bool zeitintegrationDaten;
        public void ParseZeitintegration(string[] lines, FEModell feModell)
        {
            var delimiters = new[] { '\t' };
            var modell = feModell;

            //suche "Eigenlösungen"
            for (var i = 0; i < lines.Length; i++)
            {
                if (lines[i] != "Eigenloesungen") continue;
                FeParser.InputFound += "\nEigenloesungen";
                do
                {
                    substrings = lines[i + 1].Split(delimiters);
                    switch (substrings.Length)
                    {
                        case 2:
                            {
                                var id = substrings[0];
                                int numberOfStates = short.Parse(substrings[1]);
                                modell.Eigenstate = new Eigenzustand(id, numberOfStates);
                                i++;
                                break;
                            }
                        default:
                            throw new ParseException((i + 2) + ": Eigenlösungen, falsche Anzahl Parameter");
                    }
                } while (lines[i + 1].Length != 0);
                break;
            }

            // suche "Zeitintegration"
            for (var i = 0; i < lines.Length; i++)
            {
                if (lines[i] != "Zeitintegration") continue;
                FeParser.InputFound += "\nZeitintegration";
                var teilStrings = lines[i + 1].Split(delimiters);
                var tmax = double.Parse(teilStrings[1]);
                var dt = double.Parse(teilStrings[2]);
                var alfa = double.Parse(teilStrings[3]);
                modell.Zeitintegration = new Zeitintegration(tmax, dt, alfa) { FromStationary = false };
                zeitintegrationDaten = true;
                break;
            }

            // suche Anfangstemperaturen
            for (var i = 0; i < lines.Length; i++)
            {
                // stationaere Loesung oder nodeId (incl. "alle")
                if (lines[i] != "Anfangstemperaturen") continue;
                FeParser.InputFound += "\nAnfangstemperaturen";

                var teilStrings = lines[i + 1].Split(delimiters);
                if (teilStrings[0] == "stationaere Loesung")
                    modell.Zeitintegration.FromStationary = true;

                else do
                    {
                        // knotenId inkl. alle
                        var knotenId = teilStrings[0];
                        var t0 = double.Parse(teilStrings[1]);
                        var initial = new double[1];
                        initial[0] = t0;
                        modell.Zeitintegration.Anfangsbedingungen.Add(new Knotenwerte(knotenId, initial));
                        i++;
                        teilStrings = lines[i + 1].Split(delimiters);
                    } while (lines[i + 1].Length != 0);
                break;
            }

            // suche zeitabhängige Randtemperaturen, eingeprägte Temperatur am Rand
            //  5:Name,NodeId,Amplitude,Frequenz,Phase 
            // >5:Name,NodeId,Wertepaare für stückweise linearen Verlauf
            for (var i = 0; i < lines.Length; i++)
            {
                if (lines[i] != "Zeitabhaengige Randtemperaturen") continue;
                FeParser.InputFound += "\nZeitabhaengige Randtemperaturen";
                do
                {
                    var teilStrings = lines[i + 1].Split(delimiters);
                    var supportId = teilStrings[0];
                    var nodeId = teilStrings[1];

                    ZeitabhängigeRandbedingung zeitabhängigeRandbedingung;
                    switch (teilStrings.Length)
                    {
                        case 2:
                            {
                                const bool datei = true;
                                zeitabhängigeRandbedingung =
                                    new ZeitabhängigeRandbedingung(nodeId, datei);
                                break;
                            }
                        case 3:
                            {
                                var konstanteTemperatur = double.Parse(teilStrings[2]);
                                zeitabhängigeRandbedingung =
                                    new ZeitabhängigeRandbedingung(nodeId, konstanteTemperatur);
                                break;
                            }
                        case 5:
                            {
                                var amplitude = double.Parse(teilStrings[2]);
                                var frequency = 2 * Math.PI * double.Parse(teilStrings[3]);
                                var phaseAngle = Math.PI / 180 * double.Parse(teilStrings[4]);
                                zeitabhängigeRandbedingung =
                                    new ZeitabhängigeRandbedingung(nodeId, amplitude, frequency, phaseAngle);
                                break;
                            }
                        default:
                            {
                                var interval = new double[teilStrings.Length - 2];
                                interval[0] = double.Parse(teilStrings[2]);
                                interval[1] = double.Parse(teilStrings[3]);
                                for (var j = 2; j < teilStrings.Length - 2; j += 2)
                                {
                                    interval[j] = double.Parse(teilStrings[j + 2]);
                                    interval[j + 1] = double.Parse(teilStrings[j + 3]);
                                }
                                zeitabhängigeRandbedingung = new ZeitabhängigeRandbedingung(nodeId, interval);
                                break;
                            }
                    }
                    zeitabhängigeRandbedingung.Prescribed = new double[1];
                    modell.ZeitabhängigeRandbedingung.Add(supportId, zeitabhängigeRandbedingung);
                    i++;
                } while (lines[i + 1].Length != 0);
                break;
            }

            // suche zeitabhängige Knotenlast (Temperaturen) Knotentemperaturen
            //  5:Name,NodeId,Amplitude,Frequenz,Phase 
            // >5:Name,NodeId,Wertepaare für stückweise linearen Verlauf
            for (var i = 0; i < lines.Length; i++)
            {
                if (lines[i] != "Zeitabhaengige Knotenlast") continue;
                FeParser.InputFound += "\nZeitabhaengige Knotenlast";
                do
                {
                    var teilStrings = lines[i + 1].Split(delimiters);
                    var supportId = teilStrings[0];
                    var nodeId = teilStrings[1];

                    ZeitabhängigeKnotenLast zeitabhängigeKnotenLast;
                    switch (teilStrings.Length)
                    {
                        case 2:
                            {
                                zeitabhängigeKnotenLast =
                                    new ZeitabhängigeKnotenLast(nodeId, true) { VariationType = 0 };
                                break;
                            }
                        case 5:
                            {
                                var amplitude = double.Parse(teilStrings[2]);
                                var frequency = double.Parse(teilStrings[3]);
                                var phaseAngle = double.Parse(teilStrings[4]);
                                zeitabhängigeKnotenLast =
                                    new ZeitabhängigeKnotenLast(nodeId, amplitude, frequency, phaseAngle)
                                    { VariationType = 2 };
                                break;
                            }
                        default:
                            {
                                var interval = new double[teilStrings.Length - 2];
                                interval[0] = double.Parse(teilStrings[2]);
                                interval[1] = double.Parse(teilStrings[3]);
                                for (var j = 2; j < teilStrings.Length - 2; j += 2)
                                {
                                    interval[j] = double.Parse(teilStrings[j + 2]);
                                    interval[j + 1] = double.Parse(teilStrings[j + 3]);
                                }
                                zeitabhängigeKnotenLast = new ZeitabhängigeKnotenLast(nodeId, interval)
                                { VariationType = 1 };
                                break;
                            }
                    }
                    modell.ZeitabhängigeKnotenLasten.Add(supportId, zeitabhängigeKnotenLast);
                    i++;
                } while (lines[i + 1].Length != 0);
                break;
            }

            // suche zeitabhängigeElementLast auf Dreieckselementen
            //  5:Name,ElementId,Knotenwert1, Knotenwert2, Knotenwert3 
            for (var i = 0; i < lines.Length; i++)
            {
                if (lines[i] != "Zeitabhaengige Elementlast") continue;
                FeParser.InputFound += "\nZeitabhaengige Elementlast";
                var knotenWerte = new double[3];
                do
                {
                    var teilStrings = lines[i + 1].Split(delimiters);
                    var supportId = teilStrings[0];
                    var elementId = teilStrings[1];
                    for (var k = 0; k < 3; k++) knotenWerte[k] =
                        double.Parse(teilStrings[k + 2]);
                    var zeitabhängigeElementLast = new ZeitabhängigeElementLast(elementId, knotenWerte)
                    { VariationType = 0 };
                    modell.ZeitabhängigeElementLasten.Add(supportId, zeitabhängigeElementLast);
                    i++;
                } while (lines[i + 1].Length != 0);
                break;
            }
        }
        [Serializable]
        private class ParseException : Exception
        {
            public ParseException() { }
            public ParseException(string message) : base(message) { }
            public ParseException(string message, Exception innerException) : base(message, innerException) { }
            protected ParseException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        }
    }
}
