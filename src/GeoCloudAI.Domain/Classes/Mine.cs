namespace GeoCloudAI.Domain.Classes
{
    public class Mine
    {
        public int          Id { get; set; }
        public int          DepositId { get; set; }
        public Deposit?     Deposit { get; set; }
        public string?      Name { get; set; }
        public double?      Latitude { get; set; }
        public double?      Longitude { get; set; }
        public int?         StartYear { get; set; }
        public int?         EndYear { get; set; }
        public int?         Resource { get; set; }
        public int?         Reserve { get; set; }
        public int?         OreMined { get; set; }
        public string?      Comments { get; set; }
        public int?         SizeId { get; set; }
        public MineSize?    Size { get; set; }
        public int?         StatusId { get; set; }
        public MineStatus?  Status { get; set; }
        public int?         StatusPreviousId { get; set; }
        public MineStatus?  StatusPrevious { get; set; }
        public string?      ImgTypeProfile { get; set; }
        public string?      ImgTypeCover { get; set; }
        public int          UserId { get; set; }
        public User?        User { get; set; }
        public DateTime?    Register { get; set; }
        public int?         QttMineAreas { get; set; }
        public int?         QttDrillHoles { get; set; }
        public int?         QttDrillBoxes { get; set; }
    }
}