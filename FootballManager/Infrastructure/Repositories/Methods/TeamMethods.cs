using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Methods
{
    public static class TeamMethods
    {
        public static async Task<Manager> AddManager(ApplicationDbContext context, Team team, Manager manager, CancellationToken cancellationToken)
        {
            if (team.Id == 0)
                throw new Exception("Can't link the manager with a null team ID");

            var list = context.Managers.Include(m => m.Profile).ToList();

            var requestedManager = list.FirstOrDefault(
                mn => mn.Id == manager.Id
                   && mn.FreeAgent == true);

            if (requestedManager != null)
            {
                var items = context.Teams.ToList();
                var tempTeam = items.Find(t => t.Id == team.Id);

                if (tempTeam.Manager == null)
                {
                    requestedManager.FreeAgent = false;
                    requestedManager.TeamIdManager = team.Id;
                    items.Find(t => t.Id == team.Id).Manager = requestedManager;
                }
                else
                    throw new Exception("This team has already a manager");

            }
            else
                throw new Exception("Manager not found in the database or it's already taken");


            await context.SaveChangesAsync(cancellationToken);
            return requestedManager;
        }

        public static async Task<IList<Player>> AddPlayers(ApplicationDbContext context, ITeamRepository repository, IPlayerRepository playerRepository, Team team, int players_count, CancellationToken cancellationToken)
        {
            if (players_count > context.Players.Count())
                throw new Exception("Player's count overflow the size of the list of players");

            if (context.Teams.Find(team.Id).Players == null)
                context.Teams.Find(team.Id).Players = new List<Player>();

            Player player;
            player = await playerRepository.GetPlayer(cancellationToken);
            await repository.BuyPlayer(team, player, buy: false, cancellationToken);

            return context.Teams.Find(team.Id).Players.ToList();
        }

        public static async Task<Team> Create(ApplicationDbContext context, Team team, CancellationToken cancellationToken)
        {
            context.Teams.Add(team);
            await context.SaveChangesAsync(cancellationToken);
            return team;
        }

        public static async Task<List<Player>> BuyPlayers(ApplicationDbContext context, Team team, List<int> playerIds, bool buy, CancellationToken cancellationToken)
        {
            if (team.Id == 0)
                throw new Exception("Can't link the player with a null team ID");

            var list = context.Players
                .Include(player => player.Profile)
                .Include(player => player.PlayerAttribute)
                .ThenInclude(player => player.Traits).ToList();

            context.Players
                .Include(player => player.Profile)
                .Include(player => player.PlayerAttribute)
                .ThenInclude(player => player.Traits);

            List<Player> players = new List<Player>();

            foreach(int p in playerIds)
            {
                var requestedPlayer = list.FirstOrDefault(
                    pl => pl.Id == p
                    && pl.FreeAgent == true);

                if(requestedPlayer == null)
                    throw new Exception($"Player with ID: {p} was not found in the database or it's already taken");
                else
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

                if (context.Teams.Find(team.Id).Players == null)
                    context.Teams.Find(team.Id).Players = new List<Player>();

                context.Teams.Find(team.Id).Players.Add(requestedPlayer);
                players.Add(requestedPlayer);
            }

            await context.SaveChangesAsync(cancellationToken);
            return players;



        }

        public static async Task<Player> BuyPlayer(ApplicationDbContext context, Team team, Player player, bool buy, CancellationToken cancellationToken)
        {
            if (team.Id == 0)
                throw new Exception("Can't link the player with a null team ID");

            var list = context.Players
                .Include(player => player.Profile)
                .Include(player => player.PlayerAttribute)
                .ThenInclude(player => player.Traits).ToList();

            context.Players
                .Include(player => player.Profile)
                .Include(player => player.PlayerAttribute)
                .ThenInclude(player => player.Traits);


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

            await context.SaveChangesAsync(cancellationToken);

            return requestedPlayer;
        }

        public static async Task<Team> RemovePlayers(ApplicationDbContext context, Team team, CancellationToken cancellationToken)
        {
            Team t = new Team("default", "default", "default");
            t.Id = team.Id;

            context.Players.RemoveRange(context.Players.ToArray().Where(p => p.TeamIdPlayer == team.Id));

            await context.SaveChangesAsync(cancellationToken);

            return t;

        }

        public static async Task<Player> FirePlayer(ApplicationDbContext context, int TeamId, int PlayerId, CancellationToken cancellationToken)
        {

            Player player = context.Teams
                .Include(p => p.Players)
                .ThenInclude(p => p.Profile)
                .Include(p => p.Players)
                .ThenInclude(p => p.PlayerAttribute)
                .Include(p => p.Players)
                .ThenInclude(p => p.PlayerAttribute.Traits)
                .FirstOrDefault().Players
                .Where(p => p.Id == PlayerId).FirstOrDefault();

            if (player == null)
                throw new Exception($"Player with ID {PlayerId} was not found in team's database");

            else
            {
                context.Teams.Include(p => p.Players).First().Players.Where(p => p.Id == PlayerId).First().FreeAgent = true;
                context.Teams.Include(p => p.Players).First().Players.Where(p => p.Id == PlayerId).First().TeamIdPlayer = 0;

                //context.Teams.Include(p => p.Players).First().Players.Remove(player);

                await context.SaveChangesAsync(cancellationToken);
            }

            return player;
        }

        public static async Task<int> RemoveTeamById(ApplicationDbContext context, int id, CancellationToken cancellationToken)
        {
            Team team = context.Teams.Where(p => p.Id == id).First();

            var managerTeam = context.Managers.Where(p => p.TeamIdManager == id).FirstOrDefault();

            if(managerTeam != null)
                context.Managers.Remove(managerTeam);

            var playersList = context.Players.Where(p => p.TeamIdPlayer == id).ToList();

            if(playersList.Count != 0)
                foreach (var player in playersList)
                    context.Players.Remove(player);

            context.Teams.Remove(team);

            await context.SaveChangesAsync(cancellationToken);
            return id;
        }

    }
}
