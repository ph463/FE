namespace FEALibrary.Model
{
    public class CrossSection
    {
        public string QuerschnittId { get; set; }
        public double[] QuerschnittsWerte { get; set; }


        public CrossSection(double area, double ixx)
        {
            QuerschnittsWerte = new double[2];
            QuerschnittsWerte[0] = area;
            QuerschnittsWerte[1] = ixx;
        }

        public CrossSection(double area)
        {
            QuerschnittsWerte = new double[1];
            QuerschnittsWerte[0] = area;
        }
    }
}
