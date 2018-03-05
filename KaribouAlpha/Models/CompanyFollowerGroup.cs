using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaribouAlpha.Models
{
    public class CompanyFollowerGroup
    {
        [Key]
        public long ID { get; set; }

        [Required]
        public long CompanyID { get; set; }

        [Required]
        public String Name { get; set; }
        
        [ForeignKey("CompanyID")]
        public virtual Company Company { get; set; }
        public ICollection<CompanyConnection> Followers { get; set; }
        public ICollection<Product> VisibleProducts { get; set; }
        public ICollection<ProductFile> VisibleProductFiles { get; set; }
    }
}