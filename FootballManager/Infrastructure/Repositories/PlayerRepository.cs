using Application.Contracts.Persistence;
using Domain;
using Infrastructure.Repositories.Methods;
using Infrastructure.Repositories.TraitsDecorator;
using Infrastructure.Static_Methods;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IPlayerRepository _playerRepository;
        private readonly Random rnd = new Random();

        public PlayerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Player Create(Player player)
        {
            _context.Players.Add(player);
            _context.SaveChanges();

            return player;
        }


        public async Task<Player> CreateAsync(Player player, CancellationToken cancellationToken)
        {

            _context.Players.Add(player);
            _context.SaveChanges();

            return await Task.FromResult(player);
        }

        // LIST
        public async Task<IList<Player>> ListAll()
        { 

            return await _context.Players
                .Include(player => player.Profile)
                .Include(player => player.PlayerAttribute)
                .Include(player => player.PlayerAttribute.Traits).ToListAsync();
        }

        // LIST ASYNC
        public async Task<IList<Player>> ListAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Players
                .Include(player => player.Profile)
                .Include(player => player.PlayerAttribute)
                .Include(player => player.PlayerAttribute.Traits)
                .ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        // LIST FREE PLAYERS
        public IList<Player> ListFreePlayers()
        {
            return _context.Players
                .Include(player => player.Profile)
                .Where(player => player.FreeAgent == true)
                .Include(player => player.PlayerAttribute)
                .Include(player => player.PlayerAttribute.Traits).ToList();
        }

        // LIST FREE PLAYERS ASYNC
        public async Task<IList<Player>> ListFreePlayersAsync(CancellationToken cancellationToken)
        {
            return await _context.Players
                .Include(player => player.Profile)
                .Where(player => player.FreeAgent == true)
                .Include(player => player.PlayerAttribute)
                .Include(player => player.PlayerAttribute.Traits)
                .ToListAsync().ConfigureAwait(false);
        }


        // LIST TAKEN PLAYERS
        public IList<Player> ListTakenPlayers()
        {
            return _context.Players
                .Include(player => player.Profile)
                .Where(player => player.FreeAgent == false)
                .Include(player => player.PlayerAttribute)
                .Include(player => player.PlayerAttribute.Traits).ToList();
        }

        // LIST TAKEN PLAYERS ASYNC

        public async Task<IList<Player>> ListTakenPlayersAsync(CancellationToken cancellationToken)
        {
            return await _context.Players
                .Include(player => player.Profile)
                .Where(player => player.FreeAgent == false)
                .Include(player => player.PlayerAttribute)
                .Include(player => player.PlayerAttribute.Traits)
                .ToListAsync().ConfigureAwait(false);
        }

        // GET ALL PLAYERS BY OVR

        public IList<Player> GetPlayersByOvr(bool ascending, int count, CancellationToken cancellationToken)
        {
            List<Player> players = new List<Player>();

            if(ascending)
                players = _context.Players
                    .Include(player => player.Profile)
                    .Include(player => player.PlayerAttribute)
                    .Include(player => player.PlayerAttribute.Traits)
                    .OrderBy(x => x.PlayerAttribute.OVR)
                    .Take(count)
                    .ToList();

            else
                players = _context.Players
                    .Include(player => player.Profile)
                    .Include(player => player.PlayerAttribute)
                    .Include(player => player.PlayerAttribute.Traits)
                    .OrderByDescending(x => x.PlayerAttribute.OVR)
                    .Take(count)
                    .ToList();


            return players;
        }

        // GET ALL PLAYERS BY AGE
        public IList<Player> GetPlayersByAge(bool ascending, int count, CancellationToken cancellationToken)
        {
            List<Player> players = new List<Player>();

            if (ascending)
                players = _context.Players
                    .Include(player => player.Profile)
                    .Include(player => player.PlayerAttribute)
                    .Include(player => player.PlayerAttribute.Traits)
                    .OrderBy(x => x.Profile.Age)
                    .Take(count)
                    .ToList();

            else
                players = _context.Players
                    .Include(player => player.Profile)
                    .Include(player => player.PlayerAttribute)
                    .Include(player => player.PlayerAttribute.Traits)
                    .OrderByDescending(x => x.Profile.Age)
                    .Take(count)
                    .ToList();


            return players;
        }


        // GET PLAYER
        public Player GetPlayer()
        {
            Player player = PlayerMethods.GetPlayer(_context);
            return player;
        }


        // GET PLAYER ASYNC
        public async Task<Player> GetPlayerAsync(CancellationToken cancellationToken)
        {
            Player player = PlayerMethods.GetPlayer(_context);
            return await Task.FromResult(player).ConfigureAwait(false);
        }


        // SET ATTRIBUTES
        public Player SetAttributes(Player player, bool randomAttributes)
        {
            Player playerInstance = PlayerMethods.SetAttributes(_context, _playerRepository, player, randomAttributes);
            return playerInstance;
        }

        // SET ATTRIBUTES ASYNC
        public async Task<Player> SetAttributesAsync(Player player, bool randomAttributes, CancellationToken cancellationToken)
        {
            Player playerInstance = PlayerMethods.SetAttributes(_context, _playerRepository, player, randomAttributes);
            return await Task.FromResult(playerInstance).ConfigureAwait(false);
        }


        // TAKEN
        public bool Taken(Player player)
        {
            if (player.FreeAgent == false)
                return true;
            else
                return false;

        }

        // TAKEN ASYNC
        public async Task<bool> TakenAsync(Player player, CancellationToken cancellationToken)
        {
            if (player.FreeAgent == false)
                return await Task.FromResult(true).ConfigureAwait(false);
            else
                return await Task.FromResult(false).ConfigureAwait(false);
        }


        // SET MARKET VALUE 
        public Player SetMarketValue(Player player)
        {
            Player playerInstance = PlayerMethods.SetMarketValue(_context, player);
            return playerInstance;

        }

        // SET MARKET VALUE ASYNC

        public async Task<Player> SetMarketValueAsync(Player player, CancellationToken cancellationToken)
        {
            Player playerInstance = PlayerMethods.SetMarketValue(_context, player);
            return await Task.FromResult(playerInstance).ConfigureAwait(false);
        }

        // REMOVE PLAYER BY ID
        public async Task<int> RemovePlayerByIdAsync(int id, CancellationToken cancellationToken)
        {
            PlayerMethods.RemovePlayerById(_context, id);
            return await Task.FromResult(id).ConfigureAwait(false);
        }
    }
}
