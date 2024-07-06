namespace GeoCloudAI.Domain.Classes
{
    public class MetalGroupSub
    {
        public int         Id { get; set; }
        public int         MetalGroupId { get; set; }
        public MetalGroup? MetalGroup { get; set; }
        public string?     Name { get; set; }
    }
}