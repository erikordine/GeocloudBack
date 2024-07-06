namespace GeoCloudAI.Domain.Classes
{
    public class OreGeneticType
    {
        public int          Id { get; set; }
        public int          DepositTypeId { get; set; }
        public DepositType? DepositType { get; set; }
        public string?      Name { get; set; }
    }
}