using competra.wwwapi.DTO;
using competra.wwwapi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace competra.wwwapi.Controllers
{
    public static class GroupController
    {
        public static void configureGroupController(this WebApplication app)
        {
            var group = app.MapGroup("group");
            group.MapGet("/", GetAll);
            group.MapGet("/{userId}", GetAllUnjoined);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> GetAll(IGroup repo)
        {
            var groups = await repo.GetAll();
            if (groups == null) { return TypedResults.NotFound("There are no groups here!"); }
            var groupsdto = groups.Select(ug => new GroupDTO
            {
                GroupId = ug.Id,
                GroupName = ug.GroupName
            });
            
            return TypedResults.Ok(groupsdto);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> GetAllUnjoined(IGroup repo, int userId)
        {

            var unjoinedGroups = await repo.GetAllUnjoinedGroups(userId);
          
            if (unjoinedGroups == null || !unjoinedGroups.Any())
            {
                return TypedResults.NotFound("You are in all groups!");
            }
            var unjoinedDTO = unjoinedGroups.Select(ung => new GroupDTO
            {
                GroupId=ung.Id,
                GroupName = ung.GroupName
            });
            return TypedResults.Ok(unjoinedDTO);
        }
    }
}
 