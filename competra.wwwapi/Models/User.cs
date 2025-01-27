namespace competra.wwwapi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
        public List<UserActivity> UserActivities { get; set; } = new List<UserActivity>();

    }
}
