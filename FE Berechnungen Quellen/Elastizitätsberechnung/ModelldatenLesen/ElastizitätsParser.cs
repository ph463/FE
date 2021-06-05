﻿using FEALibrary.Modell;

namespace FE_Berechnungen.Elastizitätsberechnung.ModelldatenLesen
{
    public class ElastizitätsParser : FeParser
    {
        private FEModell modell;
        public ElementParser parseElastizitätsElemente;
        public MaterialParser parseElastizitätsMaterial;
        public LastParser parseElastizitätsLasten;
        public static RandbedingungenParser parseElastizitätsRandbedingungen;

        // Eingabedaten für eine Elastizitätsberechnung aus Detei lesen
        public void ParseElastizität(string[] lines, FEModell feModell)
        {
            modell = feModell;
            parseElastizitätsElemente = new ElementParser();
            parseElastizitätsElemente.ParseElements(lines, modell);

            parseElastizitätsMaterial = new MaterialParser();
            parseElastizitätsMaterial.ParseMaterials(lines, modell);

            parseElastizitätsLasten = new LastParser();
            parseElastizitätsLasten.ParseLasten(lines, modell);

            parseElastizitätsRandbedingungen = new RandbedingungenParser();
            parseElastizitätsRandbedingungen.ParseRandbedingungen(lines, modell);
        }
    }
}
