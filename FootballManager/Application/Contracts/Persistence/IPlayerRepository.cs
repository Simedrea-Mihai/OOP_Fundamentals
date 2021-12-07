using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Domain;
using Domain.Entities.Enums;

namespace Application.Contracts.Persistence
{
    public interface IPlayerRepository
    {
        Task<Player> Create(Player player, CancellationToken cancellationToken);
        Task<Player> SetAttributes(Player player, bool randomAttributes, CancellationToken cancellationToken);
        Task<Player> SetMarketValue(Player player, CancellationToken cancellationToken);
        Task<IList<Player>> ListAll(CancellationToken cancellationToken);
        Task<IList<Player>> ListByPosition(PlayerPosition position, CancellationToken cancellationToken);
        Task<Player> ListById(int id, CancellationToken cancellationToken);
        Task<IList<Player>> ListFreePlayers(CancellationToken cancellationToken);
        Task<IList<Player>> ListTakenPlayers(CancellationToken cancellationToken);
        Task<IList<Player>> GetTopPlayersPotential(bool ascending, int count, CancellationToken cancellationToken);
        Task<IList<Player>> GetPlayersByOvr(bool ascending, int count, CancellationToken cancellationToken);
        Task<IList<Player>> GetPlayersByAge(bool ascending, int count, CancellationToken cancellationToken);
        Task<Player> GetPlayer(CancellationToken cancellationToken);
        bool Taken(Player player);
        Task<int> RemovePlayerByIdAsync(int id, CancellationToken cancellationToken);
    }
}
