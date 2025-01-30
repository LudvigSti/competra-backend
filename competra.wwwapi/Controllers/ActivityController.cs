
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
            group.MapPost("/", Create);
            group.MapGet("/", GetAll);
            group.MapGet("/{groupId}", GetAllByGId);
            group.MapGet("/leaderboard/{activityId}", GetLeaderboard);

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> GetAllByGId(IActivity repo, int groupId)
        {
            var groupActivities = await repo.GetAllByGroupId(groupId);

            if (groupActivities == null || !groupActivities.Any())
            {
                return TypedResults.NotFound("No activities found for this group.");
            }

            var groupActivitiesDTO = groupActivities.Select(a => new GroupActivityDTO
            {
                
                GroupId = a.Group.Id, 
                GroupName = a.Group.GroupName,
                ActivityId = a.Id,
                ActivityName = a.ActivityName
            });

            return TypedResults.Ok(groupActivitiesDTO);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> GetLeaderboard(IActivity repo, int activityId)
        {
            try
            {
                var activity = await repo.Leaderboard(activityId);
                if (activity == null)
                {
                    return TypedResults.NotFound("Activity does not exist");
                }
                var leaderboards = activity.UserActivities.Select(ldb => new LeaderboardDTO
                {
                    UserName = ldb.User.Username,
                    Elo = ldb.Elo,

                }).OrderByDescending(user => user.Elo);
                return TypedResults.Ok(leaderboards);
            }
            catch (Exception)
            {

                throw;
            }
            
        
        }

    }
    
}
