using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KaribouAlpha.Models
{
    public class Invite
    {
        public string EmailAddress { get; set; }
        public long ToUserId { get; set; }
    }
}