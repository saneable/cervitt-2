using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaribouAlpha.Models
{
    public enum ActivityVerb
    {
        ConnectedWithCompany = 0,
        AddedProduct = 1,
        AddedProductFile = 2,
        UpdatedProduct = 3, 
        UpdatedProductFile = 4,
        DeletedProduct = 5,
        DeletedProductFile = 6,
        PostedNewsItem = 7,
        PostedFeedback = 8,
        DownloadedFile = 9
    }
    public class Activity
    {
        public long ID { get; set; }
        [Required]
        public long UserID { get; set; }
        [Required]
        public ActivityVerb Verb { get; set; }
        [Required]
        public long TargetEntityID { get; set; }
        public long ContextEntityID { get; set; }
        [Required]
        public DateTime DateTime;

        [ForeignKey("UserID")]
        public virtual User User { get; set; }
    }
}