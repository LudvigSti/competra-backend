using competra.wwwapi.Models;

namespace competra.wwwapi.Repositories.Interfaces
{
    public interface IUserGroup
    {
        Task<ICollection<UserGroup>> GetAll();
        Task<ICollection<UserGroup>> GetById(int id);
        Task<UserGroup> Create(UserGroup group);
        Task AddUserToGroup(int groupId, int userId);
        Task LeaveGroup(int userId, int groupId);

    }
}
