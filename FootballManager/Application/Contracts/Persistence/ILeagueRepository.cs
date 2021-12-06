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
        Task<League> Create(League league, CancellationToken cancellationToken);

        Task<IList<League>> ListAll(CancellationToken cancellationToken);
        Task<League> ListById(int id, CancellationToken cancellationToken);

        Task<League> AddTeams(League league, IList<Team> TeamIds, CancellationToken cancellationToken);

        Task<int> RemoveLeagueByIdAsync(int id, CancellationToken cancellationToken);
    }
}
