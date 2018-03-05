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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace KaribouAlpha.Controllers
{
    public class ProductsController : ApiController
    {
        private KaribouAlphaContext db = new KaribouAlphaContext();

        [HttpGet]
        [Route("api/GetMyCompanyProducts")]
        [Authorize]
        public IQueryable<ProductDTO> GetMyCompanyProducts()
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            if (user.Company != null)
            {
                IQueryable<Product> products = db.Products.Where(product => product.Company.Owner.Id == user.Id)
                                                                                                  .Include(product => product.GroupsVisibleTo);
                return products.ProjectTo<ProductDTO>();

            }
            if (user.IsIndividual.HasValue && user.IsIndividual.Value)
            {
                IQueryable<Product> products = db.Products.Where(product => product.Company.Owner.Id == user.Id)
                                                                                                  .Include(product => product.GroupsVisibleTo);

                IQueryable<Product> myProducts = db.Products.Where(product => product.UserId.HasValue && product.UserId.Value == user.Id && !product.CompanyID.HasValue)
                                                                                               .Include(product => product.GroupsVisibleTo);
                return products.Concat(myProducts).ProjectTo<ProductDTO>();
            }
            else if(user.IsIndividual == false && user.Company == null)
            {
                if (!user.OnboardingSkipped)
                {
                    CompanyMember companyMember = db.CompanyMembers.Where(cm => cm.UserID == user.Id).FirstOrDefault();
                    if (companyMember != null)
                    {
                        try
                        {
                            IQueryable<Product> myProducts = db.Products.Where(product => product.UserId.HasValue && product.CompanyID.HasValue && product.UserId.Value == user.Id && companyMember.CompanyID == product.CompanyID);
                            return myProducts.ProjectTo<ProductDTO>();
                        }
                        catch(Exception x)
                        {
                            //todo: need to log error
                        }
                    }
                }
                else
                {
                    IQueryable<Product> myProducts = db.Products.Where(product => product.UserId.HasValue && product.UserId.Value == user.Id);
                    return myProducts.ProjectTo<ProductDTO>();
                }
            }

            throw new HttpResponseException(HttpStatusCode.Unauthorized);

        }

        [HttpGet]
        [Route("api/GetMyPublicCompanyProducts")]
        [Authorize]
        public IQueryable<ProductDTO> GetMyPublicCompanyProducts()
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            IQueryable<Product> products = db.Products.Where(product => product.Company.Owner.Id == user.Id && product.Privacy == ProductPrivacy.Public)
                                                                                              .Include(product => product.GroupsVisibleTo);

            return products.ProjectTo<ProductDTO>();
        }


        [HttpGet]
        [Route("api/GetByUserIdPublicCompanyProducts")]
        [Authorize]
        public async Task<IQueryable<ProductDTO>> GetByUserIdPublicCompanyProducts()
        {
            string userName = User.Identity.Name;
            User user = await db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefaultAsync();
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            long userId = user.Id;

            User userToGetProduct = await db.Users.Where(_user => _user.Id == userId).AsNoTracking().SingleOrDefaultAsync();
            if(userToGetProduct == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            if (userToGetProduct.Company != null)
            {
                IQueryable<Product> products = db.Products.Where(product => product.Company.Owner.Id == userToGetProduct.Id && product.Privacy == ProductPrivacy.Public)
                                                                                                  .Include(product => product.GroupsVisibleTo);
                return products.ProjectTo<ProductDTO>();
            }
            else if (userToGetProduct.IsIndividual.HasValue && userToGetProduct.IsIndividual.Value)
            {
                IQueryable<Product> myProducts = db.Products.Where(product => product.UserId.HasValue && product.UserId.Value == userToGetProduct.Id && !product.CompanyID.HasValue)
                                                                                              .Include(product => product.GroupsVisibleTo);
                return myProducts.ProjectTo<ProductDTO>();
            }
            else if (userToGetProduct.IsIndividual == false && userToGetProduct.Company == null)
            {
                IQueryable<Product> myProducts = db.Products.Where(product => product.UserId.HasValue && product.UserId.Value == userToGetProduct.Id && product.Privacy == ProductPrivacy.Public);
                return myProducts.ProjectTo<ProductDTO>();
            }
            else
            {
                return null;
            }
        }



        [HttpGet]
        [Route("api/GetMyCompanyProductsForFollowerGroup")]
        [Authorize]
        public IQueryable<ProductDTO> GetMyCompanyProductsForFollowerGroup(long followerGroupId)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            IQueryable<Product> products = db.Products.Where(product => 
                                                                                              product.Company.Owner.Id == user.Id &&
                                                                                              product.GroupsVisibleTo.Any(followerGroup =>
                                                                                                followerGroup.ID == followerGroupId))
                                                                                              .OrderBy(product => product.Name);

            return products.ProjectTo<ProductDTO>();
        }

        [HttpGet]
        [Route("api/GetMyCompanyProductSummaries")]
        [Authorize]
        public IQueryable<ProductSummaryDTO> GetMyCompanyProductSummaries()
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            IQueryable<Product> products = db.Products.Where(product => product.Company.Owner.Id == user.Id);

            return products.ProjectTo<ProductSummaryDTO>();
        }

        [HttpGet]
        [Route("api/GetMyPublicCompanyProductSummaries")]
        [Authorize]
        public IQueryable<ProductSummaryDTO> GetMyPublicCompanyProductSummaries()
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            IQueryable<Product> products = db.Products.Where(product => product.Company.Owner.Id == user.Id && product.Privacy == ProductPrivacy.Public)
                                                                                              .Include(product => product.GroupsVisibleTo);

            return products.ProjectTo<ProductSummaryDTO>();
        }

        [HttpGet]
        [Route("api/GetMyCompanyProductSummariesForFollowerGroup")]
        [Authorize]
        public IQueryable<ProductSummaryDTO> GetMyCompanyProductSummariesForFollowerGroup(long followerGroupId)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            IQueryable<Product> products = db.Products.Where(product =>
                                                                                              product.Company.Owner.Id == user.Id &&
                                                                                              product.GroupsVisibleTo.Any(followerGroup =>
                                                                                                followerGroup.ID == followerGroupId))
                                                                                              .OrderBy(product => product.Name);

            return products.ProjectTo<ProductSummaryDTO>();
        }

        [HttpGet]
        [Route("api/GetCompanyProducts")]
        [Authorize]
        public IQueryable<ProductViewDTO> GetCompanyProducts(long companyId)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            IQueryable<Product> products = db.Products.Where(product => 
                                                                                               product.CompanyID == companyId && 
                                                                                               (product.Privacy == ProductPrivacy.Public ||
                                                                                               (product.Privacy != ProductPrivacy.Public &&
                                                                                               (product.GroupsVisibleTo.Intersect(db.CompanyFollowerGroups.Where(companyFollowerGroup =>
                                                                                                     companyFollowerGroup.CompanyID == companyId &&
                                                                                                     companyFollowerGroup.Followers.Any(follower => follower.UserID == user.Id))).Any() == false))))
                                                                                               .OrderBy(product => product.Name);

            return products.ProjectTo<ProductViewDTO>();
        }
        
        [HttpGet]
        [Route("api/GetCompanyProductSummaries")]
        [Authorize]
        public IQueryable<ProductSummaryDTO> GetCompanyProductSummaries(long companyId)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            IQueryable<Product> products = db.Products.Where(product =>  product.CompanyID.HasValue && product.CompanyID == companyId &&
                                                                                               (product.Privacy == ProductPrivacy.Public ||
                                                                                               (product.Privacy != ProductPrivacy.Public &&
                                                                                               (product.GroupsVisibleTo.Intersect(db.CompanyFollowerGroups.Where(companyFollowerGroup =>
                                                                                                     companyFollowerGroup.CompanyID == companyId &&
                                                                                                     companyFollowerGroup.Followers.Any(follower => follower.UserID == user.Id))).Any() == false))))
                                                                                               .OrderBy(product => product.Name);

            return products.ProjectTo<ProductSummaryDTO>();
        }

        [HttpGet]
        [Route("api/GetCompanyProductSummariesByCompanyUri")]
        [Authorize]
        public IQueryable<ProductSummaryDTO> GetCompanyProductSummariesByCompanyUri(string companyUri)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            Company company = db.Companies.Where(_company => _company.URI == companyUri).SingleOrDefault();

            if (company == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            IQueryable<Product> products = db.Products.Where(product =>
                                                                                               product.CompanyID == company.ID &&
                                                                                               (product.Privacy == ProductPrivacy.Public ||
                                                                                               (product.Privacy != ProductPrivacy.Public &&
                                                                                               (product.GroupsVisibleTo.Intersect(db.CompanyFollowerGroups.Where(companyFollowerGroup =>
                                                                                                     companyFollowerGroup.CompanyID == company.ID &&
                                                                                                     companyFollowerGroup.Followers.Any(follower => follower.UserID == user.Id))).Any() == false))))
                                                                                               .OrderBy(product => product.Name);

            return products.ProjectTo<ProductSummaryDTO>();
        }

        [HttpGet]
        [Route("api/GetProductForEditing")]
        [Authorize]
        [ResponseType(typeof(ProductDTO))]
        public async Task<IHttpActionResult> GetProductForEditing(long id)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            Product product = db.Products.Where(_product => _product.ID == id)
                                                                    .Include(_product => _product.GroupsVisibleTo)
                                                                    .SingleOrDefault();

            if (product == null)
            {
                return NotFound();
            }

            if((product.CompanyID != user.Company.ID) && (product.TeamMembers.Where(teamMember => teamMember.UserID == user.Id && teamMember.CanEditTheProduct == true).SingleOrDefault() == null))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            return Ok(Mapper.Map<Product, ProductDTO>(product));
        }

        [HttpGet]
        [Route("api/GetProduct")]
        [Authorize]
        [ResponseType(typeof(ProductViewDTO))]
        public async Task<IHttpActionResult> GetProduct(long id)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            Product product = db.Products.Where(_product => _product.ID == id)
                                                   .Include(_product => _product.TeamMembers)
                                                   .Include(_product => _product.GroupsVisibleTo)
                                                   .SingleOrDefault();

            if (product == null)
            {
                return NotFound();
            }

            if ((product.CompanyID != user.Company.ID) && (product.TeamMembers.Where(teamMember => teamMember.UserID == user.Id && teamMember.CanEditTheProduct == true).SingleOrDefault() == null))
            {
                if (product.Privacy == ProductPrivacy.Private)
                {
                    throw new HttpResponseException(HttpStatusCode.Unauthorized);
                }

                if ((product.Privacy == ProductPrivacy.VisibleToSelectedGroups) && product.GroupsVisibleTo.Contains(db.CompanyFollowerGroups.FirstOrDefault(followerGroup =>
                                                                                                                                              followerGroup.CompanyID == product.CompanyID &&
                                                                                                                                              followerGroup.Followers.Any(follower => follower.UserID == user.Id))) == false)
                {
                    throw new HttpResponseException(HttpStatusCode.Unauthorized);
                }

                ProductView productView = new ProductView();

                productView.UserID = user.Id;
                productView.User = user;
                productView.ProductID = product.ID;
                productView.Product = product;
                productView.DateTime = DateTime.Now;
                db.ProductViews.Add(productView);
                await db.SaveChangesAsync();
            }

            return Ok(Mapper.Map<Product, ProductViewDTO>(product));
        }

        [HttpGet]
        [Route("api/GetProductByUri")]
        [Authorize]
        [ResponseType(typeof(ProductViewDTO))]
        public async Task<IHttpActionResult> GetProductByUri(string companyUri, string productUri)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            Product product = db.Products.Where(_product => _product.URI == productUri && _product.Company.URI == companyUri)
                                                   .Include(_product => _product.TeamMembers)
                                                   .Include(_product => _product.GroupsVisibleTo)
                                                   .SingleOrDefault();

            if (product == null)
            {
                return NotFound();
            }

            if ((product.CompanyID != user.Company.ID) && (product.TeamMembers.Where(teamMember => teamMember.UserID == user.Id && teamMember.CanEditTheProduct == true).SingleOrDefault() == null))
            {
                if (product.Privacy == ProductPrivacy.Private)
                {
                    throw new HttpResponseException(HttpStatusCode.Unauthorized);
                }

                if ((product.Privacy == ProductPrivacy.VisibleToSelectedGroups) && product.GroupsVisibleTo.Contains(db.CompanyFollowerGroups.FirstOrDefault(followerGroup =>
                                                                                                                                              followerGroup.CompanyID == product.CompanyID &&
                                                                                                                                              followerGroup.Followers.Any(follower => follower.UserID == user.Id))) == false)
                {
                    throw new HttpResponseException(HttpStatusCode.Unauthorized);
                }

                ProductView productView = new ProductView();

                productView.UserID = user.Id;
                productView.User = user;
                productView.ProductID = product.ID;
                productView.Product = product;
                productView.DateTime = DateTime.Now;
                db.ProductViews.Add(productView);
                await db.SaveChangesAsync();
            }

            return Ok(Mapper.Map<Product, ProductViewDTO>(product));
        }

        [HttpGet]
        [Route("api/GetMyProduct")]
        [Authorize]
        [ResponseType(typeof(ProductViewDTO))]
        public async Task<IHttpActionResult> GetMyProduct(long id)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).Include(_user=>_user.Company).SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            Product product = db.Products.Where(_product => _product.ID == id).Include(_product => _product.TeamMembers).SingleOrDefault();


            if (product == null)
            {
                return NotFound();
            }

            if ((product.CompanyID != user.Company.ID) && (product.TeamMembers.Where(teamMember => teamMember.UserID == user.Id && ( teamMember.CanEditTheProduct == true || (teamMember.UserLevelId.HasValue && teamMember.UserLevelId.Value >0))).SingleOrDefault() == null))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            return Ok(Mapper.Map<Product, ProductViewDTO>(product));
        }

        [HttpGet]
        [Route("api/GetMyProductAccessLevel")]
        [Authorize]
        [ResponseType(typeof(EntityUserLevel))]
        public async Task<IHttpActionResult> GetMyProductAccessLevel(long id)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).Include(_user => _user.Company).SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            Product product = db.Products.Where(_product => _product.ID == id).Include(_product => _product.TeamMembers).SingleOrDefault();
            if (product == null)
            {
                return NotFound();
            }

            if(product.CompanyID == user.Company.ID)
            {
                EntityUserLevel entityUserLevel = new EntityUserLevel();
                entityUserLevel.EntityId = product.ID;
                entityUserLevel.IsAdmin = true;
                return Ok(entityUserLevel);
            }

            if ((product.CompanyID != user.Company.ID) && (product.TeamMembers.Where(teamMember => teamMember.UserID == user.Id && (teamMember.CanEditTheProduct == true || (teamMember.UserLevelId.HasValue && teamMember.UserLevelId.Value > 0))).SingleOrDefault() == null))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            ProductTeamMember productTeamMember = product.TeamMembers.Where(teamMember => teamMember.UserID == user.Id).SingleOrDefault();
            if (productTeamMember != null)
            {
                EntityUserLevel entityUserLevel = new EntityUserLevel();
                entityUserLevel.EntityId = product.ID;
                entityUserLevel.EntityUserLevelId = productTeamMember.UserLevelId.HasValue ?  productTeamMember.UserLevelId.Value : 0;
                UserLevel userLevel = db.UserLevels.Where(uLevel => uLevel.Id == entityUserLevel.EntityUserLevelId).SingleOrDefault();
                if(userLevel != null)
                {
                    entityUserLevel.UserLevel = userLevel.Name;
                    if (!productTeamMember.CanEditTheProduct)
                    {
                        if (userLevel.Name.ToLower() == "admin")
                            entityUserLevel.IsAdmin = true;
                        if (userLevel.Name.ToLower() == "viewer")
                            entityUserLevel.IsViewer = true;
                        if (userLevel.Name.ToLower() == "editor")
                            entityUserLevel.IsEditor = true;
                    }
                }

                if (productTeamMember.CanEditTheProduct)
                    entityUserLevel.IsAdmin = true;

                return Ok(entityUserLevel);
            }
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        [HttpPost]
        [Route("api/UpdateProduct")]
        [Authorize]
        [ResponseType(typeof(ProductDTO))]
        public async Task<IHttpActionResult> UpdateProduct(ProductDTO productDTO)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName)
                                                  .Include(_user => _user.Company)
                                                  .SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product product = db.Products.Where(_product => _product.ID == productDTO.ID)
                                                                    .Include(_product => _product.TeamMembers)
                                                                    .Include(_product => _product.GroupsVisibleTo)
                                                                    .Include(_product => _product.ProductFiles
                                                                        .Select(productFile => productFile.GroupsVisibleTo))
                                                                    .SingleOrDefault();

            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if ((product.CompanyID != user.Company.ID) && (product.TeamMembers.Where(teamMember => teamMember.UserID == user.Id && teamMember.CanEditTheProduct == true).SingleOrDefault() == null))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            Regex regex = new Regex("[^a-zA-Z0-9_-]");

            product.URI = productDTO.Name.Replace(" ", "-");
            product.URI = regex.Replace(product.URI, "").ToLower();

            if (product.URI.Length == 0)
            {
                product.URI = null;
            }
            else if (db.Products.Where(_product => _product.URI == product.URI && _product.ID != product.ID && _product.CompanyID == product.CompanyID).SingleOrDefault() != null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            product.Name = productDTO.Name;
            product.Description = productDTO.Description;
            product.Privacy = productDTO.Privacy;
            product.Logo = productDTO.Logo;

            if(productDTO.Privacy == ProductPrivacy.VisibleToSelectedGroups)
            {
                List<long> followerGroupIdsToRemove = new List<long>();
                List<long> followerGroupIdsToAdd = new List<long>();
                bool keepFollowerGroupInProduct = false;
                bool addFollowerGroupToProduct = false;

                foreach (CompanyFollowerGroup companyFollowerGroup in product.GroupsVisibleTo)
                {
                    keepFollowerGroupInProduct = false;

                    foreach (CompanyFollowerGroupDTO companyFollowerGroupDto in productDTO.GroupsVisibleTo)
                    {
                        if (companyFollowerGroup.ID == companyFollowerGroupDto.ID)
                        {
                            keepFollowerGroupInProduct = true;
                            break;
                        }
                    }

                    if (keepFollowerGroupInProduct == false)
                    {
                        followerGroupIdsToRemove.Add(companyFollowerGroup.ID);
                    }
                }

                foreach (CompanyFollowerGroupDTO companyFollowerGroupDto in productDTO.GroupsVisibleTo)
                {
                    addFollowerGroupToProduct = true;

                    foreach (CompanyFollowerGroup companyFollowerGroup in product.GroupsVisibleTo)
                    {
                        if (companyFollowerGroup.ID == companyFollowerGroupDto.ID)
                        {
                            addFollowerGroupToProduct = false;
                            break;
                        }
                    }

                    if (addFollowerGroupToProduct == true)
                    {
                        followerGroupIdsToAdd.Add(companyFollowerGroupDto.ID);
                    }
                }

                foreach (long followerGroupId in followerGroupIdsToRemove)
                {
                    product.GroupsVisibleTo.Remove(product.GroupsVisibleTo.Where(followerGroup => followerGroup.ID == followerGroupId).SingleOrDefault());
                }

                foreach (long followerGroupId in followerGroupIdsToAdd)
                {
                    product.GroupsVisibleTo.Add(db.CompanyFollowerGroups.Where(followerGroup => followerGroup.ID == followerGroupId && followerGroup.CompanyID == user.Company.ID).SingleOrDefault());
                }
            }
            else if(productDTO.Privacy == ProductPrivacy.Public || productDTO.Privacy == ProductPrivacy.Private)
            {
                product.GroupsVisibleTo.Clear();
            }

            db.Entry(product).State = EntityState.Modified;

            ProductUpdate productUpdate = new ProductUpdate();

            productUpdate.UserID = user.Id;
            productUpdate.User = user;
            productUpdate.ProductID = product.ID;
            productUpdate.Product = product;
            productUpdate.DateTime = DateTime.Now;
            productUpdate.UpdateType = UpdateType.ProductEdited;
            db.ProductUpdates.Add(productUpdate);
            await db.SaveChangesAsync();

            return Ok(Mapper.Map<Product, ProductDTO>(product));
        }

        [HttpPost]
        [Route("api/NewProduct")]
        [Authorize]
        [ResponseType(typeof(ProductDTO))]
        public async Task<IHttpActionResult> NewProduct(NewProductDTO newProductDTO)
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

            Company company = user.Company;
            Product product = new Product();
            ProductUpdate productUpdate = new ProductUpdate();

            Regex regex = new Regex("[^a-zA-Z0-9_-]");

            product.URI = regex.Replace(newProductDTO.Name, "").ToLower();

            if (product.URI.Length == 0)
            {
                product.URI = null;
            }
            else if (company != null && db.Products.Where(_product => _product.URI == product.URI && _product.CompanyID == company.ID).SingleOrDefault() != null)

            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            if (company != null)
            {
                product.CompanyID = company.ID;
                product.Company = company;
                product.User = null;
            }
            else if(user.IsIndividual.HasValue && user.IsIndividual.Value)
            {
                product.UserId = user.Id;
                product.User = user;
                product.Company = null;
            }
            else
            {
                CompanyMember companyMember = await db.CompanyMembers.Where(cm => cm.UserID == user.Id).FirstOrDefaultAsync();
                if(companyMember != null)
                {
                    product.CompanyID = companyMember.CompanyID;
                    product.UserId = user.Id;
                    product.Company = null;
                }

            }

            product.Name = newProductDTO.Name;
            product.Description = newProductDTO.Description;
            product.Privacy = newProductDTO.Privacy;
            product.Logo = newProductDTO.Logo;

            if (newProductDTO.Privacy == ProductPrivacy.VisibleToSelectedGroups)
            {
                foreach (CompanyFollowerGroupDTO companyFollowerGroupDto in newProductDTO.GroupsVisibleTo)
                {
                    product.GroupsVisibleTo.Add(db.CompanyFollowerGroups.Where(followerGroup => followerGroup.ID == companyFollowerGroupDto.ID && followerGroup.CompanyID == company.ID).SingleOrDefault());
                }
            }

            product = db.Products.Add(product);
            productUpdate.UserID = user.Id;
            productUpdate.User = user;
            productUpdate.ProductID = product.ID;
            productUpdate.Product = product;
            productUpdate.DateTime = DateTime.Now;
            productUpdate.UpdateType = UpdateType.ProductAdded;
            db.ProductUpdates.Add(productUpdate);
            await db.SaveChangesAsync();

            return Ok(Mapper.Map<Product, ProductDTO>(product));
        }

        [HttpGet]
        [Route("api/GetProductLogo")]
        public async Task<IHttpActionResult> GetProductLogo(long productId)
        {
            Product product = db.Products.Where(_product => _product.ID == productId).AsNoTracking().SingleOrDefault();

            if (product == null)
            {
                return NotFound();
            }

            byte[] imageBytes;
            string mediaTypeHeader;

            if (product.Logo != null)
            {
                imageBytes = product.Logo;
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
        [Route("api/GetProductSharedByCompany")]
        [Authorize]
        public List<ProductSummaryDTO> GetProductSharedByCompany(long companyId)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            if(!db.Companies.Any(c=>c.ID == companyId))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            bool isUserAssociatedWithCompany = false;

            if (db.CompanyMembers.Any(cm => cm.CompanyID == companyId && cm.UserID == user.Id))
            {
                isUserAssociatedWithCompany = true;
            }

            if(isUserAssociatedWithCompany)
            {
                CompanyMember companyMember = db.CompanyMembers.SingleOrDefault(cm => cm.CompanyID == companyId && cm.UserID == user.Id);
                IQueryable<Product> products = db.Products.Where(p => p.CompanyID == companyMember.CompanyID && p.TeamMembers.Select(c => c.UserID).Contains(user.Id)).Include(p => p.TeamMembers);
                return products.ProjectTo<ProductSummaryDTO>().ToList();
            }
            return new List<ProductSummaryDTO>();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(long id)
        {
            return db.Products.Count(e => e.ID == id) > 0;
        }
    }
}