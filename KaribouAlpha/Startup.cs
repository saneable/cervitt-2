using System;
using AutoMapper;
using KaribouAlpha.Authentication;
using KaribouAlpha.Models;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Owin.Security.Providers.LinkedIn;
using Microsoft.Owin.Security.Google;
using KaribouAlpha.Providers;
using Microsoft.Owin.Security.Facebook;
using System.Linq;
using System.Linq.Expressions;

[assembly: OwinStartup(typeof(KaribouAlpha.Startup))]

namespace KaribouAlpha
{
    public class Startup
    {
        internal static IDataProtectionProvider DataProtectionProvider { get; private set; }
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public static LinkedInAuthenticationOptions LinkedInAuthenticationOptions { get; private set; }
        public static GoogleOAuth2AuthenticationOptions googleAuthOptions { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            this.ConfigureOAuth(app);
            this.ConfigureAutoMapper();
            DataProtectionProvider = app.GetDataProtectionProvider();
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);

            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(24),
                Provider = new SimpleAuthorizationServerProvider(),
                RefreshTokenProvider = new SimpleRefreshTokenProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);

            KaribouAlpha.DAL.KaribouAlphaContext db = new DAL.KaribouAlphaContext();

            LinkedInAuthClient linkedInAuthClient = db.LinkedInAuthClients.SingleOrDefault(_linked => _linked.Active);
            if (linkedInAuthClient != null)
            {
                ILinkedInAuthenticationProvider providerLnk = new KaribouAlpha.Authentication.LinkedInAuthenticationProvider();
                LinkedInAuthenticationOptions = new LinkedInAuthenticationOptions()
                {
                    ClientId = linkedInAuthClient.ClientId,
                    ClientSecret = linkedInAuthClient.ClientSecret,
                    Provider = providerLnk,
                    CallbackPath = new PathString("/AuthCallBack."),
                    Scope = { "r_basicprofile", "r_emailaddress" },
                    //BackchannelHttpHandler = new LinkedInBackChannelHandler()
                };
            }
            //http://www.c-sharpcorner.com/article/implementing-oauth2-0-authorization-for-google-in-asp-net/
            //https://developers.google.com/actions/identity/oauth2-code-flow
            
            GoogleAuthClient googleClient = db.GoogleAuthClients.SingleOrDefault(_google => _google.Active);
            if (googleClient != null)
            {
                GoogleAuthProvider gProvider = new GoogleAuthProvider();
                googleAuthOptions = new GoogleOAuth2AuthenticationOptions()
                {
                    ClientId = googleClient.ClientId,
                    ClientSecret = googleClient.ClientSecret,
                    Provider = gProvider
                };
            }
            
            KaribouAlpha.Models.FaceBookClient clientFb = db.FaceBookClients.SingleOrDefault(_fb => _fb.Active);
            if (clientFb != null)
            {
                var fbProvider = new FacebookAuthProvider();
                var facebookAuthOptions = new FacebookAuthenticationOptions()
                {
                    AppId = clientFb.AppId,
                    AppSecret = clientFb.AppSecret,
                    Provider = fbProvider,
                };
                app.UseFacebookAuthentication(facebookAuthOptions);
            }

        }

        private void ConfigureAutoMapper()
        {
            Mapper.Initialize(configuration =>
            {
                configuration.CreateMap<User, UserProfileDTO>()
                    .ForMember("IsIndividual", m => m.MapFrom( a=>a.IsIndividual.HasValue ? a.IsIndividual.Value : false));
                
            configuration.CreateMap<UserProfileDTO, User>();
            configuration.CreateMap<User, UserSocialMediaLinksDTO>();
            configuration.CreateMap<UserSocialMediaLinksDTO, User>();
            configuration.CreateMap<User, UserEmailNotificationFrequenciesDTO>();
            configuration.CreateMap<UserEmailNotificationFrequenciesDTO, User>();
            configuration.CreateMap<User, UserProfileImageEditDTO>();
            configuration.CreateMap<UserProfileImageEditDTO, User>();
            configuration.CreateMap<User, UserFromSearchDTO>();
            configuration.CreateMap<User, UserOnboardingStatusDTO>();

           configuration.CreateMap<Company, CompanyProfileDTO>();

           configuration.CreateMap<CompanyProfileDTO, Company>().ForMember("Countries", opt => opt.Ignore());


            configuration.CreateMap<Company, CompanySocialMediaLinksDTO>();
            configuration.CreateMap<CompanySocialMediaLinksDTO, Company>();
            configuration.CreateMap<Company, CompanyEmailNotificationFrequenciesDTO>();
            configuration.CreateMap<CompanyEmailNotificationFrequenciesDTO, Company>();
            configuration.CreateMap<Company, CompanyViewDTO>();

                //configuration.CreateMap<Company, CompanySearchDTO>()
                //.ForMember(d => d.ProductCount, opt => opt.MapFrom(src => src.Products.Count));

                configuration.CreateMap<Company, CompanySearchDTO>().ForMember(X=>X.ProductCount , m => m.MapFrom(a => a.Products.Count));

                configuration.CreateMap<CompanyConnection, CompanyConnectionDTO>();
                configuration.CreateMap<CompanyConnection, CompanyFollowerDTO>();
                configuration.CreateMap<PersonalConnection, PersonalConnectionDTO>(); 
                configuration.CreateMap<Product, ProductDTO>();
                configuration.CreateMap<ProductDTO, Product>();

                configuration.CreateMap<Product, ProductSummaryDTO>();


                configuration.CreateMap<Product, ProductViewDTO>();
                configuration.CreateMap<NewProductDTO, Product>();
                configuration.CreateMap<Product, CompanyConnectionProductDTO>();
                configuration.CreateMap<Product, ProductSearchDTO>();
                configuration.CreateMap<ProductTeamMember, ProductTeamMemberDTO>();
                configuration.CreateMap<ProductFile, ProductFileDTO>();
                configuration.CreateMap<NewProductFileDTO, ProductFile>();
                configuration.CreateMap<ProductFile, ProductFileViewDTO>();
                configuration.CreateMap<CompanyMember, CompanyMemberDTO>().ForMember(src => src.UserLevel, opt => opt.MapFrom(src => src.UserLevelId.HasValue ?  src.UserLevel.Name : ""));
                configuration.CreateMap<Customer, CustomerDTO>();
                configuration.CreateMap<CustomerDTO, Customer>();
                configuration.CreateMap<NewCustomerDTO, Customer>();
                configuration.CreateMap<Customer, CustomerViewDTO>();
                configuration.CreateMap<Customer, CustomerNoLogoDTO>();
                configuration.CreateMap<Article, ArticleDTO>()
                    .ForMember(dest => dest.HasImage, opt => opt.MapFrom(src => src.Image.Length > 0));
                configuration.CreateMap<NewsItem, NewsItemDTO>()
                    .IncludeBase<Article, ArticleDTO>();
                configuration.CreateMap<NewNewsItemDTO, NewsItem>();
                configuration.CreateMap<ProductUpdate, ProductUpdateDTO>();
                configuration.CreateMap<ProductFileFeedback, FeedbackDTO>()
                    .IncludeBase<Article, ArticleDTO>();
                configuration.CreateMap<NewFeedbackDTO, ProductFileFeedback>();
                configuration.CreateMap<NewsItemFeedback, NewsItemFeedbackDTO>()
                    .IncludeBase<Article, ArticleDTO>();
                configuration.CreateMap<NewNewsItemFeedbackDTO, NewsItemFeedback>();
                configuration.CreateMap<CompanyFollowerGroup, CompanyFollowerGroupDTO>();
                configuration.CreateMap<CompanyFollowerGroupDTO, CompanyFollowerGroup>();
                configuration.CreateMap<CompanyFollowerGroup, CompanyFollowerGroupSummaryDTO>();

                configuration.CreateMap<Sector, SectorDTO>();
                configuration.CreateMap<SectorDTO, Sector>();
                configuration.CreateMap<Skill, SkillDTO>();
                configuration.CreateMap<SkillDTO, Skill>();


            });
        }
    }
}
