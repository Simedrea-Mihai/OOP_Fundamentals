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

namespace Application.Features.Leagues.CreateLeague
{
    public class CreateLeagueCommand: IRequest<League>
    {
        [Required]
        public string Name { get; set; }
    }

    public class CreateLeagueCommandHandler: IRequestHandler<CreateLeagueCommand, League>
    {
        private readonly ILeagueRepository _repository;

        public CreateLeagueCommandHandler(ILeagueRepository repository) => _repository = repository;

        public async Task<League> Handle(CreateLeagueCommand command, CancellationToken cancellationToken)
        {
            League league = new(command.Name);
            var createdLeague = await _repository.Create(league, cancellationToken);

            return createdLeague;
        }
    }
}
