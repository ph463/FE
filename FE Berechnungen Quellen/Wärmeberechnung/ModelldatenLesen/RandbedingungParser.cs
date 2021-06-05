﻿using FE_Berechnungen.Wärmeberechnung.Modelldaten;
using FEALibrary.Modell;

namespace FE_Berechnungen.Wärmeberechnung.ModelldatenLesen
{
    public class RandbedingungParser
    {
        private FEModell modell;
        private string[] substrings;
        private string supportId;
        private string nodeId;
        private Randbedingung randbedingung;

        public void ParseRandbedingungen(string[] lines, FEModell feModell)
        {
            modell = feModell;
            var delimiters = new[] { '\t' };

            for (var i = 0; i < lines.Length; i++)
            {
                if (lines[i] != "Randbedingungen") continue;
                FeParser.InputFound += "\nRandbedingungen";
                do
                {
                    substrings = lines[i + 1].Split(delimiters);
                    switch (substrings.Length)
                    {
                        case 3:
                            {
                                supportId = substrings[0];
                                nodeId = substrings[1];
                                var pre = double.Parse(substrings[2]);
                                randbedingung = new Randbedingung(nodeId, pre);
                                modell.Randbedingungen.Add(supportId, randbedingung);
                                i++;
                                break;
                            }
                        default:
                            throw new ParseAusnahme((i + 2) + ": Randbedingungen, falsche Anzahl Parameter");
                    }
                } while (lines[i + 1].Length != 0);
                break;
            }
        }
    }
}