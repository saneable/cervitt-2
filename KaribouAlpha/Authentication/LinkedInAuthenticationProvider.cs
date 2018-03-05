using Owin.Security.Providers.LinkedIn;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KaribouAlpha.Authentication
{
    public class LinkedInAuthenticationProvider : Owin.Security.Providers.LinkedIn.LinkedInAuthenticationProvider
    {
        public override Task Authenticated(LinkedInAuthenticatedContext context)
        {
            context.Identity.AddClaim(new Claim("ExternalAccessToken", context.AccessToken));
            return Task.FromResult<object>(null);
        }
    }
}