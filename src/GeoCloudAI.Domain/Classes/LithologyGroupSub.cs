namespace GeoCloudAI.Domain.Classes
{
    public class LithologyGroupSub
    {
        public int             Id { get; set; }
        public int             GroupId { get; set; }
        public LithologyGroup? Group { get; set; }
        public string?         Name { get; set; }
    }
}