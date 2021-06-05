using FE_Berechnungen.Tragwerksberechnung.Modelldaten;
using FEALibrary.Modell;

namespace FE_Berechnungen.Tragwerksberechnung.ModelldatenLesen
{
    public class RandbedingungParser
    {
        private FEModell modell;
        private readonly char[] delimiters = { '\t' };
        private string[] substrings;
        private string supportId;
        private string nodeId;
        private Lager lager;

        public void ParseRandbedingungen(string[] lines, FEModell feModell)
        {
            modell = feModell;

            for (var i = 0; i < lines.Length; i++)
            {
                if (lines[i] != "Lager") continue;
                FeParser.InputFound += "\nLager";
                do
                {
                    substrings = lines[i + 1].Split(delimiters);
                    if (substrings.Length < 7)
                    {
                        supportId = substrings[0];
                        nodeId = substrings[1];
                        var conditions = 0;
                        var restrained = 1;
                        var type = substrings[2];
                        for (var k = 0; k < type.Length; k++)
                        {
                            var subType = type.Substring(k, 1);
                            switch (subType)
                            {
                                case "x":
                                    conditions += Lager.X_FIXED;
                                    break;
                                case "y":
                                    conditions += Lager.Y_FIXED; restrained = 2;
                                    break;
                                case "r":
                                    conditions += Lager.R_FIXED; restrained = 3;
                                    break;
                            }
                        }
                        var prescribed = new double[restrained];
                        if (substrings.Length > 3) prescribed[0] = double.Parse(substrings[3]);
                        if (substrings.Length > 4) prescribed[1] = double.Parse(substrings[4]);
                        if (substrings.Length > 5) prescribed[2] = double.Parse(substrings[5]);
                        lager = new Lager(nodeId, conditions, prescribed, modell) { SupportId = supportId };
                        modell.Randbedingungen.Add(supportId, lager);
                        i++;
                    }
                    else
                    {
                        throw new ParseAusnahme((i + 2) + ": Lager" + supportId);
                    }
                } while (lines[i + 1].Length != 0);
                break;
            }
        }
    }
}
