using KaribouAlpha.DAL;
using KaribouAlpha.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using AutoMapper;
using System.Web.Http.Description;
using System.Threading.Tasks;

namespace KaribouAlpha.Controllers
{
    public class ProductTeamMembersController : ApiController
    {
        private KaribouAlphaContext db = new KaribouAlphaContext();

        [HttpGet]
        [Route("api/GetProductTeamMembersForEditing")]
        [Authorize]
        public IEnumerable<ProductTeamMemberDTO> GetProductTeamMembersForEditing(long productId)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

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

            return Mapper.Map<IEnumerable<ProductTeamMember>, IEnumerable<ProductTeamMemberDTO>>(product.TeamMembers);
        }

        [HttpGet]
        [Route("api/GetProductSelectedAsTeamMember")]
        [Authorize]
        public async Task<List<long>> GetProductSelectedAsTeamMember(long userId, long userLevelId)
        {
            List<long> productIds = new List<long>();
            string userName = User.Identity.Name;
            User authUser = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();
            if (authUser == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            User teamMemberUser =  await db.Users.Where(u => u.Id == userId).AsNoTracking().SingleOrDefaultAsync();
            if (teamMemberUser == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            UserLevel userLevel =  db.UserLevels.SingleOrDefault(c => c.Id == userLevelId);
            bool isAdmin = false;
            if (userLevel.Name.ToLower() == "admin")
                isAdmin = true;

            if (isAdmin)
            {
                productIds = await db.ProductTeamMembers.Where(ptm => ptm.UserID == teamMemberUser.Id && ptm.CanEditTheProduct).Select(ptm => ptm.ProductID).ToListAsync();
            }
            else
            {
                productIds = await db.ProductTeamMembers.Where(ptm => ptm.UserID == teamMemberUser.Id && ptm.UserLevelId.HasValue && ptm.UserLevelId.Value == userLevelId).Select(ptm => ptm.ProductID).ToListAsync();
            }
            
            return productIds;
        }


        [HttpPost]
        [Route("api/AddNewProductTeamMember")]
        [Authorize]
        [ResponseType(typeof(ProductTeamMemberDTO))]
        public async Task<IHttpActionResult> AddNewProductTeamMember(NewProductTeamMemberDTO newProductTeamMemberDTO)
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

            Product product = db.Products.Where(_product => _product.ID == newProductTeamMemberDTO.ProductID)
                                                .Include(_product => _product.TeamMembers
                                                    .Select(teamMember => teamMember.User)
                                                    .Select(teamMember => teamMember.Company))
                                                .SingleOrDefault();

            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if ((product.CompanyID != authenticatedUser.Company.ID) && (product.TeamMembers.Where(teamMember => teamMember.UserID == authenticatedUser.Id && teamMember.CanEditTheProduct == true).SingleOrDefault() == null))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            User productTeamMemberUser = db.Users.Where(_user => _user.Id == newProductTeamMemberDTO.UserID)
                                                                                .Include(_user => _user.CompaniesAsMembers)
                                                                                .Include(_user => _user.ProductTeamMembers)
                                                                                .SingleOrDefault();

            if (productTeamMemberUser == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Company company = product.Company;

            if (company == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            CompanyMember companyMember = productTeamMemberUser.CompaniesAsMembers.Where(_companyMember => _companyMember.CompanyID == company.ID).SingleOrDefault();

            if (companyMember == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            ProductTeamMember productTeamMember = productTeamMemberUser.ProductTeamMembers.Where(_productTeamMember => _productTeamMember.ProductID == newProductTeamMemberDTO.ProductID).SingleOrDefault();

            if (productTeamMember != null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            productTeamMember = new ProductTeamMember();
            productTeamMember.UserID = productTeamMemberUser.Id;
            productTeamMember.ProductID = product.ID;
            productTeamMember.User = productTeamMemberUser;
            productTeamMember.Product = product;
            productTeamMember.CanEditTheProduct = false;
            productTeamMember = db.ProductTeamMembers.Add(productTeamMember);
            await db.SaveChangesAsync();

            return Ok(Mapper.Map<ProductTeamMember, ProductTeamMemberDTO>(productTeamMember));
        }

        [HttpPost]
        [Route("api/RemoveProductTeamMember")]
        [Authorize]
        public async Task<IHttpActionResult> RemoveProductTeamMember(ProductTeamMemberDTO productTeamMemberDTO)
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

            Product product = db.Products.Where(_product => _product.ID == productTeamMemberDTO.ProductID)
                                                .Include(_product => _product.TeamMembers
                                                    .Select(teamMember => teamMember.User))
                                                .SingleOrDefault();

            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if ((product.CompanyID != user.Company.ID) && (product.TeamMembers.Where(teamMember => teamMember.UserID == user.Id && teamMember.CanEditTheProduct == true).SingleOrDefault() == null))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            ProductTeamMember productTeamMember = product.TeamMembers.Where(_productTeamMember => _productTeamMember.UserID == productTeamMemberDTO.UserID).SingleOrDefault();

            if (productTeamMember == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            db.ProductTeamMembers.Remove(productTeamMember);
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("api/RemoveProductTeamMemberMapping")]
        [Authorize]
        public async Task<IHttpActionResult> RemoveProductTeamMemberMapping(ProductTeamMemberDTO productTeamMemberDTO)
        {
            string userName = User.Identity.Name;

            User authUser = db.Users.Where(_user => _user.UserName == userName).SingleOrDefault();
            if (authUser == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(productTeamMemberDTO.ProductID == 0 && productTeamMemberDTO.UserID > 0)
            {
                User user =  await db.Users.Where(u => u.Id == productTeamMemberDTO.UserID).SingleOrDefaultAsync();
                if (user != null)
                {
                    db.ProductTeamMembers.RemoveRange(db.ProductTeamMembers.Where(u => u.UserID == productTeamMemberDTO.UserID));
                    await db.SaveChangesAsync();
                }
            }
            return Ok();
        }

        [HttpPost]
        [Route("api/UpdateProductTeamMember")]
        [Authorize]
        [ResponseType(typeof(ProductTeamMemberDTO))]
        public async Task<IHttpActionResult> UpdateProductTeamMember(ProductTeamMemberUpdateDTO productTeamMemberUpdateDTO)
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

            Product product = db.Products.Where(_product => _product.ID == productTeamMemberUpdateDTO.ProductID)
                                                .Include(_product => _product.TeamMembers
                                                    .Select(teamMember => teamMember.User))
                                                .SingleOrDefault();

            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if ((product.CompanyID != user.Company.ID) && (product.TeamMembers.Where(teamMember => teamMember.UserID == user.Id && teamMember.CanEditTheProduct == true).SingleOrDefault() == null))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            ProductTeamMember productTeamMember = product.TeamMembers.Where(_productTeamMember => _productTeamMember.UserID == productTeamMemberUpdateDTO.UserID).SingleOrDefault();

            if (productTeamMember == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            productTeamMember.Role = productTeamMemberUpdateDTO.Role;
            productTeamMember.CanEditTheProduct = productTeamMemberUpdateDTO.CanEditTheProduct;
            db.Entry(productTeamMember).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Ok(Mapper.Map<ProductTeamMember, ProductTeamMemberDTO>(productTeamMember));
        }

        [HttpPost]
        [Route("api/UpdateProductTeamMemberMutiple")]
        [Authorize]
        public async Task<IHttpActionResult> UpdateProductTeamMemberMutiple(List<ProductTeamMemberUpdateDTO> productTeamMemberUpdateDTO)
        {
            CervittApiResult result = new CervittApiResult();
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

            if(productTeamMemberUpdateDTO == null || productTeamMemberUpdateDTO.Count == 0)
            {
                return BadRequest("Invalid data or no any data passed.");
            }

            long userId = productTeamMemberUpdateDTO.FirstOrDefault().UserID;
            db.ProductTeamMembers.RemoveRange(db.ProductTeamMembers.Where(u => u.UserID == userId));
            await db.SaveChangesAsync();

            foreach (ProductTeamMemberUpdateDTO postItem in productTeamMemberUpdateDTO)
            {
                if(postItem.ProductID<=0 || postItem.UserID<=0)
                {
                    continue;
                }

                Product product = db.Products.Where(_product => _product.ID == postItem.ProductID)
                                                    .Include(_product => _product.TeamMembers
                                                        .Select(teamMember => teamMember.User))
                                                    .SingleOrDefault();

                if (product != null)
                {
                    UserLevel userLevel =  db.UserLevels.SingleOrDefault(c => c.Id == postItem.UserLevelId);
                    bool isAdmin = false;
                    if(userLevel.Name.ToLower() == "admin")
                    {
                        isAdmin = true;
                    }
                    ProductTeamMember productTeamMember = product.TeamMembers.Where(_productTeamMember => _productTeamMember.UserID == postItem.UserID).SingleOrDefault();

                    if (productTeamMember == null)
                    {
                        productTeamMember = new ProductTeamMember();
                        productTeamMember.CanEditTheProduct = isAdmin;
                        productTeamMember.ProductID = postItem.ProductID;
                        productTeamMember.UserID = postItem.UserID;
                        productTeamMember.UserLevelId = postItem.UserLevelId;
                        db.ProductTeamMembers.Add(productTeamMember);
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        productTeamMember.Role = postItem.Role;
                        productTeamMember.UserLevelId = postItem.UserLevelId;
                        productTeamMember.CanEditTheProduct = isAdmin;
                        db.Entry(productTeamMember).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                    }
                }
            }

            result.Success = true;
            result.SuccessMessage = "Records updated successfully.";
            result.ErrorMessage = "";
            return Ok(result);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductTeamMemberExists(long id)
        {
            return db.ProductTeamMembers.Count(e => e.ID == id) > 0;
        }
    }
}