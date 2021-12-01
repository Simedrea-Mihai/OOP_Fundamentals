using Application.Contracts.Persistence;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Repositories.Methods;
using System.Threading;

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
            League leagueInstance = LeagueMethods.AddTeams(_context, league, TeamIds);
            return leagueInstance;

        }

        public async Task<League> AddTeamsAsync(League league, IList<Team> TeamIds, CancellationToken cancellationToken)
        {
            League leagueInstance = LeagueMethods.AddTeams(_context, league, TeamIds);
            return await Task.FromResult(leagueInstance);
        }

        public League Create(League league)
        {
            _context.Leagues.Add(league);
            _context.SaveChanges();
            return league;
        }

        public async Task<League> CreateAsync(League league, CancellationToken cancellationToken)
        {
            League leagueInstance = LeagueMethods.Create(_context, league);
            return await Task.FromResult(leagueInstance).ConfigureAwait(false);
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

        public async Task<IList<League>> ListAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Leagues

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
                .ThenInclude(player => player.Traits)
                .ToListAsync().ConfigureAwait(false);
        }

        public async Task<int> RemoveLeagueByIdAsync(int id, CancellationToken cancellationToken)
        {
            LeagueMethods.RemoveLeagueById(_context, id);
            return await Task.FromResult(id).ConfigureAwait(false);
        }
    }
}
