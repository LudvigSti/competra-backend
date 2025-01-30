using competra.wwwapi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace competra.wwwapi.Repositories.Interfaces
{
    public interface IMatch
    {
        Task<ICollection<Match>> GetAll();
        Task<ICollection<Match>> GetUserMatches(int userId, int activityId);
        Task<Match> Create(Match match);
    }
}