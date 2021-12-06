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
    public class RemoveLeagueCommand : IRequest<League>
    {
        [Required]
        public int LeagueId { get; set; }

    }

    public class RemoveLeagueCommandHandler : IRequestHandler<RemoveLeagueCommand, League>
    {
        private readonly ILeagueRepository _repository;

        public RemoveLeagueCommandHandler(ILeagueRepository repository)
        {
            _repository = repository;
        }

        public async Task<League> Handle(RemoveLeagueCommand command, CancellationToken cancellationToken)
        {
            var league = await _repository.RemoveLeagueByIdAsync(command.LeagueId, cancellationToken);

            return league;
        }
    }
}
