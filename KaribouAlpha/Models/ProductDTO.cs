using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KaribouAlpha.Models
{
    public class ProductDTO
    {
        [Required]
        public long ID { get; set; }
        public long? CompanyID { get; set; }
        public long? UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string URI { get; set; }
        public byte[] Logo { get; set; }
        public ProductPrivacy Privacy { get; set; }

        public ICollection<CompanyFollowerGroupDTO> GroupsVisibleTo { get; set; }
    }
}