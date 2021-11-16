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
        public int TeamId { get; set; }
        public int ManagerId { get; set; }
    }

    public class AddManagerHandler : IRequestHandler<AddManager, int>
    {
        public readonly IManagerRepository _repository;
        public readonly ITeamRepository _teamRepository;
        private readonly Team Team = new Team("default");
        private readonly Manager Manager = new Manager(new("defalut", "default", DateTime.Now));

        public AddManagerHandler(IManagerRepository repository, ITeamRepository teamRepository)
        {
            _repository = repository;
            _teamRepository = teamRepository;
        }


        public Task<int> Handle(AddManager command, CancellationToken cancellationToken)
        {
            Team.Id = command.TeamId;
            Team.Manager = Manager;
            Team.Manager.Id = command.ManagerId;

            _teamRepository.AddManager(Team, Team.Manager);

            return Task.FromResult(Team.Manager.Id);
        }

    }
}
