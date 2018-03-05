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
    public class InviteController : ApiController
    {
        private KaribouAlphaContext db = new KaribouAlphaContext();

        [HttpGet]
        [Route("api/GetInviteByCode")]
        [AllowAnonymous]
        [ResponseType(typeof(Models.Invite))]
        public async Task<IHttpActionResult> GetInviteByCode(string code)
        {
            Invite invite = new Invite();
            if (!string.IsNullOrEmpty(code))
            {
                Guid inviteCode = Guid.Parse(code);
                InviteRequest inviteRequest = await db.InviteRequests.SingleOrDefaultAsync(c => c.InviteCode == inviteCode && !c.Processed);
                if(inviteRequest != null)
                {
                    invite = new Invite { EmailAddress = inviteRequest.InviteToEmailAddress, ToUserId = 0 };
                }
            }
            return Ok(invite);
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
