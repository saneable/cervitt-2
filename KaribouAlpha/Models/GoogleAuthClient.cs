using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace KaribouAlpha.Models
{
    public class GoogleAuthClient
    {
        [Key]
        public Int64 Id { get; set; }

        [Required]
        public string ClientId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        [Required]
        public string RedirectUrl { get; set; }

        public bool Active { get; set; }
    }
}