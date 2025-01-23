namespace competra.wwwapi.Models
{
    public class Match
    {
        public int Id { get; set; }
        public int P1Id { get; set; }
        public int P2Id { get; set;}
        public DateOnly CreatedAt { get; set; }
        public int ActivityId { get; set; }

        public double EloChangeP1 { get; set; }
        public double EloChangeP2 { get; set; }
        public double P1Result { get; set; }
        public double P2Result { get; set;}
        public Activity Activity { get; set; }
        
    }
}
