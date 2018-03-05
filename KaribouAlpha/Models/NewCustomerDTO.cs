using System.ComponentModel.DataAnnotations;

namespace KaribouAlpha.Models
{
    public class NewCustomerDTO
    {
        [Required]
        public long ProductID { get; set; }
        [Required]
        public byte[] Logo { get; set; }
    }
}
 