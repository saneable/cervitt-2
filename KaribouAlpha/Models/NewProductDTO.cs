using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KaribouAlpha.Models
{
    public class NewProductDTO
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Logo { get; set; }
        public ProductPrivacy Privacy { get; set; }

        public ICollection<CompanyFollowerGroupDTO> GroupsVisibleTo { get; set; }
    }
}