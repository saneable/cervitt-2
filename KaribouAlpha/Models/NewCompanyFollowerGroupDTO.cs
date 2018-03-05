using System;
using System.ComponentModel.DataAnnotations;

namespace KaribouAlpha.Models
{
    public class NewCompanyFollowerGroupDTO
    {
        [Required]
        public String Name { get; set; }
    }
}