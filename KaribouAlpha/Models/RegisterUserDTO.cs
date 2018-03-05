using System;
using System.ComponentModel.DataAnnotations;

namespace KaribouAlpha.Models
{
    public class RegisterUserDTO
    {
        public long NewUserID { get; set; }
        [Required]
        public String Email { get; set; }
        //[Required]
        public String FirstName { get; set; }
        //[Required]
        public String LastName { get; set; }
        [Required]
        public String Password { get; set; }
    }
}