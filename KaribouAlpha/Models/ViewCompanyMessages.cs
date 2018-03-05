using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KaribouAlpha.Models
{
    public class ViewCompanyMessages
    {
        
        public Int64 MessageId { get; set; }
        public Int64 CompanyId { get; set; }
        public Int64? UserId { get; set; }
        public string Message { get; set; }
        public string LinkUrl { get; set; }
        public byte[] Image { get; set; }
        public DateTime Date { get; set; }
        public string UserName {get; set; }
    }
}