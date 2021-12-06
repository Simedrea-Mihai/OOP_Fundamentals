using Application.Contracts.Persistence;
using Domain;
using Domain.Entities.CommandEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Players.Commands.Create
{
    public class CreatePlayerCommand : IRequest<int>
    {
        [Required]
        public CProfile Profile { get; set; }
        [Required]

        public CPlayerAttribute PlayerAttribute { get; set; }

    }

    public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, int>
    {
        private readonly IPlayerRepository _repository;
        private readonly IProfileRepository _profileRepository;

        public CreatePlayerCommandHandler(IPlayerRepository repository, IProfileRepository profileRepository)
        {
            _repository = repository;
            _profileRepository = profileRepository;
        }

        public async Task<int> Handle(CreatePlayerCommand command, CancellationToken cancellationToken)
        {

            Profile profile = new Profile();
            profile.FirstName = command.Profile.FirstName;
            profile.LastName = command.Profile.LastName;
            profile.BirthDate = command.Profile.BirthDate;


            Player player = new Player(profile);

            player.PlayerAttribute = new PlayerAttribute();
            player.PlayerAttribute.OVR = command.PlayerAttribute.OVR;
            player.PlayerAttribute.Potential = command.PlayerAttribute.Potential;
            player.PlayerAttribute.Traits = new Traits(0, "Basic");


            if (player.PlayerAttribute.Potential > 99 || player.PlayerAttribute.OVR > 99
            || player.PlayerAttribute.Potential <= 0 || player.PlayerAttribute.OVR <= 0
            || player.PlayerAttribute.Potential < player.PlayerAttribute.OVR)
                throw new Exception("Invalid values ( Potential or OVR > 99 OR < 1 )");


            await _profileRepository.SetProfilePlayer(player.Profile, randomProfile: false, cancellationToken);
            await _repository.SetAttributes(player, randomAttributes: false, cancellationToken);
            await _repository.Create(player, cancellationToken);

            return player.Id;
        }
    }
}
