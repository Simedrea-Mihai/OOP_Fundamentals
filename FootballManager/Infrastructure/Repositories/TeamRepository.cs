using Application.Contracts.Persistence;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IPlayerRepository _playerRepository;

        public TeamRepository(ApplicationDbContext context, IPlayerRepository playerRepository)
        {
            _context = context;
            _playerRepository = playerRepository;
        }

        public Manager AddManager(Team team, Manager manager)
        {
          
            if (team.Id == 0)
                throw new Exception("Can't link the manager with a null team ID");

            var list = _context.Managers.Include(manager => manager.Profile).ToList();

            var requestedManager = list.FirstOrDefault(
                mn => mn.Profile.FirstName == manager.Profile.FirstName
                   && mn.Profile.LastName == manager.Profile.LastName
                   && mn.FreeAgent == true
                );

            if (requestedManager != null)
            {
                requestedManager.FreeAgent = false;

                var items = _context.Teams.Include(team => team.Manager).ThenInclude(manager => manager.Profile).ToList();
                items.Find(t => t.Id == team.Id).Manager = requestedManager;
                
            }
            else
                throw new Exception("Manager not found in the database or it's already taken");


            _context.SaveChanges();

            return manager;
        }

        public void AddPlayers(Team team, int players_count)
        {
            _context.Teams.Include(team => team.Players).ToList();
            _context.Players.Include(player => player.Profile).ToList();

            if (players_count > _context.Players.Count())
                throw new Exception("Player's count overflow the size of the list of players");

            while (players_count >= 1)
            {
                var random_player = _playerRepository.GetPlayer();
                random_player.FreeAgent = false;

                var t = _context.Teams.Find(team.Id);
                t.Players.Add(random_player);
                players_count -= 1;
                
            }

            _context.SaveChanges();
        }

        public Team Create(Team team)
        {
            _context.Teams.Add(team);
            _context.SaveChanges();
            return team;
        }



        public IList<Team> ListAll()
        {
            return _context.Teams.Include(team => team.Manager).Include(team => team.Manager.Profile).Include(team => team.Players).Include(team => team.Player.PlayerAttribute).ThenInclude(player => player.Traits).Include(team => team.Players).Include(team => team.Players).ThenInclude(player => player.Profile).ToList();
        }

        public Player BuyPlayer(Team team, Player player)
        {
            if (team.Id == 0)
                throw new Exception("Can't link the player with a null team ID");

            var list = _context.Players.Include(player => player.Profile).Include(player => player.PlayerAttribute).ThenInclude(player => player.Traits).ToList();

            var requestedPlayer = list.FirstOrDefault(
                pl => pl.Profile.FirstName == player.Profile.FirstName
                   && pl.Profile.LastName == player.Profile.LastName
                   && pl.FreeAgent == true
                );

            if (requestedPlayer != null)
            {
                if (_context.Teams.Find(team.Id).Budget > requestedPlayer.Market_Value)
                {
                    requestedPlayer.FreeAgent = false;

                    _context.Teams.Find(team.Id).Budget -= requestedPlayer.Market_Value;
                    _context.Teams.Find(team.Id).Player = requestedPlayer;
                    _context.Teams.Find(team.Id).Player.PlayerAttribute = requestedPlayer.PlayerAttribute;
                    _context.Teams.Find(team.Id).Player.PlayerAttribute.Traits = requestedPlayer.PlayerAttribute.Traits;

                    var items = _context.Teams.Include(team => team.Player).Include(team => team.Player.PlayerAttribute).ThenInclude(player => player.Traits).Include(team => team.Players).ThenInclude(player => player.Profile).ToList();
                    items.Find(t => t.Id == team.Id).Players.Add(requestedPlayer);
                }
            }
            else
                throw new Exception("Player not found in the database or it's already taken");

            _context.SaveChanges();

            return requestedPlayer;

        }
    }
}
