namespace GeoCloudAI.Domain.Classes
{
    public class Company
    {
        public int            Id { get; set; }
        public int            AccountId { get; set; }
        public Account?       Account { get; set; }
        public string         Name { get; set; }
        public int?           TypeId { get; set; }
        public CompanyType?   Type { get; set; }
        public string?        Comments { get; set; }
        public string?        ImgTypeProfile { get; set; }
        public string?        ImgTypeCover { get; set; }
        public int            UserId { get; set; }
        public User?          User { get; set; }
        public DateTime?      Register { get; set; }
    }
}