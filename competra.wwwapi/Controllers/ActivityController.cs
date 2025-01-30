
using competra.wwwapi.DTO;
using competra.wwwapi.Models;
using competra.wwwapi.Repositories.Interfaces;
using competra.wwwapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace competra.wwwapi.Controllers
{
    public static class ActivityController
    {
        public static void configureActivityController(this WebApplication app)
        {
            var group = app.MapGroup("activity");
            group.MapPost("/create", Create);
            group.MapGet("/", GetAll);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> GetAll(IActivity repo)
        {
            try
            {
                
                var activities = await repo.GetAll();
                if(activities == null)
                {
                    return TypedResults.NotFound();
                }
                return TypedResults.Ok(activities);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        private static async Task<IResult> Create(IActivity repo, CreateActivityDTO dto)
        {
            if (dto.ActivityName == null || dto.ActivityName == null)
            {
                return TypedResults.BadRequest("No empty fields allowed.");
            }

            var activity = new Activity
            {
                ActivityName = dto.ActivityName,
                GroupId = dto.GroupId,
            
            };

            var createdActivity = await repo.Create(activity);

            return TypedResults.Ok(createdActivity);
        }

    }
    
}
