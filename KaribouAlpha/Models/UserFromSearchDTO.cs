using System;
using System.Collections.Generic;

namespace KaribouAlpha.Models
{
    public class UserFromSearchDTO
    {
        public long UserID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String UserName { get; set; }
    }

    public class UserFromSearchCompare : IEqualityComparer<UserFromSearchDTO>
    {
        public bool Equals(UserFromSearchDTO x, UserFromSearchDTO y)
        {
            if( x.UserID == y.UserID) { return true; } else { return false; }
        }

        public int GetHashCode(UserFromSearchDTO obj)
        {
            return (int) obj.UserID;
        }
    }
}