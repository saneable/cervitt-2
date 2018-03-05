using System.Security.Claims;

namespace KaribouAlpha.Authentication
{
    public class ApplicationClaimsPrincipal : ClaimsPrincipal
    {
        public ApplicationClaimsPrincipal(ClaimsPrincipal principal) : base(principal)
        {
        }

        public long UserId
        {
            get {
                return long.Parse(this.FindFirst(ClaimTypes.Sid).Value);
            }
        }
    }
}