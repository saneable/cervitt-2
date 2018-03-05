using AutoMapper;
using AutoMapper.QueryableExtensions;
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
using System.Web.Http;
using System.Web.Http.Description;

namespace KaribouAlpha.Controllers
{
    public class CustomersController : ApiController
    {
        private KaribouAlphaContext db = new KaribouAlphaContext();

        [HttpGet]
        [Route("api/GetMyProductCustomers")]
        [Authorize]
        public IEnumerable<CustomerNoLogoDTO> GetMyProductCustomers(long productId)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            Product product = db.Products.Where(_product => _product.ID == productId)
                                                                    .Include(_product => _product.Customers)
                                                                    .Include(_product => _product.TeamMembers)
                                                                    .SingleOrDefault();

            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if ((product.CompanyID != user.Company.ID) && (product.TeamMembers.Where(teamMember => teamMember.UserID == user.Id && (teamMember.CanEditTheProduct == true || (teamMember.UserLevelId.HasValue && teamMember.UserLevelId.Value > 0))).SingleOrDefault() == null))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            return Mapper.Map<IEnumerable<CustomerNoLogoDTO>>(product.Customers);
        }

        [HttpGet]
        [Route("api/GetProductCustomers")]
        [Authorize]
        public IQueryable<CustomerNoLogoDTO> GetProductCustomers(long productId)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            IQueryable<Customer> customers = db.Customers.Where(customer => 
                                                                                                    customer.ProductID == productId && 
                                                                                                    (customer.Product.Privacy == ProductPrivacy.Public || 
                                                                                                    (customer.Product.Privacy == ProductPrivacy.VisibleToSelectedGroups && 
                                                                                                    customer.Product.GroupsVisibleTo.Any(followerGroup => followerGroup.Followers.Any(follower => follower.User.Id == user.Id)))));

            return customers.ProjectTo<CustomerNoLogoDTO>();
        }

        [HttpPost]
        [Route("api/NewCustomer")]
        [Authorize]
        [ResponseType(typeof(CustomerDTO))]
        public async Task<IHttpActionResult> NewCustomer(NewCustomerDTO newCustomerDTO)
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

            Product product = db.Products.Find(newCustomerDTO.ProductID);

            if (product == null)
            {
                return NotFound();
            }

            if ((product.CompanyID != user.Company.ID) && (product.TeamMembers.Where(teamMember => teamMember.UserID == user.Id && teamMember.CanEditTheProduct == true).SingleOrDefault() == null))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            Customer customer = Mapper.Map<NewCustomerDTO, Customer>(newCustomerDTO);
            ProductUpdate productUpdate = new ProductUpdate();

            customer.Product = product;
            customer = db.Customers.Add(customer);
            productUpdate.UserID = user.Id;
            productUpdate.User = user;
            productUpdate.ProductID = product.ID;
            productUpdate.Product = product;
            productUpdate.DateTime = DateTime.Now;
            productUpdate.UpdateType = UpdateType.ProductCustomerAdded;
            db.ProductUpdates.Add(productUpdate);
            await db.SaveChangesAsync();

            return Ok(Mapper.Map<Customer, CustomerDTO>(customer));
        }

        [HttpGet]
        [Route("api/DeleteCustomer")]
        [Authorize]
        public async Task<IHttpActionResult> DeleteCustomer(long id)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            Customer customer = db.Customers.Where(_customer => _customer.ID == id).SingleOrDefault();

            if (customer == null)
            {
                return NotFound();
            }

            if ((customer.Product.CompanyID != user.Company.ID) && (customer.Product.TeamMembers.Where(teamMember => teamMember.UserID == user.Id && teamMember.CanEditTheProduct == true).SingleOrDefault() == null))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            ProductUpdate productUpdate = new ProductUpdate();

            productUpdate.UserID = user.Id;
            productUpdate.User = user;
            productUpdate.ProductID = customer.Product.ID;
            productUpdate.Product = customer.Product;
            productUpdate.DateTime = DateTime.Now;
            productUpdate.UpdateType = UpdateType.ProductCustomerDeleted;
            db.ProductUpdates.Add(productUpdate);
            db.Customers.Remove(customer);
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("api/GetCustomerLogo")]
        public async Task<IHttpActionResult> GetCustomerLogo(long id)
        {
            Customer customer = db.Customers.Where(_customer => _customer.ID == id).AsNoTracking().SingleOrDefault();

            if (customer == null)
            {
                return NotFound();
            }

            byte[] imageBytes;
            string mediaTypeHeader;

            if (customer.Logo != null)
            {
                imageBytes = customer.Logo;
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(long id)
        {
            return db.Customers.Count(e => e.ID == id) > 0;
        }
    }
}