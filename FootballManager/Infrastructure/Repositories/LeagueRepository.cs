using Application.Contracts.Persistence;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class LeagueRepository : ILeagueRepository
    {
        private readonly ApplicationDbContext _context;

        public LeagueRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public League Create(League league)
        {
            _context.Leagues.Add(league);
            _context.SaveChanges();
            return league;
        }

        public IList<League> ListAll()
        {
            return _context.Leagues.ToList();
        }
    }
}
