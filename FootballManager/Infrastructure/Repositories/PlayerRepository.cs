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
            var list = _context.Players.Where(player => player.FreeAgent == true).ToList();

            List<int> ids = new List<int>();

            for (int i = 0; i < list.Count; i++)
                ids.Add(list[i].Id);

            var player_id = rnd.Next(ids.Count);

            var player = _context.Players
                .Include(player => player.Profile)
                .Include(player => player.PlayerAttribute)
                .ThenInclude(player => player.Traits)
                .Where(player => player.FreeAgent == true && player.Id == list[player_id].Id).FirstOrDefault();

            return player;
        }

        public Player SetAttributes(Player player, bool randomAttributes)
        {
            player.FreeAgent = true;
            IPlayerTraits traits = new Basic();

            if (randomAttributes)
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
                player.Market_Value = rnd.Next(2, 9) * 10000000;
            else if (potential > 80 && potential <= 90)
                player.Market_Value = rnd.Next(1, 5) * 10000000;
            else if (potential > 75 && potential <= 80)
                player.Market_Value = rnd.Next(5, 9) * 1000000;
            else
                player.Market_Value = rnd.Next(1, 5) * 1000000;

            return player;

        }
    }
}
