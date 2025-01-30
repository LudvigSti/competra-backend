using competra.wwwapi.DTO;
using competra.wwwapi.Models;
using competra.wwwapi.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace competra.wwwapi.Controllers
{
    public static class UserGroupsController
    {
        public static void configureUserGroupController(this WebApplication app)
        {
            var group = app.MapGroup("usergroup");

            group.MapGet("/", GetAll);
            group.MapGet("/{userId}", GetByUserId);
            group.MapPost("/", Create);
            group.MapPost("/{groupId}/addUser/{userId}", AddUserToGroup);
            group.MapDelete("/{userId}/{groupId}", RemoveUser);

        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> RemoveUser(IUserGroup repo, int userId, int groupId)
        {
            var userGroups = repo.GetAll().Result;
            if (userGroups.FirstOrDefault(ug => ug.UserId == userId && ug.GroupId == groupId) == null) { 
                return TypedResults.NotFound("User is not in that group");
            }
           
            await repo.LeaveGroup(userId, groupId);
            return TypedResults.Ok("Successfully removed from group");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        private static async Task<IResult> GetAll(IUserGroup repo)
        {
            var userGroups = await repo.GetAll();
            var userGroupsDTO = userGroups.Select(ug => new GetUserGroupsDTO
            {
                GroupId = ug.GroupId,
                GroupName = ug.Group.GroupName
            }).ToList();

            return TypedResults.Ok(userGroupsDTO);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> GetByUserId(IUserGroup repo, int userId)
        {
            var userGroups = await repo.GetById(userId);
            if(userGroups == null)
            {
                return TypedResults.NotFound("You have no groups");
            }

            if (userGroups == null || !userGroups.Any())
            {
                return TypedResults.NotFound($"{userId} not found.");
            }
            var usergroupsDTO = userGroups.Select(ug => new GetUserGroupsDTO
            {
                GroupName = ug.Group.GroupName,
                GroupId = ug.GroupId
            });

            return TypedResults.Ok(usergroupsDTO);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        private static async Task<IResult> Create(IUserGroup repo, CreateUserGroupDTO ugDTO)
        {
            if (ugDTO == null)
            {
                return TypedResults.BadRequest("Invalid user group data.");
            }

            var userGroup = new Models.UserGroup
            {
                UserId = ugDTO.UserId,
                GroupId = ugDTO.GroupId
            };

            var createdGroup = await repo.Create(userGroup);
            return TypedResults.Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        private static async Task<IResult> AddUserToGroup(IUserGroup repo, int groupId, int userId)
        {
            
            await repo.AddUserToGroup(groupId, userId);
            return TypedResults.Ok("User successfully added to group.");
        }
    }
}