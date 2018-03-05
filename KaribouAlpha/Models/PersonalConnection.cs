using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaribouAlpha.Models
{
    public class PersonalConnection
    {
        public long ID { get; set; }
        [Required]
        public long FollowingID { get; set; }
        [Required]
        public long FollowerID { get; set; }
        [Required]
        public ConnectionStatus Status { get; set; }

        [ForeignKey("FollowingID")]
        public virtual User Following { get; set; }
        [ForeignKey("FollowerID")]
        public virtual User Follower { get; set; }
    }
}