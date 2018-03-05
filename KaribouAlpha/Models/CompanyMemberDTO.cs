using System;

namespace KaribouAlpha.Models
{
    public class CompanyMemberDTO
    {
        public long ID { get; set; }
        public long UserID { get; set; }
        public String UserUserName { get; set; }
        public String UserFirstName { get; set; }
        public String UserLastName { get; set; }
        public String CompanyDisplayName { get; set; }
        public String Role { get; set; }
        public String Email { get; set; }
        public String UserLevel { get; set; }
        public long UserLevelId { get; set; }
    }
}