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

        public async Task<League> AddTeams(League league, IList<Team> TeamIds, CancellationToken cancellationToken)
        {
            League leagueInstance = await LeagueMethods.AddTeams(_context, league, TeamIds, cancellationToken);
            return leagueInstance;

        }

        public async Task<League> Create(League league, CancellationToken cancellationToken)
        {
            _context.Leagues.Add(league);
            await _context.SaveChangesAsync();
            return league;
        }


        public async Task<League> ListById(int id, CancellationToken cancellationToken)
        {
            return await _context.Leagues
                .Include(p => p.Teams)
                .ThenInclude(p => p.Manager)
                .ThenInclude(p => p.Profile)
                .Where(p => p.Id == id).FirstAsync();
        }

        public async Task<IList<League>> ListAll(CancellationToken cancellationToken)
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
                .ThenInclude(player => player.Traits).ToListAsync();
        }

        public async Task<int> RemoveLeagueByIdAsync(int id, CancellationToken cancellationToken)
        {
            int leagueId = await LeagueMethods.RemoveLeagueById(_context, id, cancellationToken);
            return leagueId;
        }
    }
}
