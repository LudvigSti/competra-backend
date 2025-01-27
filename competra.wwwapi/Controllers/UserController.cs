
using competra.wwwapi.DTO;
using competra.wwwapi.Models;
using competra.wwwapi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace competra.wwwapi.Controllers
{
    public static class UserController
    {
        public static void configureUserController(this WebApplication app)
        {
            var group = app.MapGroup("user");
            group.MapGet("/", GetAll);
            group.MapPost("/", CreateUser);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        private static async Task<IResult> CreateUser(IUser repo, CreateUserDTO dto) 
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

            return TypedResults.Ok(createdUser);
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

    }
    
}
