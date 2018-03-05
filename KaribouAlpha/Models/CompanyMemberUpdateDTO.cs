using System;
using System.ComponentModel.DataAnnotations;

namespace KaribouAlpha.Models
{
    public class CompanyMemberUpdateDTO
    {
        [Required]
        public long ID { get; set; }
        public String Role { get; set; }
        public String Email { get; set;}
        public long UserLevelId { get; set; }
    }
}