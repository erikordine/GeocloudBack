namespace GeoCloudAI.Domain.Classes
{
    public class Project
    {
        public int             Id { get; set; }
        public int             AccountId { get; set; }
        public Account?        Account { get; set; }
        public string?         Name { get; set; }
        public DateTime?       StartDate { get; set; }
        public DateTime?       EndDate { get; set; }
        public string?         Summary { get; set; }
        public string?         Comments { get; set; }
        public int?            TypeId { get; set; }
        public ProjectType?    Type { get; set; }
        public int?            StatusId { get; set; }
        public ProjectStatus?  Status { get; set; }
        public int             UserId { get; set; }
        public User?           User { get; set; }
        public DateTime?       Register { get; set; }
    }
}