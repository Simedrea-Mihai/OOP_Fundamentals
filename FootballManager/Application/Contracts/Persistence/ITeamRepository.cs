using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain;

namespace Application.Contracts.Persistence
{
    public interface ITeamRepository
    {
        Team Create(Team team);
        IList<Team> ListAll();
        Manager AddManager(Team team, Manager manager);
        Player BuyPlayer(Team team, Player player, bool buy);
        void AddPlayers(Team team, int players_count);
    }
}
