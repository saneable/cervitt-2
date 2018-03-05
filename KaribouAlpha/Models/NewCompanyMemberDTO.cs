using System;
using System.ComponentModel.DataAnnotations;

namespace KaribouAlpha.Models
{
    public class NewCompanyMemberDTO
    {
        [Required]
        public String UserName { get; set; }
        public String Role { get; set; }
    }
}