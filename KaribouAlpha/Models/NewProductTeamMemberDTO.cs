using System.ComponentModel.DataAnnotations;

namespace KaribouAlpha.Models
{
    public class NewProductTeamMemberDTO
    {
        [Required]
        public long ProductID { get; set; }
        [Required]
        public long UserID { get; set; }
    }
}