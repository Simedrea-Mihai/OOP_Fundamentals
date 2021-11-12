using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Application.Contracts.Persistence;
using Domain;
using MediatR;

namespace Application.Teams.AddPlayers
{
    public class AddPlayers : IRequest<int>
    { 
        public int TeamId { get; set; }
        public int players_count { get; set; }
    }

    public class AddPlayersHandler : IRequestHandler<AddPlayers, int>
    {
        public readonly IPlayerRepository _PlayerRepository;
        public readonly IProfileRepository _ProfileRepository;
        public readonly ITeamRepository _teamRepository;

        private readonly Team Team = new Team("default");


        public AddPlayersHandler(IPlayerRepository PlayerRepository, IProfileRepository ProfileRepository, ITeamRepository teamRepository)
        {
            _PlayerRepository = PlayerRepository;
            _ProfileRepository = ProfileRepository;
            _teamRepository = teamRepository;
        }

        public Task<int> Handle(AddPlayers command, CancellationToken cancellationToken)
        {
            Team.Id = command.TeamId;

            _teamRepository.AddPlayers(Team, command.players_count);
            return Task.FromResult(command.players_count);
        }
    }
}
