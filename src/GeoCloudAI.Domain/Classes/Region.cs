namespace GeoCloudAI.Domain.Classes
{
    public class Region
    {
        public int         Id { get; set; }
        public int         AccountId { get; set; }
        public Account?    Account { get; set; }
        public string?     Name { get; set; }
        public int         CountryId { get; set; }
        public Country?    Country { get; set; }
        public string?     State { get; set; }
        public string?     City { get; set; }
        public double?     Latitude { get; set; }
        public double?     Longitude { get; set; }
        public string?     Comments { get; set; }
        public string?     ImgTypeProfile { get; set; }
        public string?     ImgTypeCover { get; set; }
        public int         UserId { get; set; }
        public User?       User { get; set; }
        public DateTime?   Register { get; set; }
        public int?        QttDeposits { get; set; }
        public int?        QttMines { get; set; }
        public int?        QttMineAreas { get; set; }
        public int?        QttDrillHoles { get; set; }
        public int?        QttDrillBoxes { get; set; }
    }
}