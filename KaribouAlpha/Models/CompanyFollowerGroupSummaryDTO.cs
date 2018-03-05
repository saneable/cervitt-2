using System;

namespace KaribouAlpha.Models
{
    public class CompanyFollowerGroupSummaryDTO
    {
        public long ID { get; set; }
        public long CompanyID { get; set; }
        public String Name { get; set; }
        public int FollowersCount { get; set; }
        public int VisibleProductsCount { get; set; }
        public int VisibleProductFilesCount { get; set; }
    }
}