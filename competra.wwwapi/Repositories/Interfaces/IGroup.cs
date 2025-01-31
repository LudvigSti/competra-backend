using competra.wwwapi.Models;

namespace competra.wwwapi.Repositories.Interfaces
{
    public interface IGroup
    {
        Task<ICollection<Group>> GetAll();
        Task<ICollection<Group>> GetAllUnjoinedGroups(int userId);
        Task<Group> GetGroup(int groupId);

    }
}
