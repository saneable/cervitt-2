using System.ComponentModel.DataAnnotations;

namespace KaribouAlpha.Models
{
    public class UserProfileImageEditDTO
    {
        [Required]
        public long ID { get; set; }
        [Required]
        public byte[] Image { get; set; }
    }
}