using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Domain;
using MediatR;

namespace Application.Features.Managers.Commands.Create
{
    public class CreateManagerCommand : IRequest<int>
    {
        public Profile Profile { get; set; }

    }

    public class CreateManagerCommandHandler : IRequestHandler<CreateManagerCommand, int>
    {
        private readonly IManagerRepository _repository;

        public CreateManagerCommandHandler(IManagerRepository repository) => _repository = repository;

        public int Handle(CreateManagerCommand command)
        {
            Manager manager = new Manager(command.Profile);

            _repository.Create(manager);

            return manager.Id;

        }

        public Task<int> Handle(CreateManagerCommand command, CancellationToken cancellationToken)
        {
            Manager manager = new Manager(command.Profile);

            _repository.Create(manager);

            return Task.FromResult(manager.Id);
        }


    }
}
