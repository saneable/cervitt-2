using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KaribouAlpha.Models
{
    public class UserSocialMediaLinksDTO
    {
        public long ID { get; set; }
        public String LinkedInUrl { get; set; }
        public String TwitterUrl { get; set; }
        public String FacebookUrl { get; set; }
        public String InstagramUrl { get; set; }
        public String PinterestUrl { get; set; }
        public String TumblrUrl { get; set; }
    }
}