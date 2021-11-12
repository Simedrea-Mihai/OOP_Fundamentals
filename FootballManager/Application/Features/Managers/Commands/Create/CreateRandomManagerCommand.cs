using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Constants;
using Domain;
using MediatR;

namespace Application.Features.Managers.Commands.Create
{
    public class CreateRandomManagerCommand : IRequest<int>
    {
        public Profile Profile { get; set; }

    }

    public class CreateRandomManagerCommandHandler : IRequestHandler<CreateRandomManagerCommand, int>
    {
        private readonly IManagerRepository _repository;
        private readonly IProfileRepository _profileRepository;

        public CreateRandomManagerCommandHandler(IManagerRepository repository, IProfileRepository profileRepository)
        {
            _repository = repository;
            _profileRepository = profileRepository;

        }

        public int Handle(CreateRandomManagerCommand command)
        {
            string[] name = _profileRepository.GetName();
            Manager manager = new(new(name[0], name[1], DateTime.Now));

            _profileRepository.SetProfileManager(manager.Profile);
            _repository.Create(manager);

            return manager.Id;

        }

        public Task<int> Handle(CreateRandomManagerCommand command, CancellationToken cancellationToken)
        {
            string[] name = _profileRepository.GetName();
            Manager manager = new(new(name[0], name[1], DateTime.Now));

            _profileRepository.SetProfileManager(manager.Profile);
            _repository.Create(manager);

            return Task.FromResult(manager.Id);
        }


    }
}
