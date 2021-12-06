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
    public class CreateRandomManagerCommand : IRequest<Manager> { }

    public class CreateRandomManagerCommandHandler : IRequestHandler<CreateRandomManagerCommand, Manager>
    {
        private readonly IManagerRepository _repository;
        private readonly IProfileRepository _profileRepository;

        public CreateRandomManagerCommandHandler(IManagerRepository repository, IProfileRepository profileRepository)
        {
            _repository = repository;
            _profileRepository = profileRepository;
        }

        public async Task<Manager> Handle(CreateRandomManagerCommand command, CancellationToken cancellationToken)
        {
            string[] name = await _profileRepository.GetName(cancellationToken);
            Manager manager = new(new(name[0], name[1], DateTime.Now));

            await _profileRepository.SetProfileManager(manager.Profile, randomProfile: true, cancellationToken);
            var createdManager = await _repository.Create(manager, cancellationToken);

            return createdManager;
        }


    }
}
