using System;

namespace KaribouAlpha.Models
{
    public abstract class ArticleDTO
    {
        public long ID { get; set; }
        public long UserID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string LinkUrl { get; set; }
        public String UserUserName { get; set; }
        public String UserFirstName { get; set; }
        public String UserLastName { get; set; }
        public String UserDisplayName { get; set; }
        public String UserCompanyDisplayName { get; set; }
        public DateTime PostedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Boolean HasImage { get; set; }
        public Boolean IsNew { get; set; }
    }
}