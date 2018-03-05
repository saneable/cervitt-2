using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KaribouAlpha.Models
{
    public class ProductFileDTO
    {
        public long ID { get; set; }
        [Required]
        public String Name { get; set; }
        public String Description { get; set; }
        [Required]
        public ProductFileType Type { get; set; }
        [Required]
        public ProductFileCategory Category { get; set; }
        [Required]
        public byte[] Bytes { get; set; }
        public ProductFilePrivacy Privacy { get; set; }
        public DateTime UploadedAt { get; set; }

        public ICollection<CompanyFollowerGroupDTO> GroupsVisibleTo { get; set; }
    }
}