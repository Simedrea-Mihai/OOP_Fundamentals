using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public int TeamId { get; set; }
        [Required]
        public int PlayersCount { get; set; }
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

        public async Task<int> Handle(AddPlayers command, CancellationToken cancellationToken)
        {
            Team.Id = command.TeamId;

            for (int i = 0; i < command.PlayersCount; i++)
                await _teamRepository.BuyPlayerAsync(Team, _PlayerRepository.GetPlayer(), buy: false, cancellationToken);

            return await Task.FromResult(command.PlayersCount);
        }
    }
}
