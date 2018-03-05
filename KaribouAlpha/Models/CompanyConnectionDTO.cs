using System;
using System.Collections.Generic;

namespace KaribouAlpha.Models
{
    public class CompanyConnectionDTO
    {
        public long CompanyID { get; set; }
        public string CompanyDisplayName { get; set; }
        public DateTime? AcceptedAt { get; set; }
        public ICollection<CompanyConnectionProductDTO> CompanyProducts { get; set; }
    }
}