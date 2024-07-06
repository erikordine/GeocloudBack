namespace GeoCloudAI.Domain.Classes
{
    public class MineArea
    {
        public int              Id { get; set; }
        public int              MineId { get; set; }
        public Mine?            Mine { get; set; }
        public string?          Name { get; set; }
        public double?          Latitude { get; set; }
        public double?          Longitude { get; set; }
        public int?             StartYear { get; set; }
        public int?             EndYear { get; set; }
        public int?             Resource { get; set; }
        public int?             Reserve { get; set; }
        public int?             OreMined { get; set; }
        public string?          Comments { get; set; }
        public int?             TypeId { get; set; }
        public MineAreaType?    Type { get; set; }
        public int?             StatusId { get; set; }
        public MineAreaStatus?  Status { get; set; }
        public int?             ShapeId { get; set; }
        public MineAreaShape?   Shape { get; set; }
        public string?          ImgTypeProfile { get; set; }
        public string?          ImgTypeCover { get; set; }
        public int              UserId { get; set; }
        public User?            User { get; set; }
        public DateTime?        Register { get; set; }
        public int?             QttDrillHoles { get; set; }
        public int?             QttDrillBoxes { get; set; }
    }
}