using competra.wwwapi.Data;
using competra.wwwapi.DTO;
using competra.wwwapi.Models;
using competra.wwwapi.Repositories.Interfaces;
using competra.wwwapi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
namespace competra.wwwapi.Controllers
{
    public static class MatchController
    {
        public static void configureMatchController(this WebApplication app)
        {
            var group = app.MapGroup("match");
            group.MapPost("/", Create);
            group.MapGet("/{activityId}/{userId}",GetMatchesByUId);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> GetMatchesByUId(IMatch repo, int activityId, int userId)
        { 
            var matches = await repo.GetUserMatches(activityId,userId);


            if (matches == null )
            {
                return TypedResults.NotFound("User has no matches");
            }
            var matchHistory = matches.Select(m => new UserMatchesDTO
            {
                UserId = userId,
                CreatedAt = m.CreatedAt,
                EloChange = (m.P1Id == userId) ? m.EloChangeP1 : m.EloChangeP2,
                P1Id = m.P1Id,
                P2Id = m.P2Id,
                P1Result = m.P1Result,
                P2Result = m.P2Result,


            });

            return TypedResults.Ok(matchHistory);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> Create(IMatch repo, CreateMatchDTO dto, DataContext context, EloCalculator calculator)
        {
            if (dto.P1Id == null || dto.P2Id == null || dto.ActivityId == null || dto.P1Result == null || dto.P2Result == null)
            {
                return TypedResults.BadRequest("No empty fields allowed.");
            }

            var p1Activity = await context.UserActivities
                .FirstOrDefaultAsync(ua => ua.UserId == dto.P1Id && ua.ActivityId == dto.ActivityId);
            var p2Activity = await context.UserActivities.
                FirstOrDefaultAsync(ua => ua.UserId == dto.P2Id && ua.ActivityId == dto.ActivityId);

            if (p1Activity.Activity == null || p2Activity.User == null)
            {
                TypedResults.NotFound("One of the users are not in the activity");
            }

            var p1OldElo = p1Activity.Elo;
            var p2OldElo = p2Activity.Elo;

            var (eloChangeP1, eloChangeP2, p1Expected, p2Expected) =
              await calculator.CalculateEloChange(dto.P1Result, dto.P2Result, dto.P1Id, dto.P2Id, dto.ActivityId);

            var match = new Match
            {
                P1Id = dto.P1Id,
                P2Id = dto.P2Id,
                ActivityId = dto.ActivityId,
                P1Result = dto.P1Result,
                P2Result = dto.P2Result,
                CreatedAt = DateTime.UtcNow,
                EloChangeP1 = eloChangeP1,
                EloChangeP2 = eloChangeP2
            };

            var createdMatch = await repo.Create(match);



            return TypedResults.Ok(new
            {
                Match = createdMatch,
                p1OldElo,
                P1NewElo = p1Activity.Elo,
                p2OldElo,
                P2NewElo = p2Activity.Elo,
                p1Expected,
                p2Expected
            });
        }
    }
}
