namespace FEALibrary.Model.abstractClasses
{
    public abstract class AbstractMaterial
    {
        public string MaterialId { get; set; }
        public double[] MaterialWerte { get; set; }
        //public double[] QuerschnittsWerte { get; set; }
    }
}