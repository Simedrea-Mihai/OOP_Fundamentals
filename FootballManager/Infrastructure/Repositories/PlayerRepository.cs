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

        public Player SetAttributes(Player player)
        {
            player.Free_Agent = true;

            IPlayerTraits traits = new Basic();
            player.PlayerAttribute = new PlayerAttribute(rnd.Next(60, 70), SPlayer.SetPotential(player), new Traits(traits.ExtraOvr(), traits.Description()));

            return player;
        }

    }
}
