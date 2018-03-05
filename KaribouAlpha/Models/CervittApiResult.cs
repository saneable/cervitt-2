using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KaribouAlpha.Models
{
    public class CervittApiResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

        public string SuccessMessage { get; set; }
    }
}