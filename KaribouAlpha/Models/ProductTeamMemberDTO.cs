using System;
using System.ComponentModel.DataAnnotations;

namespace KaribouAlpha.Models
{
    public class ProductTeamMemberDTO
    {
        public long ID { get; set; }
        [Required]
        public long ProductID { get; set; }
        [Required]
        public long UserID { get; set; }
        public String Role { get; set; }
        public Boolean CanEditTheProduct { get; set; }

        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserUserName { get; set; }
    }
}