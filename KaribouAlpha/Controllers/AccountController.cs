using KaribouAlpha.Authentication;
using KaribouAlpha.Models;
using Facebook;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Linq;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using KaribouAlpha;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using KaribouAlpha.BLL;
using System.Data.Entity;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CarlitoAlpha.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private const String SENDGRID_API_KEY = @"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";
        private AuthenticationRepository _authenticationRepository = null;
        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IInviteRequestService inviteRequestService = null;

        public AccountController()
        {
            this._authenticationRepository = new AuthenticationRepository();
            this.inviteRequestService = new InviteRequestService();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._authenticationRepository.Dispose();
            }

            base.Dispose(disposing);
        }

        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {
            string redirectUri = string.Empty;

            if (error != null)
            {
                return BadRequest(Uri.EscapeDataString(error));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }

            string redirectUriValidationResult = ValidateClientAndRedirectUri(this.Request, ref redirectUri);

            if (!string.IsNullOrWhiteSpace(redirectUriValidationResult))
            {
                return BadRequest(redirectUriValidationResult);
            }

            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return InternalServerError();
            }

            if (externalLogin.LoginProvider != provider)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }

            User user = await this._authenticationRepository.FindAsync(new UserLoginInfo(externalLogin.LoginProvider, externalLogin.ProviderKey));
            bool hasRegistered = user != null;

            redirectUri = string.Format("{0}#external_access_token={1}&provider={2}&haslocalaccount={3}&external_user_name={4}",
                                            redirectUri,
                                            externalLogin.ExternalAccessToken,
                                            externalLogin.LoginProvider,
                                            hasRegistered.ToString(),
                                            externalLogin.UserName);

            return Redirect(redirectUri);
        }

        [AllowAnonymous]
        [Route("forgotPassword")]
        [HttpPost]
        public async Task<IHttpActionResult> ForgotPassword([FromBody] string email)
        {

            try
            {
                var result = this._authenticationRepository.FindUserExists(email);

                //here need to check if user verified or not?
                if (result == 0)
                {
                    return Json(HttpStatusCode.NotFound);
                }

                User user = this._authenticationRepository.FindUserByUserName(email);
                if (user != null && user.EmailConfirmed == false)
                {
                    return Json(HttpStatusCode.NotFound);
                }

                int _min = 1000;
                int _max = 9999;
                Random _rdm = new Random();
                var OTP = _rdm.Next(_min, _max);
                string returnString = "";
                ForgotPassword model = new ForgotPassword();
                model.Email = email;
                model.OTP = OTP.ToString();


                var result1 = this._authenticationRepository.ForgotPassword(model);

                if (result1 > 0)
                {
                    //code otp saved in db  .ge
                    // SmtpSetting smtpSetting = this._authenticationRepository.GetDbContext().SmtpSettings.FirstOrDefault();
                    if (true)
                    {
                        string emailContent = ""; //we need to get this email content from client

                        string passwordResetToken = await this._authenticationRepository.GeneratePasswordResetTokenAsync(result);

                        emailContent = "your Password reset code: " + OTP;

                        //emailContent += string.Format("<br>Password reset Url : <a target='_blank' href='{0}/reset_password?token={1}'>Click here to reset password</a>", this.Request.RequestUri.Host, passwordResetToken);
                        string webSiteUrl = System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"];

                        emailContent += string.Format("<br><b>Enter above reset code by clicking  </b> : <a target='_blank' href='{0}/VarificationCode'>Verify Code Url</a> Or <br></br> Using Verify code link {1}.", webSiteUrl, webSiteUrl + "/VarificationCode");

                        //string error = string.Empty;
                        //EmailService emailService = new EmailService();
                        //emailService.SendEmail(smtpSetting, "Password reset code for Cervitt", emailContent, email, "", "", out error);


                        string apiKey = SENDGRID_API_KEY;
                        SendGridClient sendGridClient = new SendGridClient(apiKey, "https://api.sendgrid.com");
                        EmailAddress emailSender = new EmailAddress("noreply@cervitt.co.uk", "Cervitt");
                        String subject = "Password reset code for Cervitt account.";
                        EmailAddress emailRecipient = new EmailAddress(email);
                        Content content = new Content("text/html", emailContent);
                        SendGridMessage mail = MailHelper.CreateSingleEmail(emailSender, emailRecipient, subject, "", emailContent);

                        //mail.TemplateId = "7f2e9773-771d-47e2-b254-c0ea601da608";
                        //mail.AddSubstitution("<%userId%>", registerUserDTO.NewUserID.ToString());
                        //mail.AddSubstitution("<%confirmationToken%>", HttpServerUtility.UrlTokenEncode(emailConfirmationTokenBytes));

                        dynamic response = sendGridClient.SendEmailAsync(mail);
                    }


                }

                return Ok();
            }
            catch (Exception ex)
            {
                return Json(HttpStatusCode.NotFound);
            }


        }

        [AllowAnonymous]
        [Route("CheckCode")]
        [HttpPost]
        public async Task<IHttpActionResult> CheckCode([FromBody] string Code)
        {


            try
            {
                var EmailId = this._authenticationRepository.CheckCode(Code);

                if (EmailId == null)
                {
                    return this.Content(HttpStatusCode.NotFound, new { response = "Email Id not found" });
                    //return Json(HttpStatusCode.NotFound);
                }

                var id = this._authenticationRepository.FindUserExists(EmailId);
                if (id == 0)
                {

                    return this.Content(HttpStatusCode.NotFound, new { response = "User Id not found" });
                }

                string passwordResetToken = await this._authenticationRepository.GeneratePasswordResetTokenAsync(id);

                ResetPasswordTokenDTO obj = new ResetPasswordTokenDTO() { Id = id, Token = passwordResetToken };

                return Ok(obj);
            }
            catch (Exception ex)
            {
                return Json(HttpStatusCode.NotFound);
            }



        }

        [AllowAnonymous]
        [Route("register")]
        [HttpPost]
        public async Task<IHttpActionResult> Register(RegisterUserDTO registerUserDTO)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await this._authenticationRepository.RegisterNewUser(registerUserDTO);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }


            //if (errorResult != null)
            //    {
            //    List<string> errors = new List<string>();
            //    errors.Add("Email address is already exists. Please Click on forgot password if you forgot your password");
            //    return Json(errors);

            //      }

            string emailConfirmationToken = await this._authenticationRepository.GenerateEmailConfirmationToken(registerUserDTO.NewUserID);
            byte[] emailConfirmationTokenBytes = Encoding.UTF8.GetBytes(emailConfirmationToken);
            string apiKey = SENDGRID_API_KEY;
            SendGridClient sendGridClient = new SendGridClient(apiKey, "https://api.sendgrid.com");
            EmailAddress emailSender = new EmailAddress("noreply@cervitt.co.uk", "Cervitt");
            String subject = "Welcome to Cervitt. Activate your account.";
            EmailAddress emailRecipient = new EmailAddress(registerUserDTO.Email, "User");


            string webSiteUrl = System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"];

            string url = string.Format("{0}/api/account/ConfirmEmail?userId={1}&confirmationToken={2}", webSiteUrl, registerUserDTO.NewUserID.ToString(), HttpServerUtility.UrlTokenEncode(emailConfirmationTokenBytes));

            string msgBody = string.Format("Please click here to confirm your email.<br><a href='{0}' target='_blank'>Confirm Email Address</a><br>Thank you, Cervitt", url);

            Content content = new Content("text/html", msgBody);
            SendGridMessage mail = MailHelper.CreateSingleEmail(emailSender, emailRecipient, subject, "", msgBody);

            //mail.TemplateId = "7f2e9773-771d-47e2-b254-c0ea601da608";
            //mail.AddSubstitution("<%userId%>", "");
            //mail.AddSubstitution("<%confirmationToken%>", "");



            dynamic response = sendGridClient.SendEmailAsync(mail);

            return Ok();
        }

        [AllowAnonymous]
        [Route("RegisterExternal")]
        public async Task<IHttpActionResult> RegisterExternal(ExternalUserBinding model)
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
                System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + " called..RegisterExternal...");

            if (ModelState.IsValid == false)
            {
                if (writeLog)
                    System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + " modelstate is invalid...");
                return BadRequest(ModelState);
            }

            if (model.Provider.ToLower() == "google")
            {
                ParsedExternalAccessToken googleVerifiedAccessToken = await VerifyGoogleExternalAccessToken(model.ExternalAccessToken);
                if (googleVerifiedAccessToken == null)
                {
                    if (writeLog)
                        System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + " google provider or external access token is not valid...");
                    return BadRequest("Invalid Provider or External Access Token");
                }
            }

            var providerId = string.Empty;
            string email = string.Empty;
            string firstName = string.Empty;
            string lastName = string.Empty;

            if (model.Provider.ToLower() == "facebook")
            {
                if (writeLog)
                    System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + " provider is facebook checking to get client info...");
                FacebookClient facebookClient = new FacebookClient(model.ExternalAccessToken);
                dynamic facebookUserInfo = facebookClient.Get("/me?fields=email,first_name,last_name,id");

                if (String.IsNullOrEmpty(facebookUserInfo.email) == true)
                {
                    if (writeLog)
                        System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + " email is not configured in fb so not proceed...");
                    return BadRequest("The user has not configured an email address in Facebook.");
                }
                providerId = facebookUserInfo.id;
                email = facebookUserInfo.email;
                firstName = String.IsNullOrEmpty(facebookUserInfo.first_name) ? null : facebookUserInfo.first_name;
                lastName = String.IsNullOrEmpty(facebookUserInfo.last_name) ? null : facebookUserInfo.last_name;
            }

            if (model.Provider.ToLower() == "google")
            {
                GoogleUserOutputData userData = await GetGoogleUserInfo(model.ExternalAccessToken);
                if (userData != null)
                {
                    providerId = userData.id;
                    email = userData.email;
                    firstName = userData.given_name;
                    lastName = userData.family_name;
                }
            }

            if(model.Provider.ToLower() == "linkedin")
            {
                LinkedProfile profileInfo = await GetProfileInfo(model.ExternalAccessToken, path, writeLog);
                if (profileInfo != null)
                {
                    providerId = profileInfo.id;
                    firstName = profileInfo.firstName;
                    lastName = profileInfo.lastName;
                    email = model.UserName;
                }
            }

            if (writeLog)
                System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + " provider id is.." + providerId + " - " + model.Provider);

            User user = await this._authenticationRepository.FindAsync(new UserLoginInfo(model.Provider, providerId));
            bool hasRegistered = user != null;
            JObject accessTokenResponse = null;
            if (hasRegistered == true)
            {
                if (writeLog)
                    System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + " has registered already...generate local access token...");
                accessTokenResponse = this.GenerateLocalAccessTokenResponse(user);
                return Ok(accessTokenResponse);
            }

            IdentityResult result;
            ExternalLoginInfo externalLoginInfo = new ExternalLoginInfo()
            {
                DefaultUserName = email,
                Login = new UserLoginInfo(model.Provider, providerId)
            };

            if (writeLog)
                System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + " find by email... " + email + " - " + model.Provider);

            user = this._authenticationRepository.FindUserByUserName(email);

            bool hasRegisteredLocally = user != null;

            if (hasRegisteredLocally == true)
            {
                if (writeLog)
                    System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + " registered locaslly so... adding login ..." + email + "-" + model.Provider);
                if(model.Provider.ToLower() == "linkedin")
                {
                    if (writeLog)
                        System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + " email already exist error..." + email + "-" + model.Provider);
                    return BadRequest("User already exist with given email address.");
                }

                result = await this._authenticationRepository.AddLoginAsync(user.Id, externalLoginInfo.Login);
                if (!result.Succeeded)
                {
                    return this.GetErrorResult(result);
                }
                if (writeLog)
                    System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + " adding login222222 success 3333 so generating access token LOCAL....." + email);

                accessTokenResponse = this.GenerateLocalAccessTokenResponse(user);

                return Ok(accessTokenResponse);
            }


            RegisterExternalUserDTO registerExternalUserDTO = new RegisterExternalUserDTO()
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };

            if (writeLog)
                System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + " registered new external user..." + email);

            result = await this._authenticationRepository.RegisterNewExternalUser(registerExternalUserDTO);

            if (!result.Succeeded)
            {
                return this.GetErrorResult(result);
            }

            if (writeLog)
                System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + " registered success so getting user again...by FindUserByUserName-" + email);

            user = this._authenticationRepository.FindUserByUserName(email);

            result = await this._authenticationRepository.AddLoginAsync(user.Id, externalLoginInfo.Login);

            if (!result.Succeeded)
            {
                return this.GetErrorResult(result);
            }
            if (writeLog)
                System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + " generating..local access token user..." + email);
            //generate access token response
            accessTokenResponse = this.GenerateLocalAccessTokenResponse(email);
            /*
            string apiKey = SENDGRID_API_KEY;
            SendGridClient sendGridClient = new SendGridClient(apiKey, "https://api.sendgrid.com");
            EmailAddress emailSender = new EmailAddress("noreply@carlito.co", "Carlito");
            String subject = "Welcome to Carlito.";
            EmailAddress emailRecipient = new EmailAddress(user.Email);
            Content content = new Content("text/html", "Hello world!");
            SendGridMessage mail = MailHelper.CreateSingleEmail(emailSender, emailRecipient, subject, "", "");

            mail.TemplateId = "e69e88c0-facf-4512-9e23-d1214e4765a3";

            dynamic response = sendGridClient.SendEmailAsync(mail);
            */
            return Ok(accessTokenResponse);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ObtainLocalAccessToken")]
        public async Task<IHttpActionResult> ObtainLocalAccessToken(string provider, string externalAccessToken)
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
                System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + " start of ObtainLocalAccessToken");
            }

            if (string.IsNullOrWhiteSpace(provider) || string.IsNullOrWhiteSpace(externalAccessToken))
            {
                if (writeLog)
                    System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + "  ObtainLocalAccessToken : Provider or external access token is not sent");
                return BadRequest("Provider or external access token is not sent");
            }

            string providerId = string.Empty;

            if (provider.ToLower() == "google")
            {
                ParsedExternalAccessToken verifyGoogleAccessToken = await VerifyGoogleExternalAccessToken(externalAccessToken);
                if (verifyGoogleAccessToken == null)
                {
                    return BadRequest("Invalid Provider or External Access Token");
                }
                providerId = verifyGoogleAccessToken.user_id;
            }

            if (provider.ToLower() == "facebook")
            {
                var fb = new Facebook.FacebookClient();
                fb.AccessToken = externalAccessToken;
                dynamic me = fb.Get("me?fields=first_name,last_name,id,email");
                if (string.IsNullOrEmpty(me.email) == true)
                {
                    if (writeLog)
                        System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + "  ObtainLocalAccessToken : email not setup in Facebook.");
                    return BadRequest("Email is not setup or registered in Facebook.");
                }
                providerId = me.id;
            }

            if(provider.ToLower()  == "linkedin")
            {
                LinkedProfile profileInfo = await GetProfileInfo(externalAccessToken, path, writeLog) ;
                if (profileInfo == null)
                {
                    System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + "- 1 obtain access token Linkedin..profile into not found ... ");
                    return BadRequest("Invalid Provider or External Access Token");
                }
                providerId = profileInfo.id;
            }

            User user = await this._authenticationRepository.FindAsync(new UserLoginInfo(provider, providerId));
            bool hasRegistered = user != null;
            if (hasRegistered == false)
            {
                if (writeLog)
                    System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + "  ObtainLocalAccessToken : External user is not registered..");
                return BadRequest("External user is not registered");
            }
            if (writeLog)
                System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + "  ObtainLocalAccessToken : start generating local access token..");
            JObject accessTokenResponse = this.GenerateLocalAccessTokenResponse(user);

            return Ok(accessTokenResponse);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ConfirmEmail")]
        public async Task<IHttpActionResult> ConfirmEmail(long userId, string confirmationToken)
        {
            if (string.IsNullOrWhiteSpace(confirmationToken) == true)
            {
                return BadRequest("Confirmation token is required.");
            }
            string webSiteUrl = System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"];
            try
            {
                User user = this._authenticationRepository.GetDbContext().Users.Where(u => u.Id == userId).SingleOrDefault();

                if (user != null)
                {
                    bool isConfirmed = await this._authenticationRepository.IsEmailConfirmedAsync(userId);

                    if (isConfirmed == false)
                    {

                        byte[] urlDecodedConfirmationTokenBytes = HttpServerUtility.UrlTokenDecode(confirmationToken);
                        string urlDecodedConfirmationToken = Encoding.UTF8.GetString(urlDecodedConfirmationTokenBytes);
                        IdentityResult result = await this._authenticationRepository.ConfirmEmailAsync(userId, urlDecodedConfirmationToken);

                        if (!result.Succeeded)
                        {
                            return this.GetErrorResult(result);
                        }

                        InviteRequest inviteRequest = inviteRequestService.GetByInviteEmailAddress(user.Email);

                        if (inviteRequest != null)
                        {
                            User inviteFromUser = this._authenticationRepository.GetDbContext().Users.SingleOrDefault(c => c.Id == inviteRequest.InviteFromUserId);

                            if (inviteFromUser != null)
                            {
                                CompanyMember companyMember = this._authenticationRepository.GetDbContext().CompanyMembers.Where(_companyMember => _companyMember.UserID == user.Id && _companyMember.CompanyID == inviteFromUser.Company.ID).SingleOrDefault();

                                if (companyMember == null)
                                {
                                    companyMember = new CompanyMember();
                                    companyMember.UserID = user.Id;
                                    companyMember.CompanyID = inviteFromUser.Company.ID;
                                    companyMember.User = user;
                                    companyMember.Company = inviteFromUser.Company;
                                    companyMember.Role = "";
                                    companyMember = this._authenticationRepository.GetDbContext().CompanyMembers.Add(companyMember);

                                    inviteRequest.Processed = true;
                                    this._authenticationRepository.GetDbContext().Entry(inviteRequest).State = EntityState.Modified;
                                    await this._authenticationRepository.GetDbContext().SaveChangesAsync();
                                }
                            }
                        }

                    }
                    else
                    {
                        return Redirect(string.Format("{0}/emailalreadyconfirmed", webSiteUrl));
                    }
                }
                else
                {
                    return BadRequest("Invalid user.");
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }


            return Redirect(string.Format("{0}/confirmemail", webSiteUrl));

            //return Redirect("http://localhost:53009/confirmemail");
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("SendPasswordResetToken")]
        public async Task<IHttpActionResult> SendPasswordResetToken(string userName)
        {
            User user = this._authenticationRepository.FindUserByUserName(userName);

            //if((user == null) || (await this._authenticationRepository.IsEmailConfirmedAsync(user.Id) == false))
            if (user == null)
            {
                return Ok();
            }

            string userDisplayName = user.UserName;

            if ((String.IsNullOrEmpty(user.FirstName) == false) && (String.IsNullOrEmpty(user.FirstName) == false))
            {
                userDisplayName = user.FirstName + " " + user.LastName;
            }
            string passwordResetToken = await this._authenticationRepository.GeneratePasswordResetTokenAsync(user.Id);

            /*
            string passwordResetToken = await this._authenticationRepository.GeneratePasswordResetTokenAsync(user.Id);
            string apiKey = SENDGRID_API_KEY;
            SendGridClient sendGridClient = new SendGridClient(apiKey, "https://api.sendgrid.com");
            EmailAddress emailSender = new EmailAddress("noreply@carlito.co", "Carlito");
            String subject = "Reset your password.";
            EmailAddress emailRecipient = new EmailAddress(user.Email);
            Content content = new Content("text/html", "Hello world!");
            SendGridMessage mail = MailHelper.CreateSingleEmail(emailSender, emailRecipient, subject, "", "");

            mail.TemplateId = "8208f402-9d27-4035-9437-b75e0f6d3bd7";
            mail.AddSubstitution("<%userId%>", user.Id.ToString());
            mail.AddSubstitution("<%userDisplayName%>", userDisplayName);
            mail.AddSubstitution("<%passwordResetToken%>", HttpUtility.UrlEncode(HttpUtility.UrlEncode(passwordResetToken)));

            dynamic response = sendGridClient.SendEmailAsync(mail);
            */
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IHttpActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {

            try
            {


                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //IdentityResult result = await this._authenticationRepository.ResetPasswordAsync(resetPasswordDTO.UserID, resetPasswordDTO.ResetPasswordToken, resetPasswordDTO.NewPassword);

                IdentityResult result = await this._authenticationRepository.ResetPasswordAsync(resetPasswordDTO.UserID, resetPasswordDTO.Token, resetPasswordDTO.NewPassword);

                if (result.Succeeded == false)
                {
                    return BadRequest();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        //[AllowAnonymous]
        //[HttpPost]
        //[Route("ResetPassword")]
        //public async Task<IHttpActionResult> ResetPassword(ChangePasswordDTO changePasswordDTO)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    IdentityResult result = this._authenticationRepository.ChangePassword("alirazalakhani56@gmail.com", "", changePasswordDTO.NewPassword);

        //    if (result.Succeeded == false)
        //    {
        //        return BadRequest();
        //    }

        //    return Ok();
        //}



        [Authorize]
        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = this._authenticationRepository.ChangePassword(User.Identity.Name, changePasswordDTO.CurrentPassword, changePasswordDTO.NewPassword);

            if (result.Succeeded == false)
            {
                return BadRequest();
            }

            return Ok();
        }

        private string ValidateClientAndRedirectUri(HttpRequestMessage request, ref string redirectUriOutput)
        {
            Uri redirectUri;
            string redirectUriString = this.GetQueryString(Request, "redirect_uri");

            if (string.IsNullOrWhiteSpace(redirectUriString))
            {
                return "redirect_uri is required";
            }

            bool validUri = Uri.TryCreate(redirectUriString, UriKind.Absolute, out redirectUri);

            if (!validUri)
            {
                return "redirect_uri is invalid";
            }

            string clientId = this.GetQueryString(Request, "client_id");

            if (string.IsNullOrWhiteSpace(clientId))
            {
                return "client_Id is required";
            }

            //var client = this._authenticationRepository.FindClient(clientId);

            //if (client == null)
            //{
            //    return string.Format("Client_id '{0}' is not registered in the system.", clientId);
            //}

            //if (!string.Equals(client.AllowedOrigin, redirectUri.GetLeftPart(UriPartial.Authority), StringComparison.OrdinalIgnoreCase))
            //{
            //    return string.Format("The given URL is not allowed by Client_id '{0}' configuration.", clientId);
            //}

            redirectUriOutput = redirectUri.AbsoluteUri;

            return string.Empty;
        }

        private string GetQueryString(HttpRequestMessage request, string key)
        {
            IEnumerable<KeyValuePair<string, string>> queryStrings = request.GetQueryNameValuePairs();
            KeyValuePair<string, string> queryStringkeyValue;

            if (queryStrings == null)
            {
                return null;
            }

            queryStringkeyValue = queryStrings.FirstOrDefault(keyValue => string.Compare(keyValue.Key, key, true) == 0);

            if (string.IsNullOrEmpty(queryStringkeyValue.Value))
            {
                return null;
            }

            return queryStringkeyValue.Value;
        }

        private async Task<ParsedExternalAccessToken> VerifyGoogleExternalAccessToken(string accessToken)
        {
            //https://developers.google.com/identity/sign-in/web/backend-auth#verify-the-integrity-of-the-id-token
            //https://developers.google.com/identity/protocols/OAuth2WebServer
           
            ParsedExternalAccessToken parsedToken = null;

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
                    System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + " - start of VerifyGoogleExternalAccessToken ");

                string verifyTokenEndPoint = "";
                verifyTokenEndPoint = string.Format("https://www.googleapis.com/oauth2/v1/tokeninfo?access_token={0}", accessToken);
                HttpResponseMessage response;
                Uri uri = new Uri(verifyTokenEndPoint);
                using (HttpClient httpClient = new HttpClient())
                {
                    response = await httpClient.GetAsync(uri);
                }
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(content))
                    {
                        dynamic googleValidResponse = (dynamic)Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                        if (googleValidResponse != null)
                        {
                            ParsedExternalAccessToken token = new ParsedExternalAccessToken();
                            token.user_id = googleValidResponse.user_id;
                            token.email = googleValidResponse.email;
                            parsedToken = token;
                            if (writeLog)
                                System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + " - success in VerifyGoogleExternalAccessToken ");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                parsedToken = null;
                if (writeLog)
                    System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + " error in VerifyGoogleExternalAccessToken " + ex.ToString());
            }

            return parsedToken;
        }

        private JObject GenerateLocalAccessTokenResponse(string userName)
        {
            User user = this._authenticationRepository.FindUserByUserName(userName);

            return this.GenerateLocalAccessTokenResponse(user);
        }

        private JObject GenerateLocalAccessTokenResponse(User user)
        {
            TimeSpan tokenExpiration = TimeSpan.FromDays(1);
            ClaimsIdentity identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);

            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            identity.AddClaim(new Claim("role", "user"));

            AuthenticationProperties props = new AuthenticationProperties()
            {
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.Add(tokenExpiration),
            };
            AuthenticationTicket ticket = new AuthenticationTicket(identity, props);
            string accessToken = Startup.OAuthBearerOptions.AccessTokenFormat.Protect(ticket);

            string expireTime = ticket.Properties.ExpiresUtc.Value.ToString("ddd, dd MMM yyyy HH:mm:ss") + " GMT";

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
            if(writeLog)
            System.IO.File.AppendAllText(path, Environment.NewLine + System.DateTime.Now.ToString() + " GenerateLocalAccessTokenResponse -expireTime-" + expireTime + " - " + user.UserName);

            JObject tokenResponse = new JObject(
                                        new JProperty("userName", user.UserName),
                                        new JProperty("access_token", accessToken),
                                        new JProperty("token_type", "bearer"),
                                        new JProperty("expires_in", tokenExpiration.TotalSeconds.ToString()),
                                        new JProperty(".issued", ticket.Properties.IssuedUtc.ToString()),
                                        new JProperty(".expires", expireTime)
                                        );

            this._authenticationRepository.UpdateLoginDates(user);

            return tokenResponse;
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (result.Succeeded == true)
            {
                return null;
            }

            if (result.Errors != null)
            {
                foreach (string error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            if (ModelState.IsValid == true)
            {
                return BadRequest();
            }

            return BadRequest(ModelState);
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
                if (!string.IsNullOrEmpty(response))
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

        private async Task<LinkedProfile> GetProfileInfo(string accessToken, string logFileName, bool writeLog)
        {
            LinkedProfile profile = null;
            string file = logFileName;
            try
            {
                Uri uri = new Uri("https://api.linkedin.com/v1/people/~?format=json");
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    var response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        profile = new LinkedProfile();
                        string content = await response.Content.ReadAsStringAsync();
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