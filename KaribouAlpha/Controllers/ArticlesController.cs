using AutoMapper;
using AutoMapper.QueryableExtensions;
using KaribouAlpha.DAL;
using KaribouAlpha.Models;
using PagedList;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace KaribouAlpha.Controllers
{
    public class ArticlesController : ApiController
    {
        private KaribouAlphaContext db = new KaribouAlphaContext();
        private const String SENDGRID_API_KEY = @"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";

        [HttpPost]
        [Route("api/NewNewsItem")]
        [Authorize]
        [ResponseType(typeof(NewsItemDTO))]
        public async Task<IHttpActionResult> NewNewsItem(NewNewsItemDTO newNewsItemDTO)
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

            Product product = null;

            if (newNewsItemDTO.ProductID != null)
            {
                product = db.Products.Where(_product => _product.ID == newNewsItemDTO.ProductID)
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
            }

            NewsItem newsItem = Mapper.Map<NewNewsItemDTO, NewsItem>(newNewsItemDTO);

            if (product != null)
            {
                newsItem.ProductID = product.ID;
                newsItem.Product = product;
            }

            newsItem.UserID = user.Id;
            newsItem.User = user;
            newsItem.PostedAt = DateTime.Now;
            newsItem.UpdatedAt = DateTime.Now;
            newsItem = db.NewsItems.Add(newsItem);
            await db.SaveChangesAsync();

            return Ok(Mapper.Map<NewsItem, NewsItemDTO>(newsItem));
        }

        [HttpPost]
        [Route("api/NewNewsItemFeedback")]
        [Authorize]
        [ResponseType(typeof(NewsItemFeedbackDTO))]
        public async Task<IHttpActionResult> NewNewsItemFeedback(NewNewsItemFeedbackDTO newNewsItemFeedbackDTO)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).Include(_user => _user.Company).SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            NewsItem newsItem = db.NewsItems.Where(_newsItem => _newsItem.ID == newNewsItemFeedbackDTO.NewsItemID)
                                                .Include(_newsItem => _newsItem.Product.TeamMembers
                                                    .Select(teamMember => teamMember.User)
                                                    .Select(teamMember => teamMember.Company))
                                                .Include(_newsItem => _newsItem.User.Company.Followers)
                                                .Include(_newsItem => _newsItem.User.CompaniesAsMembers)
                                                .SingleOrDefault();

            if (newsItem == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if (newsItem.Product != null)
            {
                if ((newsItem.Product.CompanyID != user.Company.ID) && 
                    (newsItem.Product.TeamMembers.Where(teamMember => teamMember.UserID == user.Id && 
                    teamMember.CanEditTheProduct == true).SingleOrDefault() == null) && 
                    (newsItem.Product.Privacy == ProductPrivacy.Private) ||
                    ((newsItem.Product.Privacy == ProductPrivacy.VisibleToSelectedGroups) && 
                    (newsItem.Product.GroupsVisibleTo.Any(followerGroup => followerGroup.Followers.Any(follower => follower.UserID == user.Id)) == false)))
                {
                    throw new HttpResponseException(HttpStatusCode.Unauthorized);
                }
            }

            if ((newsItem.UserID != user.Id) && (newsItem.User.Company.Members.Where(member => member.UserID == user.Id).SingleOrDefault() == null) && (newsItem.User.Company.Followers.Where(follower => follower.UserID == user.Id).SingleOrDefault() == null) && (newsItem.User.CompaniesAsMembers.Where(companyAsMember => companyAsMember.Company.Followers.Any(follower => follower.UserID == user.Id)).SingleOrDefault() == null))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            NewsItemFeedback newsItemFeedback = Mapper.Map<NewNewsItemFeedbackDTO, NewsItemFeedback>(newNewsItemFeedbackDTO);

            newsItemFeedback.UserID = user.Id;
            newsItemFeedback.User = user;
            newsItemFeedback.NewsItemID = newsItem.ID;
            newsItemFeedback.NewsItem = newsItem;
            newsItemFeedback.PostedAt = DateTime.Now;
            newsItemFeedback = db.NewsItemFeedback.Add(newsItemFeedback);
            newsItem.UpdatedAt = DateTime.Now;
            db.Entry(newsItem).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Ok(Mapper.Map<NewsItemFeedback, NewsItemFeedbackDTO>(newsItemFeedback));
        }

        [HttpPost]
        [Route("api/NewProductFileFeedback")]
        [Authorize]
        [ResponseType(typeof(FeedbackDTO))]
        public async Task<IHttpActionResult> NewProductFileFeedback(NewFeedbackDTO newFeedbackDTO)
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

            ProductFile productFile = db.ProductFiles.Where(_productFile => _productFile.ID == newFeedbackDTO.ProductFileID)
                                                .Include(_productFile => _productFile.GroupsVisibleTo)
                                                .Include(_productFile => _productFile.Product.GroupsVisibleTo)
                                                .Include(_productFile => _productFile.Product.TeamMembers
                                                    .Select(_teamMember => _teamMember.User))
                                                .Include(_productFile => _productFile.Product.Company)
                                                .Include(_productFile => _productFile.Product.Company.Owner)
                                                .SingleOrDefault();

            if (productFile == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if ((productFile.Product.CompanyID != user.Company.ID) &&
                (productFile.Product.TeamMembers.Any(teamMember => teamMember.UserID == user.Id && teamMember.CanEditTheProduct == true)) &&
                ((productFile.Product.Privacy == ProductPrivacy.Private) || 
                (productFile.Product.Privacy == ProductPrivacy.VisibleToSelectedGroups && 
                productFile.Product.GroupsVisibleTo.Any(followerGroup => followerGroup.Followers.Any(follower => follower.UserID == user.Id)) == false)) &&
                ((productFile.Privacy == ProductFilePrivacy.Private) ||
                (productFile.Privacy == ProductFilePrivacy.VisibleToSelectedGroups && 
                productFile.GroupsVisibleTo.Any(followerGroup => followerGroup.Followers.Any(follower => follower.UserID == user.Id)) == false)))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            ProductFileFeedback replyTo = null;

            if (newFeedbackDTO.ReplyToID != null)
            {
                replyTo = db.Feedback.Where(_feedback => _feedback.ID == newFeedbackDTO.ReplyToID).SingleOrDefault();

                if (replyTo == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
            }

            ProductFileFeedback feedback = Mapper.Map<NewFeedbackDTO, ProductFileFeedback>(newFeedbackDTO);

            if (replyTo != null)
            {
                feedback.ReplyToID = replyTo.ID;
                feedback.ReplyTo = replyTo;
                replyTo.UpdatedAt = DateTime.Now;
                db.Entry(replyTo).State = EntityState.Modified;
            }

            feedback.UserID = user.Id;
            feedback.User = user;
            feedback.ProductFileID = productFile.ID;
            feedback.ProductFile = productFile;
            feedback.PostedAt = DateTime.Now;
            feedback.UpdatedAt = DateTime.Now;
            feedback = db.Feedback.Add(feedback);
            await db.SaveChangesAsync();

            string feedbackAuthorUserName = user.UserName;

            if(user.FirstName != null || user.LastName != null)
            {
                feedbackAuthorUserName = (user.FirstName == null ? "" : user.FirstName) + " " + (user.LastName == null ? "" : user.LastName);
            }

            string feedbackAuthorCompanyName = user.Company == null ? "" : user.Company.DisplayName;
            string apiKey = SENDGRID_API_KEY;
            SendGridClient sendGridClient = new SendGridClient(apiKey, "https://api.sendgrid.com");
            EmailAddress emailSender = new EmailAddress("noreply@cervitt.co.uk", "Cervitt");
            String subject = "Feedback notification";
            EmailAddress emailRecipient = new EmailAddress(productFile.Product.Company.Owner.Email);
            Content content = new Content("text/html", "Hello world!");
            SendGridMessage mail = MailHelper.CreateSingleEmail(emailSender, emailRecipient, subject, "", "");

            mail.TemplateId = "976c0e75-4105-4f08-b924-aefb27bf44e8";
            mail.AddSubstitution("<%companyName%>", productFile.Product.Company.DisplayName);
            mail.AddSubstitution("<%productName%>", productFile.Product.Name);
            mail.AddSubstitution("<%feedbackAuthorUserId%>", user.Id.ToString());
            mail.AddSubstitution("<%feedbackAuthorUserName%>", feedbackAuthorUserName);
            mail.AddSubstitution("<%feedbackAuthorUserTitle%>", user.JobTitle);
            mail.AddSubstitution("<%feedbackAuthorCompanyName%>", feedbackAuthorCompanyName);
            mail.AddSubstitution("<%feedbackTitle%>", feedback.Title);
            mail.AddSubstitution("<%feedbackBody%>", feedback.Body);

            dynamic response = sendGridClient.SendEmailAsync(mail);

            return Ok(Mapper.Map<ProductFileFeedback, FeedbackDTO>(feedback));
        }

        [HttpGet]
        [Route("api/GetNewsItemImage")]
        public async Task<IHttpActionResult> GetNewsItemImage(long newsItemId)
        {
            NewsItem newsItem = db.NewsItems.Where(_newsItem => _newsItem.ID == newsItemId).SingleOrDefault();

            if (newsItem == null)
            {
                return NotFound();
            }

            byte[] imageBytes;
            string mediaTypeHeader;

            if (newsItem.Image != null)
            {
                imageBytes = newsItem.Image;
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
        [Route("api/GetNewsItemFeedbackImage")]
        public async Task<IHttpActionResult> GetNewsItemFeedbackImage(long newsItemFeedbackId)
        {
            NewsItemFeedback newsItemFeedback = db.NewsItemFeedback.Where(_newsItemFeedback => _newsItemFeedback.ID == newsItemFeedbackId).SingleOrDefault();

            if (newsItemFeedback == null)
            {
                return NotFound();
            }

            byte[] imageBytes;
            string mediaTypeHeader;

            if (newsItemFeedback.Image != null)
            {
                imageBytes = newsItemFeedback.Image;
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
        [Route("api/GetProductFileFeedbackImage")]
        public async Task<IHttpActionResult> GetProductFileFeedbackImage(long productFileFeedbackId)
        {
            ProductFileFeedback productFileFeedback = db.Feedback.Where(_productFileFeedback => _productFileFeedback.ID == productFileFeedbackId).SingleOrDefault();

            if (productFileFeedback == null)
            {
                return NotFound();
            }

            byte[] imageBytes;
            string mediaTypeHeader;

            if (productFileFeedback.Image != null)
            {
                imageBytes = productFileFeedback.Image;
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
        [Route("api/GetLatestNewsItemsPaged")]
        [Authorize]
        public async Task<IEnumerable<NewsItemDTO>> GetLatestNewsItemsPaged(int pageNumber, int pageSize)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).Include(_user => _user.Company).SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            if (pageNumber <= 0 || pageSize <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            IQueryable<NewsItem> newsItems = db.NewsItems.Where(newsItem =>
                                                                                                                    (newsItem.UserID == user.Id) ||
                                                                                                                    (newsItem.User.CompaniesAsMembers.Any(companyAsMember => companyAsMember.CompanyID == user.Company.ID)) ||
                                                                                                                    ((newsItem.Product != null) && ((newsItem.Product.Company.Owner.Id == user.Id) || (newsItem.Product.Company.Members.Any(companyMember => companyMember.UserID == user.Id)))) ||
                                                                                                                    (newsItem.User.Company.Followers.Any(follower => follower.UserID == user.Id)) ||
                                                                                                                    (newsItem.User.CompaniesAsMembers.Any(companyAsMember => companyAsMember.Company.Followers.Any(follower => follower.UserID == user.Id))) ||
                                                                                                                    ((newsItem.Product != null) && 
                                                                                                                    ((newsItem.Product.Privacy == ProductPrivacy.Public) || 
                                                                                                                    (newsItem.Product.Privacy == ProductPrivacy.VisibleToSelectedGroups && 
                                                                                                                    newsItem.Product.GroupsVisibleTo.Any(followerGroup => followerGroup.Followers.Any(follower => follower.UserID == user.Id))))))
                                                                                                                    .Include(newsItem => newsItem.Feedback
                                                                                                                        .Select(feedback => feedback.User))
                                                                                                                    .OrderByDescending(productUpdate => productUpdate.UpdatedAt);
            IEnumerable<NewsItemDTO> newsItemDtos = Mapper.Map<IEnumerable<NewsItemDTO>>(newsItems.ToPagedList(pageNumber, pageSize).ToList());

            foreach (NewsItemDTO newsItemDto in newsItemDtos)
            {
                newsItemDto.IsNew = (user.LastCheckForNewsItems == null) || (user.LastCheckForNewsItems < newsItemDto.PostedAt);
            }

            user.LastCheckForNewsItems = DateTime.Now;
            db.Entry(user).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return newsItemDtos;
        }

        [HttpGet]
        [Route("api/GetProductFileFeedback")]
        [Authorize]
        public async Task<IEnumerable<FeedbackDTO>> GetProductFileFeedback(long productFileId)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            ProductFile productFile = db.ProductFiles.Where(_productFile => _productFile.ID == productFileId)
                                                .Include(_productFile => _productFile.GroupsVisibleTo)
                                                .Include(_productFile => _productFile.Product.GroupsVisibleTo)
                                                .Include(_productFile => _productFile.Product.TeamMembers
                                                    .Select(_teamMember => _teamMember.User))
                                                .Include(_productFile => _productFile.Product.Company)
                                                .Include(_productFile => _productFile.Feedback
                                                    .Select(_feedback => _feedback.Replies))
                                                .SingleOrDefault();

            if (productFile == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if ((productFile.Product.CompanyID != user.Company.ID) &&
                (productFile.Product.TeamMembers.Where(teamMember => teamMember.UserID == user.Id && teamMember.CanEditTheProduct == true).SingleOrDefault() == null) &&
                ((productFile.Product.Privacy == ProductPrivacy.Private) ||
                (productFile.Product.Privacy == ProductPrivacy.VisibleToSelectedGroups && 
                productFile.Product.GroupsVisibleTo.Any(followerGroup => followerGroup.Followers.Any(follower => follower.UserID == user.Id)) == false)) &&
                ((productFile.Privacy == ProductFilePrivacy.Private) ||
                (productFile.Privacy == ProductFilePrivacy.VisibleToSelectedGroups && 
                productFile.GroupsVisibleTo.Any(followerGroup => followerGroup.Followers.Any(follower => follower.UserID == user.Id)) == false)))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            return Mapper.Map<IEnumerable<FeedbackDTO>>(productFile.Feedback.OrderByDescending(_feedback => _feedback.PostedAt));
        }

        [HttpGet]
        [Route("api/GetLatestProductFileFeedbackPaged")]
        [Authorize]
        public async Task<IEnumerable<FeedbackDTO>> GetLatestProductFileFeedbackPaged(int pageNumber, int pageSize)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            IQueryable<ProductFileFeedback> productFileFeedback = db.Feedback.Where(feedback =>
                                                    feedback.ReplyToID == null &&
                                                    ((feedback.ProductFile.Product.CompanyID == user.Company.ID) ||
                                                    (feedback.ProductFile.Product.TeamMembers.Any(teamMember => teamMember.UserID == user.Id && teamMember.CanEditTheProduct == true)) ||
                                                    (((feedback.ProductFile.Product.Privacy == ProductPrivacy.Public) ||
                                                    (feedback.ProductFile.Product.Privacy == ProductPrivacy.VisibleToSelectedGroups && 
                                                    feedback.ProductFile.Product.GroupsVisibleTo.Any(followerGroup => followerGroup.Followers.Any(follower => follower.UserID == user.Id)))) &&
                                                    ((feedback.ProductFile.Privacy == ProductFilePrivacy.Public) ||
                                                    (feedback.ProductFile.Privacy == ProductFilePrivacy.VisibleToSelectedGroups && 
                                                    feedback.ProductFile.GroupsVisibleTo.Any(followerGroup => followerGroup.Followers.Any(follower => follower.UserID == user.Id)))))))
                                                .OrderByDescending(feedback => feedback.UpdatedAt);
            IEnumerable<FeedbackDTO> productFileFeedbackDtos = Mapper.Map<IEnumerable<FeedbackDTO>>(productFileFeedback.ToPagedList(pageNumber, pageSize).ToList());

            foreach (FeedbackDTO productFileFeedbackDto in productFileFeedbackDtos)
            {
                productFileFeedbackDto.IsNew = (user.LastCheckForFeedback == null) || (user.LastCheckForFeedback < productFileFeedbackDto.PostedAt);
            }

            user.LastCheckForFeedback = DateTime.Now;
            db.Entry(user).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return productFileFeedbackDtos;
        }

        [HttpGet]
        [Route("api/GetProductFileFeedbackReplies")]
        [Authorize]
        public IEnumerable<FeedbackDTO> GetProductFileFeedbackReplies(long productFileFeedbackId)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            ProductFileFeedback productFileFeedback = db.Feedback.Where(_productFileFeedback => _productFileFeedback.ID == productFileFeedbackId)
                                                .Include(_productFileFeedback => _productFileFeedback.ProductFile.GroupsVisibleTo)
                                                .Include(_productFileFeedback => _productFileFeedback.ProductFile.Product.GroupsVisibleTo)
                                                .Include(_productFileFeedback => _productFileFeedback.Replies)
                                                .SingleOrDefault();

            if (productFileFeedback == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if ((productFileFeedback.ProductFile.Product.Privacy == ProductPrivacy.Private) ||
                (productFileFeedback.ProductFile.Product.Privacy == ProductPrivacy.VisibleToSelectedGroups && 
                productFileFeedback.ProductFile.Product.GroupsVisibleTo.Any(followerGroup => followerGroup.Followers.Any(follower => follower.UserID == user.Id)) == false) ||
                (productFileFeedback.ProductFile.Privacy == ProductFilePrivacy.Private) ||
                (productFileFeedback.ProductFile.Privacy == ProductFilePrivacy.VisibleToSelectedGroups &&
                productFileFeedback.ProductFile.GroupsVisibleTo.Any(followerGroup => followerGroup.Followers.Any(follower => follower.UserID == user.Id)) == false))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            /*
            IEnumerable<ProductFile> productFiles = product.ProductFiles.Where(productFile => productFile.Privacy == ProductFilePrivacy.Public || productFile.Viewers.Any(viewer => viewer.UserID == user.Id))
                                                                                                                                .OrderBy(productFile => productFile.Category)
                                                                                                                                .ThenByDescending(productFile => productFile.UploadedAt);
            */
            return Mapper.Map<IEnumerable<FeedbackDTO>>(productFileFeedback.Replies.OrderByDescending(reply => reply.PostedAt));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArticleExists(long id)
        {
            return db.Articles.Count(e => e.ID == id) > 0;
        }
    }
}