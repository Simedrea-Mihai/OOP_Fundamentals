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

        public async Task<Player> Create(Player player, CancellationToken cancellationToken)
        {
            _context.Players.Add(player);
            await _context.SaveChangesAsync(cancellationToken);

            return player;
        }



        // LIST
        public async Task<IList<Player>> ListAll(CancellationToken cancellationToken)
        { 

            return await _context.Players
                .Include(player => player.Profile)
                .Include(player => player.PlayerAttribute)
                .Include(player => player.PlayerAttribute.Traits).ToListAsync(cancellationToken);
        }

        // LIST BY ID
        public async Task<Player> ListById(int id, CancellationToken cancellationToken)
        {
            return await _context.Players
                .Include(player => player.Profile)
                .Include(player => player.PlayerAttribute)
                .Include(player => player.PlayerAttribute.Traits)
                .Where(p => p.Id == id).FirstAsync();
        }

        // LIST FREE PLAYERS
        public async Task<IList<Player>> ListFreePlayers(CancellationToken cancellationToken)
        {
            return await _context.Players
                .Include(player => player.Profile)
                .Where(player => player.FreeAgent == true)
                .Include(player => player.PlayerAttribute)
                .Include(player => player.PlayerAttribute.Traits).ToListAsync(cancellationToken);
        }



        // LIST TAKEN PLAYERS
        public async Task<IList<Player>> ListTakenPlayers(CancellationToken cancellationToken)
        {
            return await _context.Players
                .Include(player => player.Profile)
                .Where(player => player.FreeAgent == false)
                .Include(player => player.PlayerAttribute)
                .Include(player => player.PlayerAttribute.Traits).ToListAsync(cancellationToken);
        }

        // GET ALL PLAYERS BY OVR

        public async Task<IList<Player>> GetPlayersByOvr(bool ascending, int count, CancellationToken cancellationToken)
        {
            if(ascending)
                return await _context.Players
                    .Include(player => player.Profile)
                    .Include(player => player.PlayerAttribute)
                    .Include(player => player.PlayerAttribute.Traits)
                    .OrderBy(x => x.PlayerAttribute.OVR)
                    .Take(count)
                    .ToListAsync(cancellationToken);

            else
                 return await _context.Players
                    .Include(player => player.Profile)
                    .Include(player => player.PlayerAttribute)
                    .Include(player => player.PlayerAttribute.Traits)
                    .OrderByDescending(x => x.PlayerAttribute.OVR)
                    .Take(count)
                    .ToListAsync(cancellationToken);
        }

        // GET ALL PLAYERS BY AGE
        public async Task<IList<Player>> GetPlayersByAge(bool ascending, int count, CancellationToken cancellationToken)
        {

            if (ascending)
                return await _context.Players
                    .Include(player => player.Profile)
                    .Include(player => player.PlayerAttribute)
                    .Include(player => player.PlayerAttribute.Traits)
                    .OrderBy(x => x.Profile.Age)
                    .Take(count)
                    .ToListAsync(cancellationToken);

            else
                return await _context.Players
                    .Include(player => player.Profile)
                    .Include(player => player.PlayerAttribute)
                    .Include(player => player.PlayerAttribute.Traits)
                    .OrderByDescending(x => x.Profile.Age)
                    .Take(count)
                    .ToListAsync(cancellationToken);
        }


        // GET PLAYER
        public async Task<Player> GetPlayer(CancellationToken cancellationToken)
        {
            Player player = await PlayerMethods.GetPlayer(_context, cancellationToken);
            return player;
        }


        // SET ATTRIBUTES
        public async Task<Player> SetAttributes(Player player, bool randomAttributes, CancellationToken cancellationToken)
        {
            Player playerInstance = await PlayerMethods.SetAttributes(_context, _playerRepository, player, randomAttributes, cancellationToken);
            return playerInstance;
        }


        // TAKEN
        public bool Taken(Player player)
        {
            if (player.FreeAgent == false)
                return true;
            else
                return false;

        }


        // SET MARKET VALUE 
        public async Task<Player> SetMarketValue(Player player, CancellationToken cancellationToken)
        {
            Player playerInstance = await PlayerMethods.SetMarketValue(_context, player, cancellationToken);
            return playerInstance;

        }


        // REMOVE PLAYER BY ID
        public async Task<int> RemovePlayerByIdAsync(int id, CancellationToken cancellationToken)
        {
            int playerId = await PlayerMethods.RemovePlayerById(_context, id, cancellationToken);
            return playerId;
        }
    }
}
