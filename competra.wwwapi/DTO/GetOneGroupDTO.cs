using System.ComponentModel.DataAnnotations;

namespace competra.wwwapi.DTO
{
    public class GetOneGroupDTO
    {
        public int Id { get; set; }
        [Required]
        public string GroupName { get; set; }
        public string LogoUrl { get; set; }
        public DateOnly CreatedAt { get; set; }
    }
}
