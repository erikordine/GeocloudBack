namespace GeoCloudAI.Domain.Classes
{
    public class Unit
    {
        public int       Id { get; set; }
        public int       TypeId { get; set; }
        public UnitType? Type { get; set; }
        public string?   Name { get; set; }
    }
}