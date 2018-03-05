namespace KaribouAlpha.Models
{
    public class UpdateGroupsVisibleToForProductFileDTO
    {
        public long ProductFileID { get; set; }
        public long[] FollowerGroupIDs { get; set; }
    }
}