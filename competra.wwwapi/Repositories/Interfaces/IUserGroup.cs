using competra.wwwapi.Models;

namespace competra.wwwapi.Repositories.Interfaces
{
    public interface IUserGroup
    {
        Task<UserGroup> GetAll();
        Task<UserGroup> GetById(int id);
        Task<UserGroup> Create(UserGroup group);
        Task<UserGroup> AddUserToGroup(int groupId, int userId);

    }
}
