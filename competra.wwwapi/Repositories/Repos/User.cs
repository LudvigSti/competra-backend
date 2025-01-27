using competra.wwwapi.Data;
using competra.wwwapi.Repositories.Interfaces;
using competra.wwwapi.Models;
using Microsoft.EntityFrameworkCore;

namespace competra.wwwapi.Repositories.Repos
{
    public class User : IUser
    {
        private DataContext _db;
        public User(DataContext db)
        {
            _db = db;
        }

        public async Task<ICollection<Models.User>> GetAll()
        {
            return await _db.Users.ToListAsync();
        }
        public async Task<ICollection<Models.User>> PostUser()
        {
            return await _db.Users.ToListAsync();
        }

        
    }
}
