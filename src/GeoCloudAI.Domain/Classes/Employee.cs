namespace GeoCloudAI.Domain.Classes
{
    public class Employee
    {
        public int            Id { get; set; }
        public int            CompanyId { get; set; }
        public Company?       Company { get; set; }
        public string         Name { get; set; }
        public int?           RoleId { get; set; }
        public EmployeeRole?  Role { get; set; }
        public string?        ImgType { get; set; }
        public int            UserId { get; set; }
        public User?          User { get; set; }
        public DateTime?      Register { get; set; }
    }
}