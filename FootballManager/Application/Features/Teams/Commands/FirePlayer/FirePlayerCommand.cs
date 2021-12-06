using Application.Contracts.Persistence;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Teams.Commands.AddManager
{
    public class FirePlayerCommand : IRequest<Player>
    {
        [Required]
        public int TeamId { get; set; }
        [Required]
        public int PlayerId { get; set; }
    }

    public class FirePlayerCommandHandler : IRequestHandler<FirePlayerCommand, Player>
    {
        public readonly ITeamRepository _teamRepository;

        public FirePlayerCommandHandler(ITeamRepository teamRepository) => _teamRepository = teamRepository;

        public async Task<Player> Handle(FirePlayerCommand command, CancellationToken cancellationToken)
        {
            Player player = await _teamRepository.FirePlayer(command.TeamId, command.PlayerId, cancellationToken);
            return player;
        }

    }
}
