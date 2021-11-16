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
        public Profile Profile { get; set; }
        public bool Free_Agent { get; set; }

    }

    public class CreateManagerCommandHandler : IRequestHandler<CreateManagerCommand, int>
    {
        private readonly IManagerRepository _repository;

        public CreateManagerCommandHandler(IManagerRepository repository) => _repository = repository;

        public Task<int> Handle(CreateManagerCommand command, CancellationToken cancellationToken)
        {
            Manager manager = new Manager(command.Profile);
            manager.Profile.BirthDate = command.Profile.BirthDate;
            manager.Profile.Age = (DateTime.Now - manager.Profile.BirthDate).Days / 365;
            manager.FreeAgent = true;

            if (manager.Profile.Age < ManagerConstants.MinimumAge)
                throw new Exception("Manager age under 30");

            else
            { 
                _repository.Create(manager);
                return Task.FromResult(manager.Id);
            }
        }

    }
}
