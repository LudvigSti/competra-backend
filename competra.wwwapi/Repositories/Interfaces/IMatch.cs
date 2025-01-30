using competra.wwwapi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace competra.wwwapi.Repositories.Interfaces
{
    public interface IMatch
    {
        Task<ICollection<Match>> GetAll();
        Task<Match> GetById(int matchId);
        Task<Match> Create(Match match);
    }
}