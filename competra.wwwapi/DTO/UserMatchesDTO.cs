using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace competra.wwwapi.DTO
{
    public class UserMatchesDTO
    {
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
       public string OpponentName { get; set; }
        public int OpponentId { get; set; }
        public string Result { get; set; }
        public int EloChange {  get; set; }
        
    }
}
