using KaribouAlpha.Authentication;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaribouAlpha.Models
{
    public enum EmailNotificationFrequency
    {
        Daily = 0,
        Weekly = 1,
        Monthly = 2
    }

    public class User : IdentityUser<long, UserLogin, UserRole, UserClaim>, IUser<long>
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Country { get; set; }
        public String Town { get; set; }
        public String JobTitle { get; set; }
        public String LinkedInUrl { get; set; }
        public String TwitterUrl { get; set; }
        public String FacebookUrl { get; set; }
        public String InstagramUrl { get; set; }
        public String PinterestUrl { get; set; }
        public String TumblrUrl { get; set; }
        public EmailNotificationFrequency ConnectionRequests { get; set; }
        public EmailNotificationFrequency ConnectionPersonalSuggestions { get; set; }
        public EmailNotificationFrequency ConnectionCompanySuggestions { get; set; }
        public EmailNotificationFrequency ConnectionUpdates { get; set; }
        public EmailNotificationFrequency Feedback { get; set; }
        public EmailNotificationFrequency CompanyPageStats { get; set; }
        public EmailNotificationFrequency CervittNewsAndTips { get; set; }
        [Required]
        public DateTime RegistrationDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? CurrentLoginDate { get; set; }
        public DateTime? LastCheckForConnectionRequests { get; set; }
        public DateTime? LastCheckForProductUpdates { get; set; }
        public DateTime? LastCheckForFeedback { get; set; }
        public DateTime? LastCheckForNewsItems { get; set; }
        public byte[] Image { get; set; }
        public Boolean OnboardingSkipped { get; set; }
        public int? OnboardingStep { get; set; }
        public bool? IsIndividual { get; set; }
        public virtual Company Company { get; set; }
        [InverseProperty("Follower")]
        public ICollection<PersonalConnection> Following { get; set; }
        [InverseProperty("Following")]
        public ICollection<PersonalConnection> Followers { get; set; }
        public ICollection<CompanyConnection> CompanyConnections { get; set; }
        public ICollection<ProductTeamMember> ProductTeamMembers { get; set; }
        public ICollection<CompanyMember> CompaniesAsMembers { get; set; }
        public ICollection<Article> Articles { get; set; }
        public ICollection<ProductFileDownload> ProductFileDownloads { get; set; }
        public ICollection<Activity> Activities { get; set; }
    }
}