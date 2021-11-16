using Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain;

namespace Application.Features.Players.Commands.CreateMultiple
{
    public class CreatePlayersCommand : IRequest<List<int>>
    {
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
                name = _profileRepository.GetName();
                Player player = new Player(new Profile(name[0], name[1], DateTime.Now));

                _profileRepository.SetProfilePlayer(player.Profile);
                _repository.SetAttributes(player);
                _repository.Create(player);

                ids.Add(player.Id);
            }

            return await Task.FromResult(ids);
        }
    }
}
