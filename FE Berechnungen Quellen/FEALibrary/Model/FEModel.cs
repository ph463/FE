using FEALibrary.Model.abstractClasses;
using System.Collections.Generic;

namespace FEALibrary.Model
{
    public class FeModel
    {
        public string ModelId { get; set; }
        public int SpatialDimension { get; set; }
        public bool Solved { get; set; } = false;
        public bool Eigen { get; set; } = false;
        public bool Timeint { get; set; } = false;

        public Dictionary<string, Node> Knoten { get; set; }
        public Dictionary<string, AbstractElement> Elemente { get; set; }
        public Dictionary<string, AbstractMaterial> Material { get; set; }
        public Dictionary<string, CrossSection> Querschnitt { get; set; }
        public Dictionary<string, AbstractLoad> Lasten { get; set; }
        public Dictionary<string, AbstractLineLoad> LinienLasten { get; set; }
        public Dictionary<string, AbstractElementLoad> ElementLasten { get; set; }
        public Dictionary<string, AbstractElementLoad> PunktLasten { get; set; }
        public Dictionary<string, AbstractBoundaryCondition> Randbedingungen { get; set; }
        public Eigenstates Eigenstate { get; set; }
        public AbstractTimeintegration Zeitintegration { get; set; }
        public Dictionary<string, AbstractTimeDependentNodeLoad> ZeitabhängigeKnotenLasten { get; set; }
        public Dictionary<string, AbstractTimeDependentElementLoad> ZeitabhängigeElementLasten { get; set; }
        public Dictionary<string, AbstractTimeDependentBoundaryCondition> ZeitabhängigeRandbedingung { get; set; }

        public FeModel(int spatialDimension)
        {
            SpatialDimension = spatialDimension;

            Knoten = new Dictionary<string, Node>();
            Elemente = new Dictionary<string, AbstractElement>();
            Material = new Dictionary<string, AbstractMaterial>();
            Querschnitt = new Dictionary<string, CrossSection>();
            Lasten = new Dictionary<string, AbstractLoad>();
            LinienLasten = new Dictionary<string, AbstractLineLoad>();
            ElementLasten = new Dictionary<string, AbstractElementLoad>();
            PunktLasten = new Dictionary<string, AbstractElementLoad>();
            Randbedingungen = new Dictionary<string, AbstractBoundaryCondition>();
            ZeitabhängigeKnotenLasten = new Dictionary<string, AbstractTimeDependentNodeLoad>();
            ZeitabhängigeElementLasten = new Dictionary<string, AbstractTimeDependentElementLoad>();
            ZeitabhängigeRandbedingung = new Dictionary<string, AbstractTimeDependentBoundaryCondition>();
        }
    }
}