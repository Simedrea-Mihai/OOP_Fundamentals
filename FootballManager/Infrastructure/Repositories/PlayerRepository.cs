using Application.Contracts.Persistence;
using Domain;
using Infrastructure.Repositories.TraitsDecorator;
using Infrastructure.Static_Methods;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {

        private readonly ApplicationDbContext _context;
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

        public IList<Player> ListAll()
        { 

            return _context.Players.Include(player => player.Profile).Include(player => player.PlayerAttribute).Include(player => player.PlayerAttribute.Traits).ToList();
        }

        public IList<Player> ListFreePlayers()
        {
            return _context.Players.Include(player => player.Profile).Where(player => player.FreeAgent == true).Include(player => player.PlayerAttribute).Include(player => player.PlayerAttribute.Traits).ToList();
        }

        public IList<Player> ListTakenPlayers()
        {
            return _context.Players.Include(player => player.Profile).Where(player => player.FreeAgent == false).Include(player => player.PlayerAttribute).Include(player => player.PlayerAttribute.Traits).ToList();
        }

        public Player GetPlayer()
        {
            var player_id = rnd.Next(0, _context.Players.Count() - 1);

            var player = _context.Players.Where(player => player.FreeAgent == true && player.Id == player_id).First();

            return player;
        }

        public Player SetAttributes(Player player)
        {
            player.FreeAgent = true;

            IPlayerTraits traits = new Basic();
            player.PlayerAttribute = new PlayerAttribute(rnd.Next(60, 70), SPlayer.SetPotential(player), new Traits(traits.ExtraOvr(), traits.Description()));
            player = SetMarketValue(player);

            return player;
        }

        public bool Taken(Player player)
        {
            if (player.FreeAgent == false)
                return true;
            else
                return false;

        }

        public Player SetMarketValue(Player player)
        {
            int potential = player.PlayerAttribute.Potential;

            if (potential > 90)
                player.Market_Value = rnd.Next(20000000, 50000000);
            else if (potential > 80 && potential <= 90)
                player.Market_Value = rnd.Next(10000000, 30000000);
            else if (potential > 75 && potential <= 80)
                player.Market_Value = rnd.Next(5000000, 10000000);
            else
                player.Market_Value = rnd.Next(1000000, 5000000);

            return player;

        }
    }
}
