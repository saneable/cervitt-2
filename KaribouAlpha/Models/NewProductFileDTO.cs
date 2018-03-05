using System;
using System.ComponentModel.DataAnnotations;

namespace KaribouAlpha.Models
{
    public class NewProductFileDTO
    {
        [Required]
        public long ProductID { get; set; }
        [Required]
        public String Name { get; set; }
        public String Description { get; set; }
        //[Required]
        //public ProductFileType Type { get; set; }
        [Required]
        public ProductFileCategory Category { get; set; }
        [Required]
        public byte[] Bytes { get; set; }
        public byte[] Image { get; set; }

        public string Visibility { get; set; }

    }
}