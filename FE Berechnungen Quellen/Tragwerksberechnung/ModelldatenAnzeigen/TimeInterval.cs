namespace FE_Berechnungen.Tragwerksberechnung.ModelldatenAnzeigen
{
    public class TimeInterval
    {
        public string KnotenId { get; set; }
        public double Time { get; set; }
        public double Force { get; set; }

        public TimeInterval(string knotenId, double time, double force)
        {
            KnotenId = knotenId;
            Time = time;
            Force = force;
        }
    }
}
