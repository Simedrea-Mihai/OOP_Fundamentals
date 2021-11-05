using Application.Contracts.Persistence;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Teams.Commands.AddManager
{
    public class AddManager : IRequest<int>
    {
        public Team Team { get; set; }
    }

    public class AddManagerHandler : IRequestHandler<AddManager, int>
    {
        public readonly IManagerRepository _repository;
        public readonly ITeamRepository _teamRepository;


        public AddManagerHandler(IManagerRepository repository, ITeamRepository teamRepository)
        {
            _repository = repository;
            _teamRepository = teamRepository;
        }

        public int Handle(AddManager command)
        {
            Manager manager = new Manager(command.Team.Manager.Profile);
            Team team = new Team(command.Team.Name);
            Console.WriteLine(team.Name);
            _teamRepository.AddManager(team, manager);

            return manager.Id;

        }

        public Task<int> Handle(AddManager command, CancellationToken cancellationToken)
        {
            _teamRepository.AddManager(command.Team, command.Team.Manager);

            Console.WriteLine(command.Team.Name + " " + command.Team.Manager.Profile.FirstName);

            return Task.FromResult(command.Team.Manager.Id);
        }

    }
}
