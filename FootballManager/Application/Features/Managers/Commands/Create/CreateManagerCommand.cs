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
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Managers.Commands.Create
{
    public class CreateManagerCommand : IRequest<int>
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }

    }

    public class CreateManagerCommandHandler : IRequestHandler<CreateManagerCommand, int>
    {
        private readonly IManagerRepository _repository;
        private readonly IProfileRepository _profileRepository;

        public CreateManagerCommandHandler(IManagerRepository repository, IProfileRepository profileRepository)
        {
            _repository = repository;
            _profileRepository = profileRepository;
        }

        public async Task<int> Handle(CreateManagerCommand command, CancellationToken cancellationToken)
        {
            Profile profile = new Profile(command.FirstName, command.LastName, command.BirthDate);
            Manager manager = new Manager(profile);

            await _profileRepository.SetProfileManager(manager.Profile, randomProfile: false, cancellationToken);
            await _repository.Create(manager, cancellationToken);
            return manager.Id;
            
            
        }

    }
}
