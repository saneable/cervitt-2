using KaribouAlpha.Models;

namespace KaribouAlpha.Authentication
{
    public class UserManager : Microsoft.AspNet.Identity.UserManager<User, long>
    {
        public UserManager(IUserStore store) : base( store )
        {

        }           
    }
}