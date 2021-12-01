using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Domain;

namespace Application.Contracts.Persistence
{
    public interface ILeagueRepository
    {
        League Create(League league);
        Task<League> CreateAsync(League league, CancellationToken cancellationToken);

        IList<League> ListAll();
        Task<IList<League>> ListAllAsync(CancellationToken cancellationToken);

        League AddTeams(League league, IList<Team> TeamIds);
        Task<League> AddTeamsAsync(League league, IList<Team> TeamIds, CancellationToken cancellationToken);

        Task<int> RemoveLeagueByIdAsync(int id, CancellationToken cancellationToken);
    }
}
