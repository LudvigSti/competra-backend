namespace competra.wwwapi.Models
{
    public class UserActivity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Elo { get; set; }
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
        public User User { get; set; }
    }
}
