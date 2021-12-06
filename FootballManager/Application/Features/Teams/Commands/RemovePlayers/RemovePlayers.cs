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
    public class RemovePlayers : IRequest<int>
    {
        public int TeamId { get; set; }
    }

    public class RemovePlayersHandler : IRequestHandler<RemovePlayers, int>
    {
        private readonly ITeamRepository _repository;
        private readonly Team team = new Team("default");

        public RemovePlayersHandler(ITeamRepository repository) => _repository = repository;

        public async Task<int> Handle(RemovePlayers command, CancellationToken cancellationToken)
        {
            team.Id = command.TeamId;

            await _repository.RemovePlayers(team, cancellationToken);

            return command.TeamId;
        }
    }
}
