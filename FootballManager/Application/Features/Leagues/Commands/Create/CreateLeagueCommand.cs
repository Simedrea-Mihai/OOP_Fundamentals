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
    public class CreateLeagueCommand: IRequest<int>
    {
        [Required]
        public string Name { get; set; }
    }

    public class CreateLeagueCommandHandler: IRequestHandler<CreateLeagueCommand, int>
    {
        private readonly ILeagueRepository _repository;

        public CreateLeagueCommandHandler(ILeagueRepository repository) => _repository = repository;

        public Task<int> Handle(CreateLeagueCommand command, CancellationToken cancellationToken)
        {
            League league = new(command.Name);
            _repository.Create(league);

            return Task.FromResult(league.Id);
        }
    }
}
