namespace GeoCloudAI.Domain.Classes
{
    public class DrillBox
    {
        public int               Id { get; set; }
        public int               DrillHoleId { get; set; }
        public DrillHole?        DrillHole { get; set; }
        public int               Number { get; set; }
        public int?              AmountCores { get; set; }
        public string?           Code { get; set; }
        public string?           Uuid { get; set; }
        public double?           StartDepth { get; set; }
        public double?           EndDepth { get; set; }
        public string?           Description { get; set; }
        public string?           Comments { get; set; }
        public int?              TypeId { get; set; }
        public DrillBoxType?     Type { get; set; }
        public int?              StatusId { get; set; }
        public DrillBoxStatus?   Status { get; set; }
        public int?              MaterialId { get; set; }
        public DrillBoxMaterial? Material { get; set; }
        public int?              CoreShedId { get; set; }
        public CoreShed?         CoreShed { get; set; }
        public string?           Shelves { get; set; }
        public string?           ImgType { get; set; }
        public int               UserId { get; set; }
        public User?             User { get; set; }
        public DateTime?         Register { get; set; }
    }
}