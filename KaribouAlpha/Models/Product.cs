using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaribouAlpha.Models
{
    public enum ProductPrivacy
    {
        Public = 0,
        Private = 1,
        Invisible = 2,
        VisibleToSelectedGroups = 3
    }

    public class Product
    {
        public long ID { get; set; }
        
        public long? UserId { get; set; }
        public long? CompanyID { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string URI { get; set; }
        public ProductPrivacy Privacy {get; set;}
        public byte[] Logo { get; set; }

        [ForeignKey("CompanyID")]
        public virtual Company Company { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public ICollection<ProductTeamMember> TeamMembers { get; set; }
        public ICollection<ProductFile> ProductFiles { get; set; }
        public ICollection<Customer> Customers { get; set; }
        public ICollection<ProductUpdate> Updates { get; set; }
        public ICollection<CompanyFollowerGroup> GroupsVisibleTo { get; set; }
        public ICollection<ProductView> Views { get; set; }
    }
}