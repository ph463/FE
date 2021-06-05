namespace FEALibrary.Modell
{
    public class Eigenzustand
    {
        // Properties
        public string Id { get; set; }
        public int NumberOfStates { get; set; }
        public double[] Eigenvalues { get; set; }
        public double[][] Eigenvectors { get; set; }
        public bool[] Status { get; set; }

        // ....Constructors....................................................
        public Eigenzustand(string id, int numberOfStates)
        {
            Id = id;
            NumberOfStates = numberOfStates;
        }
    }
}
