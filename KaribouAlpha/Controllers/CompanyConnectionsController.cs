using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using KaribouAlpha.DAL;
using KaribouAlpha.Models;
using AutoMapper.QueryableExtensions;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace KaribouAlpha.Controllers
{
    public class CompanyConnectionsController : ApiController
    {
        private KaribouAlphaContext db = new KaribouAlphaContext();
        private const String SENDGRID_API_KEY = @"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";

        [HttpGet]
        [Route("api/GetMyCompanyConnections")]
        [Authorize]
        public async Task<IQueryable<CompanyConnectionDTO>> GetMyCompanyConnections()
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            IQueryable<CompanyConnection> companyConnections = db.CompanyConnections
                                                                                                                    .Where(connection => connection.UserID == user.Id && connection.Status == ConnectionStatus.Accepted)
                                                                                                                    .Include(connection => connection.Company)
                                                                                                                    .Include(connection => connection.Company.Products);

            return companyConnections.ProjectTo<CompanyConnectionDTO>();
        }

        [HttpGet]
        [Route("api/GetMyCompanysFollowers")]
        [Authorize]
        public async Task<IQueryable<CompanyFollowerDTO>> GetMyCompanysFollowers()
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).Include(_user => _user.Company).SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            if (user.Company == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            IQueryable<CompanyConnection> companyConnections = db.CompanyConnections
                                                                                                                    .Where(connection => connection.CompanyID == user.Company.ID)
                                                                                                                    .Include(connection => connection.User)
                                                                                                                    .Include(connection => connection.Company)
                                                                                                                    .Include(connection => connection.FollowerGroups);
            IQueryable<CompanyFollowerDTO> companyFollowerDtos = companyConnections.ProjectTo<CompanyFollowerDTO>();

            foreach (CompanyFollowerDTO companyFollowerDTO in companyFollowerDtos)
            {
                companyFollowerDTO.IsNew = (user.LastCheckForConnectionRequests == null) || ((companyFollowerDTO.Status == ConnectionStatus.Pending) && (user.LastCheckForConnectionRequests < companyFollowerDTO.RequestedAt));
            }

            user.LastCheckForConnectionRequests = DateTime.Now;
            db.Entry(user).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return companyFollowerDtos;
        }

        [HttpGet]
        [Route("api/AddCompanyConnection")]
        [Authorize]
        public async Task<IHttpActionResult> AddCompanyConnection(long companyId)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            Company company = db.Companies.Where(_company => _company.ID == companyId).Include(_company => _company.Owner).SingleOrDefault();

            if (company == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if(user.Company == company)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            CompanyConnection companyConnection = db.CompanyConnections.Where(_companyConnection => _companyConnection.CompanyID == companyId && _companyConnection.UserID == user.Id).SingleOrDefault();

            if (companyConnection != null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            companyConnection = new CompanyConnection();
            companyConnection.UserID = user.Id;
            companyConnection.User = user;
            companyConnection.CompanyID = company.ID;
            companyConnection.Company = company;
            companyConnection.RequestedAt = DateTime.Now;
            companyConnection.Status = ConnectionStatus.Pending;
            companyConnection = db.CompanyConnections.Add(companyConnection);
            await db.SaveChangesAsync();
            
            string connectionRequestUserName = user.UserName;

            if (user.FirstName != null || user.LastName != null)
            {
                connectionRequestUserName = (user.FirstName == null ? "" : user.FirstName) + " " + (user.LastName == null ? "" : user.LastName);
            }

            string companyOwnerUserName = company.Owner.UserName;

            if (company.Owner.FirstName != null || company.Owner.LastName != null)
            {
                companyOwnerUserName = (company.Owner.FirstName == null ? "" : company.Owner.FirstName) + " " + (company.Owner.LastName == null ? "" : company.Owner.LastName);
            }

            string connectionRequestUserCompanyName = user.Company == null ? "" : user.Company.DisplayName;
            string connectionRequestUserCompanyId = user.Company == null ? "" : user.Company.ID.ToString();
            string apiKey = SENDGRID_API_KEY;
            SendGridClient sendGridClient = new SendGridClient(apiKey, "https://api.sendgrid.com");
            EmailAddress emailSender = new EmailAddress("noreply@cervitt.co.uk", "Cervitt");
            String subject = "Connection request";
            EmailAddress emailRecipient = new EmailAddress(company.Owner.Email);
            Content content = new Content("text/html", "Hello world!");
            SendGridMessage mail = MailHelper.CreateSingleEmail(emailSender, emailRecipient, subject, "", "");

            mail.TemplateId = "c60b6b5e-4b1f-4cf3-b884-f69b4229947c";
            mail.AddSubstitution("<%companyName%>", company.DisplayName);
            mail.AddSubstitution("<%companyOwnerUserName%>", companyOwnerUserName);
            mail.AddSubstitution("<%connectionRequestUserId%>", user.Id.ToString());
            mail.AddSubstitution("<%connectionRequestUserName%>", connectionRequestUserName);
            mail.AddSubstitution("<%connectionRequestUserTitle%>", user.JobTitle);
            mail.AddSubstitution("<%connectionRequestUserCompanyName%>", connectionRequestUserCompanyName);
            mail.AddSubstitution("<%connectionRequestUserCompanyId%>", connectionRequestUserCompanyId);

            dynamic response = sendGridClient.SendEmailAsync(mail);

            return Ok();
        }

        [HttpGet]
        [Route("api/AcceptCompanyConnection")]
        [Authorize]
        public async Task<IHttpActionResult> AcceptCompanyConnection(long companyConnectionId)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName)
                                                    .Include(_user => _user.Company)
                                                    .Include(_user => _user.Company.Followers)
                                                    .SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            if (user.Company == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            CompanyConnection companyConnection = user.Company.Followers.Where(_companyConnection => _companyConnection.ID == companyConnectionId && _companyConnection.Status == ConnectionStatus.Pending).SingleOrDefault();

            if (companyConnection == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            companyConnection.AcceptedAt = DateTime.Now;
            companyConnection.Status = ConnectionStatus.Accepted;
            db.Entry(companyConnection).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("api/RemoveCompanyConnection")]
        [Authorize]
        public async Task<IHttpActionResult> RemoveCompanyConnection(long companyConnectionId)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName)
                                                    .Include(_user => _user.Company)
                                                    .Include(_user => _user.Company.Followers)
                                                    .SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            if (user.Company == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            CompanyConnection companyConnection = user.Company.Followers.Where(_companyConnection => _companyConnection.ID == companyConnectionId).SingleOrDefault();

            if (companyConnection == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            db.CompanyConnections.Remove(companyConnection);
            await db.SaveChangesAsync();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompanyConnectionExists(long id)
        {
            return db.CompanyConnections.Count(e => e.ID == id) > 0;
        }
    }
}