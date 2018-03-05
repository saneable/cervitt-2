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
    public class SkillController : ApiController
    {
        private KaribouAlphaContext db = new KaribouAlphaContext();

        [HttpGet]
        [Route("api/GetSkills")]
        [Authorize]
        [ResponseType(typeof(Models.SkillDTO))]
        public IEnumerable<SkillDTO> GetSkills()
        {
            string userName = User.Identity.Name;
            User user = db.Users.Where(_user => _user.UserName == userName).SingleOrDefault();
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            List<Skill> skills = db.Skills.Where(s => s.Active).OrderBy(s => s.SkillName).ToList();
            return Mapper.Map<IEnumerable<SkillDTO>>(skills);
        }

        [HttpPost]
        [Route("api/NewSkill")]
        [Authorize]
        [ResponseType(typeof(SkillDTO))]
        public async Task<IHttpActionResult> NewSkill(SkillDTO newSkillDTO)
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

            Skill exist = db.Skills.SingleOrDefault(sec => sec.SkillName == newSkillDTO.SkillName);
            if (exist != null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            newSkillDTO.Active = true;
            newSkillDTO.CreateDate = DateTime.Now;
            Skill skill = Mapper.Map<SkillDTO, Skill>(newSkillDTO);
            db.Skills.Add(skill);
            await db.SaveChangesAsync();

            return Ok(Mapper.Map<Skill, SkillDTO>(skill));
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
