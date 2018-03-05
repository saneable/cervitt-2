using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KaribouAlpha.Models
{
    public class FaceBookClient
    {
        [Key]
        public Int64  Id { get; set; }

        [Required]
        public string AppId { get; set; }

        [Required]
        public string AppSecret { get; set; }

        [Required]
        public string CallBackUrl { get; set; }

        [Required]
        public bool Active { get; set; }
    }
}