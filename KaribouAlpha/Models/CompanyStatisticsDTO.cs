using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KaribouAlpha.Models
{
    public class CompanyStatisticsDTO
    {
        public long ProductViewsThisWeek { get; set; }
        public long ProductFileDownloadsThisWeek { get; set; }
        public long ProductFeedbackThisWeek { get; set; }
        public long ProductViewsLastWeek { get; set; }
        public long ProductFileDownloadsLastWeek { get; set; }
        public long ProductFeedbackLastWeek { get; set; }
        public long ProductViewsThisMonth { get; set; }
        public long ProductFileDownloadsThisMonth { get; set; }
        public long ProductFeedbackThisMonth { get; set; }
        public long ProductViewsLastMonth { get; set; }
        public long ProductFileDownloadsLastMonth { get; set; }
        public long ProductFeedbackLastMonth { get; set; }
    }
}