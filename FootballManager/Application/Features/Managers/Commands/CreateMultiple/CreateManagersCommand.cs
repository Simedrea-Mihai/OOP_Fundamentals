using Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Managers.Commands.CreateMultiple
{
    public class CreateManagersCommand : IRequest<IList<int>>
    {
        [Required]
        public int Count { get; set; }
    }

    public class CreateManagerCommandHandler : IRequestHandler<CreateManagersCommand, IList<int>>
    {
        public IManagerRepository _repository;
        public IProfileRepository _profileRepository;

        public CreateManagerCommandHandler(IManagerRepository repository, IProfileRepository profileRepository)
        {
            _repository = repository;
            _profileRepository = profileRepository;
        }


        public async Task<IList<int>> Handle(CreateManagersCommand command, CancellationToken cancellationToken)
        {
            List<int> ids = new List<int>();
            string[] name;

            for (int i = 0; i< command.Count; i++)
            {
                name = await _profileRepository.GetName(cancellationToken);
                Manager manager = new Manager(new Profile(name[0], name[1], DateTime.Now));

                await _profileRepository.SetProfileManager(manager.Profile, randomProfile: true, cancellationToken);
                await _repository.Create(manager, cancellationToken);

                ids.Add(manager.Id);
            }

            return ids;
        }
    }
}
