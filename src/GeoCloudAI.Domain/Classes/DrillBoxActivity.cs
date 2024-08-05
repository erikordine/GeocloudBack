namespace GeoCloudAI.Domain.Classes
{
    public class DrillBoxActivity
    {
        public int                      Id { get; set; }
        public int                      DrillBoxId { get; set; }
        public DrillBox?                DrillBox { get; set; }
        public int?                     ActivityId { get; set; }
        public DrillBoxActivityType?    Activity { get; set; }
        public DateTime?                StartTime   { get; set; }
        public DateTime?                EndTime     { get; set; }
        public int                      UserId { get; set; }
        public User?                    User { get; set; }
        public DateTime?                Register { get; set; }
    }
}