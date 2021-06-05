namespace FEALibrary.Modell.abstrakte_Klassen
{
    public abstract class AbstraktZeitabhängigeElementLast : AbstraktElementLast
    {
        public int VariationType { get; set; }
        public double[] P { get; set; }
        public abstract override double[] ComputeLoadVector();
    }
}
