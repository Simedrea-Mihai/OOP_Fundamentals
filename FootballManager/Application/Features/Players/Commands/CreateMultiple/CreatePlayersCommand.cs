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

namespace Application.Features.Players.Commands.CreateMultiple
{
    public class CreatePlayersCommand : IRequest<List<int>>
    {
        [Required]
        public int Count { get; set; }
    }

    public class CreatePlayersCommandHandler : IRequestHandler<CreatePlayersCommand, List<int>>
    {

        private readonly IPlayerRepository _repository;
        private readonly IProfileRepository _profileRepository;

        public CreatePlayersCommandHandler(IPlayerRepository repository, IProfileRepository profileRepository)
        {
             _repository = repository;
            _profileRepository = profileRepository;
        }

        public async Task<List<int>> Handle(CreatePlayersCommand command, CancellationToken cancellationToken)
        {
            List<int> ids = new List<int>();
            string[] name;

            for (int i = 1; i <= command.Count; i++)
            {
                name = await _profileRepository.GetName(cancellationToken);
                Player player = new Player(new Profile(name[0], name[1], DateTime.Now));
                player.Profile.Nationality = name[2];

                await _profileRepository.SetProfilePlayer(player.Profile, randomProfile: true, cancellationToken);
                await _repository.SetAttributes(player, randomAttributes: true, cancellationToken);

                if (i == 1)
                    player.PlayerAttribute.Position = Domain.Entities.Enums.PlayerPosition.GK;
                else if (i == 2)
                    player.PlayerAttribute.Position = Domain.Entities.Enums.PlayerPosition.CB;
                else if (i == 3)
                    player.PlayerAttribute.Position = Domain.Entities.Enums.PlayerPosition.CAM;
                else if (i == 4)
                    player.PlayerAttribute.Position = Domain.Entities.Enums.PlayerPosition.ST;

                await _repository.Create(player, cancellationToken);
                ids.Add(player.Id);
            }

            return ids;
        }
    }
}
