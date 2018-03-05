namespace KaribouAlpha.Models
{
    public class ProductSearchDTO
    {
        public long ID { get; set; }
        public long? CompanyID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CompanyFollowersCount { get; set; }
    }
}