
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
