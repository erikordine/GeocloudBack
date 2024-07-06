namespace GeoCloudAI.Domain.Classes
{
    public class Functionality
    {
        public int                Id { get; set; }
        public int                TypeId { get; set; }
        public FunctionalityType? Type { get; set; }
        public string?            Name { get; set; }
    }
}