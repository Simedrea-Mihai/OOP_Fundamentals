using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Domain;

namespace Application.Contracts.Persistence
{
    public interface IPlayerRepository
    {
        Player Create(Player player);
        Task<Player> CreateAsync(Player player, CancellationToken cancellationToken);


        Player SetAttributes(Player player, bool randomAttributes);
        Task<Player> SetAttributesAsync(Player player, bool randomAttributes, CancellationToken cancellationToken);


        Player SetMarketValue(Player player);
        Task<Player> SetMarketValueAsync(Player player, CancellationToken cancellationToken);


        Task<IList<Player>> ListAll();


        IList<Player> ListFreePlayers();
        Task<IList<Player>> ListFreePlayersAsync(CancellationToken cancellationToken);


        IList<Player> ListTakenPlayers();
        Task<IList<Player>> ListTakenPlayersAsync(CancellationToken cancellationToken);

        IList<Player> GetPlayersByOvr(bool ascending, int count, CancellationToken cancellationToken);
        IList<Player> GetPlayersByAge(bool ascending, int count, CancellationToken cancellationToken);


        Player GetPlayer();
        Task<Player> GetPlayerAsync(CancellationToken cancellationToken);


        bool Taken(Player player);
        Task<bool> TakenAsync(Player player, CancellationToken cancellationToken);

        Task<int> RemovePlayerByIdAsync(int id, CancellationToken cancellationToken);
    }
}
