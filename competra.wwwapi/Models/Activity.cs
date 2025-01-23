namespace competra.wwwapi.Models
{
    public class Activity
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string ActivityName { get; set; }
        public Group Group { get; set; }

        public List<Match> Matches { get; set; } = new List<Match>();
        public List<UserActivity> UserActivities { get; set; } = new List<UserActivity>();

    }


}
