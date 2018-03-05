using System;

namespace KaribouAlpha.Models
{
    public class ProductUpdateDTO
    {
        public long UserID { get; set; }
        public long ProductID { get; set; }
        public long ProductFileID { get; set; }
        public long ProductCompanyID { get; set; }
        public String UserUserName { get; set; }
        public String UserFirstName { get; set; }
        public String UserLastName { get; set; }
        public String UserDisplayName { get; set; }
        public String ProductName { get; set; }
        public String ProductFileName { get; set; }
        public DateTime DateTime { get; set; }
        public UpdateType UpdateType { get; set; }
        public Boolean IsNew { get; set; }
    }
}