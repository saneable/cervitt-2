using AutoMapper;
using KaribouAlpha.BLL;
using KaribouAlpha.DAL;
using KaribouAlpha.Models;
using Newtonsoft.Json.Linq;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace KaribouAlpha.Controllers
{
    public class CompanyMembersController : ApiController
    {
        private KaribouAlphaContext db = new KaribouAlphaContext();
        private const String SENDGRID_API_KEY = @"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";
        private IInviteRequestService inviteRequestService = null;
        public CompanyMembersController()
        {
            inviteRequestService = new InviteRequestService();
        }

        [HttpGet]
        [Route("api/GetMyCompanyMembers")]
        [Authorize]
        public IEnumerable<CompanyMemberDTO> GetMyCompanyMembers()
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName)
                                    .Include(_user => _user.Company)
                                    .Include(_user => _user.Company.Members)
                                    .SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            Company company = user.Company;

            if (company == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Mapper.Map<IEnumerable<CompanyMember>, IEnumerable<CompanyMemberDTO>>(company.Members);
        }

        [HttpGet]
        [Route("api/GetCompanyMemberLevelByCompanyId")]
        [Authorize]
        [ResponseType(typeof(CompanyMemberDTO))]
        public IHttpActionResult GetCompanyMemberLevelByCompanyId(long companyId)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).SingleOrDefault();
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            CompanyMember companyMember = db.CompanyMembers.Where(cm => cm.CompanyID == companyId && cm.UserID == user.Id).FirstOrDefault();
            if (companyMember != null && companyMember.UserLevelId.HasValue && companyMember.UserLevelId.Value > 0)
            {
                return Ok(Mapper.Map<CompanyMember, CompanyMemberDTO>(companyMember));
            }
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }


        [HttpPost]
        [Route("api/AddNewMemberToMyCompany")]
        [Authorize]
        [ResponseType(typeof(CompanyMemberDTO))]
        public async Task<IHttpActionResult> AddNewMemberToMyCompany(NewCompanyMemberDTO newCompanyMemberDTO)
        {
            string userName = User.Identity.Name;
            User authenticatedUser = db.Users.Where(_user => _user.UserName == userName).SingleOrDefault();

            if (authenticatedUser == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user = db.Users.Where(_user => _user.UserName == newCompanyMemberDTO.UserName).SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Company company = authenticatedUser.Company;

            if (company == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            CompanyMember companyMember = db.CompanyMembers.Where(_companyMember => _companyMember.UserID == user.Id && _companyMember.CompanyID == company.ID).SingleOrDefault();

            if (companyMember != null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            
            UserLevel editorUserLevel = await db.UserLevels.SingleOrDefaultAsync(ul => ul.Name == "Editor");
            companyMember = new CompanyMember();
            companyMember.UserID = user.Id;
            companyMember.CompanyID = company.ID;
            companyMember.User = user;
            companyMember.Company = authenticatedUser.Company;
            companyMember.Role = newCompanyMemberDTO.Role;
            if(editorUserLevel != null)
            {
                companyMember.UserLevelId = editorUserLevel.Id;
            }

            companyMember = db.CompanyMembers.Add(companyMember);
            await db.SaveChangesAsync();

            return Ok(Mapper.Map<CompanyMember, CompanyMemberDTO>(companyMember));
        }

        [HttpPost]
        [Route("api/RemoveMemberFromMyCompany")]
        [Authorize]
        public async Task<IHttpActionResult> RemoveMemberFromMyCompany(CompanyMemberDTO companyMemberDTO)
        {
            string userName = User.Identity.Name;
            User authenticatedUser = db.Users.Where(_user => _user.UserName == userName).SingleOrDefault();

            if (authenticatedUser == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Company company = authenticatedUser.Company;

            if (company == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            CompanyMember companyMember = db.CompanyMembers.Where(_companyMember => _companyMember.UserID == companyMemberDTO.UserID && _companyMember.CompanyID == company.ID).SingleOrDefault();

            if (companyMember == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
                        
            db.CompanyMembers.Remove(companyMember);
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("api/UpdateMemberOfMyCompany")]
        [Authorize]
        [ResponseType(typeof(CompanyMemberDTO))]
        public async Task<IHttpActionResult> UpdateMemberOfMyCompany(CompanyMemberUpdateDTO companyMemberUpdateDTO)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CompanyMember companyMember = db.CompanyMembers.Where(_companyMember => _companyMember.ID == companyMemberUpdateDTO.ID).SingleOrDefault();

            if (companyMember == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Company company = user.Company;

            if (company == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if (companyMember.Company != company)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            companyMember.UserLevelId = companyMemberUpdateDTO.UserLevelId;
            companyMember.Role = companyMemberUpdateDTO.Role;
            companyMember.Email = companyMemberUpdateDTO.Email;
            db.Entry(companyMember).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Ok(Mapper.Map<CompanyMember, CompanyMemberDTO>(companyMember));
        }

        [HttpGet]
        [Route("api/GetAvailableTeamMembersForProduct")]
        [Authorize]
        public IEnumerable<CompanyMemberDTO> GetAvailableTeamMembersForProduct(long productId)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName)
                                        .Include(_user => _user.Company.Members
                                            .Select(teamMember => teamMember.User))
                                        .SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            Product product = db.Products.Where(_product => _product.ID == productId)
                                                .Include(_product => _product.TeamMembers
                                                    .Select(teamMember => teamMember.User)
                                                    .Select(teamMember => teamMember.Company))
                                                .SingleOrDefault();

            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if ((product.CompanyID != user.Company.ID) && (product.TeamMembers.Where(teamMember => teamMember.UserID == user.Id && teamMember.CanEditTheProduct == true).SingleOrDefault() == null))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            ICollection<CompanyMember> companyMembers = new HashSet<CompanyMember>();
            bool companyMemberIsAlreadyAProductTeamMember = false;

            foreach(CompanyMember companyMember in product.Company.Members)
            {
                companyMemberIsAlreadyAProductTeamMember = false;
                foreach (ProductTeamMember productTeamMember in product.TeamMembers)
                {
                    if (productTeamMember.UserID == companyMember.UserID)
                    {
                        companyMemberIsAlreadyAProductTeamMember = true;
                        break;
                    }
                }

                if(companyMemberIsAlreadyAProductTeamMember == true)
                {
                    continue;
                }

                companyMembers.Add(companyMember);
            }

            return Mapper.Map<IEnumerable<CompanyMemberDTO>>(companyMembers);
        }

        [HttpPost]
        [Authorize]
        [Route("api/InviteToRegisterInCompany")]
        public async Task<IHttpActionResult> InviteToRegisterInCompany(Invite inviteDetail)
        {
            CervittApiResult result = new CervittApiResult();

            if (inviteDetail == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            if (string.IsNullOrEmpty(inviteDetail.EmailAddress))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }


            User user = await db.Users.SingleOrDefaultAsync(c => c.UserName == User.Identity.Name);

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            if (!User.Identity.IsAuthenticated)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            if (user.Company == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            string postEmailAddress = inviteDetail.EmailAddress.Replace(Environment.NewLine, ",");
            postEmailAddress = postEmailAddress.Replace( '\r' , ',');
            postEmailAddress= postEmailAddress.Replace('\n' , ',');
            string[] emailListToInvite = postEmailAddress.Split(',');
            
            bool hasInvalidEmailAddressInList = false;

            if (emailListToInvite.Length == 0)
            {
                result.Success = false;
                result.ErrorMessage = "No any email address to invite.";
            }
            else
            {
                hasInvalidEmailAddressInList = EmailService.CheckAnyEmailIsNotValid(emailListToInvite);

                if(hasInvalidEmailAddressInList)
                {
                    result.Success = false;
                    result.ErrorMessage = "Invalid email address found or email format is not correct.";
                    return Ok(result);
                }
                
                for (int i = 0; i <= emailListToInvite.Length - 1; i++)
                {
                    if (string.IsNullOrEmpty(emailListToInvite[i]))
                    {
                        continue;
                    }

                    string emailToInvite = emailListToInvite[i].Trim();

                    Guid inviteCode = Guid.NewGuid();

                    bool userExistInSystem = await db.Users.AnyAsync(u => u.Email == emailToInvite);

                    if (!userExistInSystem)
                    {
                        InviteRequest existingRequest = await db.InviteRequests.SingleOrDefaultAsync(c => c.InviteType == 1 && c.InviteToEmailAddress == emailToInvite && c.InviteFromUserId == user.Id && c.Processed == false);

                        if (existingRequest == null)
                        {
                            string webSiteUrl = System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"];
                            string emailContent = " You have been invited by {0} to join." + "<br><br>" + "Please click below link to register and join.";
                            emailContent = string.Format(emailContent, user.FirstName + " (" + user.Email + ")");
                            emailContent = emailContent + "<br><br>" + string.Format("Click here to <a href='{0}/{1}'><strong>Join</strong></a>", string.Concat(webSiteUrl, "/", "registration"), inviteCode.ToString());
                            string apiKey = SENDGRID_API_KEY;
                            SendGridClient sendGridClient = new SendGridClient(apiKey, "https://api.sendgrid.com");
                            EmailAddress emailSender = new EmailAddress("noreply@cervitt.co.uk", "Cervitt");
                            String subject = "Invitation from Cervitt User to join.";
                            EmailAddress emailRecipient = new EmailAddress(emailToInvite);
                            Content content = new Content("text/html", emailContent);
                            SendGridMessage mail = MailHelper.CreateSingleEmail(emailSender, emailRecipient, subject, "", emailContent);
                            dynamic response = sendGridClient.SendEmailAsync(mail);
                            InviteRequest inviteRequest = new InviteRequest();
                            inviteRequest.InviteType = 1;
                            inviteRequest.Processed = false;
                            inviteRequest.InviteDate = DateTime.Now;
                            inviteRequest.InviteCode = inviteCode;
                            inviteRequest.InviteFromCompanyId = user.Company.ID;
                            inviteRequest.InviteFromUserId = user.Id;
                            inviteRequest.InviteToEmailAddress = emailToInvite;
                            inviteRequestService.Insert(inviteRequest);
                            result.Success = true;
                            result.SuccessMessage += emailToInvite + ",";
                        }
                    }
                }
            }

            if (result.Success)
            {
                if(result.SuccessMessage.EndsWith(","))
                {
                    result.SuccessMessage = result.SuccessMessage.TrimEnd(',');
                }
                    
                result.SuccessMessage = "Invite request has been sent to " + result.SuccessMessage;
            }
            else
            {
                result.ErrorMessage = "Invite request not sent. (Possible reason: Either email is invalid, email already exist or request already sent.)"; 
            }

            return Ok(result);
        }


        [HttpPost]
        [Authorize]
        [Route("api/InviteToJoinMyCompany")]
        public async Task<IHttpActionResult> InviteToJoinMyCompany(Invite inviteDetail)
        {
            if (inviteDetail == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            if (inviteDetail.ToUserId <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            User user = await db.Users.SingleOrDefaultAsync(c => c.UserName == User.Identity.Name);

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            if (!User.Identity.IsAuthenticated)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            if (user.Company == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            Guid inviteCode = Guid.NewGuid();
            

            string webSiteUrl = System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"];

            string emailContent = " You have been invited by {0} to join." + "<br><br>" + "Please click below link to register and join.";
            emailContent = string.Format(emailContent, user.FirstName + " (" + user.Email + ")");
            

            string apiKey = SENDGRID_API_KEY;
            SendGridClient sendGridClient = new SendGridClient(apiKey, "https://api.sendgrid.com");
            EmailAddress emailSender = new EmailAddress("noreply@cervitt.co.uk", "Cervitt");
            String subject = "Invitation to Join Company on Cervitt.";
            EmailAddress emailRecipient = new EmailAddress(inviteDetail.EmailAddress);
            Content content = new Content("text/html", emailContent);
            SendGridMessage mail = MailHelper.CreateSingleEmail(emailSender, emailRecipient, subject, "", emailContent);
            dynamic response = sendGridClient.SendEmailAsync(mail);

            InviteRequest inviteRequest = new InviteRequest();
            inviteRequest.InviteType = 2;
            inviteRequest.Processed = false;
            inviteRequest.InviteDate = DateTime.Now;
            inviteRequest.InviteCode = inviteCode;
            inviteRequest.InviteFromCompanyId = user.Company.ID;
            inviteRequest.InviteFromUserId = user.Id;
            inviteRequest.InviteToUserId = inviteDetail.ToUserId;
            inviteRequest.InviteToEmailAddress = null;
            inviteRequestService.Insert(inviteRequest);
            return Ok("true");
        }


     

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompanyMemberExists(long id)
        {
            return db.CompanyMembers.Count(e => e.ID == id) > 0;
        }
    }
}