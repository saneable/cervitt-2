namespace KaribouAlpha.Models
{
    public class FeedbackDTO : ArticleDTO
    {
        public long ProductFileID { get; set; }
        public long ReplyToID { get; set; }
        public FeedbackVisibility Visibility { get; set; }
    }
}