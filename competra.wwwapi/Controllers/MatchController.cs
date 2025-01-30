using competra.wwwapi.Data;
using competra.wwwapi.DTO;
using competra.wwwapi.Models;
using competra.wwwapi.Repositories.Interfaces;
using competra.wwwapi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace competra.wwwapi.Controllers
{
    public static class MatchControllerExtensions
    {
        public static void configureMatchController(this WebApplication app)
        {
            var group = app.MapGroup("match");
            group.MapPost("/create", MatchController.Create);
        }
    }
    

    public class MatchController : ControllerBase
    {
        private readonly DataContext _context;

        public MatchController(DataContext context)
        {
            _context = context;
        }
        

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> Create(IMatch repo, CreateMatchDTO dto, DataContext context)
        {
            if (dto.P1Id == null || dto.P2Id == null || dto.ActivityId == null || dto.P1Result == null || dto.P2Result == null)
            {
                return TypedResults.BadRequest("No empty fields allowed.");
            }

            var p1Activity = await context.UserActivities.FirstOrDefaultAsync(ua => ua.UserId == dto.P1Id && ua.ActivityId == dto.ActivityId);
            var p2Activity = await context.UserActivities.FirstOrDefaultAsync(ua => ua.UserId == dto.P2Id && ua.ActivityId == dto.ActivityId);
            if (p1Activity.Activity == null || p2Activity.User == null)
            {
                TypedResults.NotFound("One of the users are not in the activity");
            }

            var p1OldElo = p1Activity.Elo;
            var p2OldElo = p2Activity.Elo;

            var controller = new MatchController(context);
            var (eloChangeP1, eloChangeP2, p1Expected, p2Expected) = await controller.CalculateEloChange(dto.P1Result, dto.P2Result, dto.P1Id, dto.P2Id, dto.ActivityId);

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

            p1Activity = await context.UserActivities.FirstOrDefaultAsync(ua => ua.UserId == dto.P1Id && ua.ActivityId == dto.ActivityId);
            p2Activity = await context.UserActivities.FirstOrDefaultAsync(ua => ua.UserId == dto.P2Id && ua.ActivityId == dto.ActivityId);

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

        public async Task<(int, int, double, double)> CalculateEloChange(double p1Result, double p2Result, int p1Id, int p2Id, int activityId)
        {
            var p1Activity = await _context.UserActivities.FirstOrDefaultAsync(ua => ua.UserId == p1Id && ua.ActivityId == activityId);
            var p2Activity = await _context.UserActivities.FirstOrDefaultAsync(ua => ua.UserId == p2Id && ua.ActivityId == activityId);

            if (p1Activity == null || p2Activity == null)
            {
                throw new Exception("UserActivity not found for one or both players.");
            }

            if (p1Result > p2Result)
            {
                p1Result = 1.0;
                p2Result = 0.0;
            }
            else if (p1Result < p2Result)
            {
                p1Result = 0.0;
                p2Result = 1.0;
            }
            else
            {
                p1Result = 0.5;
                p2Result = 0.5;
            }

            double p1Elo = p1Activity.Elo;
            double p2Elo = p2Activity.Elo;
            int k = 32;
            double p1Expected = 1 / (1 + Math.Pow(10, (p2Elo - p1Elo) / 400.0));
            double p2Expected = 1 / (1 + Math.Pow(10, (p1Elo - p2Elo) / 400.0));
            int p1Change = (int)(k * (p1Result - p1Expected));
            int p2Change = (int)(k * (p2Result - p2Expected));

            p1Activity.Elo += p1Change;
            p2Activity.Elo += p2Change;

            _context.UserActivities.Update(p1Activity);
            _context.UserActivities.Update(p2Activity);
            await _context.SaveChangesAsync();

            return (p1Change, p2Change, p1Expected, p2Expected);
        }
    }
}
