using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KaribouAlpha.Models
{
    public class EntityUserLevel
    {
        public long EntityUserLevelId { get; set; }
        public string UserLevel { get; set; }
        public long EntityId { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsViewer { get; set; }
        public bool IsEditor { get; set; }

    }
}