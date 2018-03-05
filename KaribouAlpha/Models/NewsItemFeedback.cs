using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaribouAlpha.Models
{
    public class NewsItemFeedback : Article
    {
        [Required]
        public long NewsItemID { get; set; }

        [ForeignKey("NewsItemID")]
        public virtual NewsItem NewsItem { get; set; }
    }
}