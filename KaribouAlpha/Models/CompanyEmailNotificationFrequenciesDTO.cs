namespace KaribouAlpha.Models
{
    public class CompanyEmailNotificationFrequenciesDTO
    {
        public long OwnerID { get; set; }
        public EmailNotificationFrequency ConnectionRequests { get; set; }
        public EmailNotificationFrequency ConnectionPersonalSuggestions { get; set; }
        public EmailNotificationFrequency ConnectionCompanySuggestions { get; set; }
        public EmailNotificationFrequency ConnectionUpdates { get; set; }
        public EmailNotificationFrequency Feedback { get; set; }
        public EmailNotificationFrequency CompanyPageStats { get; set; }
        public EmailNotificationFrequency CervittNewsAndTips { get; set; }
    }
}