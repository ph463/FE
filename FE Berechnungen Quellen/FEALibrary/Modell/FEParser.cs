namespace FEALibrary.Modell
{
    public class FeParser
    {
        private string nodeId, nodePrefix;
        private string[] substrings;
        private int numberNodalDof, zaehler;
        private double xInterval, yInterval, zInterval;
        private int nNodesX, nNodesY;
        private double[] crds;
        private readonly char[] delimiters = { '\t' };


        public string ModelId { get; set; }
        public FEModell FeModell { get; set; }
        public int SpatialDimension { get; set; }
        public static string InputFound { get; set; }

        // parsing a new model to be read from file
        public void ParseModel(string[] lines)
        {
            for (var i = 0; i < lines.Length; i++)
            {
                InputFound = "";
                if (lines[i] != "ModellName") continue;
                ModelId = lines[i + 1];
                InputFound = "ModellName = " + ModelId;
                break;
            }

            for (var i = 0; i < lines.Length; i++)
            {
                if (lines[i] != "Raumdimension") continue;
                InputFound += "\nRaumdimension";
                substrings = lines[i + 1].Split(delimiters);
                SpatialDimension = int.Parse(substrings[0]);
                numberNodalDof = int.Parse(substrings[1]);
                break;
            }
            FeModell = new FEModell(SpatialDimension);
        }

        // NodeId, Knotenkoordinaten
        public void ParseNodes(string[] lines)
        {
            for (var i = 0; i < lines.Length; i++)
            {
                if (lines[i] == "Knoten")
                {
                    InputFound += "\nKnoten";
                    do
                    {
                        substrings = lines[i + 1].Split(delimiters);
                        Knoten knoten;
                        var dimension = FeModell.SpatialDimension;
                        switch (substrings.Length)
                        {
                            case 1:
                                numberNodalDof = int.Parse(substrings[0]);
                                break;
                            case 2:
                                nodeId = substrings[0];
                                crds = new double[1];
                                crds[0] = double.Parse(substrings[1]);
                                knoten = new Knoten(nodeId, crds, numberNodalDof, dimension);
                                FeModell.Knoten.Add(nodeId, knoten);
                                break;
                            case 3:
                                nodeId = substrings[0];
                                crds = new double[2];
                                crds[0] = double.Parse(substrings[1]);
                                crds[1] = double.Parse(substrings[2]);
                                knoten = new Knoten(nodeId, crds, numberNodalDof, dimension);
                                FeModell.Knoten.Add(nodeId, knoten);
                                break;
                            case 4:
                                nodeId = substrings[0];
                                crds = new double[3];
                                crds[0] = double.Parse(substrings[1]);
                                crds[1] = double.Parse(substrings[2]);
                                crds[2] = double.Parse(substrings[3]);
                                knoten = new Knoten(nodeId, crds, numberNodalDof, dimension);
                                FeModell.Knoten.Add(nodeId, knoten);
                                break;
                            default:
                                {
                                    throw new ParseAusnahme((i + 2) + ": Knoten " + nodeId + " falsche Anzahl Parameter");
                                }
                        }
                        i++;
                    } while (lines[i + 1].Length != 0);
                }
                //Knotengruppe
                if (lines[i] == "Knotengruppe")
                {
                    InputFound += "\nKnotengruppe";
                    substrings = lines[i + 1].Split(delimiters);
                    if (substrings.Length == 1) nodePrefix = substrings[0];
                    else
                    {
                        throw new ParseAusnahme((i + 2) + ": Knotengruppe");
                    }
                    zaehler = 0;
                    i += 2;
                    do
                    {
                        substrings = lines[i].Split(delimiters);
                        crds = new double[SpatialDimension];
                        if (substrings.Length == SpatialDimension)
                        {
                            for (var k = 0; k < SpatialDimension; k++)
                            {
                                crds[k] = double.Parse(substrings[k]);
                            }
                        }
                        else
                        {
                            throw new ParseAusnahme(i + ": Knotengruppe");
                        }

                        //spatialDimension += numberNodalDOF;
                        nodeId = nodePrefix + zaehler.ToString().PadLeft(6, '0');
                        var node = new Knoten(nodeId, crds, numberNodalDof, SpatialDimension);
                        FeModell.Knoten.Add(nodeId, node);
                        i++;
                        zaehler++;
                    } while (lines[i].Length != 0);
                }
                //Aequidistantes Knotennetz in 1D
                if (lines[i] == "Aequidistantes Knotennetz")
                {
                    do
                    {
                        substrings = lines[i + 1].Split(delimiters);
                        nodePrefix = substrings[0];

                        //Aequidistantes Knotennetz in 1D
                        double[] nodeCrds;
                        switch (substrings.Length)
                        {
                            case 4:
                                {
                                    InputFound += "\nAequidistantes Knotennetz in 1D";
                                    crds = new double[SpatialDimension];
                                    crds[0] = double.Parse(substrings[1]);
                                    xInterval = double.Parse(substrings[2]);
                                    nNodesX = short.Parse(substrings[3]);

                                    for (var k = 0; k < nNodesX; k++)
                                    {
                                        nodeId = nodePrefix + k.ToString().PadLeft(6, '0');
                                        nodeCrds = new[] { crds[0], 0 };
                                        var node = new Knoten(nodeId, nodeCrds, numberNodalDof, SpatialDimension);
                                        FeModell.Knoten.Add(nodeId, node);
                                        crds[0] += xInterval;
                                    }
                                    break;
                                }
                            //Aequidistantes Knotennetz in 2D
                            case 7:
                                {
                                    InputFound += "\nAequidistantes Knotennetz in 2D";
                                    crds = new double[SpatialDimension];
                                    crds[0] = double.Parse(substrings[1]);
                                    xInterval = double.Parse(substrings[2]);
                                    nNodesX = short.Parse(substrings[3]);
                                    crds[1] = double.Parse(substrings[4]);
                                    yInterval = double.Parse(substrings[5]);
                                    nNodesY = short.Parse(substrings[6]);


                                    for (var k = 0; k < nNodesX; k++)
                                    {
                                        var temp = crds[1];
                                        var idX = k.ToString().PadLeft(3, '0');
                                        for (var l = 0; l < nNodesY; l++)
                                        {
                                            var idY = l.ToString().PadLeft(3, '0');
                                            nodeId = nodePrefix + idX + idY;
                                            nodeCrds = new[] { crds[0], crds[1] };
                                            var node = new Knoten(nodeId, nodeCrds, numberNodalDof, SpatialDimension);
                                            FeModell.Knoten.Add(nodeId, node);
                                            crds[1] += yInterval;
                                        }

                                        crds[0] += xInterval;
                                        crds[1] = temp;
                                    }
                                    break;
                                }
                            //Aequidistantes Knotennetz in 3D
                            case 10:
                                {
                                    InputFound += "\nAequidistantes Knotennetz in 3D";
                                    crds = new double[3];
                                    crds[0] = double.Parse(substrings[1]);
                                    xInterval = double.Parse(substrings[2]);
                                    nNodesX = short.Parse(substrings[3]);
                                    crds[1] = double.Parse(substrings[4]);
                                    yInterval = double.Parse(substrings[5]);
                                    nNodesY = short.Parse(substrings[6]);
                                    crds[2] = double.Parse(substrings[7]);
                                    zInterval = double.Parse(substrings[8]);

                                    for (var k = 0; k < nNodesX; k++)
                                    {
                                        var temp1 = crds[1];
                                        var idX = k.ToString().PadLeft(2, '0');
                                        for (var l = 0; l < nNodesX; l++)
                                        {
                                            var temp2 = crds[2];
                                            var idY = l.ToString().PadLeft(2, '0');
                                            nodeId = nodePrefix + idX + idY;
                                            for (var m = 0; m < nNodesY; m++)
                                            {
                                                var idZ = m.ToString().PadLeft(2, '0');
                                                nodeId = nodePrefix + idX + idY + idZ;
                                                nodeCrds = new[] { crds[0], crds[1], crds[2] };
                                                var node = new Knoten(nodeId, nodeCrds, numberNodalDof, SpatialDimension);
                                                FeModell.Knoten.Add(nodeId, node);
                                                crds[2] += zInterval;
                                            }

                                            crds[2] = temp2;
                                            crds[1] += yInterval;
                                        }

                                        crds[1] = temp1;
                                        crds[0] += xInterval;
                                    }
                                    break;
                                }
                            default:
                                {
                                    throw new ParseAusnahme((i + 3) + ": Aequidistantes Knotennetz");
                                }
                        }
                        i++;
                    } while (lines[i + 1].Length != 0);
                }
                //variables Knotennetz
                if (lines[i] != "Variables Knotennetz") continue;
                {
                    do
                    {
                        substrings = lines[i + 1].Split(delimiters);
                        InputFound += "\nVariables Knotennetz";
                        substrings = lines[i + 1].Split(delimiters);
                        string idX, idY;
                        crds = new double[SpatialDimension];

                        double coord0, coord1;
                        var offset = new double[substrings.Length];
                        for (var k = 0; k < substrings.Length; k++)
                            offset[k] = double.Parse(substrings[k]);

                        substrings = lines[i + 2].Split(delimiters);
                        double[] nodeCrds;
                        switch (substrings.Length)
                        {
                            case 2:
                                {
                                    nodePrefix = substrings[0];
                                    coord0 = double.Parse(substrings[1]);
                                    for (var n = 0; n < offset.Length; n++)
                                    {
                                        crds[0] = coord0 + offset[n];
                                        nodeId = nodePrefix + n.ToString().PadLeft(6, '0');
                                        nodeCrds = new[] { crds[0], 0 };
                                        var node = new Knoten(nodeId, nodeCrds, numberNodalDof, SpatialDimension);
                                        FeModell.Knoten.Add(nodeId, node);
                                    }
                                    break;
                                }
                            case 3:
                                {
                                    nodePrefix = substrings[0];
                                    coord0 = double.Parse(substrings[1]);
                                    coord1 = double.Parse(substrings[2]);
                                    for (var n = 0; n < offset.Length; n++)
                                    {
                                        idX = n.ToString().PadLeft(3, '0');
                                        crds[0] = coord0 + offset[n];
                                        for (var m = 0; m < offset.Length; m++)
                                        {
                                            idY = m.ToString().PadLeft(3, '0');
                                            crds[1] = coord1 + offset[m];
                                            nodeId = nodePrefix + idX + idY;
                                            nodeCrds = new[] { crds[0], crds[1] };
                                            var node = new Knoten(nodeId, nodeCrds, numberNodalDof, SpatialDimension);
                                            FeModell.Knoten.Add(nodeId, node);
                                        }
                                    }
                                    break;
                                }
                            case 4:
                                {
                                    nodePrefix = substrings[0];
                                    coord0 = double.Parse(substrings[1]);
                                    coord1 = double.Parse(substrings[2]);
                                    var coord2 = double.Parse(substrings[3]);
                                    for (var n = 0; n < offset.Length; n++)
                                    {
                                        idX = n.ToString().PadLeft(2, '0');
                                        var inkrement0 = coord0 + offset[n];
                                        for (var m = 0; m < offset.Length; m++)
                                        {
                                            idY = m.ToString().PadLeft(2, '0');
                                            var inkrement1 = coord1 + offset[m];
                                            for (var k = 0; k < offset.Length; k++)
                                            {
                                                crds = new double[3];
                                                crds[0] = inkrement0;
                                                crds[1] = inkrement1;
                                                var idZ = k.ToString().PadLeft(2, '0');
                                                crds[2] = coord2 + offset[k];
                                                nodeId = nodePrefix + idX + idY + idZ;
                                                nodeCrds = new[] { crds[0], crds[1], crds[2] };
                                                var node = new Knoten(nodeId, nodeCrds, numberNodalDof, SpatialDimension);
                                                FeModell.Knoten.Add(nodeId, node);
                                            }
                                        }
                                    }
                                    break;
                                }
                            default:
                                {
                                    throw new ParseAusnahme((i + 3) + ": Variables Knotennetz");
                                }
                        }
                    } while (lines[i + 3].Length != 0);
                }
            }
        }
    }
}
