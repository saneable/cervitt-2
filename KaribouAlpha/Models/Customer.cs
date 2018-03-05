using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaribouAlpha.Models
{
    public class Customer
    {
        public long ID { get; set; }
        [Required]
        public long ProductID { get; set; }
        [Required]
        public byte[] Logo { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
    }
}
 