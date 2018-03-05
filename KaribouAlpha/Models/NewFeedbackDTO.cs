using System.ComponentModel.DataAnnotations;

namespace KaribouAlpha.Models
{
    public class NewFeedbackDTO : NewArticleDTO
    {
        [Required]
        public long ProductFileID { get; set; }
        public long? ReplyToID { get; set; }
        public FeedbackVisibility Visibility { get; set; }
    }
}