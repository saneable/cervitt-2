using KaribouAlpha.Authentication;
using KaribouAlpha.DAL;
using KaribouAlpha.Models;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KaribouAlpha.Controllers
{
    public class FaceBookLoginController : Controller
    {
       
        private AuthenticationRepository _authenticationRepository = null;
        private KaribouAlphaContext db = new KaribouAlphaContext();

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        public FaceBookLoginController()
        {
            this._authenticationRepository = new AuthenticationRepository();
        }

        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("CallBack");
                return uriBuilder.Uri;
            }
        }

        public async Task<ActionResult> CallBack(string code)
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

            if (writeLog)
            {
                System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + " called..fb callback");
                System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + " code is : " + Environment.NewLine + code);
            }

            KaribouAlpha.Models.FaceBookClient client = db.FaceBookClients.SingleOrDefault(_fb => _fb.Active);
            if (client == null)
            {
                ViewBag.EmailError = true;
                return View("CallBack");
            }

            //verifyTokenEndPoint = string.Format("https://graph.facebook.com/debug_token?input_token={0}&access_token={1}", code, appToken);
            //HttpResponseMessage response;
            //Uri uri = new Uri(verifyTokenEndPoint);

            //using (HttpClient httpClient = new HttpClient())
            //{
            //    response = await httpClient.GetAsync(uri);
            //}

            //if (response.IsSuccessStatusCode)
            //{
            //    System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + " graph api status code ok..");
            //    string content = await response.Content.ReadAsStringAsync();
            //    System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() +  content);
            //}
            //else
            //{
            //    System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + " graph api status code failed..");
            //}

            var fb = new Facebook.FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = client.AppId,
                client_secret = client.AppSecret,
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });


            var accessToken = result.access_token;
            if (writeLog)
                System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + accessToken);
            fb.AccessToken = accessToken;
            dynamic me = fb.Get("me?fields=first_name,last_name,id,email");

            ViewBag.EmailError = false;
            if (string.IsNullOrEmpty(me.email) == true)
            {
                if (writeLog)
                    System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + " fb email not found...");

                ViewBag.EmailError = true;
                return View("CallBack");
            }

            var email = me.email;
            string firstname = me.first_name;
            string lastname = me.last_name;
            var id = me.id;
            if (writeLog)
                System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + "found email and names...");

            User user = await this._authenticationRepository.FindAsync(new Microsoft.AspNet.Identity.UserLoginInfo("Facebook", id));
            bool hasRegistered = user != null;

            if (writeLog)
                System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + " has registered.." + hasRegistered.ToString());
            ViewBag.haslocalaccount = hasRegistered.ToString();

            bool hasCervitUser = false;
            long existingCervitUserId = this._authenticationRepository.FindUserExists(email);
            hasCervitUser = (existingCervitUserId > 0);
            if (hasRegistered)
            {
                hasCervitUser = false;
            }

            if (writeLog)
                System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + " has hasCervitUser.." + hasCervitUser.ToString());
            ViewBag.hascervituser = hasCervitUser.ToString();
            ViewBag.provider = "Facebook";
            ViewBag.external_user_name = id;
            ViewBag.external_access_token = accessToken;
            ViewBag.email = email;
            return View("CallBack");
        }

        // GET: FaceBookLogin
        public ActionResult Index()
        {
            KaribouAlpha.Models.FaceBookClient client = db.FaceBookClients.SingleOrDefault(_fb => _fb.Active);
            var url = string.Format("https://www.facebook.com/v2.11/dialog/oauth?client_id={0}&redirect_uri={1}&state={2}&response_type=code&scope=email", client.AppId, client.CallBackUrl , System.Guid.NewGuid().ToString());
            return Redirect(url);
        }
    }
}