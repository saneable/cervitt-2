using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace KaribouAlpha.Models
{
    public class SmtpSetting
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string SmtpHost { get; set; }
        public string SmtpUserName { get; set; }
        public string SmtpPassword { get; set; }
        public string DisplayName { get; set; }

        public string SmtpEmailFrom { get; set; }
        public bool SmtpSSLEnable { get; set; }
        [Required]
        public int SmtpPort { get; set; }

    }
}
