namespace KaribouAlpha.Models
{
    public class UpdateFollowerGroupsForFollowerDTO
    {
        public long FollowerUserID { get; set; }
        public long[] FollowerGroupIDs { get; set; }
    }
}