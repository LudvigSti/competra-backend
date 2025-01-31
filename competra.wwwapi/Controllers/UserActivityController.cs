
using competra.wwwapi.DTO;
using competra.wwwapi.Models;
using competra.wwwapi.Repositories.Interfaces;
using competra.wwwapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace competra.wwwapi.Controllers
{
    public static class UserActivityController
    {
        public static void configureUserActivityController(this WebApplication app)
        {
            var group = app.MapGroup("UserActivity");
            group.MapPost("/", Create);
            group.MapPut("/", Update);
            group.MapGet("/", GetAll);
            group.MapGet("/{activityId}/{userId}",CheckInActivity);
        }

        private static async Task<IResult> CheckInActivity(IUserActivity repo, int activityId, int userId)
        {
            bool inGroup = await repo.CheckIfInActivity(activityId, userId); 
            if (!inGroup)
            {
                return TypedResults.NotFound(inGroup);
            }
            return TypedResults.Ok(inGroup);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> GetAll(IUserActivity repo)
        {
            try
            {
                
                var userActivities = await repo.GetAll();
                if(userActivities == null)
                {
                    return TypedResults.NotFound();
                }
                var getuserActivitiesDTO = userActivities.Select(ua => new GetUserActivityDTO
                {
                    Id = ua.Id,
                    UserId = ua.UserId,
                    Elo = ua.Elo,
                    ActivityId = ua.ActivityId,
                    ActivityName = ua.Activity.ActivityName,
                    Username = ua.User.Username
                }).ToList();
                return TypedResults.Ok(getuserActivitiesDTO);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        private static async Task<IResult> Create(IUserActivity repo, CreateUserActivityDTO dto)
        {
            var userActivity = new UserActivity
            {
                UserId = dto.UserId,
                ActivityId = dto.ActivityId,
                Elo = 1000,
            
            };

            var createdUserActivity = await repo.Create(userActivity);

            return TypedResults.Ok(createdUserActivity);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        private static async Task<IResult> Update(IUserActivity repo, UpdateEloDTO dto)
        {
            var userActivity = await repo.GetById(dto.UserActivityId);
            if (userActivity == null)
            {
                return TypedResults.BadRequest("UserActivity not found.");
            }

            userActivity.Elo = 1000;

            await repo.Update(userActivity);
            return TypedResults.Ok("Elo successfully updated.");
        }
        

    }
    
}
