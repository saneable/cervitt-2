using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaribouAlpha.Models
{
    public abstract class Article
    {
        public long ID { get; set; }
        [Required]
        public long UserID { get; set; }
        [Required]
        public string Title { get; set; }
        public string Body { get; set; }
        public string LinkUrl { get; set; }
        public byte[] Image { get; set; }
        [Required]
        public DateTime PostedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }
    }
}