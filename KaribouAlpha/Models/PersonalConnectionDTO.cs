namespace KaribouAlpha.Models
{
    public class PersonalConnectionDTO
    {
        public long FollowingID { get; set; }
        public long FollowerID { get; set; }
        public string FollowingFirstName { get; set; }
        public string FollowingLastName { get; set; }
        public string FollowingCompanyName { get; set; }
        public string FollowingJobRole { get; set; }
        public string FollowerFirstName { get; set; }
        public string FollowerLastName { get; set; }
        public string FollowerCompanyName { get; set; }
        public string FollowerJobRole { get; set; }
    }
}