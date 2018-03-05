using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KaribouAlpha.Models
{
    public class SkillDTO
    {
        public string SkillName { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDate { get; set; }
    }
}