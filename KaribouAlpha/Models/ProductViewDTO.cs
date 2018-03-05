namespace KaribouAlpha.Models
{
    public class ProductViewDTO
    {
        public long ID { get; set; }
        public long? CompanyID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Logo { get; set; }
    }
}