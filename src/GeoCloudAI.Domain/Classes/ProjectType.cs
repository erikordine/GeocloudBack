namespace GeoCloudAI.Domain.Classes
{
    public class ProjectType
    {
        public int      Id { get; set; }
        public int      AccountId { get; set; }
        public Account? Account { get; set; }
        public string   Name { get; set; }
        public string?  ImgType { get; set; }
    }
}