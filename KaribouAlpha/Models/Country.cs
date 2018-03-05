using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaribouAlpha.Models
{
    public class Country
    {
        [Key]
        public long Id { get; set; }

        [MaxLength(100)]
        [Required(AllowEmptyStrings =false)]
        public string Name { get; set; }
    }
}