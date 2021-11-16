using Application.Contracts.Persistence;
using Domain;
using Microsoft.EntityFrameworkCore;
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

        public League AddTeams(League league, IList<Team> TeamIds)
        {
            if(league.Id == 0)
                throw new Exception("Can't link the teams with a null league ID");

            if (_context.Leagues.Find(league.Id).Teams == null)
                _context.Leagues.Find(league.Id).Teams = new List<Team>();

            var list = _context.Teams.ToList();
            var leagueList = _context.Leagues.Include(league => league.Teams).ToList();
         
            Team team = new Team("deafult");

            int i = 0;
            while (i < TeamIds.Count())
            {
                team = list.Where(t => t.Id == TeamIds[i].Id 
                               && t.LeagueAppended == false).FirstOrDefault();

                if (team != null)
                {
                    if (leagueList[league.Id - 1].Teams.ToList().Exists(t => t.Name == team.Name) == true)
                        throw new Exception("Can't exist two teams with the same name");

                    team.LeagueAppended = true;
                    _context.Leagues.Find(league.Id).Teams.Add(team);
                }
                
                i++;
            }

            _context.SaveChanges();
            return league;

        }

        public League Create(League league)
        {
            _context.Leagues.Add(league);
            _context.SaveChanges();
            return league;
        }

        public IList<League> ListAll()
        {
            return _context.Leagues

                .Include(league => league.Teams)
                .ThenInclude(team => team.Manager)
                .ThenInclude(manager => manager.Profile)

                .Include(team => team.Teams)
                .ThenInclude(team => team.Players)
                .ThenInclude(player => player.Profile)

                .Include(team => team.Teams)
                .ThenInclude(team => team.Players)
                .ThenInclude(player => player.PlayerAttribute)

                .Include(team => team.Teams)
                .ThenInclude(team => team.Players)
                .ThenInclude(player => player.PlayerAttribute)
                .ThenInclude(player => player.Traits).ToList();
        }
    }
}
