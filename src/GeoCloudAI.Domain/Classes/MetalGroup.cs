namespace GeoCloudAI.Domain.Classes
{
    public class MetalGroup
    {
        public int             Id { get; set; }
        public int             OreGeneticTypeId { get; set; }
        public OreGeneticType? OreGeneticType { get; set; }
        public string?         Name { get; set; }
    }
}