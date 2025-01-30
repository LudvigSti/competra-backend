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
        public async Task<Models.Match> GetById(int matchId)
        {
            return await _db.Matches.FirstOrDefaultAsync(m => m.Id == matchId);
        }


    }
}
