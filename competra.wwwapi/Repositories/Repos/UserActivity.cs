using competra.wwwapi.Data;
using competra.wwwapi.Repositories.Interfaces;
using competra.wwwapi.Models;
using Microsoft.EntityFrameworkCore;

namespace competra.wwwapi.Repositories.Repos
{
    public class UserActivity : IUserActivity
    {
        private DataContext _db;
        public UserActivity(DataContext db)
        {
            _db = db;
        }

        public async Task<ICollection<Models.UserActivity>> GetAll()
        {
            return await _db.UserActivities
            .Include(ua=>ua.Activity)
            .Include(ua=>ua.User)
            .ToListAsync();
        }
        public async Task<Models.UserActivity> Create(Models.UserActivity userActivity)
        {
            await _db.UserActivities.AddAsync(userActivity);

            await _db.SaveChangesAsync();

            return  userActivity;
        }
        public async Task<Models.UserActivity> GetById(int userActivityId)
        {
            return await _db.UserActivities.FirstOrDefaultAsync(ua => ua.Id == userActivityId);
        }

        public async Task<Models.UserActivity> Update(Models.UserActivity userActivity)
        {
            var oldUserActivity = await _db.UserActivities.FindAsync(userActivity.Id);
            oldUserActivity.UserId = userActivity.UserId;
            oldUserActivity.ActivityId = userActivity.ActivityId;
            oldUserActivity.Elo = userActivity.Elo;
            await _db.SaveChangesAsync();
            return userActivity;
            
        }

        public async Task<bool> CheckIfInActivity(int activityId, int userId)
        {
            return await _db.UserActivities
                .Include(ua=> ua.Activity)
                .Include(ua => ua.User)
                    .AnyAsync(ua => ua.ActivityId == activityId && ua.UserId == userId);
        }

        public async Task DeleteUser(int activityId, int userId)
        {
            var userActivity = await _db.UserActivities
        .FirstOrDefaultAsync(ua => ua.ActivityId == activityId && ua.UserId == userId);

            if (userActivity == null)
            {
                return; 
            }

            _db.UserActivities.Remove(userActivity);
            await _db.SaveChangesAsync(); 

        }

        public async Task<Models.UserActivity> GetByUserById(int id)
        {
            return await _db.UserActivities.Include(ua => ua.User).Include(ua => ua.Activity).FirstOrDefaultAsync(ua => ua.User.Id == id);
        }

        public async Task<ICollection<Models.UserActivity>> AllUserActivitiesById(int id)
        {
            return await _db.UserActivities.Include(ua => ua.Activity).Include(u => u.User).Where(ua => ua.UserId == id).ToListAsync();
        }
    }
}
