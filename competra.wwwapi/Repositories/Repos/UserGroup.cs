using competra.wwwapi.Repositories.Interfaces;

namespace competra.wwwapi.Repositories.Repos
{
    public class UserGroup : IUserGroup
    {
        public Task<Models.UserGroup> AddUserToGroup(int groupId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Models.UserGroup> Create(Models.UserGroup group)
        {
            throw new NotImplementedException();
        }

        public Task<Models.UserGroup> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Models.UserGroup> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
