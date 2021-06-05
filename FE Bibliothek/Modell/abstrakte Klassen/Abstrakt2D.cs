using System.Windows;

namespace FE_Bibliothek.Modell.abstrakte_Klassen
{
    public abstract class Abstrakt2D : AbstraktElement
    {
        protected Querschnitt ElementCrossSection { get; set; }

        public void SetCrossSectionReferences(FEModell modell)
        {
            // Elementquerschnitt für 2D Elemente
            if (ElementCrossSectionId == null) return;
            if (modell.Querschnitt.TryGetValue(ElementCrossSectionId, out var crossSection))
            {
                ElementCrossSection = crossSection;
            }
            else
            {
                var querschnitt = MessageBox.Show("Querschnitt " + ElementCrossSectionId + " ist nicht im Modell enthalten.", "Abstract2D");
                _ = querschnitt;
            }
        }
        public abstract double[] ComputeElementState(double z0, double z1);
        public abstract Point ComputeCenterOfGravity();
    }
}
