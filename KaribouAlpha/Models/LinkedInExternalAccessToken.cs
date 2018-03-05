using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KaribouAlpha.Models
{
    public class LinkedInExternalAccessToken
    {
        public string user_id { get; set; }
        public string access_token { get; set; }
        public string expiry_in { get; set; }
    }
}