using AutoMapper;
using KaribouAlpha.DAL;
using KaribouAlpha.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace KaribouAlpha.Controllers
{
    public class CompaniesController : ApiController
    {
        private KaribouAlphaContext db = new KaribouAlphaContext();

        [HttpGet]
        [Route("api/GetMyCompanyProfile")]
        [Authorize]
        [ResponseType(typeof(CompanyProfileDTO))]
        public async Task<IHttpActionResult> GetMyCompanyProfile()
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            Company company = (user.Company != null) ? user.Company : new Company { Owner = user , Email = user.Email};

            CompanyProfileDTO dto = new CompanyProfileDTO();
            dto.ID = company.ID;
            dto.OwnerID = user.Id;
            dto.PhoneNumber = company.PhoneNumber;
            dto.Email = company.Email;
            dto.Description = company.Description;
            dto.DisplayName = company.DisplayName;
            dto.Image = company.Image;
            dto.HeaderImage = company.HeaderImage;
            dto.TradingName = company.TradingName;
            dto.Sector = company.Sector;
            dto.WebsiteUrl = company.WebsiteUrl;
            dto.URI = company.URI;

            dto.Countries = new string[0];

            if (db.CompanyCountries.Any(cc => cc.CompanyId == company.ID))
            {
                dto.Countries = db.CompanyCountries.Where(cc => cc.CompanyId == company.ID).Select(c => c.Country.Name).ToArray().Distinct().ToArray();
            }

            //if (company.Countries != null)
            //{
            //    dto.Countries = company.Countries.Select(c => c.Country.Name).ToArray();
            //}

            var companySkills = db.CompanySkills.Where(cks => cks.CompanyId == company.ID).ToList();

            if (companySkills.Count() == 0)
            {
                dto.Skills = new string[0];
            }
            else
            {
                List<string> skills = new List<string>();
                foreach (CompanySkill cs in companySkills)
                {
                    skills.Add(cs.SkillName);
                }
                dto.Skills = skills.ToArray();
            }

            //return Ok(Mapper.Map<Company, CompanyProfileDTO>(company));

            return Ok(dto);
        }

        [HttpGet]
        [Route("api/GetMyCompanyStatistics")]
        [Authorize]
        [ResponseType(typeof(CompanyStatisticsDTO))]
        public async Task<IHttpActionResult> GetMyCompanyStatistics()
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).Include(_user => _user.Company).SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            DateTime now = DateTime.Now;
            DateTime oneWeekAgo = now.AddDays(-7d);
            DateTime twoWeeksAgo = now.AddDays(-14d);
            DateTime oneMonthAgo = now.AddMonths(-1);
            DateTime twoMonthsAgo = now.AddMonths(-2);
            CompanyStatisticsDTO companyStatisticsDTO = new CompanyStatisticsDTO();

            companyStatisticsDTO.ProductViewsThisWeek = db.ProductViews.Where(productView => productView.Product.CompanyID == user.Company.ID && productView.DateTime >= oneWeekAgo && productView.DateTime < now).Count();
            companyStatisticsDTO.ProductViewsLastWeek = db.ProductViews.Where(productView => productView.Product.CompanyID == user.Company.ID && productView.DateTime >= twoWeeksAgo && productView.DateTime < oneWeekAgo).Count();
            companyStatisticsDTO.ProductViewsThisMonth = db.ProductViews.Where(productView => productView.Product.CompanyID == user.Company.ID && productView.DateTime >= oneMonthAgo && productView.DateTime < now).Count();
            companyStatisticsDTO.ProductViewsLastMonth = db.ProductViews.Where(productView => productView.Product.CompanyID == user.Company.ID && productView.DateTime >= twoMonthsAgo && productView.DateTime < oneMonthAgo).Count();
            companyStatisticsDTO.ProductFeedbackThisWeek = db.Feedback.Where(feedback => feedback.ProductFile.Product.CompanyID == user.Company.ID && feedback.UserID != user.Id && feedback.PostedAt >= oneWeekAgo && feedback.PostedAt < now).Count();
            companyStatisticsDTO.ProductFeedbackLastWeek = db.Feedback.Where(feedback => feedback.ProductFile.Product.CompanyID == user.Company.ID && feedback.UserID != user.Id && feedback.PostedAt >= twoWeeksAgo && feedback.PostedAt < oneWeekAgo).Count();
            companyStatisticsDTO.ProductFeedbackThisMonth = db.Feedback.Where(feedback => feedback.ProductFile.Product.CompanyID == user.Company.ID && feedback.UserID != user.Id && feedback.PostedAt >= oneMonthAgo && feedback.PostedAt < now).Count();
            companyStatisticsDTO.ProductFeedbackLastMonth = db.Feedback.Where(feedback => feedback.ProductFile.Product.CompanyID == user.Company.ID && feedback.UserID != user.Id && feedback.PostedAt >= twoMonthsAgo && feedback.PostedAt < oneMonthAgo).Count();
            companyStatisticsDTO.ProductFileDownloadsThisWeek = db.ProductFileDownloads.Where(productFileDownload => productFileDownload.ProductFile.Product.CompanyID == user.Company.ID && productFileDownload.DateTime >= oneWeekAgo && productFileDownload.DateTime < now).Count();
            companyStatisticsDTO.ProductFileDownloadsLastWeek = db.ProductFileDownloads.Where(productFileDownload => productFileDownload.ProductFile.Product.CompanyID == user.Company.ID && productFileDownload.DateTime >= twoWeeksAgo && productFileDownload.DateTime < oneWeekAgo).Count();
            companyStatisticsDTO.ProductFileDownloadsThisMonth = db.ProductFileDownloads.Where(productFileDownload => productFileDownload.ProductFile.Product.CompanyID == user.Company.ID && productFileDownload.DateTime >= oneMonthAgo && productFileDownload.DateTime < now).Count();
            companyStatisticsDTO.ProductFileDownloadsLastMonth = db.ProductFileDownloads.Where(productFileDownload => productFileDownload.ProductFile.Product.CompanyID == user.Company.ID && productFileDownload.DateTime >= twoMonthsAgo && productFileDownload.DateTime < oneMonthAgo).Count();
            
            return Ok(companyStatisticsDTO);
        }

        [HttpGet]
        [Route("api/GetMyCompany")]
        [Authorize]
        [ResponseType(typeof(CompanyViewDTO))]
        public async Task<IHttpActionResult> GetMyCompany()
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            if (user.Company == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Ok(Mapper.Map<Company, CompanyViewDTO>(user.Company));
        }

        [HttpGet]
        [Route("api/GetCompany")]
        [Authorize]
        [ResponseType(typeof(CompanyViewDTO))]
        public async Task<IHttpActionResult> GetCompany(long id)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            Company company = db.Companies.Where(_company => _company.ID == id).AsNoTracking().SingleOrDefault();

            if (company == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Ok(Mapper.Map<Company, CompanyViewDTO>(company));
        }

        [HttpGet]
        [Route("api/GetCompanyByUri")]
        [Authorize]
        [ResponseType(typeof(CompanyViewDTO))]
        public async Task<IHttpActionResult> GetCompanyByUri(string uri)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            Company company = db.Companies.Where(_company => _company.URI == uri).AsNoTracking().SingleOrDefault();

            if (company == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return Ok(Mapper.Map<Company, CompanyViewDTO>(company));
        }

        [HttpGet]
        [Route("api/GetMyCompanySocialMediaLinks")]
        [Authorize]
        [ResponseType(typeof(CompanySocialMediaLinksDTO))]
        public async Task<IHttpActionResult> GetMyCompanySocialMediaLinks()
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            return Ok(Mapper.Map<Company, CompanySocialMediaLinksDTO>(user.Company));
        }

        [HttpGet]
        [Route("api/GetMyCompanyEmailNotificationFrequencies")]
        [Authorize]
        [ResponseType(typeof(CompanyEmailNotificationFrequenciesDTO))]
        public async Task<IHttpActionResult> GetMyCompanyEmailNotificationFrequencies()
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            return Ok(Mapper.Map<Company, CompanyEmailNotificationFrequenciesDTO>(user.Company));
        }

        [HttpGet]
        [Route("api/GetCompanyProfileImage")]
        public async Task<IHttpActionResult> GetCompanyProfileImage(long companyId)
        {
            Company company = db.Companies.Where(_company => _company.ID == companyId).AsNoTracking().SingleOrDefault();

            if (company == null)
            {
                return NotFound();
            }

            byte[] imageBytes;
            string mediaTypeHeader;

            if (company.Image != null)
            {
                imageBytes = company.Image;
                mediaTypeHeader = "image/png";
            }
            else
            {
                string filePath = System.Web.Hosting.HostingEnvironment.MapPath("/img/user_image.svg");

                imageBytes = File.ReadAllBytes(filePath);
                mediaTypeHeader = "image/svg+xml";
            }

            MemoryStream memoryStream = new MemoryStream(imageBytes);
            HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.OK);

            responseMessage.Content = new StreamContent(memoryStream);
            responseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(mediaTypeHeader);

            return ResponseMessage(responseMessage);
        }

        [HttpGet]
        [Route("api/GetCompanyLogo")]
        public async Task<IHttpActionResult> GetCompanyLogo(long companyId)
        {
            Company company = db.Companies.Where(_company => _company.ID == companyId).AsNoTracking().SingleOrDefault();

            if (company == null)
            {
                return NotFound();
            }

            byte[] imageBytes;
            string mediaTypeHeader;

            if (company.Logo != null)
            {
                imageBytes = company.Logo;
                mediaTypeHeader = "image/png";
            }
            else
            {
                string filePath = System.Web.Hosting.HostingEnvironment.MapPath("/img/user_image.svg");

                imageBytes = File.ReadAllBytes(filePath);
                mediaTypeHeader = "image/svg+xml";
            }

            MemoryStream memoryStream = new MemoryStream(imageBytes);
            HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.OK);

            responseMessage.Content = new StreamContent(memoryStream);
            responseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(mediaTypeHeader);

            return ResponseMessage(responseMessage);
        }

        [HttpPost]
        [Route("api/UpdateMyCompanyProfile")]
        [Authorize]
        [ResponseType(typeof(CompanyProfileDTO))]
        public async Task<IHttpActionResult> UpdateMyCompanyProfile(CompanyProfileDTO companyProfileDTO)
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

            User user = db.Users.Find(companyProfileDTO.OwnerID);

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if (authenticatedUser != user)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            if (companyProfileDTO.URI != null)
            {
                Regex regex = new Regex("[^a-zA-Z0-9_-]");

                companyProfileDTO.URI = companyProfileDTO.URI.Replace(" ", "-");
                companyProfileDTO.URI = regex.Replace(companyProfileDTO.URI, "").ToLower();

                if (companyProfileDTO.URI.Length == 0)
                {
                    companyProfileDTO.URI = null;
                }
                else if (db.Companies.Where(company => company.URI == companyProfileDTO.URI && company.ID != companyProfileDTO.ID).SingleOrDefault() != null)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }
            }
            else if(companyProfileDTO.ID == 0 && companyProfileDTO.URI == null)
            {
                Regex regex = new Regex("[^a-zA-Z0-9_-]");
                companyProfileDTO.URI = companyProfileDTO.DisplayName.Replace(" ", "-");
                companyProfileDTO.URI = regex.Replace(companyProfileDTO.URI, "").ToLower();
            }
            

            user.Company = Mapper.Map(companyProfileDTO, user.Company);
            db.Entry(user).State = EntityState.Modified;
            await db.SaveChangesAsync();

            if (companyProfileDTO.Countries != null)
            {
                if(db.CompanyCountries.Any(cc=>cc.CompanyId ==  user.Company.ID))
                {
                    db.CompanyCountries.RemoveRange(db.CompanyCountries.Where(cc => cc.CompanyId == user.Company.ID));
                    await db.SaveChangesAsync();
                }

                List<CompanyCountry> countries = new List<CompanyCountry>();
                foreach (string country in companyProfileDTO.Countries)
                {
                    var countryModel = db.Countries.SingleOrDefault(c => c.Name == country);
                    if (countryModel != null)
                    {
                        CompanyCountry companyCountry = new CompanyCountry();
                        companyCountry.CompanyId = user.Company.ID;
                        companyCountry.CountryId = countryModel.Id;
                        countries.Add(companyCountry);
                    }
                }
                db.CompanyCountries.AddRange(countries);
                await db.SaveChangesAsync();
            }
                        
            if (!db.Sectors.Any(s => s.SectorName == companyProfileDTO.Sector))
            {
                Sector sector = new Sector();
                sector.Active = true;
                sector.CreateDate = DateTime.Now;
                sector.SectorName = companyProfileDTO.Sector;
                db.Sectors.Add(sector);
                await db.SaveChangesAsync();
            }

            bool existingSkills = db.CompanySkills.Any(cs => cs.CompanyId == user.Company.ID);
            if (existingSkills)
            {
                //remove it
                List<CompanySkill> companySkills = db.CompanySkills.Where(cs => cs.CompanyId == user.Company.ID).ToList();
                db.CompanySkills.RemoveRange(companySkills);
                db.SaveChanges();
            }

            
            if (companyProfileDTO.Skills != null && companyProfileDTO.Skills.Count() > 0 && user.Company.ID > 0)
            {
                bool hasSkill = false;
                foreach(string skill in companyProfileDTO.Skills)
                {
                    CompanySkill existing = db.CompanySkills.SingleOrDefault(cs => cs.SkillName == skill && cs.CompanyId == user.Company.ID);
                    if (existing == null)
                    {
                        if (!db.Skills.Any(s => s.SkillName == skill))
                        {
                            Skill skillMaster = new Skill();
                            skillMaster.SkillName = skill;
                            skillMaster.Active = true;
                            skillMaster.CreateDate = DateTime.Now;
                            db.Skills.Add(skillMaster);
                            db.SaveChanges();
                        }
                        CompanySkill newSkill = new CompanySkill();
                        newSkill.CompanyId = user.Company.ID;
                        newSkill.SkillName = skill;
                        db.CompanySkills.Add(newSkill);
                        hasSkill = true;
                    }
                }
                if (hasSkill)
                {
                    await db.SaveChangesAsync();
                }
            }

            if (!user.OnboardingSkipped)
            {
                List<CompanyMember> companyMembers =  await db.CompanyMembers.Where(c1 => c1.UserID == user.Id).ToListAsync();
                if(companyMembers != null && companyMembers.Count > 0)
                {
                    CompanyMember cm = companyMembers.FirstOrDefault();
                    if (cm != null)
                    {
                        db.CompanyMembers.Remove(cm);
                        await db.SaveChangesAsync();
                    }
                }
            }


            return Ok(Mapper.Map<Company, CompanyProfileDTO>(user.Company));
        }

        [HttpPost]
        [Route("api/UpdateMyCompanySocialMediaLinks")]
        [Authorize]
        [ResponseType(typeof(CompanySocialMediaLinksDTO))]
        public async Task<IHttpActionResult> UpdateMyCompanySocialMediaLinks(CompanySocialMediaLinksDTO companySocialMediaLinksDTO)
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

            User user = db.Users.Find(companySocialMediaLinksDTO.OwnerID);

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if (authenticatedUser != user)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            user.Company = Mapper.Map(companySocialMediaLinksDTO, user.Company);
            db.Entry(user).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Ok(Mapper.Map<Company, CompanySocialMediaLinksDTO>(user.Company));
        }

        [HttpPost]
        [Route("api/UpdateMyCompanyEmailNotificationFrequencies")]
        [Authorize]
        [ResponseType(typeof(CompanyEmailNotificationFrequenciesDTO))]
        public async Task<IHttpActionResult> UpdateMyCompanyEmailNotificationFrequencies(CompanyEmailNotificationFrequenciesDTO companyEmailNotificationFrequenciesDTO)
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

            User user = db.Users.Find(companyEmailNotificationFrequenciesDTO.OwnerID);

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if (authenticatedUser != user)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            user.Company = Mapper.Map(companyEmailNotificationFrequenciesDTO, user.Company);
            db.Entry(user).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Ok(Mapper.Map<Company, CompanyEmailNotificationFrequenciesDTO>(user.Company));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        [Route("api/MessageToCompany")]
        [Authorize]
        //[ResponseType(typeof(CompanySocialMediaLinksDTO))]
        public async Task<IHttpActionResult> MessageToCompany(CompanyMessage companymessage)
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

            Company company = db.Companies.Find(companymessage.CompanyId);

            if (company == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            MessageToCompany MessageObj = new MessageToCompany();
            MessageObj.CompanyId = companymessage.CompanyId;
            MessageObj.Message = companymessage.Message;
            MessageObj.Date = DateTime.Now;
            MessageObj.LinkUrl = companymessage.LinkUrl;
            MessageObj.Image = companymessage.Image;
            MessageObj.UserId = authenticatedUser.Id;

            db.MessageToCompanies.Add(MessageObj);
            try
            {
                db.SaveChanges();
            }
            catch(Exception e)
            {
                
            }  

            return Ok();
            
        }

        [HttpPost]
        [Route("api/GetCompanyMessage")]
        [Authorize]
        public async Task<IHttpActionResult> GetCompanyMessage(MessageToCompany Company)
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

            Company company = db.Companies.Find(Company.CompanyId);

            if (company == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

           

            try
            {

                //var Messages = db.MessageToCompanies.ToList().Where(c=>c.CompanyId == Company.CompanyId).OrderByDescending(c=> c.MessageId);
                var query = (from p in db.MessageToCompanies
                            join s in db.Users on p.UserId equals s.Id where s.Id == p.UserId && p.CompanyId== Company.CompanyId
                             select new { s.FirstName, s.LastName, p.UserId, p.MessageId, p.LinkUrl, p.Image, p.Message, p.Date,p.CompanyId }).OrderByDescending(c => c.MessageId);
                            //select new
                            //{
                            //    ContactNo = to == null ? String.Empty : to.Table1.ContactNo
                            //};
                //var result = new { Messages, userName };
                return Ok(query);
            }
            catch(Exception e) {
                return Ok();
            }
            

        }

        private bool CompanyExists(long id)
        {
            return db.Companies.Count(e => e.ID == id) > 0;
        }

       
    }
}