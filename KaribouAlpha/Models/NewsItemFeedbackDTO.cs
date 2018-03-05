using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaribouAlpha.Models
{
    public class NewsItemFeedbackDTO : ArticleDTO
    {
        [Required]
        public long NewsItemID { get; set; }
    }
}