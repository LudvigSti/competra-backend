using competra.wwwapi.Data;
using competra.wwwapi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace competra.wwwapi.Repositories.Repos
{
    public class Group : IGroup
    {
        private DataContext _db;
        public Group(DataContext db) { 
            _db = db;
        }
        public async Task<ICollection<Models.Group>> GetAll()
        {
            return await _db.Groups.ToListAsync();
        }
        public async Task<ICollection<Models.Group>> GetAllUnjoinedGroups(int userId)
        {
            var joinedGroups = await  _db.UserGroups.Where(g => g.UserId == userId).Select(ug => ug.GroupId).ToListAsync();
            var unJoinedGroups =  _db.Groups.Where(g => !joinedGroups.Contains(g.Id));
            return await unJoinedGroups.ToListAsync();
        }




    }
}
