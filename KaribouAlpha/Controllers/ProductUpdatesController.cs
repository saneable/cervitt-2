using AutoMapper;
using KaribouAlpha.DAL;
using KaribouAlpha.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace KaribouAlpha.Controllers
{
    public class ProductUpdatesController : ApiController
    {
        private KaribouAlphaContext db = new KaribouAlphaContext();

        [HttpGet]
        [Route("api/GetLatestProductUpdatesPaged")]
        [Authorize]
        public async Task<IEnumerable<ProductUpdateDTO>> GetLatestProductUpdatesPaged(int pageNumber, int pageSize)
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            if (pageNumber <= 0 || pageSize <= 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            IQueryable<ProductUpdate> productUpdates = db.ProductUpdates.Where(productUpdate =>
                                                                                                                            (productUpdate.Product.Company.Owner.Id == user.Id) ||
                                                                                                                            (productUpdate.Product.TeamMembers.Any(teamMember => teamMember.UserID == user.Id)) ||
                                                                                                                            (productUpdate.Product.Company.Followers.Any(follower => follower.UserID == user.Id) &&
                                                                                                                            ((productUpdate.Product.Privacy == ProductPrivacy.Public) ||
                                                                                                                            (productUpdate.Product.Privacy == ProductPrivacy.VisibleToSelectedGroups && 
                                                                                                                            productUpdate.Product.GroupsVisibleTo.Any(followerGroup => followerGroup.Followers.Any(follower => follower.UserID == user.Id)))) &&
                                                                                                                            ((productUpdate.ProductFile == null) ||
                                                                                                                            ((productUpdate.ProductFile != null) &&
                                                                                                                            ((productUpdate.ProductFile.Privacy == ProductFilePrivacy.Public) ||
                                                                                                                            (productUpdate.ProductFile.Privacy == ProductFilePrivacy.VisibleToSelectedGroups && 
                                                                                                                            productUpdate.ProductFile.GroupsVisibleTo.Any(followerGroup => followerGroup.Followers.Any(follower => follower.UserID == user.Id))))))))
                                                                                                                            .OrderByDescending(productUpdate => productUpdate.DateTime);
            IEnumerable<ProductUpdateDTO> productUpdateDtos = Mapper.Map<IEnumerable<ProductUpdateDTO>>(productUpdates.ToPagedList(pageNumber, pageSize).ToList());

            foreach(ProductUpdateDTO productUpdateDto in productUpdateDtos)
            {
                productUpdateDto.IsNew = (user.LastCheckForProductUpdates == null) || (user.LastCheckForProductUpdates < productUpdateDto.DateTime);
            }

            user.LastCheckForProductUpdates = DateTime.Now;
            db.Entry(user).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return productUpdateDtos;
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