namespace FE_Berechnungen.Elastizitätsberechnung.ModelldatenAnzeigen
{
    internal class Lagerbedingung
    {
        public string NodeId { get; }
        public string[] Vordefiniert { get; }

        public Lagerbedingung(string nodeId, string[] vordefiniert)
        {
            NodeId = nodeId;
            Vordefiniert = vordefiniert;
        }
    }
}