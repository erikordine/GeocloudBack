namespace GeoCloudAI.Domain.Classes
{
    public class DrillHole
    {
        public int            Id { get; set; }
        public int            RegionId { get; set; }
        public Region?        Region { get; set; }
        public int?           DepositId { get; set; }
        public Deposit?       Deposit { get; set; }
        public int?           MineId { get; set; }
        public Mine?          Mine { get; set; }
        public int?           MineAreaId { get; set; }
        public MineArea?      MineArea { get; set; }
        public string         Name { get; set; }
        public double?        Latitude { get; set; }
        public double?        Longitude { get; set; }
        public double?        Elevation { get; set; }
        public int?           Length { get; set; }
        public string?        Comments { get; set; }
        public int?           TypeId { get; set; }
        public DrillHoleType? Type { get; set; }
        public int?           DrillingTypeId { get; set; }
        public DrillingType?  DrillingType { get; set; }
        public int?           ContractorId { get; set; }
        public Company?       Contractor { get; set; }
        public int?           DrillerId { get; set; }
        public Company?       Driller { get; set; }
        public DateTime?      StartDate { get; set; }
        public DateTime?      EndDate { get; set; }
        public int            UserId { get; set; }
        public User?          User { get; set; }
        public DateTime?      Register { get; set; }
        public int?           QttDrillBoxes { get; set; }
    }
}