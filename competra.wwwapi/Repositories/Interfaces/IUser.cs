using competra.wwwapi.Models;

namespace competra.wwwapi.Repositories.Interfaces
{
    public interface IUser
    {
        Task<ICollection<User>> GetAll();
    }
}
