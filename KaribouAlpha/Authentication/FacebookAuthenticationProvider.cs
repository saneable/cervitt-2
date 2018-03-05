using Microsoft.Owin.Security.Facebook;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KaribouAlpha.Authentication
{
    public class FacebookAuthenticationProvider : Microsoft.Owin.Security.Facebook.FacebookAuthenticationProvider
    {
        public override Task Authenticated(FacebookAuthenticatedContext context)
        {
            context.Identity.AddClaim(new Claim("ExternalAccessToken", context.AccessToken));
            context.Identity.AddClaim(new Claim("FacebookAccessToken", context.AccessToken));

            return Task.FromResult<object>(null);
        }
    }
}