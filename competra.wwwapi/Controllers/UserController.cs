
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
            //group.MapPost("", PostUser);
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
 /*       [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> PostUser(IUser repo)
        {
            try
            {
                
                var users = await repo.PostUser();
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> PostUser([FromBody] CreateUserDTO userDto, IUser repo)
        {
            try
            {
                if (userDto == null || string.IsNullOrWhiteSpace(userDto.Did))
                {
                    return TypedResults.BadRequest("Invalid user data.");
                }

                // Call the repository to create a user
                var createdUser = await repo.PostUser(userDto);
                if (createdUser == null)
                {
                    return TypedResults.NotFound("User creation failed.");
                }

                // Return the created user along with its location
                return TypedResults.Created($"/user/{createdUser.Id}", createdUser);
            }
            catch (Exception ex)
            {
                // Log exception and return internal server error
                return TypedResults.Problem(ex.Message);
            }
        }*/

    }
    
}
