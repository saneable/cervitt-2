namespace KaribouAlpha.Models
{
    public class ProductSummaryDTO
    {
        public long ID { get; set; }
        public long? CompanyID { get; set; }
        public string Name { get; set; }
        public long UserLevelId { get; set; }
    }
}