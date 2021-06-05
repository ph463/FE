using FE_Berechnungen.Tragwerksberechnung.Modelldaten;
using FEALibrary.Modell;
using FEALibrary.Modell.abstrakte_Klassen;
using System;

namespace FE_Berechnungen.Tragwerksberechnung.ModelldatenLesen
{
    internal class TransientParser
    {
        private string[] substrings;
        private readonly char[] delimiters = { '\t', ';' };
        public bool zeitintegrationDaten;
        public double[] Initial { get; private set; }

        public void ParseZeitintegration(string[] lines, FEModell feModell)
        {
            var modell = feModell;
            var dimension = modell.SpatialDimension;
            Initial = new double[dimension];

            // suche "Eigenloesungen"
            for (var i = 0; i < lines.Length; i++)
            {
                if (lines[i] != "Eigenloesungen") continue;
                FeParser.InputFound += "\nEigenlösungen";

                substrings = lines[i + 1].Split(delimiters);
                if (substrings.Length == 2)
                {
                    var id = substrings[0];
                    int numberOfStates = short.Parse(substrings[1]);
                    modell.Eigenstate = new Eigenzustand(id, numberOfStates);
                    break;
                }
                else
                {
                    throw new ParseAusnahme((i + 2) + ": Eigenloesungen, falsche Anzahl Parameter");
                }
            }

            // suche "Zeitintegration"
            for (var i = 0; i < lines.Length; i++)
            {
                if (lines[i] != "Zeitintegration") continue;
                FeParser.InputFound += "\nZeitintegration";
                //id, tmax, dt, method, parameter1, parameter2
                //method=1:beta,gamma  method=2:theta  method=3: alfa
                substrings = lines[i + 1].Split(delimiters);
                switch (substrings.Length)
                {
                    case 5:
                        var tmax = double.Parse(substrings[1]);
                        var dt = double.Parse(substrings[2]);
                        var method = short.Parse(substrings[3]);
                        var parameter1 = double.Parse(substrings[4]);
                        modell.Zeitintegration =
                            new Zeitintegration(tmax, dt, method, parameter1);
                        break;
                    case 6:
                        tmax = double.Parse(substrings[1]);
                        dt = double.Parse(substrings[2]);
                        method = short.Parse(substrings[3]);
                        parameter1 = double.Parse(substrings[4]);
                        var parameter2 = double.Parse(substrings[5]);
                        modell.Zeitintegration =
                            new Zeitintegration(tmax, dt, method, parameter1, parameter2);
                        break;
                    default:
                        throw new ParseAusnahme((i + 2) + ": Zeitintegration, falsche Anzahl Parameter");
                }
                zeitintegrationDaten = true;
            }

            // suche "Daempfung"
            for (var i = 0; i < lines.Length; i++)
            {
                if (lines[i] != "Daempfung") continue;
                FeParser.InputFound += "\nDaempfung";
                do
                {
                    substrings = lines[i + 1].Split(delimiters);
                    var knotenId = substrings[0];
                    var dämpfungsRaten = new double[substrings.Length - 1];
                    for (var k = 0; k < (substrings.Length - 1); k++)
                    {
                        dämpfungsRaten[k] = double.Parse(substrings[k + 1]);
                    }
                    modell.Zeitintegration.DämpfungsRaten.Add(new Knotenwerte(knotenId, dämpfungsRaten));
                    i++;
                } while (lines[i + 1].Length != 0);
                break;
            }

            // suche "Anfangsbedingungen"
            for (var i = 0; i < lines.Length; i++)
            {
                if (lines[i] != "Anfangsbedingungen") continue;
                FeParser.InputFound += "\nAnfangsbedingungen";
                do
                {
                    substrings = lines[i + 1].Split(delimiters);
                    var anfangsKnotenId = substrings[0];
                    // Anfangsverformungen und Geschwindigkeiten
                    int nodalDof;
                    switch (substrings.Length)
                    {
                        case 3:
                            nodalDof = 1;
                            break;
                        case 5:
                            nodalDof = 2;
                            break;
                        case 7:
                            nodalDof = 3;
                            break;
                        default:
                            throw new ParseAusnahme((i + 2) + ": Anfangsbedingungen, falsche Anzahl Parameter");
                    }
                    var anfangsWerte = new double[2 * nodalDof];
                    for (var k = 0; k < 2 * nodalDof; k++)
                    {
                        anfangsWerte[k] = double.Parse(substrings[k + 1]);
                    }
                    modell.Zeitintegration.Anfangsbedingungen.Add(new Knotenwerte(anfangsKnotenId, anfangsWerte));
                    i++;
                } while (lines[i + 1].Length != 0);
                break;
            }

            // suche zeitabhängige Knotenlasten
            for (var i = 0; i < lines.Length; i++)
            {
                if (lines[i] != "Zeitabhaengige Knotenlast") continue;
                FeParser.InputFound += "\nZeitabhaengige Knotenlast";
                var boden = false;
                i++;

                substrings = lines[i].Split(delimiters);
                do
                {
                    string nodeLoadId, nodeId;
                    int nodalDof;
                    switch (substrings.Length)
                    {
                        case 3:
                            {
                                nodeLoadId = substrings[0];
                                nodeId = substrings[1];
                                nodalDof = short.Parse(substrings[2]);
                                break;
                            }
                        case 4:
                            {
                                nodeLoadId = substrings[0];
                                nodeId = substrings[1];
                                nodalDof = short.Parse(substrings[2]);
                                boden = true;
                                break;
                            }
                        default:
                            throw new ParseAusnahme((i + 2) + ": Zeitabhaengige Knotenlast, falsche Anzahl Parameter");
                    }

                    substrings = lines[i + 1].Split(delimiters);
                    ZeitabhängigeKnotenLast zeitabhängigeKnotenLast;
                    switch (substrings.Length)
                    {
                        // lies Anregung (Lastvektor) aus Datei
                        case 1:
                            {
                                const bool datei = true;
                                zeitabhängigeKnotenLast = new ZeitabhängigeKnotenLast(nodeId, nodalDof, datei);
                                var last = (AbstraktZeitabhängigeKnotenlast)zeitabhängigeKnotenLast;
                                modell.ZeitabhängigeKnotenLasten.Add(nodeLoadId, last);
                                break;
                            }
                        // lies Zeit-/Wert-Intervalle der Anregung mit linearer Interpolation
                        case 3:
                            {
                                var amplitude = double.Parse(substrings[0]);
                                var circularFrequency = 2 * Math.PI * double.Parse(substrings[1]);
                                var phaseAngle = Math.PI / 180 * double.Parse(substrings[2]);
                                zeitabhängigeKnotenLast =
                                    new ZeitabhängigeKnotenLast(nodeId, nodalDof, amplitude, circularFrequency, phaseAngle);
                                modell.ZeitabhängigeKnotenLasten.Add(nodeLoadId, zeitabhängigeKnotenLast);
                                break;
                            }
                        default:
                            {
                                var interval = new double[substrings.Length];
                                for (var j = 0; j < substrings.Length; j += 2)
                                {
                                    interval[j] = double.Parse(substrings[j]);
                                    interval[j + 1] = double.Parse(substrings[j + 1]);
                                }
                                zeitabhängigeKnotenLast = new ZeitabhängigeKnotenLast(nodeId, nodalDof, interval);
                                modell.ZeitabhängigeKnotenLasten.Add(nodeLoadId, zeitabhängigeKnotenLast);
                                break;
                            }
                    }
                    zeitabhängigeKnotenLast.Bodenanregung = boden;
                    i += 2;
                } while (lines[i].Length != 0);
            }
        }
    }
}
