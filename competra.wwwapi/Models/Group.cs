namespace competra.wwwapi.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public string LogoUrl { get; set; }
        public DateOnly CreatedAt { get; set; }
        public List<Activity> Activities { get; set; } = new List<Activity>();
        public List<UserGroup> UserGroups { get; set; } = new List<UserGroup>();

    }
}
