using competra.wwwapi.Data;
using competra.wwwapi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using competra.wwwapi.Models;

namespace competra.wwwapi.Repositories.Repos
{
    public class UserGroup : IUserGroup
    {
        private DataContext _db;
        public UserGroup(DataContext db)
        {
            _db = db;
        }
        public async Task AddUserToGroup(int groupId, int userId)
        {
            var group = await _db.Groups.FirstOrDefaultAsync(g => g.Id == groupId);
            var user = await _db.Users.FindAsync(userId);

            if (group != null && user != null)
            {
                var userGroup = new Models.UserGroup
                {
                    UserId = user.Id,
                    GroupId = group.Id
                };
                _db.UserGroups.Add(userGroup);
                await _db.SaveChangesAsync();
            }
            
        }

        public async Task<Models.UserGroup> Create(Models.UserGroup group)
        {
            _db.UserGroups.Add(group);
            await _db.SaveChangesAsync();
            return group;
        }

        public async Task<ICollection<Models.UserGroup>> GetAll()
        {
            return await _db.UserGroups
                    .Include(ug => ug.Group)
                    .Distinct()  // Ensure unique groups
                    .ToListAsync();
        }

        public async Task<ICollection<Models.UserGroup>> GetById(int userId)
        {
            var userGroups = await _db.UserGroups.Include(ug => ug.Group).Where(u => u.UserId == userId).ToListAsync();
            return  userGroups;
        }
    }
}
