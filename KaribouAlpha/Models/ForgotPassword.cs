using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KaribouAlpha.Models
{
    public class ForgotPassword
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string OTP { get; set; }
    }
}