﻿using System.Windows;

namespace FE_Bibliothek.Modell.abstrakte_Klassen
{
    public abstract class AbstraktElementLast : AbstraktLast
    {
        private AbstraktElement element;
        public string ElementId { get; set; }
        public AbstraktElement Element { get => element; set => element = value; }
        public bool InElementCoordinateSystem { get; set; } = true;

        public void SetReferences(FEModell modell)
        {
            if (modell.Elemente.TryGetValue(ElementId, out element)) { Element = element; }

            if (element != null) return;
            var message = "Element mit ID=" + ElementId + " ist nicht im Modell enthalten";
            _ = MessageBox.Show(message, "AbstraktElementLast");
        }
        public bool IsInElementCoordinateSystem() { return InElementCoordinateSystem; }
    }
}
