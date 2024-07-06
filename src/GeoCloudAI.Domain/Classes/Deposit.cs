namespace GeoCloudAI.Domain.Classes
{
    public class Deposit
    {
        public int                  Id { get; set; }
        public int                  RegionId { get; set; }
        public Region?              Region { get; set; }
        public string?              Name { get; set; }
        public string?              AlternativeNames { get; set; }
        public string?              State { get; set; }
        public string?              City { get; set; }
        public double?              Latitude { get; set; }
        public double?              Longitude { get; set; }
        public string?              GeologicalDistrict { get; set; }
        public string?              DiscoveryBy { get; set; }
        public int?                 DiscoveryYear { get; set; }
        public int?                 Resource { get; set; }
        public int?                 Reserve { get; set; }
        public string?              Comments { get; set; }
        public int?                 DepositTypeId { get; set; }
        public DepositType?         DepositType { get; set; }
        public int?                 OreGeneticTypeId { get; set; }
        public OreGeneticType?      OreGeneticType { get; set; }
        public int?                 OreGeneticTypeSubId { get; set; }
        public OreGeneticTypeSub?   OreGeneticTypeSub { get; set; }
        public int?                 MetalGroupId { get; set; }
        public MetalGroup?          MetalGroup { get; set; }
        public int?                 MetalGroupSubId { get; set; }
        public MetalGroupSub?       MetalGroupSub { get; set; }
        public string?              ImgTypeProfile { get; set; }
        public string?              ImgTypeCover { get; set; }
        public int                  UserId { get; set; }
        public User?                User { get; set; }
        public DateTime?            Register { get; set; }
        public int?                 QttMines { get; set; }
        public int?                 QttMineAreas { get; set; }
        public int?                 QttDrillHoles { get; set; }
        public int?                 QttDrillBoxes { get; set; }
    }
}