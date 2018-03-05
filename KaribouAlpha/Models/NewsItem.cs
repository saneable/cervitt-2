using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaribouAlpha.Models
{
    public class NewsItem : Article
    {
        public long? ProductID { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
        public ICollection<NewsItemFeedback> Feedback { get; set; }
    }
}