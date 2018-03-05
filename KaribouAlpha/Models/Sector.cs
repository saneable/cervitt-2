using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaribouAlpha.Models
{
    public class Sector
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string SectorName { get; set; }
        [Required]
        public bool Active { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
    }
}