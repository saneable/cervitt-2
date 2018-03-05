using System;

namespace KaribouAlpha.Models
{
    public class CompanyProfileDTO
    {
        public long ID { get; set; }
        public long OwnerID { get; set; }
        public String DisplayName { get; set; }
        public String TradingName { get; set; }
        public String URI { get; set; }
        public byte[] Image { get; set; }
        public String Description { get; set; }
        public String Email { get; set; }
        public String PhoneNumber { get; set; }
        public String WebsiteUrl { get; set; }
        public String Sector { get; set; }
        
        public byte[] HeaderImage { get; set; }
        public string[] Skills { get; set; }
        public string[] Countries { get; set; }
    }
}