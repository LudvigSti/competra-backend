
using competra.wwwapi.DTO;
using competra.wwwapi.Models;
using competra.wwwapi.Repositories.Interfaces;
using competra.wwwapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace competra.wwwapi.Controllers
{
    public static class UserController
    {
        public static void configureUserController(this WebApplication app)
        {
            var group = app.MapGroup("user");
            group.MapGet("/", GetAll);
            group.MapPost("/register", Register);
            group.MapPost("/login", Login);
            group.MapPut("/", Update);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        private static async Task<IResult> Update(IUser repo, string currentUsername, string newUsername)
        {
            if (string.IsNullOrEmpty(newUsername) || newUsername.Length < 3)
            {
                return TypedResults.BadRequest("The new username must be at least 3 characters long.");
            }
            var user = await repo.GetByUsername(currentUsername);

            if (user == null)
            {
                return TypedResults.NotFound($"{user} not found.");
            }
            var takenUser = await repo.GetByUsername(newUsername);
            if (takenUser != null) {
                return TypedResults.BadRequest($"The username '{newUsername}' is already taken.");
            }
            user.Username = newUsername;

            await repo.Update(user);
            return TypedResults.Ok($"Username successfully updated to '{newUsername}'.");
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        private static async Task<IResult> Register(IUser repo, CreateUserDTO dto, AuthService authService) 
        {
            if (string.IsNullOrEmpty(dto.Username) || string.IsNullOrEmpty(dto.Password))
            {
                return TypedResults.BadRequest("Username and password cannot be empty.");
            }
            if (dto.Password.Length < 5 || dto.Username.Length < 3) 
            {
                return TypedResults.BadRequest("Username has to be more than 3 letters and password more than 5");
            }

            
            var user = new User
            {
                Username = dto.Username, 
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            
        };

            var createdUser = await repo.Create(user);
            var token = authService.GenerateToken(createdUser);




            return TypedResults.Ok(new { Token = token });

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> GetAll(IUser repo)
        {
            try
            {
                
                var users = await repo.GetAll();
                if(users == null)
                {
                    return TypedResults.NotFound();
                }
                return TypedResults.Ok(users);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        private static async Task<IResult> Login(IUser repo, LoginDTO dto, AuthService authService)
        {
            // Check for missing username or password
            if (string.IsNullOrEmpty(dto.Username) || string.IsNullOrEmpty(dto.Password))
            {
                return TypedResults.BadRequest("Username and password cannot be empty.");
            }

            var user = await repo.GetByUsername(dto.Username);
            if (user == null)
            {
                return TypedResults.Unauthorized();
            }


            // Verify password
            var passwordVerified = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);
            if (!passwordVerified)
            {
                return TypedResults.Unauthorized();
            }

            // Generate JWT token
            var token = authService.GenerateToken(user);

            // Return the token
            return TypedResults.Ok(new { Token = token });
        }

    }
    
}
