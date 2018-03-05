using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaribouAlpha.Models
{
    public class NewNewsItemFeedbackDTO : NewArticleDTO
    {
        [Required]
        public long NewsItemID { get; set; }
    }
}