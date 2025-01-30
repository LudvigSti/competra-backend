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

            return  match;
        }
        public async Task<ICollection<Models.Match>> GetUserMatches(int userId, int activityId)
        {
            return await _db.Matches.Include(m => m.Activity)
        .ThenInclude(a => a.UserActivities)
        .Where(m => (m.P1Id == userId || m.P2Id == userId) && m.Activity.Id == activityId) // Ensure the activity matches
        .ToListAsync();
        }


    }
}
