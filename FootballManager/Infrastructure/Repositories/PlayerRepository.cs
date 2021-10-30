using Application.Contracts.Persistence;
using Domain;
using Infrastructure.Static_Methods;
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

            Console.WriteLine(player.FirstName);

            return player;
        }

        public IList<Player> ListAll()
        { 

            foreach (var player in _context.Players)
                Console.WriteLine(player.FirstName);

            return _context.Players.ToList();
        }

        public Player SetAttributes(Player player)
        {
            player.OVR = rnd.Next(60, 70);
            player.BirthDate =  new DateTime(rnd.Next(1975, DateTime.Now.Year - 15), rnd.Next(1, 12), rnd.Next(1, 28)); // aici e ceva problema
            player.Age = (DateTime.Now - player.BirthDate).Days / 365;
            player.Potential = SPlayer.SetPotential(player);

            return player;
        }
    }
}
