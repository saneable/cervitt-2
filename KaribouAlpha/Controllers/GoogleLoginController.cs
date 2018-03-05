using KaribouAlpha.Authentication;
using KaribouAlpha.DAL;
using KaribouAlpha.Models;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KaribouAlpha.Controllers
{
    public class GoogleLoginController : Controller
    {
        private AuthenticationRepository _authenticationRepository = null;
        private KaribouAlphaContext db = new KaribouAlphaContext();

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        public GoogleLoginController()
        {
            this._authenticationRepository = new AuthenticationRepository();
        }
                
        public ActionResult Index()
        {
            KaribouAlpha.DAL.KaribouAlphaContext db = new DAL.KaribouAlphaContext();
            GoogleAuthClient googleClient = db.GoogleAuthClients.SingleOrDefault(_google => _google.Active);
            if(googleClient != null)
            {
                string loginUrl = "https://accounts.google.com/o/oauth2/auth?";
                string scope = "email%20profile";
                StringBuilder urlBuilder = new StringBuilder(loginUrl);
                urlBuilder.Append("client_id=" + googleClient.ClientId);
                urlBuilder.Append("&redirect_uri=" + googleClient.RedirectUrl);
                urlBuilder.Append("&response_type=" + "code");
                urlBuilder.Append("&scope=" + scope);
                urlBuilder.Append("&access_type=" + "offline");
                urlBuilder.Append("&state=" + System.Guid.NewGuid().ToString());
                return Redirect(urlBuilder.ToString());
            }
            ViewBag.EmailError = true;
            return View("CallBack");
        }

        public async Task<ActionResult> CallBack(string state, string code)
        {
            ViewBag.EmailError = false;

            string path = "";
            bool writeLog = false;
            if (System.Configuration.ConfigurationManager.AppSettings["DebugLogFile"] != null)
            {
                if (string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["DebugLogFile"].ToString()) == false)
                {
                    path = System.Configuration.ConfigurationManager.AppSettings["DebugLogFile"].ToString();
                    writeLog = true;
                }
            }

            KaribouAlpha.DAL.KaribouAlphaContext db = new DAL.KaribouAlphaContext();
            GoogleAuthClient googleClient = db.GoogleAuthClients.SingleOrDefault(_google => _google.Active);
            if (googleClient == null || (!string.IsNullOrEmpty(code) && code.ToLower() == "error"))
            {
                if (writeLog && googleClient == null)
                    System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + " google client is not setup..");
                else  if (writeLog)
                    System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + "user may be declined code is error...");

                ViewBag.EmailError = true;
                return View();
            }
            try
            {
                if (writeLog)
                    System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + "- google - start getting token from code....");

                System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("https://accounts.google.com/o/oauth2/token");
                webRequest.Method = "POST";
                string parameters = "code=" + code + "&client_id=" + googleClient.ClientId + "&client_secret=" + googleClient.ClientSecret + "&redirect_uri=" + googleClient.RedirectUrl + "&grant_type=authorization_code";
                byte[] byteArray = Encoding.UTF8.GetBytes(parameters);
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = byteArray.Length;
                Stream postStream = webRequest.GetRequestStream();
                postStream.Write(byteArray, 0, byteArray.Length);
                postStream.Close();

                WebResponse response = webRequest.GetResponse();
                postStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(postStream);
                string responseFromServer = reader.ReadToEnd();

                GooglePlusAccessToken responseToken = JsonConvert.DeserializeObject<GooglePlusAccessToken>(responseFromServer);

                if (responseToken != null)
                {
                    string accessToken = string.Empty;
                    accessToken = responseToken.access_token;
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        Task<GoogleUserOutputData> data = GetGoogleUserInfo(accessToken);
                        if (data != null && data.Result != null)
                        {
                            
                            User user = await this._authenticationRepository.FindAsync(new Microsoft.AspNet.Identity.UserLoginInfo("Google", data.Result.id));
                            bool hasRegistered = user != null;
                            bool hasCervitUser = false;
                            long existingCervitUserId =  this._authenticationRepository.FindUserExists(data.Result.email);
                            hasCervitUser = (existingCervitUserId > 0);
                            if(hasRegistered)
                            {
                                hasCervitUser = false;
                            }
                            ViewBag.hascervituser = hasCervitUser.ToString();
                            ViewBag.haslocalaccount = hasRegistered.ToString();
                            ViewBag.provider = "Google";
                            ViewBag.external_user_name = data.Result.id;
                            ViewBag.external_access_token = accessToken;
                            ViewBag.email = data.Result.email;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                if (writeLog)
                    System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + "- google - error during getting token from code...." + ex.ToString());
                ViewBag.EmailError = true;
            }
            return View();
        }


        private async Task<GoogleUserOutputData> GetGoogleUserInfo(string access_token)
        {
            string path = "";
            bool writeLog = false;
            if (System.Configuration.ConfigurationManager.AppSettings["DebugLogFile"] != null)
            {
                if (string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["DebugLogFile"].ToString()) == false)
                {
                    path = System.Configuration.ConfigurationManager.AppSettings["DebugLogFile"].ToString();
                    writeLog = true;
                }
            }

            try
            {
                if (writeLog)
                    System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + " start in GetGoogleUserInfo..");

                var urlProfile = "https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token=" + access_token;
                WebClient webClient = new WebClient();
                string response = webClient.DownloadString(urlProfile);
                if (writeLog)
                    System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + " GetGoogleUserInfo.." + response);
                if(!string.IsNullOrEmpty(response))
                {
                    GoogleUserOutputData userData = JsonConvert.DeserializeObject<GoogleUserOutputData>(response);
                    return userData;
                }
                return null;
            }
            catch (Exception ex)
            {
                if (writeLog)
                    System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + " Error in GetGoogleUserInfo.." + ex.ToString());
                return null;
            }
        }

    }
}