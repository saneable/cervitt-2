using KaribouAlpha.DAL;
using KaribouAlpha.Models;

namespace KaribouAlpha.Authentication
{
    public interface IUserStore : Microsoft.AspNet.Identity.IUserStore<User, long> { }

    public class UserStore : Microsoft.AspNet.Identity.EntityFramework.UserStore<User, ApplicationRole, long, UserLogin, UserRole, UserClaim>, IUserStore
    {
        public UserStore() : base( new KaribouAlphaContext() )
        {

        }
        public UserStore(KaribouAlphaContext context) : base(context)
        {

        }
    }
}