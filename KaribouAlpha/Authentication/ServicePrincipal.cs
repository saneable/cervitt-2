using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Configuration;
using System.Threading.Tasks;

namespace KaribouAlpha.Authentication
{
    public static class ServicePrincipal
    {
        static string authority = ConfigurationManager.AppSettings["ida:Authority"];
        static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        static string clientSecret = ConfigurationManager.AppSettings["ida:ClientSecret"];
        static string resource = ConfigurationManager.AppSettings["ida:Resource"];

        public static async Task<AuthenticationResult> GetS2SAccessTokenForProdMSA()
        {
            return await GetS2SAccessToken(authority, resource, clientId, clientSecret);
        }

        static async Task<AuthenticationResult> GetS2SAccessToken(string authority, string resource, string clientId, string clientSecret)
        {
            ClientCredential clientCredential = new ClientCredential(clientId, clientSecret);
            AuthenticationContext context = new AuthenticationContext(authority, false);
            AuthenticationResult authenticationResult = await context.AcquireTokenAsync(resource, clientCredential);

            return authenticationResult;
        }
    }
}