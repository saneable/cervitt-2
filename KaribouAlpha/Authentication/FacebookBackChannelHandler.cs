using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace KaribouAlpha.Authentication
{
    public class FacebookBackChannelHandler : HttpClientHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage result = await base.SendAsync(request, cancellationToken);

            if (request.RequestUri.AbsolutePath.Contains("access_token") == false)
            {
                return result;
            }

            // For the access token we need to now deal with the fact that the response is now in JSON format, not form values. Owin looks for form values.
            String content = await result.Content.ReadAsStringAsync();
            FacebookOAuthResponse facebookOAuthResponse = JsonConvert.DeserializeObject<FacebookOAuthResponse>(content);
            NameValueCollection outgoingQueryString = HttpUtility.ParseQueryString(string.Empty);

            outgoingQueryString.Add(nameof(facebookOAuthResponse.access_token), facebookOAuthResponse.access_token);
            outgoingQueryString.Add(nameof(facebookOAuthResponse.expires_in), facebookOAuthResponse.expires_in + string.Empty);
            outgoingQueryString.Add(nameof(facebookOAuthResponse.token_type), facebookOAuthResponse.token_type);

            String postData = outgoingQueryString.ToString();
            HttpResponseMessage modifiedResult = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(postData)
            };

            return modifiedResult;
        }
    }
}