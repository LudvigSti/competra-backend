using competra.wwwapi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace competra.wwwapi.Repositories.Interfaces
{
    public interface IMatch
    {
        Task<ICollection<Match>> GetAll();
        Task<ICollection<Match>> GetUserMatches(int activityId, int userId);
        Task<ICollection<Match>> GetUserMatchesByUserId(int userId);
        Task<ICollection<Match>> GetUserMatchesByActivityId(int activityId);
        Task<ICollection<Match>> GetUserMatchesByGroupId(int groupId);
        Task<Match> Create(Match match);

    }
}