namespace competra.wwwapi.DTO
{
    public class GetUserActivityDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Elo { get; set; }
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public string Username { get; set; }
    }
}
