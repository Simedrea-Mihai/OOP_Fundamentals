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



        public Manager AddManager(Team team, Manager manager)
        {
            TeamMethods.AddManager(_context, team, manager);
            return manager;
        }

        public async Task<Manager> AddManagerAsync(Team team, Manager manager, CancellationToken cancellationToken)
        {
            TeamMethods.AddManager(_context, team, manager);
            return await Task.FromResult(manager);
        }


        public IList<Player> AddPlayers(Team team, int players_count)
        {
            IList<Player> players = TeamMethods.AddPlayers(_context, _repository, _playerRepository, team, players_count);
            return players;
        }

        public async Task<IList<Player>> AddPlayersAsync(Team team, int players_count, CancellationToken cancellationToken)
        {
            IList<Player> players = TeamMethods.AddPlayers(_context, _repository, _playerRepository, team, players_count);
            return await Task.FromResult(players).ConfigureAwait(false);
        }


        public Team Create(Team team)
        {
            TeamMethods.Create(_context, team);
            return team;
        }

        public async Task<Team> CreateAsync(Team team, CancellationToken cancellationToken)
        {
            TeamMethods.Create(_context, team);
            return await Task.FromResult(team).ConfigureAwait(false);
        }



        public IList<Team> ListAll()
        {
            return _context.Teams
                .Include(team => team.Manager)
                .Include(team => team.Manager.Profile)
                .Include(team => team.Players)
                .Include(team => team.Players)
                .ThenInclude(player => player.PlayerAttribute)
                .ThenInclude(player => player.Traits)
                .Include(team => team.Players)
                .Include(team => team.Players)
                .ThenInclude(player => player.Profile).ToList();
        }

        public async Task<IList<Team>> ListAllAsync(CancellationToken cancellationToken)
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
                .ThenInclude(player => player.Profile)
                .ToListAsync().ConfigureAwait(false);
        }

        public Player BuyPlayer(Team team, Player player, bool buy)
        {
            Player requestedPlayer = TeamMethods.BuyPlayer(_context, team, player, buy);
            return requestedPlayer;

        }

        public async Task<Player> BuyPlayerAsync(Team team, Player player, bool buy, CancellationToken cancellationToken)
        {
            Player requestedPlayer = TeamMethods.BuyPlayer(_context, team, player, buy);
            return await Task.FromResult(requestedPlayer).ConfigureAwait(false);
        }

        public async Task<Team> RemovePlayers(Team team, CancellationToken cancellationToken)
        {
            Team t = TeamMethods.RemovePlayers(_context, team);
            return await Task.FromResult(team).ConfigureAwait(false);
        }

    }
}