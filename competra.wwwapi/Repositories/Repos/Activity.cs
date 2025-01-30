using competra.wwwapi.Data;
using competra.wwwapi.Repositories.Interfaces;
using competra.wwwapi.Models;
using Microsoft.EntityFrameworkCore;

namespace competra.wwwapi.Repositories.Repos
{
    public class Activity : IActivity
    {
        private DataContext _db;
        public Activity(DataContext db)
        {
            _db = db;
        }

        public async Task<ICollection<Models.Activity>> GetAll()
        {
            return await _db.Activities.ToListAsync();
        }
        public async Task<Models.Activity> Create(Models.Activity activity)
        {
            await _db.Activities.AddAsync(activity);

            await _db.SaveChangesAsync();

            return  activity;
        }
        public async Task<Models.Activity> GetById(int activityId)
        {
            return await _db.Activities.FirstOrDefaultAsync(a => a.Id == activityId);
        }
    }
}
