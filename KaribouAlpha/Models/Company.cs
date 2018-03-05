using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaribouAlpha.Models
{
    public class Company
    {
        [Key, ForeignKey("Owner")]
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
        
        public EmailNotificationFrequency ConnectionRequests { get; set; }
        public EmailNotificationFrequency ConnectionPersonalSuggestions { get; set; }
        public EmailNotificationFrequency ConnectionCompanySuggestions { get; set; }
        public EmailNotificationFrequency ConnectionUpdates { get; set; }
        public EmailNotificationFrequency Feedback { get; set; }
        public EmailNotificationFrequency CompanyPageStats { get; set; }
        public EmailNotificationFrequency CervittNewsAndTips { get; set; }
        public byte[] Logo { get; set; }
        public byte[] Image { get; set; }
        public byte[] HeaderImage { get; set; }

        public virtual User Owner { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<CompanyMember> Members { get; set; }
        public ICollection<CompanyConnection> Followers { get; set; }
        public ICollection<CompanyFollowerGroup> FollowerGroups { get; set; }
        public ICollection<CompanyCountry> Countries { get; set; }
    }
}