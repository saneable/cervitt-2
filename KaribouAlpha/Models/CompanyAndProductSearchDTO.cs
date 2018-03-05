namespace KaribouAlpha.Models
{
    public enum CompanyAndProductSearchType
    {
        Company = 0,
        Product = 1
    }

    public class CompanyAndProductSearchDTO
    {
        public long CompanyID { get; set; }
        public long ProductID { get; set; }
        public string Name { get; set; }
        public CompanyAndProductSearchType Type { get; set; }
        public int ConnectionCount { get; set; }
        public int ProductCount { get; set; }
    }
}