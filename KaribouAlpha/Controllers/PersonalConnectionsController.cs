using AutoMapper.QueryableExtensions;
using KaribouAlpha.DAL;
using KaribouAlpha.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace KaribouAlpha.Controllers
{
    public class PersonalConnectionsController : ApiController
    {
        private KaribouAlphaContext db = new KaribouAlphaContext();


        [HttpGet]
        [Route("api/GetMyPersonalConnections")]
        [Authorize]
        public IQueryable<PersonalConnectionDTO> GetMyPersonalConnections()
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).AsNoTracking().SingleOrDefault();

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            IQueryable<PersonalConnection> personalConnections = db.PersonalConnections
                                                                                                                    .Where(connection => connection.FollowerID == user.Id || connection.FollowingID == user.Id)
                                                                                                                    .Include(connection => connection.Follower)
                                                                                                                    .Include(connection => connection.Following);

            return personalConnections.ProjectTo<PersonalConnectionDTO>();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonalConnectionExists(long id)
        {
            return db.PersonalConnections.Count(e => e.ID == id) > 0;
        }
    }
}