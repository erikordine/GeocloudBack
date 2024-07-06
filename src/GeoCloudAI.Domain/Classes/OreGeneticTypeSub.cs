namespace GeoCloudAI.Domain.Classes
{
    public class OreGeneticTypeSub
    {
        public int             Id { get; set; }
        public int             OreGeneticTypeId { get; set; }
        public OreGeneticType? OreGeneticType { get; set; }
        public string?         Name { get; set; }
    }
}