namespace competra.wwwapi.DTO
{
    public class GetMatchDTO
    {
        public int Id { get; set; }
        public string P1Name { get; set; }
        public string P2Name { get; set; }
        public string ActivityName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string P1Result { get; set; }
        public string P2Result { get; set; }
        public int EloChangeP1 { get; set; }
        public int EloChangeP2 { get; set; }
    }
}