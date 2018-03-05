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
using AutoMapper;

namespace KaribouAlpha.Controllers
{
    public class CompanyFollowerGroupsController : ApiController
    {
        private KaribouAlphaContext db = new KaribouAlphaContext();

        [HttpGet]
        [Route("api/GetMyCompanyFollowerGroups")]
        [Authorize]
        public IQueryable<CompanyFollowerGroupDTO> GetMyCompanyFollowerGroups()
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            IQueryable<CompanyFollowerGroup> companyFollowerGroups = db.CompanyFollowerGroups.Where(companyFollowerGroup => companyFollowerGroup.Company.Owner.Id == user.Id);

            return companyFollowerGroups.ProjectTo<CompanyFollowerGroupDTO>();
        }
               

        [HttpGet]
        [Route("api/GetMyCompanyFollowerGroupSummaries")]
        [Authorize]
        public IQueryable<CompanyFollowerGroupSummaryDTO> GetMyCompanyFollowerGroupSummaries()
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            IQueryable<CompanyFollowerGroup> companyFollowerGroups = db.CompanyFollowerGroups.Where(companyFollowerGroup => companyFollowerGroup.Company.Owner.Id == user.Id);

            return companyFollowerGroups.ProjectTo<CompanyFollowerGroupSummaryDTO>();
        }

        [HttpGet]
        [Route("api/GetAllowedFollowerGroupsForProductFile")]
        [Authorize]
        public IEnumerable<CompanyFollowerGroupDTO> GetAllowedFollowerGroupsForProductFile(long productFileId)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName)
                                                  .Include(_user => _user.Company)
                                                  .AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            ProductFile productFile = db.ProductFiles.Where(_productFile => _productFile.ID == productFileId)
                                                                                        .Include(_productFile => _productFile.Product)
                                                                                        .Include(_productFile => _productFile.Product.Company)
                                                                                        .Include(_productFile => _productFile.Product.Company.FollowerGroups)
                                                                                        .Include(_productFile => _productFile.Product.TeamMembers)
                                                                                        .Include(_productFile => _productFile.Product.GroupsVisibleTo)
                                                                                        .SingleOrDefault();
            if (productFile == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if ((productFile.Product.CompanyID != user.Company.ID) && (productFile.Product.TeamMembers.Where(teamMember => teamMember.UserID == user.Id && teamMember.CanEditTheProduct == true).SingleOrDefault() == null))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            if(productFile.Product.Privacy == ProductPrivacy.Public)
            {
                return Mapper.Map<IEnumerable<CompanyFollowerGroupDTO>>(productFile.Product.Company.FollowerGroups);
            }

            if (productFile.Product.Privacy == ProductPrivacy.VisibleToSelectedGroups)
            {
                return Mapper.Map<IEnumerable<CompanyFollowerGroupDTO>>(productFile.Product.GroupsVisibleTo);
            }

            return new List<CompanyFollowerGroupDTO>();
        }

        [HttpPost]
        [Route("api/NewCompanyFollowerGroup")]
        [Authorize]
        [ResponseType(typeof(CompanyFollowerGroupDTO))]
        public async Task<IHttpActionResult> NewCompanyFollowerGroup(NewCompanyFollowerGroupDTO newCompanyFollowerGroupDTO)
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
            CompanyFollowerGroup companyFollowerGroup = new CompanyFollowerGroup();

            companyFollowerGroup.CompanyID = company.ID;
            companyFollowerGroup.Company = company;
            companyFollowerGroup.Name = newCompanyFollowerGroupDTO.Name;
            companyFollowerGroup = db.CompanyFollowerGroups.Add(companyFollowerGroup);
            await db.SaveChangesAsync();

            return Ok(Mapper.Map<CompanyFollowerGroup, CompanyFollowerGroupDTO>(companyFollowerGroup));
        }

        [HttpPost]
        [Route("api/UpdateCompanyFollowerGroup")]
        [Authorize]
        [ResponseType(typeof(CompanyFollowerGroupDTO))]
        public async Task<IHttpActionResult> UpdateProduct(CompanyFollowerGroupDTO companyFollowerGroupDTO)
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

            CompanyFollowerGroup companyFollowerGroup = db.CompanyFollowerGroups.Find(companyFollowerGroupDTO.ID);

            if (companyFollowerGroup == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if ((companyFollowerGroup.CompanyID != user.Company.ID) && (companyFollowerGroup.Company.Members.Any(member => member.UserID == user.Id) == false))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            Mapper.Map(companyFollowerGroupDTO, companyFollowerGroup);
            db.Entry(companyFollowerGroup).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Ok(Mapper.Map<CompanyFollowerGroup, CompanyFollowerGroupDTO>(companyFollowerGroup));
        }


        [HttpPost]
        [Route("api/DeleteCompanyFollowerGroup")]
        [Authorize]
        public async Task<IHttpActionResult> DeleteCompanyFollowerGroup(CompanyFollowerGroupDTO companyFollowerGroupDTO)
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

            CompanyFollowerGroup companyFollowerGroup = db.CompanyFollowerGroups.Find(companyFollowerGroupDTO.ID);

            if (companyFollowerGroup == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if ((companyFollowerGroup.CompanyID != user.Company.ID) && (companyFollowerGroup.Company.Members.Any(member => member.UserID == user.Id) == false))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            db.CompanyFollowerGroups.Remove(companyFollowerGroup);
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("api/AddFollowerToGroup")]
        [Authorize]
        public async Task<IHttpActionResult> AddFollowerToGroup(long followerUserId, long followerGroupId)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            CompanyFollowerGroup companyFollowerGroup = db.CompanyFollowerGroups.Where(_companyFollowerGroup => _companyFollowerGroup.ID == followerGroupId).SingleOrDefault();

            if (companyFollowerGroup == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if((companyFollowerGroup.Company.Owner.Id != user.Id) && (companyFollowerGroup.Company.Members.Any(member => member.UserID == user.Id) == false))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            CompanyConnection companyConnection = db.CompanyConnections.Where(_companyConnection => _companyConnection.UserID == followerUserId && _companyConnection.CompanyID == companyFollowerGroup.CompanyID).SingleOrDefault();
            
            if (companyConnection == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if(companyFollowerGroup.Followers.Contains(companyConnection))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            companyFollowerGroup.Followers.Add(companyConnection);
            db.Entry(companyFollowerGroup).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("api/UpdateFollowerGroupsForFollower")]
        [Authorize]
        [ResponseType(typeof(UpdateFollowerGroupsForFollowerDTO))]
        public async Task<IHttpActionResult> UpdateFollowerGroupsForFollower(UpdateFollowerGroupsForFollowerDTO updateFollowerGroupsForFollowerDTO)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName)
                                                 .Include(_user => _user.Company)
                                                 .Include(_user => _user.Company.FollowerGroups)
                                                 .SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            Company company = user.Company;

            if (company == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                       
            CompanyConnection follower = db.CompanyConnections.Where(_follower => _follower.UserID == updateFollowerGroupsForFollowerDTO.FollowerUserID && _follower.CompanyID == company.ID)
                                                                                                                    .Include(_follower => _follower.FollowerGroups).
                                                                                                                    SingleOrDefault();

            if (follower == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            follower.FollowerGroups.Clear();
            db.Entry(follower).State = EntityState.Modified;

            IEnumerable<CompanyFollowerGroup> followerGroupsToAdd = db.CompanyFollowerGroups.Where(followerGroup => followerGroup.CompanyID == company.ID && updateFollowerGroupsForFollowerDTO.FollowerGroupIDs.Contains(followerGroup.ID));

            if (followerGroupsToAdd.Count() > 0)
            {
                foreach (CompanyFollowerGroup followerGroupToAdd in followerGroupsToAdd)
                {
                    follower.FollowerGroups.Add(followerGroupToAdd);
                }
            }

            await db.SaveChangesAsync();
           
            return Ok(updateFollowerGroupsForFollowerDTO);
        }

        [HttpPost]
        [Route("api/UpdateGroupsVisibleToForProduct")]
        [Authorize]
        [ResponseType(typeof(UpdateGroupsVisibleToForProductDTO))]
        public async Task<IHttpActionResult> UpdateGroupsVisibleToForProduct(UpdateGroupsVisibleToForProductDTO updateGroupsVisibleToForProductDTO)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName)
                                                 .Include(_user => _user.Company)
                                                 .SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            Company company = user.Company;

            if (company == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product product = db.Products.Where(_product => _product.ID == updateGroupsVisibleToForProductDTO.ProductID)
                                                                    .Include(_product => _product.GroupsVisibleTo)
                                                                    .Include(_product => _product.TeamMembers)
                                                                    .SingleOrDefault();

            if(product.Privacy != ProductPrivacy.VisibleToSelectedGroups)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            if ((product.CompanyID != company.ID) && (product.TeamMembers.Where(teamMember => teamMember.UserID == user.Id && teamMember.CanEditTheProduct == true).SingleOrDefault() == null))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            product.GroupsVisibleTo.Clear();
            db.Entry(product).State = EntityState.Modified;

            IEnumerable<CompanyFollowerGroup> followerGroupsToAdd = db.CompanyFollowerGroups.Where(followerGroup => followerGroup.CompanyID == company.ID && updateGroupsVisibleToForProductDTO.FollowerGroupIDs.Contains(followerGroup.ID));

            if (followerGroupsToAdd.Count() > 0)
            {
                foreach (CompanyFollowerGroup followerGroupToAdd in followerGroupsToAdd)
                {
                    product.GroupsVisibleTo.Add(followerGroupToAdd);
                }
            }

            await db.SaveChangesAsync();

            return Ok(updateGroupsVisibleToForProductDTO);
        }

        [HttpPost]
        [Route("api/UpdateGroupsVisibleToForProductFile")]
        [Authorize]
        [ResponseType(typeof(UpdateGroupsVisibleToForProductFileDTO))]
        public async Task<IHttpActionResult> UpdateGroupsVisibleToForProductFile(UpdateGroupsVisibleToForProductFileDTO updateGroupsVisibleToForProductFileDTO)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName)
                                                 .Include(_user => _user.Company)
                                                 .SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            Company company = user.Company;

            if (company == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ProductFile productFile = db.ProductFiles.Where(_productFile => _productFile.ID == updateGroupsVisibleToForProductFileDTO.ProductFileID)
                                                                    .Include(_productFile => _productFile.GroupsVisibleTo)
                                                                    .Include(_productFile => _productFile.Product)
                                                                    .Include(_productFile => _productFile.Product.TeamMembers)
                                                                    .Include(_productFile => _productFile.Product.GroupsVisibleTo)
                                                                    .SingleOrDefault();

            if ((productFile.Product.CompanyID != company.ID) && (productFile.Product.TeamMembers.Where(teamMember => teamMember.UserID == user.Id && teamMember.CanEditTheProduct == true).SingleOrDefault() == null))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            if (productFile.Product.Privacy != ProductPrivacy.VisibleToSelectedGroups && productFile.Product.Privacy != ProductPrivacy.Public)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            if (productFile.Privacy != ProductFilePrivacy.VisibleToSelectedGroups)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            productFile.GroupsVisibleTo.Clear();
            db.Entry(productFile).State = EntityState.Modified;

            IEnumerable<CompanyFollowerGroup> followerGroupsToAdd = db.CompanyFollowerGroups.Where(followerGroup => followerGroup.CompanyID == company.ID && updateGroupsVisibleToForProductFileDTO.FollowerGroupIDs.Contains(followerGroup.ID));

            if (followerGroupsToAdd.Count() > 0)
            {
                foreach (CompanyFollowerGroup followerGroupToAdd in followerGroupsToAdd)
                {
                    if(productFile.Product.Privacy != ProductPrivacy.Public && productFile.Product.GroupsVisibleTo.Contains(followerGroupToAdd) == false)
                    {
                        throw new HttpResponseException(HttpStatusCode.BadRequest);
                    }

                    productFile.GroupsVisibleTo.Add(followerGroupToAdd);
                }
            }


            ProductUpdate productUpdate = new ProductUpdate();

            productUpdate.UserID = user.Id;
            productUpdate.User = user;
            productUpdate.ProductID = productFile.ProductID;
            productUpdate.Product = productFile.Product;
            productUpdate.ProductFileID = productFile.ID;
            productUpdate.ProductFile = productFile;
            productUpdate.DateTime = DateTime.Now;
            productUpdate.UpdateType = UpdateType.ProductFileEdited;
            db.ProductUpdates.Add(productUpdate);
            await db.SaveChangesAsync();

            return Ok(updateGroupsVisibleToForProductFileDTO);
        }

        [HttpGet]
        [Route("api/RemoveFollowerFromGroup")]
        [Authorize]
        public async Task<IHttpActionResult> RemoveFollowerFromGroup(long followerUserId, long followerGroupId)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            CompanyFollowerGroup companyFollowerGroup = db.CompanyFollowerGroups.Where(_companyFollowerGroup => _companyFollowerGroup.ID == followerGroupId).SingleOrDefault();

            if (companyFollowerGroup == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if ((companyFollowerGroup.Company.Owner.Id != user.Id) && (companyFollowerGroup.Company.Members.Any(member => member.UserID == user.Id) == false))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            CompanyConnection companyConnection = companyFollowerGroup.Followers.Where(_follower => _follower.UserID == followerUserId).SingleOrDefault();

            if (companyConnection == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            companyFollowerGroup.Followers.Remove(companyConnection);
            db.Entry(companyFollowerGroup).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("api/AddProductToFollowerGroup")]
        [Authorize]
        public async Task<IHttpActionResult> AddProductToFollowerGroup(long productId, long followerGroupId)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            CompanyFollowerGroup companyFollowerGroup = db.CompanyFollowerGroups.Where(_companyFollowerGroup => _companyFollowerGroup.ID == followerGroupId).SingleOrDefault();

            if (companyFollowerGroup == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if ((companyFollowerGroup.Company.Owner.Id != user.Id) && (companyFollowerGroup.Company.Members.Any(member => member.UserID == user.Id) == false))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            Product product = db.Products.Where(_product => _product.ID == productId).SingleOrDefault();

            if (product == null)
            {
                return NotFound();
            }

            if ((product.CompanyID != user.Company.ID) && (product.TeamMembers.Where(teamMember => teamMember.UserID == user.Id && teamMember.CanEditTheProduct == true).SingleOrDefault() == null))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            if (companyFollowerGroup.VisibleProducts.Contains(product))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            if(product.Privacy == ProductPrivacy.Public)
            {
                product.Privacy = ProductPrivacy.Private;
                db.Entry(product).State = EntityState.Modified;
            }

            companyFollowerGroup.VisibleProducts.Add(product);
            db.Entry(companyFollowerGroup).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("api/RemoveProductFromFollowerGroup")]
        [Authorize]
        public async Task<IHttpActionResult> RemoveProductFromFollowerGroup(long productId, long followerGroupId)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            CompanyFollowerGroup companyFollowerGroup = db.CompanyFollowerGroups.Where(_companyFollowerGroup => _companyFollowerGroup.ID == followerGroupId).SingleOrDefault();

            if (companyFollowerGroup == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if ((companyFollowerGroup.Company.Owner.Id != user.Id) && (companyFollowerGroup.Company.Members.Any(member => member.UserID == user.Id) == false))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            Product product = companyFollowerGroup.VisibleProducts.Where(_product => _product.ID == productId).SingleOrDefault();

            if (product == null)
            {
                return NotFound();
            }

            if ((product.CompanyID != user.Company.ID) && (product.TeamMembers.Where(teamMember => teamMember.UserID == user.Id && teamMember.CanEditTheProduct == true).SingleOrDefault() == null))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            companyFollowerGroup.VisibleProducts.Remove(product);
            db.Entry(companyFollowerGroup).State = EntityState.Modified;

            if(product.GroupsVisibleTo.Count == 0)
            {
                product.Privacy = ProductPrivacy.Public;
                db.Entry(product).State = EntityState.Modified;
            }

            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("api/AddProductFileToFollowerGroup")]
        [Authorize]
        public async Task<IHttpActionResult> AddProductFileToFollowerGroup(long productFileId, long followerGroupId)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            CompanyFollowerGroup companyFollowerGroup = db.CompanyFollowerGroups.Where(_companyFollowerGroup => _companyFollowerGroup.ID == followerGroupId).SingleOrDefault();

            if (companyFollowerGroup == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if ((companyFollowerGroup.Company.Owner.Id != user.Id) && (companyFollowerGroup.Company.Members.Any(member => member.UserID == user.Id) == false))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            ProductFile productFile = db.ProductFiles.Where(_productFile => _productFile.ID == productFileId).SingleOrDefault();

            if (productFile == null)
            {
                return NotFound();
            }

            if ((productFile.Product.CompanyID != user.Company.ID) && (productFile.Product.TeamMembers.Where(teamMember => teamMember.UserID == user.Id && teamMember.CanEditTheProduct == true).SingleOrDefault() == null))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            if (companyFollowerGroup.VisibleProductFiles.Contains(productFile))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            companyFollowerGroup.VisibleProductFiles.Add(productFile);
            db.Entry(companyFollowerGroup).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("api/RemoveProductFileFromFollowerGroup")]
        [Authorize]
        public async Task<IHttpActionResult> RemoveProductFileFromFollowerGroup(long productFileId, long followerGroupId)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            CompanyFollowerGroup companyFollowerGroup = db.CompanyFollowerGroups.Where(_companyFollowerGroup => _companyFollowerGroup.ID == followerGroupId).SingleOrDefault();

            if (companyFollowerGroup == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            if ((companyFollowerGroup.Company.Owner.Id != user.Id) && (companyFollowerGroup.Company.Members.Any(member => member.UserID == user.Id) == false))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            ProductFile productFile = companyFollowerGroup.VisibleProductFiles.Where(_productFile => _productFile.ID == productFileId).SingleOrDefault();

            if (productFile == null)
            {
                return NotFound();
            }

            if ((productFile.Product.CompanyID != user.Company.ID) && (productFile.Product.TeamMembers.Where(teamMember => teamMember.UserID == user.Id && teamMember.CanEditTheProduct == true).SingleOrDefault() == null))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            companyFollowerGroup.VisibleProductFiles.Remove(productFile);
            db.Entry(companyFollowerGroup).State = EntityState.Modified;

            if (productFile.GroupsVisibleTo.Count == 0)
            {
                productFile.Privacy = ProductFilePrivacy.Public;
                db.Entry(productFile).State = EntityState.Modified;
            }

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

        private bool CompanyFollowerGroupExists(long id)
        {
            return db.CompanyFollowerGroups.Count(e => e.ID == id) > 0;
        }
    }
}