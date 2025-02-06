using competra.wwwapi.Data;
using competra.wwwapi.Repositories.Interfaces;
using competra.wwwapi.Models;
using Microsoft.EntityFrameworkCore;

namespace competra.wwwapi.Repositories.Repos
{
    public class Match : IMatch
    {
        private DataContext _db;
        public Match(DataContext db)
        {
            _db = db;
        }

        public async Task<ICollection<Models.Match>> GetAll()
        {
            return await _db.Matches.ToListAsync();
        }
        public async Task<Models.Match> Create(Models.Match match)
        {
            await _db.Matches.AddAsync(match);

            await _db.SaveChangesAsync();

            return match;
        }
        //public async Task<ICollection<Models.Match>> GetUserMatches(int userId, int activityId)
        //{
        //    return await _db.Matches.Include(m => m.Activity)
        //.ThenInclude(a => a.UserActivities)
        //.Where(m => (m.P1Id == userId || m.P2Id == userId) && m.Activity.Id == activityId) // Ensure the activity matches
        //.ToListAsync();
        //}
        public async Task<ICollection<Models.Match>> GetUserMatches(int activityId, int userId)
        {
            var matches = await _db.Matches
         .Where(m => (m.P1Id == userId || m.P2Id == userId) && m.ActivityId == activityId) // <-- Added ActivityId filter
         .ToListAsync();

            Console.WriteLine($"Repository: Found {matches.Count} matches for User {userId} in Activity {activityId}");

            foreach (var match in matches)
            {
                Console.WriteLine($"MatchId: {match.Id}, P1Id: {match.P1Id}, P2Id: {match.P2Id}, ActivityId: {match.ActivityId}");
            }

            return matches;
        }

        public async Task<ICollection<Models.Match>> GetUserMatchesByUserId(int userId)
        {
            var matches = await _db.Matches
                .Where(m => m.P1Id == userId || m.P2Id == userId)
                .ToListAsync();

            Console.WriteLine($"Repository: Found {matches.Count} matches for User {userId}");

            foreach (var match in matches)
            {
                Console.WriteLine($"MatchId: {match.Id}, P1Id: {match.P1Id}, P2Id: {match.P2Id}, ActivityId: {match.Activity}");
            }
            return matches;
        }

        public async Task<ICollection<Models.Match>> GetUserMatchesByActivityId(int activityId)
        {
            var matches = await _db.Matches
                .Where(m => m.ActivityId == activityId)
                .ToListAsync();

            Console.WriteLine($"Repository: Found {matches.Count} matches for Activity {activityId}");

            foreach (var match in matches)
            {
                Console.WriteLine($"MatchId: {match.Id}, P1Id: {match.P1Id}, P2Id: {match.P2Id}, ActivityId: {match.ActivityId}");
            }
            return matches;
        }

        public async Task<ICollection<Models.Match>> GetUserMatchesByGroupId(int groupId)
        {
            // Find all activities with the specified groupId
            var activities = await _db.Activities
                .Where(a => a.GroupId == groupId)
                .ToListAsync();

            var matches = new List<Models.Match>();

            // For each activity, find all matches
            foreach (var activity in activities)
            {
                var activityMatches = await _db.Matches
                    .Where(m => m.ActivityId == activity.Id)
                    .Include(m => m.P1) // Include Player 1
                    .Include(m => m.P2) // Include Player 2
                    .Include(m => m.Activity) // Include Activity
                    .ToListAsync();

                matches.AddRange(activityMatches);
            }

            // Order all matches by CreatedAt in descending order and take the last 3 matches
            var lastMatches = matches
                .OrderByDescending(m => m.CreatedAt)
                .Take(3)
                .ToList();

            return lastMatches;
        }


    }
}
