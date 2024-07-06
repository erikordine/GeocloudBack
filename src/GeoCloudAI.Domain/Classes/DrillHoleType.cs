namespace GeoCloudAI.Domain.Classes
{
    public class DrillHoleType
    {
        public int      Id { get; set; }
        public int      AccountId { get; set; }
        public Account? Account { get; set; }
        public string   Name { get; set; }
        public int      Diameter { get; set; }
    }
}