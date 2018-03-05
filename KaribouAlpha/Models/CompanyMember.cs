using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaribouAlpha.Models
{
    public class CompanyMember
    {
        public long ID { get; set; }
        [Required]
        public long UserID { get; set; }
        [Required]
        public long CompanyID { get; set; }
        public String Role { get; set; }
        public String Email { get; set;}

        public long? UserLevelId { get; set; }

        [ForeignKey("UserLevelId")]
        public virtual UserLevel UserLevel { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }
        [ForeignKey("CompanyID")]
        public virtual Company Company { get; set; }
    }
}