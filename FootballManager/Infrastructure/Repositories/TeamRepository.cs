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
                mn => mn.Id == manager.Id
                   && mn.FreeAgent == true
                   && mn.TeamIdManager == 0);

            if (requestedManager != null)
            {
                requestedManager.FreeAgent = false;
                requestedManager.TeamIdManager = team.Id;

                var items = _context.Teams.Include(team => team.Manager).ThenInclude(manager => manager.Profile).ToList();

                if (items.Find(t => t.Id == team.Id).Manager == null)
                    items.Find(t => t.Id == team.Id).Manager = requestedManager;
                else
                    throw new Exception("This team has already a manager");
                
            }
            else
                throw new Exception("Manager not found in the database or it's already taken");


            _context.SaveChanges();

            return manager;
        }

        public void AddPlayers(Team team, int players_count)
        {

            if (players_count > _context.Players.Count())
                throw new Exception("Player's count overflow the size of the list of players");

            if (_context.Teams.Find(team.Id).Players == null)
                _context.Teams.Find(team.Id).Players = new List<Player>();

            Player player;
            player = _playerRepository.GetPlayer();
            BuyPlayer(team, player, buy: false);

        }

        public Team Create(Team team)
        {
            _context.Teams.Add(team);
            _context.SaveChanges();
            return team;
        }



        public IList<Team> ListAll()
        {
            return _context.Teams
                .Include(team => team.Manager)
                .Include(team => team.Manager.Profile)
                .Include(team => team.Players)
                .Include(team => team.Players)
                .ThenInclude(player => player.PlayerAttribute)
                .ThenInclude(player => player.Traits)
                .Include(team => team.Players)
                .Include(team => team.Players)
                .ThenInclude(player => player.Profile).ToList();
        }

        public Player BuyPlayer(Team team, Player player, bool buy)
        {
            if (team.Id == 0)
                throw new Exception("Can't link the player with a null team ID");

            var list = _context.Players.Include(player => player.Profile).Include(player => player.PlayerAttribute).ThenInclude(player => player.Traits).ToList();
            _context.Players.Include(player => player.Profile).Include(player => player.PlayerAttribute).ThenInclude(player => player.Traits);


            var requestedPlayer = list.FirstOrDefault(
                pl => pl.Id == player.Id
                   && pl.FreeAgent == true
                );


            if (requestedPlayer != null)
            {
                if (_context.Teams.Find(team.Id).Budget > requestedPlayer.Market_Value)
                {
                    requestedPlayer.FreeAgent = false;
                    requestedPlayer.TeamIdPlayer = team.Id;
           

                    if(buy == true)
                        _context.Teams.Find(team.Id).Budget -= requestedPlayer.Market_Value;

                }
                else
                    throw new Exception("Budget < Player's market value");
            }
            else
                throw new Exception("Player not found in the database or it's already taken");


            if (_context.Teams.Find(team.Id).Players == null)
                _context.Teams.Find(team.Id).Players = new List<Player>();

            _context.Teams.Find(team.Id).Players.Add(requestedPlayer);

            _context.SaveChanges();
            return requestedPlayer;

        }
    }
}
