using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KaribouAlpha.Models
{
    public class ExternalUserBinding
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Provider { get; set; }
        [Required]
        public string ExternalAccessToken { get; set; }
    }
}