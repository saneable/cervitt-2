using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaribouAlpha.Models
{
    public class CompanyCountry
    {
        [Key]
        public long Id { get; set; }

        //[ForeignKey("CompanyId")]
        public long CompanyId { get; set; }

        
        public long CountryId { get; set; }

        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }

        //public virtual Company Company { get; set; }

    }
}