using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KaribouAlpha.Models
{
    public class InviteRequest
    {
        [Key]
        public long Id { get; set; }

        public long InviteFromUserId { get; set; }

        public long InviteFromCompanyId { get; set; }

        public DateTime InviteDate { get; set; }

        public Guid InviteCode { get; set; }

        public string InviteToEmailAddress { get; set; }

        public long? InviteToUserId { get; set; }

        /// <summary>
        /// 1 = register invite , 2 company invite
        /// </summary>
        public Int16 InviteType { get; set; }

        public bool Processed { get; set; }
    }
}