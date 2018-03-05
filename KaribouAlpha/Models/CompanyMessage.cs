using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KaribouAlpha.Models
{
    public class CompanyMessage
    {
        public int MessageId { get; set; }
        public int CompanyId { get; set; }
        public string Message { get; set; }
        public string LinkUrl { get; set; }
        public byte[] Image { get; set; }
        public DateTime Date { get; set; }

    }
}