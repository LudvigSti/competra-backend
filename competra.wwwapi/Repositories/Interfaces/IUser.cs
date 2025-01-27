using competra.wwwapi.Models;

namespace competra.wwwapi.Repositories.Interfaces
{
    public interface IUser
    {
        Task<ICollection<User>> GetAll();
        Task<User> Create(Models.User user);
        Task<User> GetById(int id);
    }
}
