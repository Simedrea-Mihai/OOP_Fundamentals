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
    public class AddManager : IRequest<int>
    {
        [Required]
        public int TeamId { get; set; }
        [Required]
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


        public async Task<int> Handle(AddManager command, CancellationToken cancellationToken)
        {
            Team.Id = command.TeamId;
            Manager.Id = command.ManagerId;

            await _teamRepository.AddManagerAsync(Team, Manager, cancellationToken);

            return Team.Id;
        }

    }
}
