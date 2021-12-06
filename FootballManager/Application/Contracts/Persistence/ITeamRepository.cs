using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Domain;

namespace Application.Contracts.Persistence
{
    public interface ITeamRepository
    {
        Task<Team> Create(Team team, CancellationToken cancellationToken);
        Task<IList<Team>> ListAll(CancellationToken cancellationToken);
        Task<Team> ListById(int id, CancellationToken cancellationToken);
        Task<Manager> AddManager(Team team, Manager manager, CancellationToken cancellationToken);
        Task<Player> BuyPlayer(Team team, Player player, bool buy, CancellationToken cancellationToken);
        Task<IList<Player>> AddPlayers(Team team, int players_count, CancellationToken cancellationToken);
        Task<Team> RemovePlayers(Team team, CancellationToken cancellationToken);
        Task<int> FirePlayer(int TeamId, int PlayerId, CancellationToken cancellationToken);
        Task<int> RemoveTeamByIdAsync(int id, CancellationToken cancellationToken);
    }
}
