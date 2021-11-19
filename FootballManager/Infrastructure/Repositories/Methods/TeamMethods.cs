using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Methods
{
    public static class TeamMethods
    {
        public static void AddManager(ApplicationDbContext context, Team team, Manager manager)
        {
            if (team.Id == 0)
                throw new Exception("Can't link the manager with a null team ID");

            var list = context.Managers.Include(manager => manager.Profile).ToList();

            var requestedManager = list.FirstOrDefault(
                mn => mn.Id == manager.Id
                   && mn.FreeAgent == true
                   && mn.TeamIdManager == 0);

            if (requestedManager != null)
            {
                requestedManager.FreeAgent = false;
                requestedManager.TeamIdManager = team.Id;

                var items = context.Teams.Include(team => team.Manager).ThenInclude(manager => manager.Profile).ToList();

                if (items.Find(t => t.Id == team.Id).Manager == null)
                    items.Find(t => t.Id == team.Id).Manager = requestedManager;
                else
                    throw new Exception("This team has already a manager");

            }
            else
                throw new Exception("Manager not found in the database or it's already taken");


            context.SaveChanges();
        }

        public static IList<Player> AddPlayers(ApplicationDbContext context, ITeamRepository repository, IPlayerRepository playerRepository, Team team, int players_count)
        {
            if (players_count > context.Players.Count())
                throw new Exception("Player's count overflow the size of the list of players");

            if (context.Teams.Find(team.Id).Players == null)
                context.Teams.Find(team.Id).Players = new List<Player>();

            Player player;
            player = playerRepository.GetPlayer();
            repository.BuyPlayer(team, player, buy: false);

            return context.Teams.Find(team.Id).Players.ToList();
        }

        public static void Create(ApplicationDbContext context, Team team)
        {
            context.Teams.Add(team);
            context.SaveChanges();
        }

        public static Player BuyPlayer(ApplicationDbContext context, Team team, Player player, bool buy)
        {
            if (team.Id == 0)
                throw new Exception("Can't link the player with a null team ID");

            var list = context.Players.Include(player => player.Profile).Include(player => player.PlayerAttribute).ThenInclude(player => player.Traits).ToList();
            context.Players.Include(player => player.Profile).Include(player => player.PlayerAttribute).ThenInclude(player => player.Traits);


            var requestedPlayer = list.FirstOrDefault(
                pl => pl.Id == player.Id
                   && pl.FreeAgent == true
                );


            if (requestedPlayer != null)
            {
                if (context.Teams.Find(team.Id).Budget > requestedPlayer.MarketValue)
                {
                    requestedPlayer.FreeAgent = false;
                    requestedPlayer.TeamIdPlayer = team.Id;


                    if (buy == true)
                        context.Teams.Find(team.Id).Budget -= requestedPlayer.MarketValue;

                }
                else
                    throw new Exception("Budget < Player's market value");
            }
            else
                throw new Exception("Player not found in the database or it's already taken");


            if (context.Teams.Find(team.Id).Players == null)
                context.Teams.Find(team.Id).Players = new List<Player>();

            context.Teams.Find(team.Id).Players.Add(requestedPlayer);

            context.SaveChanges();

            return requestedPlayer;
        }

    }
}
