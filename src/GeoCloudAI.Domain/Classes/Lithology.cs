namespace GeoCloudAI.Domain.Classes
{
    public class Lithology
    {
        public int                  Id { get; set; }
        public int                  GroupSubId { get; set; }
        public LithologyGroupSub?   GroupSub { get; set; }
        public string?              Name { get; set; }
    }
}