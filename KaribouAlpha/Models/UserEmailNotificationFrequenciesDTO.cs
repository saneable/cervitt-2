using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KaribouAlpha.Models
{
    public class UserEmailNotificationFrequenciesDTO
    {
        public long ID { get; set; }
        public EmailNotificationFrequency ConnectionRequests { get; set; }
        public EmailNotificationFrequency ConnectionPersonalSuggestions { get; set; }
        public EmailNotificationFrequency ConnectionCompanySuggestions { get; set; }
        public EmailNotificationFrequency ConnectionUpdates { get; set; }
        public EmailNotificationFrequency Feedback { get; set; }
        public EmailNotificationFrequency CompanyPageStats { get; set; }
        public EmailNotificationFrequency CervittNewsAndTips { get; set; }
    }
}