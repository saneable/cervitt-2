using KaribouAlpha.Authentication;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using KaribouAlpha.Models;

namespace KaribouAlpha.Controllers
{
    public class LinkedInMVCController : Controller
    {
        private AuthenticationRepository _authenticationRepository = null;
        private LinkedInAuthClient _linkedInAuthClient = null;

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        public LinkedInMVCController()
        {
            this._authenticationRepository = new AuthenticationRepository();
        }
        // GET: LinkedInMVC
        public ActionResult Index()
        {
            _linkedInAuthClient = this._authenticationRepository.GetDbContext().LinkedInAuthClients.Where(_linked => _linked.Active).SingleOrDefault();
            string rootPath = "";
            bool writeLog = false;
            if (System.Configuration.ConfigurationManager.AppSettings["DebugLogFile"] != null)
            {
                if (string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["DebugLogFile"].ToString()) == false)
                {
                    rootPath = System.Configuration.ConfigurationManager.AppSettings["DebugLogFile"].ToString();
                    writeLog = true;
                }
            }

            string file = rootPath + System.DateTime.Now.ToString("yyyyMMddhhmmss") + "LNK_Index.txt";

            if (_linkedInAuthClient != null)
            {
                if (writeLog)
                    System.IO.File.AppendAllText(file, "redirecting to linked login...");

                string state = System.Guid.NewGuid().ToString().Replace("-", "");
                Session["LinkedInState"] = state;
                string clientId = _linkedInAuthClient.ClientId;
                string callBackUrl = string.Format("{0}/LinkedInMVC/AuthCallBack", System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"].ToString());
                string scope = "r_basicprofile%20r_emailaddress";
                string redirectUri = string.Format("https://www.linkedin.com/oauth/v2/authorization?response_type=code&client_id={0}&redirect_uri={1}&state={2}&scope={3}", clientId, callBackUrl, state, scope);
                return Redirect(redirectUri);
            }
            else
            {
                ViewBag.Result = false;
                ViewBag.ErrorMessage = "LinkedIn application not configured.";
                return View("AuthCallBack");
            }
        }


        public async Task<ActionResult> AuthCallBack(string code, string state)
        {
            string rootPath = "";
            bool writeLog = false;
            if (System.Configuration.ConfigurationManager.AppSettings["DebugLogFile"] != null)
            {
                if (string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["DebugLogFile"].ToString()) == false)
                {
                    rootPath = System.Configuration.ConfigurationManager.AppSettings["DebugLogFile"].ToString();
                    writeLog = true;
                }
            }

            string file = rootPath + System.DateTime.Now.ToString("yyyyMMddhhmm") + "LNK_AuthCallBack.txt";

            _linkedInAuthClient = this._authenticationRepository.GetDbContext().LinkedInAuthClients.Where(_linked => _linked.Active).SingleOrDefault();

            if (Session["LinkedInState"] != null)
            {
                if(writeLog)
                System.IO.File.AppendAllText(file, System.DateTime.Now.ToString() + " Start Callback Linked Process...");

                string stateOriginal = Session["LinkedInState"].ToString();
                if (stateOriginal == state)
                {
                    LinkedInExternalAccessToken verifiedAccessToken = await VerifyExternalAccessToken(code, file, writeLog);
                    if (verifiedAccessToken == null)
                    {
                        if(writeLog)
                        System.IO.File.AppendAllText(file, Environment.NewLine + System.DateTime.Now.ToString() + " Linked In  External Access Token not found");

                        return Content("Error in validating response. Please close window and try again.");
                    }
                    else
                    {
                        if (writeLog)
                            System.IO.File.AppendAllText(file, Environment.NewLine + System.DateTime.Now.ToString() + " start to read profile from linkedin...");
                    }

                    if (verifiedAccessToken != null)
                    {
                        LinkedProfile profileInfo = await GetProfileInfo(verifiedAccessToken.access_token, file, writeLog);
                        if (profileInfo != null)
                        {
                            User user = await this._authenticationRepository.FindAsync(new UserLoginInfo("linkedin", profileInfo.id));
                            bool hasRegistered = user != null;
                            if (hasRegistered == false)
                            {
                                if (writeLog)
                                    System.IO.File.AppendAllText(file, System.Environment.NewLine + System.DateTime.Now.ToString() + " local account is NOT FOUND for given linked in provider key...");
                            }
                            else
                            {
                                if (writeLog)
                                    System.IO.File.AppendAllText(file, System.Environment.NewLine + System.DateTime.Now.ToString() + " local account FOUND for given linked in provider key...");
                            }

                            ViewBag.Result = true;
                            ViewBag.ErrorMessage = "";
                            ViewBag.haslocalaccount = hasRegistered.ToString();
                            ViewBag.Id = profileInfo.id;
                            ViewBag.Token = verifiedAccessToken.access_token;
                            return View();
                        }
                    }
                }
                else
                {
                    ViewBag.Result = false;
                    ViewBag.ErrorMessage = "Invalid state for linkedin response. Please close window and try again to login.";
                    return View();
                }
            }
            ViewBag.Result = false;
            ViewBag.ErrorMessage = "Error during validating response. Please close window and try again to login.";
            return View();
        }

        private async Task<LinkedInExternalAccessToken> VerifyExternalAccessToken(string accessToken, string logFile, bool writeLog)
        {
            _linkedInAuthClient = this._authenticationRepository.GetDbContext().LinkedInAuthClients.Where(_linked => _linked.Active).SingleOrDefault();
            LinkedInExternalAccessToken parsedToken = null;
            string file = logFile;
            try
            {
                string verifyTokenEndPoint = "";
                verifyTokenEndPoint = "https://www.linkedin.com/oauth/v2/accessToken";
                string redirectURl = string.Format("{0}/LinkedInMVC/AuthCallBack", System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"].ToString());

                HttpResponseMessage response;
                Uri uri = new Uri(verifyTokenEndPoint);

                if (writeLog)
                    System.IO.File.AppendAllText(file, Environment.NewLine + System.DateTime.Now.ToString() + "| start verify linked access token...");

                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Host = "www.linkedin.com";
                    var content = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("grant_type", "authorization_code"),
                    new KeyValuePair<string, string>("code", accessToken),
                    new KeyValuePair<string, string>("redirect_uri", redirectURl),
                    new KeyValuePair<string, string>("client_id",  _linkedInAuthClient.ClientId),
                    new KeyValuePair<string, string>("client_secret", _linkedInAuthClient.ClientSecret),
                });

                    content.Headers.ContentType.MediaType = "application/x-www-form-urlencoded";
                    response = await httpClient.PostAsync(uri, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    dynamic jObj = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                    parsedToken = new LinkedInExternalAccessToken();
                    parsedToken.access_token = jObj["access_token"];
                    parsedToken.expiry_in = jObj["expires_in"];
                }
            }
            catch (Exception ex)
            {
                if (writeLog)
                    System.IO.File.AppendAllText(file, Environment.NewLine + System.DateTime.Now.ToString() + "| Exception during verify linked access token " + ex.ToString());
            }
            return parsedToken;
        }

        private async Task<LinkedProfile> GetProfileInfo(string accessToken, string logFileName, bool writeLog)
        {
            LinkedProfile profile = null;
            string file = logFileName;
            try
            {

                //Uri uri = new Uri("https://api.linkedin.com/v1/people/~:(id,first-name,last-nam‌​e,picture-url,headli‌​ne,email-address)?format=json");
                Uri uri = new Uri("https://api.linkedin.com/v1/people/~?format=json");

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    var response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        profile = new LinkedProfile();
                        string content = await response.Content.ReadAsStringAsync();
                        if (writeLog)
                        {
                            System.IO.File.AppendAllText(file, Environment.NewLine + System.DateTime.Now.ToString() + "| linked in profile content is :" + content);
                        }
                        profile = Newtonsoft.Json.JsonConvert.DeserializeObject<LinkedProfile>(content);
                    }
                }
            }
            catch (Exception ex)
            {
                if (writeLog)
                    System.IO.File.AppendAllText(file, Environment.NewLine + System.DateTime.Now.ToString() + "| Exception during getting linkedin profile " + ex.ToString());
            }
            return profile;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._authenticationRepository.Dispose();
            }

            base.Dispose(disposing);
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }
            public string ExternalAccessToken { get; set; }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer) || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name),
                    ExternalAccessToken = identity.FindFirstValue("ExternalAccessToken"),
                };
            }
        }
    }
}