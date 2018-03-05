using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaribouAlpha.Models
{
    public class UserLevel
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public bool Active { get; set; }

        public bool ShowProductEditOption { get; set; }
    }
}