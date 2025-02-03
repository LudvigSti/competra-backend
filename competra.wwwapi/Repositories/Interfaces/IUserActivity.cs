using competra.wwwapi.Models;

namespace competra.wwwapi.Repositories.Interfaces
{
    public interface IUserActivity
    {
        Task<ICollection<UserActivity>> GetAll();
        Task<UserActivity> Create(UserActivity userActivity);
        Task<UserActivity> GetById(int id);
        Task<UserActivity> Update(UserActivity userActivity);
        Task<bool> CheckIfInActivity(int userActivityId, int userId);
        Task DeleteUser(int userActivityId, int userId);
        Task<UserActivity> GetByUserById(int id);
        Task<ICollection<UserActivity>> AllUserActivitiesById(int id);
    }
}
