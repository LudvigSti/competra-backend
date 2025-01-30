using competra.wwwapi.Models;

namespace competra.wwwapi.Repositories.Interfaces
{
    public interface IUserActivity
    {
        Task<ICollection<UserActivity>> GetAll();
        Task<UserActivity> Create(UserActivity userActivity);
        Task<UserActivity> GetById(int id);
        Task<UserActivity> Update(UserActivity userActivity);
    }
}
