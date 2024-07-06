namespace GeoCloudAI.Domain.Classes
{
    public class Account
    {
        public int     Id { get; set; }
        public string? Name { get; set; }
        public string? Company { get; set; }
        public string? Employees { get; set; }
        public int?    AcessMaxAttempts { get; set; }
        public int?    ValidityUserPassword { get; set; }
        public int?    ValidityInviteUser { get; set; }
        public int?    ValidityInviteProject { get; set; }
        public string? Guid { get; set; }
    }
}