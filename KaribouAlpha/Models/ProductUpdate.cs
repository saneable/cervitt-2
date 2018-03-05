using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaribouAlpha.Models
{
    public enum UpdateType
    {
        ProductAdded = 0,
        ProductEdited = 1,
        ProductDeleted = 2,
        ProductFileAdded = 3,
        ProductFileEdited = 4,
        ProductFileDeleted = 5,
        ProductCustomerAdded = 6,
        ProductCustomerDeleted = 7
    }

    public class ProductUpdate
    {
        public long ID { get; set; }
        [Required]
        public long UserID { get; set; }
        [Required]
        public long ProductID { get; set; }
        public long? ProductFileID { get; set; }
        [Required]
        public UpdateType UpdateType { get; set; }
        [Required]
        public DateTime DateTime { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
        [ForeignKey("ProductFileID")]
        public virtual ProductFile ProductFile { get; set; }
    }
}