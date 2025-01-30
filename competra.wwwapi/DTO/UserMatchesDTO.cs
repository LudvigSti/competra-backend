using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace competra.wwwapi.DTO
{
    public class UserMatchesDTO
    {
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
       public int P1Id { get; set; }
        public  int P2Id { get; set; }
        public double P1Result { get; set; }
        public double P2Result { get; set; }
        public int EloChange {  get; set; }
        
    }
}
