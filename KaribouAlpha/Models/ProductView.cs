using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaribouAlpha.Models
{
    public class ProductView
    {
        public long ID { get; set; }
        [Required]
        public long ProductID { get; set; }
        [Required]
        public long UserID { get; set; }
        [Required]
        public DateTime DateTime { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
    }
}
 