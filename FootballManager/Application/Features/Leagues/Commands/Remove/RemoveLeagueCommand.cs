using Application.Contracts.Persistence;
using Domain;
using Domain.Entities.CommandEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Players.Commands.Create
{
    public class RemoveLeagueCommand : IRequest<int>
    {
        [Required]
        public int LeagueId { get; set; }

    }

    public class RemoveLeagueCommandHandler : IRequestHandler<RemoveLeagueCommand, int>
    {
        private readonly ILeagueRepository _repository;

        public RemoveLeagueCommandHandler(ILeagueRepository repository)
        {
            _repository = repository;
        }

        public Task<int> Handle(RemoveLeagueCommand command, CancellationToken cancellationToken)
        {
            _repository.RemoveLeagueByIdAsync(command.LeagueId, cancellationToken);

            return Task.FromResult(command.LeagueId);
        }
    }
}
