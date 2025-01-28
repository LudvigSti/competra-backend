using System.ComponentModel.DataAnnotations;

namespace competra.wwwapi.DTO
{
    public class CreateUserDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
