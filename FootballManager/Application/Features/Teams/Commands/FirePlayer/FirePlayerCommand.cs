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
    public class FirePlayerCommand : IRequest<int>
    {
        [Required]
        public int TeamId { get; set; }
        [Required]
        public int PlayerId { get; set; }
    }

    public class FirePlayerCommandHandler : IRequestHandler<FirePlayerCommand, int>
    {
        public readonly ITeamRepository _teamRepository;

        public FirePlayerCommandHandler(ITeamRepository teamRepository) => _teamRepository = teamRepository;

        public async Task<int> Handle(FirePlayerCommand command, CancellationToken cancellationToken)
        {

            await _teamRepository.FirePlayerAsync(command.TeamId, command.PlayerId, cancellationToken);
            return await Task.FromResult(command.TeamId);
        }

    }
}
