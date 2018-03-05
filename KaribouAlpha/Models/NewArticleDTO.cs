using System.ComponentModel.DataAnnotations;

namespace KaribouAlpha.Models
{
    public class NewArticleDTO
    {
        [Required]
        public string Title { get; set; }
        public string Body { get; set; }
        public string LinkUrl { get; set; }
        public byte[] Image { get; set; }
    }
}