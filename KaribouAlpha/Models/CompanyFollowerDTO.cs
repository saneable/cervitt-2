using System;
using System.Collections.Generic;

namespace KaribouAlpha.Models
{
    public class CompanyFollowerDTO
    {
        public long ID { get; set; }
        public long UserID { get; set; }
        public string UserUserName { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserJobTitle { get; set; }
        public string UserCompanyDisplayName { get; set; }
        public Boolean IsNew { get; set; }
        public ConnectionStatus Status { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime? AcceptedAt { get; set; }
        public ICollection<CompanyFollowerGroupDTO> FollowerGroups { get; set; }
    }
}