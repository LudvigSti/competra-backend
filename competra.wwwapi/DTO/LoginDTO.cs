using System.ComponentModel.DataAnnotations;

namespace competra.wwwapi.DTO
{
    public class LoginDTO
    {
        

            [Required]
            public string Username { get; set; }

            [Required]
            public string Password { get; set; }

        }
    
}
