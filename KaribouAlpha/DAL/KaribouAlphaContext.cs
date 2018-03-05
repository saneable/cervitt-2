using System.Data.Entity;
using KaribouAlpha.Authentication;
using KaribouAlpha.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace KaribouAlpha.DAL
{
    public class KaribouAlphaContext : IdentityDbContext<User, ApplicationRole, long, UserLogin, UserRole, UserClaim>
    {
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductTeamMember> ProductTeamMembers { get; set; }
        public DbSet<ProductFile> ProductFiles { get; set; }
        public DbSet<CompanyConnection> CompanyConnections { get; set; }
        public DbSet<PersonalConnection> PersonalConnections { get; set; }
        public DbSet<CompanyMember> CompanyMembers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<NewsItem> NewsItems { get; set; }
        public DbSet<ProductUpdate> ProductUpdates { get; set; }
        public DbSet<ProductFileFeedback> Feedback { get; set; }
        public DbSet<Article> Articles { get; set; }        
        public DbSet<NewsItemFeedback> NewsItemFeedback { get; set; }
        public DbSet<CompanyFollowerGroup> CompanyFollowerGroups { get; set; }
        public DbSet<ProductFileDownload> ProductFileDownloads { get; set; }
        public DbSet<ProductView> ProductViews { get; set; }
        public DbSet<ForgotPassword> ForgotPasswords { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<SmtpSetting> SmtpSettings { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<CompanySkill> CompanySkills { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CompanyCountry> CompanyCountries { get; set; }

        public DbSet<UserLevel> UserLevels { get; set; }
        public DbSet<InviteRequest> InviteRequests { get; set; }
        public DbSet<MessageToCompany> MessageToCompanies { get; set; }
        public DbSet<FaceBookClient> FaceBookClients { get; set; }
        public DbSet<GoogleAuthClient> GoogleAuthClients { get; set; }
        public DbSet<LinkedInAuthClient> LinkedInAuthClients { get; set; }

        public KaribouAlphaContext() : base("name=KaribouAlphaContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
