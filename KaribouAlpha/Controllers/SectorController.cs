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
    public class SectorController : ApiController
    {
        private KaribouAlphaContext db = new KaribouAlphaContext();

        [HttpGet]
        [Route("api/GetSectors")]
        [Authorize]
        [ResponseType(typeof(Models.SectorDTO))]
        public IEnumerable<SectorDTO> GetSectors()
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).SingleOrDefault();
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            List<Sector> sectors = db.Sectors.Where(s => s.Active).OrderBy(s => s.SectorName).ToList();
            return Mapper.Map<IEnumerable<SectorDTO>>(sectors);

        }

        [HttpPost]
        [Route("api/NewSector")]
        [Authorize]
        [ResponseType(typeof(SectorDTO))]
        public async Task<IHttpActionResult> NewSector(SectorDTO newSectorDTO)
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

            Sector exist = db.Sectors.SingleOrDefault(sec => sec.SectorName == newSectorDTO.SectorName);
            if (exist != null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }


            newSectorDTO.CreateDate = DateTime.Now;
            newSectorDTO.Active = true;

            Sector sector = Mapper.Map<SectorDTO, Sector>(newSectorDTO);
            db.Sectors.Add(sector);
            await db.SaveChangesAsync();

            return Ok(Mapper.Map<Sector, SectorDTO>(sector));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
