using competra.wwwapi.Models;

namespace competra.wwwapi.Repositories.Interfaces
{
    public interface IActivity
    {
        Task<ICollection<Activity>> GetAll();
        Task<Activity> Create(Activity activity);
        Task<Activity> GetById(int id);
        Task <ICollection<Activity>> GetAllByGroupId(int groupId);
        Task<Activity>Leaderboard(int activityId);
    }
}
