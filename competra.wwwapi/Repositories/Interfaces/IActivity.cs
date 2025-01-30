using competra.wwwapi.Models;

namespace competra.wwwapi.Repositories.Interfaces
{
    public interface IActivity
    {
        Task<ICollection<Activity>> GetAll();
        Task<Activity> Create(Activity activity);
        Task<Activity> GetById(int id);
    }
}
