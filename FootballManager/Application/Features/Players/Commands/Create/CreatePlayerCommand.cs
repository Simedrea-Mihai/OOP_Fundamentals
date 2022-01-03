using Application.Contracts.Persistence;
using Domain;
using Domain.Entities.CommandEntities;
using Domain.Entities.Enums;
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
    public class CreatePlayerCommand : IRequest<Player>
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Nationality { get; set; }
        [Required]
        public int OVR { get; set; }
        [Required]
        public int Potential { get; set; }
        [Required]
        public PlayerPosition Position { get; set; }

    }

    public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, Player>
    {
        private readonly IPlayerRepository _repository;
        private readonly IProfileRepository _profileRepository;

        public CreatePlayerCommandHandler(IPlayerRepository repository, IProfileRepository profileRepository)
        {
            _repository = repository;
            _profileRepository = profileRepository;
        }

        public async Task<Player> Handle(CreatePlayerCommand command, CancellationToken cancellationToken)
        {

            Profile profile = new Profile();
            profile.FirstName = command.FirstName;
            profile.LastName = command.LastName;
            profile.BirthDate = command.BirthDate;
            profile.Nationality = command.Nationality;


            Player player = new Player(profile);

            player.PlayerAttribute = new PlayerAttribute();
            player.PlayerAttribute.OVR = command.OVR;
            player.PlayerAttribute.Potential = command.Potential;
            player.PlayerAttribute.Position = command.Position;
            player.PlayerAttribute.Traits = new Traits(0, "Basic");


            if (player.PlayerAttribute.Potential > 99 || player.PlayerAttribute.OVR > 99
            || player.PlayerAttribute.Potential <= 0 || player.PlayerAttribute.OVR <= 0
            || player.PlayerAttribute.Potential < player.PlayerAttribute.OVR)
                throw new Exception("Invalid values ( Potential or OVR > 99 OR < 1 )");


            await _profileRepository.SetProfilePlayer(player.Profile, randomProfile: false, cancellationToken);
            await _repository.SetAttributes(player, randomAttributes: false, cancellationToken);
            await _repository.Create(player, cancellationToken);

            return player;
        }
    }
}
