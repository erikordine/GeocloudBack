namespace GeoCloudAI.Domain.Classes
{
    public class ProfileRole
    {
        public int       Id { get; set; }
        public int       ProfileId { get; set; }
        public Profile?  Profile { get; set; }
        public int       RoleId { get; set; }
        public Role?     Role { get; set; }
    }
}