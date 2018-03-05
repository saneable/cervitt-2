using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KaribouAlpha.Models
{
    public class UserProfileDTO
    {
        public long ID { get; set; }
        public byte[] Image { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String PhoneNumber { get; set; }
        public String Country { get; set; }
        public String Town { get; set; }
        public String CompanyDisplayName { get; set; }
        public String JobTitle { get; set; }
        public bool IsIndividual { get; set; }
    }
}