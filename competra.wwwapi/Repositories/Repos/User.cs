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
        public async Task<Models.User> Create(Models.User user)
        {
            await _db.Users.AddAsync(user);

            await _db.SaveChangesAsync();

            return  user;
        }
        public async Task<Models.User> GetById(int userId)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }


    }
}
