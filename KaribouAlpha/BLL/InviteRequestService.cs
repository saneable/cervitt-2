using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KaribouAlpha.Models;
using KaribouAlpha.DAL;

namespace KaribouAlpha.BLL
{
    public class InviteRequestService : IInviteRequestService, IDisposable
    {
        private KaribouAlphaContext db = null;
        public InviteRequestService()
        {
            db = new KaribouAlphaContext();
        }

        public void Dispose()
        {
           if(db != null)
            {
                db.Dispose();
            }
        }

        public long Insert(InviteRequest request)
        {
            if (request == null)
            {
                return 0;
            }
            if (string.IsNullOrEmpty(request.InviteToEmailAddress))
            {
                return 0;
            }
            db.InviteRequests.Add(request);
            db.SaveChangesAsync();
            return request.Id;
        }

        public InviteRequest Get(Guid inviteCode)
        {
            return db.InviteRequests.SingleOrDefault(c => c.InviteCode == inviteCode);
        }

        public InviteRequest GetByInviteEmailAddress(string emailAddress)
        {
            return db.InviteRequests.SingleOrDefault(c => c.InviteToEmailAddress == emailAddress && !c.Processed);
        }
    }

    public interface IInviteRequestService : IDisposable
    {
        long Insert(InviteRequest request);

        InviteRequest Get(Guid inviteCode);

        InviteRequest GetByInviteEmailAddress(string emailAddress);
    }
}