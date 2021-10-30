using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Domain;
using MediatR;

namespace Application.Teams.CreateTeam
{
    public class CreateTeamCommand : IRequest<int>
    {
        public string Name { get; set; }
    }

    public class CreateTeamHandler : IRequestHandler<CreateTeamCommand, int>
    {
        private readonly ITeamRepository _repository;

        public CreateTeamHandler(ITeamRepository repository) => _repository = repository;

        public int Handle(CreateTeamCommand command)
        {
            Team team = new(command.Name);
            _repository.Create(team);

            return team.Id;
                
        }

        public Task<int> Handle(CreateTeamCommand command, CancellationToken cancellationToken)
        {
            Team team = new(command.Name);
            _repository.Create(team);

            return Task.FromResult(team.Id);
        }
    }

    
}
