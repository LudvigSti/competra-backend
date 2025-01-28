using competra.wwwapi.Models;

namespace competra.wwwapi.Repositories.Interfaces
{
    public interface IUser
    {
        Task<ICollection<User>> GetAll();
        Task<User> Create(User user);
        Task<User> GetById(int id);
        Task<User> Update( User user);
        Task<User>GetByUsername(string username);
    }
}
