using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaribouAlpha.Models
{
    public enum ConnectionStatus
    {
        Pending = 0,
        Accepted = 1
    }

    public class CompanyConnection
    {
        public long ID { get; set; }
        [Required]
        public long UserID { get; set; }
        [Required]
        public long CompanyID { get; set; }
        [Required]
        public ConnectionStatus Status { get; set; }
        [Required]
        public DateTime RequestedAt { get; set; }
        public DateTime? AcceptedAt { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }
        [ForeignKey("CompanyID")]
        public virtual Company Company { get; set; }
        public ICollection<CompanyFollowerGroup> FollowerGroups { get; set; }
    }
}