namespace FEALibrary.Modell.abstrakte_Klassen
{
    public abstract class AbstraktLast
    {
        public string LoadId { get; set; }
        public string NodeId { get; set; }
        public double[] Intensity { get; set; }
        public abstract double[] ComputeLoadVector();
    }
}
