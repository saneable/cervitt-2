using System;
namespace KaribouAlpha.Models
{
    public class CompanyViewDTO
    {
        public long ID { get; set; }
        public String DisplayName { get; set; }
        public String TradingName { get; set; }
        public String URI { get; set; }
        public String Description { get; set; }
        public String Email { get; set; }
        public String PhoneNumber { get; set; }
        public String WebsiteUrl { get; set; }
        public String Sector { get; set; }
        public String LinkedInUrl { get; set; }
        public String TwitterUrl { get; set; }
        public String FacebookUrl { get; set; }
        public String InstagramUrl { get; set; }
        public String PinterestUrl { get; set; }
        public String TumblrUrl { get; set; }
        public byte[] Logo { get; set; }
        public byte[] Image { get; set; }
        public byte[] HeaderImage { get; set; }
    }
}