namespace GeoCloudAI.Domain.Classes
{
    public class LithologyGroup
    {
        public int      Id { get; set; }
        public int      AccountId { get; set; }
        public Account? Account { get; set; }
        public string   Name { get; set; }
    }
}