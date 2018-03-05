using System;
namespace KaribouAlpha.Models
{
    public class CompanySearchDTO
    {
        public long ID { get; set; }
        public String DisplayName { get; set; }
        public String Description { get; set; }
        public int FollowersCount { get; set; }
        public int ProductCount { get; set; }
    }
}