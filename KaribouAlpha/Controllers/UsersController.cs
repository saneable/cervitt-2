using AutoMapper;
using AutoMapper.QueryableExtensions;
using FlowJs;
using FlowJs.Interface;
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
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace KaribouAlpha.Controllers
{
    public class UsersController : ApiController
    {
        private KaribouAlphaContext db = new KaribouAlphaContext();
        private string _tempFolderPath = Path.GetTempPath();
        private readonly IFlowJsRepo _flowJs;

        public UsersController()
        {
            this._flowJs = new FlowJsRepo();
        }

        [HttpGet]
        [Route("api/GetMyPersonalProfile")]
        [Authorize]
        [ResponseType(typeof(UserProfileDTO))]
        public async Task<IHttpActionResult> GetMyPersonalProfile()
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            return Ok(Mapper.Map<User, UserProfileDTO>(user));
        }

        [HttpGet]
        [Route("api/GetAllUsers")]
        [Authorize]
        [ResponseType(typeof(UserProfileDTO))]
        public async Task<IHttpActionResult> GetAllUsers()
        {
            string userName = User.Identity.Name;

            //List<User> user = db.Users.ToList();

            List<User> user = db.Users.ToList().Where(c => c.FirstName != null).ToList();

            return Ok(user.Select(c => new UserProfileDTO()  { FirstName = c.FirstName, LastName = c.LastName, ID = c.Id }));

        }

        [HttpPost]
        [Route("api/GetUserName")]
        [Authorize]
        [ResponseType(typeof(UserProfileDTO))]
        public async Task<IHttpActionResult> GetUserName(MessageToCompany Model)
        {


            //List<User> user = db.Users.ToList();

            var user = db.Users.Where(c => c.Id == Model.UserId);

            return Ok(user.Select(c => new UserProfileDTO() { FirstName = c.FirstName, LastName = c.LastName, ID = c.Id }));

        }

        [HttpGet]
        [Route("api/GetMyOnboardingStatus")]
        [Authorize]
        [ResponseType(typeof(UserOnboardingStatusDTO))]
        public async Task<IHttpActionResult> GetMyOnboardingStatus()
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            return Ok(Mapper.Map<User, UserOnboardingStatusDTO>(user));
        }

        [HttpPost]
        [Route("api/JumpToWizardStep")]
        [Authorize]
        [ResponseType(typeof(UserOnboardingStatusDTO))]
        public async Task<IHttpActionResult> JumpToWizardStep(UserOnboardingStatusDTO inputStep)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).SingleOrDefault();
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            if(!inputStep.OnboardingStep.HasValue)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            if(inputStep.OnboardingStep.HasValue && ( inputStep.OnboardingStep.Value < 0 || inputStep.OnboardingStep.Value > 4))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            user.OnboardingStep = inputStep.OnboardingStep.Value;
            db.Entry(user).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return Ok(Mapper.Map<User, UserOnboardingStatusDTO>(user));
        }

        [HttpPost]
        [Route("api/SetMyCurrentOnboardingStep")]
        [Authorize]
        [ResponseType(typeof(UserOnboardingStatusDTO))]
        public async Task<IHttpActionResult> SetMyCurrentOnboardingStep(UserOnboardingStatusDTO inputStep)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            if (!inputStep.OnboardingStep.HasValue)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            if (inputStep.OnboardingStep.HasValue && (inputStep.OnboardingStep.Value < 0 || inputStep.OnboardingStep.Value > 4))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            if (user.OnboardingSkipped == true)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            bool isPrev = false;
            bool showFirstStep = false;
            if (user.OnboardingStep.HasValue)
            {
                int diff = user.OnboardingStep.Value - inputStep.OnboardingStep.Value;
                if (!(diff == 1 || diff == -1 || (diff == -3 && inputStep.IsIndividual.HasValue && inputStep.IsIndividual.Value)))
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }

                if (diff > 0)
                {
                    isPrev = true;
                }
                if (diff == 1 && inputStep.OnboardingStep.Value == 3 && user.OnboardingStep.Value == 4 && user.IsIndividual.HasValue && user.IsIndividual.Value)
                {
                    showFirstStep = true;
                }

                if (showFirstStep)
                {
                    inputStep.OnboardingStep = 1;
                }
            }

            user.OnboardingStep = inputStep.OnboardingStep.Value;

            if ((inputStep.OnboardingStep.Value == 2 || inputStep.OnboardingStep.Value == 4) && !isPrev)
            {
                user.IsIndividual = false;
                if (inputStep.IsIndividual.HasValue && inputStep.IsIndividual.Value)
                {
                    user.IsIndividual = true;
                }
            }
            db.Entry(user).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Ok(Mapper.Map<User, UserOnboardingStatusDTO>(user));
        }



        [HttpGet]
        [Route("api/SkipOnboarding")]
        [Authorize]
        [ResponseType(typeof(UserOnboardingStatusDTO))]
        public async Task<IHttpActionResult> SkipOnboarding()
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            if (user.OnboardingSkipped == true)
            {
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            user.OnboardingSkipped = true;
            db.Entry(user).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Ok(Mapper.Map<User, UserOnboardingStatusDTO>(user));
        }

        [HttpGet]
        [Route("api/CompleteOnboarding")]
        [Authorize]
        [ResponseType(typeof(UserOnboardingStatusDTO))]
        public async Task<IHttpActionResult> CompleteOnboarding()
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            if (user.OnboardingSkipped == true)
            {
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            user.OnboardingSkipped = true;
            db.Entry(user).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Ok(Mapper.Map<User, UserOnboardingStatusDTO>(user));
        }

        [HttpGet]
        [Route("api/GetMySocialMediaLinks")]
        [Authorize]
        [ResponseType(typeof(UserSocialMediaLinksDTO))]
        public async Task<IHttpActionResult> GetMySocialMediaLinks()
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            return Ok(Mapper.Map<User, UserSocialMediaLinksDTO>(user));
        }

        [HttpGet]
        [Route("api/GetMyEmailNotificationFrequencies")]
        [Authorize]
        [ResponseType(typeof(UserEmailNotificationFrequenciesDTO))]
        public async Task<IHttpActionResult> GetMyEmailNotificationFrequencies()
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            return Ok(Mapper.Map<User, UserEmailNotificationFrequenciesDTO>(user));
        }

        [HttpPost]
        [Route("api/UpdateMyPersonalProfile")]
        [Authorize]
        [ResponseType(typeof(UserProfileDTO))]
        public async Task<IHttpActionResult> UpdateMyPersonalProfile(UserProfileDTO userProfileDTO)
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

            // User user = db.Users.Find(userProfileDTO.ID);
            userProfileDTO.ID = authenticatedUser.Id;

            User user = db.Users.Find(authenticatedUser.Id);

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if (authenticatedUser != user)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            Mapper.Map(userProfileDTO, user);

            db.Entry(user).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Ok(Mapper.Map<User, UserProfileDTO>(user));
        }

        [HttpPost]
        [Route("api/UpdateMySocialMediaLinks")]
        [Authorize]
        [ResponseType(typeof(UserSocialMediaLinksDTO))]
        public async Task<IHttpActionResult> UpdateMySocialMediaLinks(UserSocialMediaLinksDTO userSocialMediaLinksDTO)
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

            User user = db.Users.Find(userSocialMediaLinksDTO.ID);

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if (authenticatedUser != user)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            Mapper.Map(userSocialMediaLinksDTO, user);
            db.Entry(user).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Ok(Mapper.Map<User, UserSocialMediaLinksDTO>(user));
        }

        [HttpPost]
        [Route("api/UpdateMyEmailNotificationFrequencies")]
        [Authorize]
        [ResponseType(typeof(UserEmailNotificationFrequenciesDTO))]
        public async Task<IHttpActionResult> UpdateMyEmailNotificationFrequencies(UserEmailNotificationFrequenciesDTO userEmailNotificationFrequenciesDTO)
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

            User user = db.Users.Find(userEmailNotificationFrequenciesDTO.ID);

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if (authenticatedUser != user)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            Mapper.Map(userEmailNotificationFrequenciesDTO, user);
            db.Entry(user).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Ok(Mapper.Map<User, UserEmailNotificationFrequenciesDTO>(user));
        }

        [HttpPost]
        [Route("api/UpdateMyProfileImage")]
        [Authorize]
        [ResponseType(typeof(UserProfileImageEditDTO))]
        public async Task<IHttpActionResult> UpdateMyProfileImage(UserProfileImageEditDTO userProfileImageEditDTO)
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

            User user = db.Users.Find(userProfileImageEditDTO.ID);

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if (authenticatedUser != user)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            Mapper.Map(userProfileImageEditDTO, user);
            db.Entry(user).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Ok(Mapper.Map<User, UserProfileImageEditDTO>(user));
        }

        [HttpGet]
        [Authorize]
        [Route("api/GetMyProfileImage")]
        public async Task<IHttpActionResult> GetMyProfileImage()
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            byte[] imageBytes;
            string mediaTypeHeader;

            if (user.Image != null)
            {
                imageBytes = user.Image;
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
        [Route("api/GetUserProfileImage")]
        public async Task<IHttpActionResult> GetUserProfileImage(long userId)
        {
            User user = db.Users.Where(_user => _user.Id == userId).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            byte[] imageBytes;
            string mediaTypeHeader;

            if (user.Image != null)
            {
                imageBytes = user.Image;
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
        [Route("api/GetUserProfileImage")]
        public async Task<IHttpActionResult> GetUserProfileImage(String userName)
        {
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            byte[] imageBytes;
            string mediaTypeHeader;

            if (user.Image != null)
            {
                imageBytes = user.Image;
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

        [Authorize]
        [HttpGet]
        [Route("api/UserProfileImageUpload")]
        public async Task<IHttpActionResult> PictureUploadGet()
        {
            HttpRequest request = HttpContext.Current.Request;
            bool chunkExists = _flowJs.ChunkExists(this._tempFolderPath, request);

            if (chunkExists == true)
            {
                return Ok();
            }

            return ResponseMessage(new HttpResponseMessage(HttpStatusCode.NoContent));
        }

        [Authorize]
        [HttpPost]
        [Route("api/UserProfileImageUpload")]
        public async Task<IHttpActionResult> PictureUploadPost()
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            HttpRequest request = HttpContext.Current.Request;
            FlowValidationRules validationRules = new FlowValidationRules();

            validationRules.AcceptedExtensions.AddRange(new List<string> { "jpeg", "jpg", "png", "bmp" });
            validationRules.MaxFileSize = 5000000;

            try
            {
                FlowJsPostChunkResponse status = _flowJs.PostChunk(request, this._tempFolderPath, validationRules);

                if (status.Status == PostChunkStatus.Done)
                {
                    // file uploade is complete. Below is an example of further file handling
                    string filePath = Path.Combine(this._tempFolderPath, status.FileName);
                    byte[] imageBytes = File.ReadAllBytes(filePath);

                    user.Image = imageBytes;
                    File.Delete(filePath);
                    await db.SaveChangesAsync();

                    return Ok();
                }

                if (status.Status == PostChunkStatus.PartlyDone)
                {
                    return Ok();
                }

                status.ErrorMessages.ForEach(x => ModelState.AddModelError("file", x));

                return BadRequest(ModelState);
            }
            catch (Exception)
            {
                ModelState.AddModelError("file", "exception");

                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        [Route("api/SearchUsersForCompanyTeamMembers")]
        [Authorize]
        public List<UserFromSearchDTO> SearchUsersForCompanyTeamMembers(String query)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            if (user.IsIndividual.HasValue && user.IsIndividual.Value)
            {

            }
            else
            {
                if (user.Company == null)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }
            }

            if ((query == null) || (query.Length < 2))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            IQueryable<User> users = db.Users.Where(_user => (_user.Id != user.Id) && (_user.CompaniesAsMembers.Any(companyMember => companyMember.CompanyID == user.Company.ID) == false) && (_user.FirstName.StartsWith(query) || _user.LastName.StartsWith(query) || _user.UserName.StartsWith(query))).AsNoTracking();

            IQueryable<User> companyUsers = db.Companies.Where(c => (c.ID != user.Id) && (c.DisplayName.StartsWith(query) || c.TradingName.StartsWith(query))).AsNoTracking().Select(c => c.Owner).Distinct();

            List<UserFromSearchDTO> searchUsers = users.Select(u => new UserFromSearchDTO() { FirstName = u.FirstName, LastName = u.LastName, UserID = u.Id, UserName = u.UserName }).ToList();
            List<UserFromSearchDTO> usersFromCompany = companyUsers.Where(u => u.CompaniesAsMembers.Any(companyMember => companyMember.CompanyID == user.Company.ID) == false).Select(u => new UserFromSearchDTO() { FirstName = u.FirstName, LastName = u.LastName, UserID = u.Id, UserName = u.UserName }).ToList();
            searchUsers = searchUsers.Union(usersFromCompany, new UserFromSearchCompare()).ToList();

            IQueryable<User> productUsers = db.Products.Where(c => c.CompanyID.HasValue && c.CompanyID.Value != user.Company.ID && c.Name.StartsWith(query)).Select(c => c.Company.Owner).Distinct();
            List<UserFromSearchDTO> usersFromProduct = productUsers.Where(u => u.CompaniesAsMembers.Any(companyMember => companyMember.CompanyID == user.Company.ID) == false).Select(u => new UserFromSearchDTO() { FirstName = u.FirstName, LastName = u.LastName, UserID = u.Id, UserName = u.UserName }).ToList();
            searchUsers = searchUsers.Union(usersFromProduct, new UserFromSearchCompare()).ToList();
   
            return searchUsers;
        }

        [HttpGet]
        [Route("api/GetMyNotificationCounters")]
        [Authorize]
        [ResponseType(typeof(UserNotificationCountersDTO))]
        public async Task<IHttpActionResult> GetMyNotificationCounters()
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            UserNotificationCountersDTO userNotificationsDTO = new UserNotificationCountersDTO();

            userNotificationsDTO.NewNewsItems = db.NewsItems.Where(newsItem =>
                                                                                                            (newsItem.PostedAt > user.LastCheckForNewsItems) &&
                                                                                                            ((newsItem.UserID == user.Id) ||
                                                                                                            (newsItem.User.CompaniesAsMembers.Any(companyAsMember => companyAsMember.CompanyID == user.Company.ID)) ||
                                                                                                            ((newsItem.Product != null) && ((newsItem.Product.Company.Owner.Id == user.Id) || (newsItem.Product.Company.Members.Any(companyMember => companyMember.UserID == user.Id)))) ||
                                                                                                            (newsItem.User.Company.Followers.Any(follower => follower.UserID == user.Id)) ||
                                                                                                            (newsItem.User.CompaniesAsMembers.Any(companyAsMember => companyAsMember.Company.Followers.Any(follower => follower.UserID == user.Id))) ||
                                                                                                            ((newsItem.Product != null) && 
                                                                                                            ((newsItem.Product.Privacy == ProductPrivacy.Public) ||
                                                                                                            (newsItem.Product.Privacy == ProductPrivacy.VisibleToSelectedGroups && newsItem.Product.GroupsVisibleTo.Any(followerGroup => followerGroup.Followers.Any(follower => follower.UserID == user.Id)))))))
                                                                                                            .Count();
            userNotificationsDTO.NewFeedback = db.Feedback.Where(feedback =>
                                                                                                        (feedback.PostedAt > user.LastCheckForFeedback) &&
                                                                                                        ((feedback.ProductFile.Product.CompanyID == user.Company.ID) ||
                                                                                                        (feedback.ProductFile.Product.TeamMembers.Any(teamMember => teamMember.UserID == user.Id && teamMember.CanEditTheProduct == true)) ||
                                                                                                        ((feedback.ProductFile.Product.Privacy == ProductPrivacy.Public) ||
                                                                                                        ((feedback.ProductFile.Product.Privacy == ProductPrivacy.VisibleToSelectedGroups) && 
                                                                                                        (feedback.ProductFile.Product.GroupsVisibleTo.Any(followerGroup => followerGroup.Followers.Any(follower => follower.UserID == user.Id))))) ||
                                                                                                        ((feedback.ProductFile.Privacy == ProductFilePrivacy.Public) ||
                                                                                                        ((feedback.ProductFile.Privacy == ProductFilePrivacy.VisibleToSelectedGroups) && 
                                                                                                        (feedback.ProductFile.GroupsVisibleTo.Any(followerGroup => followerGroup.Followers.Any(follower => follower.UserID == user.Id)))))))
                                                                                                        .Count();
            userNotificationsDTO.NewProductUpdates = db.ProductUpdates.Where(productUpdate =>
                                                                                                    (productUpdate.DateTime > user.LastCheckForProductUpdates) &&
                                                                                                    ((productUpdate.Product.Company.Owner.Id == user.Id) ||
                                                                                                    (productUpdate.Product.TeamMembers.Any(teamMember => teamMember.UserID == user.Id)) ||
                                                                                                    (productUpdate.Product.Company.Followers.Any(follower => follower.UserID == user.Id) &&
                                                                                                    ((productUpdate.ProductFile == null && 
                                                                                                    ((productUpdate.Product.Privacy == ProductPrivacy.Public) || 
                                                                                                    ((productUpdate.Product.Privacy == ProductPrivacy.VisibleToSelectedGroups) && 
                                                                                                    productUpdate.Product.GroupsVisibleTo.Any(followerGroup => followerGroup.Followers.Any(follower => follower.UserID == user.Id))))) ||
                                                                                                    (productUpdate.ProductFile != null && 
                                                                                                    ((productUpdate.ProductFile.Privacy == ProductFilePrivacy.Public) || 
                                                                                                    ((productUpdate.ProductFile.Privacy == ProductFilePrivacy.VisibleToSelectedGroups) && 
                                                                                                    productUpdate.ProductFile.GroupsVisibleTo.Any(followerGroup => followerGroup.Followers.Any(follower => follower.UserID == user.Id)))))))))
                                                                                                    .Count();

            if(user.Company != null)
            {
                userNotificationsDTO.NewConnectionRequests = db.CompanyConnections.Where(companyConnection =>
                                                                                                                                                        companyConnection.Status == ConnectionStatus.Pending &&
                                                                                                                                                        companyConnection.AcceptedAt > user.LastCheckForConnectionRequests &&
                                                                                                                                                        companyConnection.CompanyID == user.Company.ID)
                                                                                                                                                        .Count();
            }
            else
            {
                userNotificationsDTO.NewConnectionRequests = 0;
            }

            return Ok(userNotificationsDTO);
        }

        [Authorize]
        [HttpPost]
        [Route("api/LinkCompanyMember")]
        public async Task<IHttpActionResult> LinkCompanyMember([FromBody]int id)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).SingleOrDefault();
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            Company company = await db.Companies.SingleOrDefaultAsync(c => c.ID == id);

            if (company != null)
            {
                if (!user.OnboardingSkipped)
                {
                    CompanyMember existingLinkedCompany = await db.CompanyMembers.Where(cmu => cmu.UserID == user.Id).FirstOrDefaultAsync();
                    if (existingLinkedCompany != null)
                    {
                        db.CompanyMembers.Remove(existingLinkedCompany);
                        await db.SaveChangesAsync();
                    }
                }

                if (!db.CompanyMembers.Any(cm => cm.UserID == user.Id && cm.CompanyID == company.ID))
                {
                    UserLevel editorUserLevel = await db.UserLevels.SingleOrDefaultAsync(ul => ul.Name == "Editor");

                    CompanyMember companyMember = new CompanyMember();
                    companyMember.CompanyID = company.ID;
                    companyMember.UserID = user.Id;
                    companyMember.Role = "";
                    companyMember.Email = "";
                    if(editorUserLevel != null)
                          companyMember.UserLevelId = editorUserLevel.Id;
                    

                    db.CompanyMembers.Add(companyMember);
                    await db.SaveChangesAsync();

                    if(!user.OnboardingSkipped)
                    {
                        if (user.Company != null)
                        {
                            Company cmp =  await db.Companies.Where(cmu => cmu.ID == user.Id).FirstOrDefaultAsync();
                            if (cmp != null)
                            {
                                try
                                {
                                    List<CompanySkill> skills = db.CompanySkills.Where(cmsk => cmsk.CompanyId == cmp.ID).ToList();
                                    if(skills != null)
                                    {
                                        db.CompanySkills.RemoveRange(skills);
                                        await db.SaveChangesAsync();
                                    }
                                    user.Company = null;
                                    db.Companies.Remove(cmp);
                                    await db.SaveChangesAsync();
                                }
                                catch(Exception ex)
                                {

                                }
                            }
                        }
                    }
                    return Ok();
                }
                else
                {
                    throw new HttpResponseException(HttpStatusCode.Forbidden);
                }
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(long id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}