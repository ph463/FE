using FEALibrary.Modell.abstrakte_Klassen;
using System.Collections.Generic;

namespace FEALibrary.Modell
{
    public class FEModell
    {
        public string ModelId { get; set; }
        public int SpatialDimension { get; set; }
        public bool Solved { get; set; } = false;
        public bool Eigen { get; set; } = false;
        public bool Timeint { get; set; } = false;

        public Dictionary<string, Knoten> Knoten { get; set; }
        public Dictionary<string, AbstraktElement> Elemente { get; set; }
        public Dictionary<string, AbstraktMaterial> Material { get; set; }
        public Dictionary<string, Querschnitt> Querschnitt { get; set; }
        public Dictionary<string, AbstraktLast> Lasten { get; set; }
        public Dictionary<string, AbstraktLinienlast> LinienLasten { get; set; }
        public Dictionary<string, AbstraktElementLast> ElementLasten { get; set; }
        public Dictionary<string, AbstraktElementLast> PunktLasten { get; set; }
        public Dictionary<string, AbstraktRandbedingung> Randbedingungen { get; set; }
        public Eigenzustand Eigenstate { get; set; }
        public AbstraktZeitintegration Zeitintegration { get; set; }
        public Dictionary<string, AbstraktZeitabhängigeKnotenlast> ZeitabhängigeKnotenLasten { get; set; }
        public Dictionary<string, AbstraktZeitabhängigeElementLast> ZeitabhängigeElementLasten { get; set; }
        public Dictionary<string, AbstraktZeitabhängigeRandbedingung> ZeitabhängigeRandbedingung { get; set; }

        public FEModell(int spatialDimension)
        {
            SpatialDimension = spatialDimension;

            Knoten = new Dictionary<string, Knoten>();
            Elemente = new Dictionary<string, AbstraktElement>();
            Material = new Dictionary<string, AbstraktMaterial>();
            Querschnitt = new Dictionary<string, Querschnitt>();
            Lasten = new Dictionary<string, AbstraktLast>();
            LinienLasten = new Dictionary<string, AbstraktLinienlast>();
            ElementLasten = new Dictionary<string, AbstraktElementLast>();
            PunktLasten = new Dictionary<string, AbstraktElementLast>();
            Randbedingungen = new Dictionary<string, AbstraktRandbedingung>();
            ZeitabhängigeKnotenLasten = new Dictionary<string, AbstraktZeitabhängigeKnotenlast>();
            ZeitabhängigeElementLasten = new Dictionary<string, AbstraktZeitabhängigeElementLast>();
            ZeitabhängigeRandbedingung = new Dictionary<string, AbstraktZeitabhängigeRandbedingung>();
        }
    }
}
