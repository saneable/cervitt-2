using System;
using System.Collections.Generic;

namespace KaribouAlpha.Models
{
    public class NewsItemDTO : ArticleDTO
    {
        public long? ProductID { get; set; }
        public long? ProductCompanyID { get; set; }
        public String ProductName { get; set; }

        public ICollection<NewsItemFeedbackDTO> Feedback { get; set; }
    }
}