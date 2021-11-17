using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.Threading;
using Profile = Domain.Profile;
using Application.Constants;

namespace Application.Features.Managers.Commands.Create
{
    public class CreateManagerCommand : IRequest<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int TeamIdManager { get; set; }

    }

    public class CreateManagerCommandHandler : IRequestHandler<CreateManagerCommand, int>
    {
        private readonly IManagerRepository _repository;
        private readonly ITeamRepository _teamRepository;

        public CreateManagerCommandHandler(IManagerRepository repository, ITeamRepository teamRepository)
        {
            _repository = repository;
            _teamRepository = teamRepository;
        }

        public Task<int> Handle(CreateManagerCommand command, CancellationToken cancellationToken)
        {
            Profile profile = new Profile(command.FirstName, command.LastName, command.BirthDate);


            Manager manager = new Manager(profile);
            manager.Profile.BirthDate = command.BirthDate;
            manager.Profile.Age = DateTime.Now.Year - manager.Profile.BirthDate.Year;
            manager.TeamIdManager = command.TeamIdManager;

            if (manager.Profile.Age < ManagerConstants.MinimumAge)
                throw new Exception("Manager age under 30");

            else
            {

                if (command.TeamIdManager != 0)
                {
                    Team team = new Team("default");
                    team.Id = command.TeamIdManager;
                    _repository.Create(manager);
                    manager.FreeAgent = true;
                    manager.TeamIdManager = 0;
                    _teamRepository.AddManagerAsync(team, manager, cancellationToken);

                    return Task.FromResult(manager.Id);
                }
                else
                {
                    manager.FreeAgent = true;
                    _repository.Create(manager);
                    return Task.FromResult(manager.Id);
                }
            }
        }

    }
}
