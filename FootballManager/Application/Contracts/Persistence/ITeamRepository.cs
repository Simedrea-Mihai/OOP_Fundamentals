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

        // -------------- ADD SECTION --------------
        Team Create(Team team);
        Task<Team> CreateAsync(Team team, CancellationToken cancellationToken);


        IList<Team> ListAll();
        Task<IList<Team>> ListAllAsync(CancellationToken cancellationToken);

        Manager AddManager(Team team, Manager manager);
        Task<Manager> AddManagerAsync(Team team, Manager manager, CancellationToken cancellationToken);


        Player BuyPlayer(Team team, Player player, bool buy);
        Task<Player> BuyPlayerAsync(Team team, Player player, bool buy, CancellationToken cancellationToken);


        //void AddPlayers(Team team, int players_count);
        IList<Player> AddPlayers(Team team, int players_count);
        Task<IList<Player>> AddPlayersAsync(Team team, int players_count, CancellationToken cancellationToken);



        // -------------- REMOVE SECTION --------------
        Task<Team> RemovePlayers(Team team, CancellationToken cancellationToken);
    }
}
