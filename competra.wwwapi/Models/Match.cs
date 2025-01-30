namespace competra.wwwapi.Models
{
    public class Match
    {
        public int Id { get; set; }
        public int P1Id { get; set; }
        public int P2Id { get; set;}
        public DateTime CreatedAt { get; set; }
        public int ActivityId { get; set; }

        public int EloChangeP1 { get; set; }
        public int EloChangeP2 { get; set; }
        public double P1Result { get; set; }
        public double P2Result { get; set;}
        public Activity Activity { get; set; }
        
    }
}
