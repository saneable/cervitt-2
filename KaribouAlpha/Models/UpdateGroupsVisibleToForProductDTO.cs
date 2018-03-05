namespace KaribouAlpha.Models
{
    public class UpdateGroupsVisibleToForProductDTO
    {
        public long ProductID { get; set; }
        public long[] FollowerGroupIDs { get; set; }
    }
}