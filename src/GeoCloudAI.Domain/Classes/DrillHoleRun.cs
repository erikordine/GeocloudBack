namespace GeoCloudAI.Domain.Classes
{
    public class DrillHoleRun
    {
        public int             Id          { get; set; }
        public int             DrillHoleId { get; set; }
        public DrillHole?      DrillHole   { get; set; }
        public double?         StartDepth  { get; set; }
        public double?         EndDepth    { get; set; }
        public DateTime?       StartTime   { get; set; }
        public DateTime?       EndTime     { get; set; }
        public int             UserId      { get; set; }
        public User?           User        { get; set; }
        public DateTime?       Register    { get; set; }
    }
}