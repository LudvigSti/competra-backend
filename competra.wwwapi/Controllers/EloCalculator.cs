using Microsoft.EntityFrameworkCore;
using competra.wwwapi.Data;

namespace competra.wwwapi.Controllers
{
    public class EloCalculator
    {
        DataContext _context;
        public EloCalculator(DataContext context)
        {
            _context = context;
        }
        public async Task<(int, int, double, double)> CalculateEloChange(double p1Result, double p2Result, int p1Id, int p2Id, int activityId)
        {
            var p1Activity = await _context.UserActivities.FirstOrDefaultAsync(ua => ua.UserId == p1Id && ua.ActivityId == activityId);
            var p2Activity = await _context.UserActivities.FirstOrDefaultAsync(ua => ua.UserId == p2Id && ua.ActivityId == activityId);

            if (p1Activity == null || p2Activity == null)
            {
                throw new Exception("UserActivity not found for one or both players.");
            }

            // Normalize results to Elo format
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

            // Get match count for both players
            int p1Matches = await _context.Matches.CountAsync(m => m.P1Id == p1Id || m.P2Id == p1Id);
            int p2Matches = await _context.Matches.CountAsync(m => m.P1Id == p2Id || m.P2Id == p2Id);

            // Determine K-factor for both players
            int k1 = GetKFactor(p1Elo, p1Matches);
            int k2 = GetKFactor(p2Elo, p2Matches);

            // Calculate expected scores
            double p1Expected = 1 / (1 + Math.Pow(10, (p2Elo - p1Elo) / 400.0));
            double p2Expected = 1 / (1 + Math.Pow(10, (p1Elo - p2Elo) / 400.0));

            // Calculate Elo changes based on K-factors
            int p1Change = (int)(k1 * (p1Result - p1Expected));
            int p2Change = (int)(k2 * (p2Result - p2Expected));

            // Update Elo ratings
            p1Activity.Elo += p1Change;
            p2Activity.Elo += p2Change;

            _context.UserActivities.Update(p1Activity);
            _context.UserActivities.Update(p2Activity);
            await _context.SaveChangesAsync();

            return (p1Change, p2Change, p1Expected, p2Expected);

        }
        private int GetKFactor(double elo, int matches)
        {
            if (elo < 2300 && matches < 30)
                return 40;
            else if (elo < 2400)
                return 20;
            else if (matches >= 30 && elo >= 2400)
                return 10;

            return 20; // Default K-factor
        }

    }
}
