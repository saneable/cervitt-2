using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaribouAlpha.Models
{
    public enum FeedbackVisibility
    {
        Public = 0,
        Private = 1
    }

    public class ProductFileFeedback : Article
    {
        [Required]
        public long ProductFileID { get; set; }
        public long? ReplyToID { get; set; }
        public FeedbackVisibility Visibility { get; set; }

        [ForeignKey("ProductFileID")]
        public virtual ProductFile ProductFile { get; set; }
        [ForeignKey("ReplyToID")]
        public virtual ProductFileFeedback ReplyTo { get; set; }
        public ICollection<ProductFileFeedback> Replies { get; set; }
    }
}