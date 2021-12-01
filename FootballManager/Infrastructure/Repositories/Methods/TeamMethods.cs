﻿using System;
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

        public static Team RemovePlayers(ApplicationDbContext context, Team team)
        {
            Team t = new Team("default");
            t.Id = team.Id;

            context.Players.RemoveRange(context.Players.ToArray().Where(p => p.TeamIdPlayer == team.Id));

            context.SaveChanges();

            return t;

        }

        public static void FirePlayer(ApplicationDbContext context, int TeamId, int PlayerId)
        {

            Player player = context.Teams.Include(p => p.Players).FirstOrDefault().Players.Where(p => p.Id == PlayerId).FirstOrDefault();

            if (player == null)
                throw new Exception($"Player with ID {PlayerId} was not found in team's database");

            else
            {
                context.Teams.Include(p => p.Players).First().Players.Where(p => p.Id == PlayerId).First().FreeAgent = true;
                context.Teams.Include(p => p.Players).First().Players.Where(p => p.Id == PlayerId).First().TeamIdPlayer = 0;

                context.Teams.Include(p => p.Players).First().Players.Remove(player);

                context.SaveChanges();
            }
        }

        public static void RemoveTeamById(ApplicationDbContext context, int id)
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

            context.SaveChanges();
        }

    }
}
