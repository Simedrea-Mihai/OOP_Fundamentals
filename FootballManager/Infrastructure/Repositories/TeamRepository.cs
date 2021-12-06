using Application.Contracts.Persistence;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Repositories.Methods;

namespace Infrastructure.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ITeamRepository _repository;
        private readonly IPlayerRepository _playerRepository;

        public TeamRepository(ApplicationDbContext context, IPlayerRepository playerRepository)
        {
            _context = context;
            _playerRepository = playerRepository;
        }

        public async Task<Team> ListById(int id, CancellationToken cancellationToken)
        {
            return await _context.Teams
                .Include(p => p.Manager)
                .ThenInclude(p => p.Profile)
                .Include(p => p.Players)
                .ThenInclude(p => p.Profile)
                .Include(p => p.Players)
                .ThenInclude(p => p.PlayerAttribute)
                .ThenInclude(p => p.Traits)
                .Where(p => p.Id == id).FirstAsync();
        }

        public async Task<Manager> AddManager(Team team, Manager manager, CancellationToken cancellationToken)
        {
            await TeamMethods.AddManager(_context, team, manager, cancellationToken);
            return manager;
        }

        public async Task<IList<Player>> AddPlayers(Team team, int players_count, CancellationToken cancellationToken)
        {
            IList<Player> players = await TeamMethods.AddPlayers(_context, _repository, _playerRepository, team, players_count, cancellationToken);
            return players;
        }

        public async Task<Team> Create(Team team, CancellationToken cancellationToken)
        {
            _context.Teams.Add(team);
            await _context.SaveChangesAsync(cancellationToken);
            return team;
        }

        public async Task<IList<Team>> ListAll(CancellationToken cancellationToken)
        {
            return await _context.Teams
                .Include(team => team.Manager)
                .Include(team => team.Manager.Profile)
                .Include(team => team.Players)
                .Include(team => team.Players)
                .ThenInclude(player => player.PlayerAttribute)
                .ThenInclude(player => player.Traits)
                .Include(team => team.Players)
                .Include(team => team.Players)
                .ThenInclude(player => player.Profile).ToListAsync(cancellationToken);
        }

        public async Task<Player> BuyPlayer(Team team, Player player, bool buy, CancellationToken cancellationToken)
        {
            Player requestedPlayer = await TeamMethods.BuyPlayer(_context, team, player, buy, cancellationToken);
            return requestedPlayer;
        }

        public async Task<Team> RemovePlayers(Team team, CancellationToken cancellationToken)
        {
            Team t = await TeamMethods.RemovePlayers(_context, team, cancellationToken);
            return t;
        }

        public async Task<int> FirePlayer(int TeamId, int PlayerId, CancellationToken cancellationToken)
        {
            await TeamMethods.FirePlayer(_context, TeamId, PlayerId, cancellationToken);
            return TeamId;
        }


        public async Task<int> RemoveTeamByIdAsync(int id, CancellationToken cancellationToken)
        {
            int teamId = await TeamMethods.RemoveTeamById(_context, id, cancellationToken);
            return teamId;
        }

    }
}