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
            group.MapGet("/{activityId}/{userId}", GetMatchHistoryByUserId);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> GetMatchHistoryByUserId(IMatch repo, int activityId, int userId, DataContext context)
        {
            var matches = await repo.GetUserMatches(activityId, userId);

            if (matches == null || !matches.Any())
            {
                return TypedResults.NotFound("User has no matches in this activity.");
            }

            var userIds = matches.SelectMany(m => new[] { m.P1Id, m.P2Id }).Distinct();
            var users = await context.Users
                .Where(u => userIds.Contains(u.Id))
                .ToDictionaryAsync(u => u.Id, u => u.Username); 

            var matchHistory = matches.Select(m => new UserMatchesDTO
            {
                UserId = userId,
                CreatedAt = m.CreatedAt,
                EloChange = (m.P1Id == userId) ? m.EloChangeP1 : m.EloChangeP2,
                Result = (m.P1Id == userId)
                    ? (m.P1Result > m.P2Result ? "Won" : "Lost")
                    : (m.P2Result > m.P1Result ? "Won" : "Lost"),
                OpponentId = (m.P1Id == userId) ? m.P2Id : m.P1Id,
                OpponentName = users.TryGetValue((m.P1Id == userId) ? m.P2Id : m.P1Id, out var name) ? name : " "
            }).ToList();

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
