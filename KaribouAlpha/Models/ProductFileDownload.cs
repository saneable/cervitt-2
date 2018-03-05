using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaribouAlpha.Models
{
    public class ProductFileDownload
    {
        public long ID { get; set; }
        [Required]
        public long ProductFileID { get; set; }
        [Required]
        public long UserID { get; set; }
        [Required]
        public DateTime DateTime { get; set; }

        [ForeignKey("ProductFileID")]
        public virtual ProductFile ProductFile { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
    }
}
 