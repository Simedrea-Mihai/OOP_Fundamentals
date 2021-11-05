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
    public class CreatePlayersCommand : IRequest<IList<int>>
    {
        public int Count { get; set; }
    }

    public class CreatePlayersCommandHandler : IRequestHandler<CreatePlayersCommand, IList<int>>
    {

        private readonly IPlayerRepository _repository;
        private readonly IProfileRepository _profileRepository;

        public CreatePlayersCommandHandler(IPlayerRepository repository, IProfileRepository profileRepository)
        {
             _repository = repository;
            _profileRepository = profileRepository;
        }

        public async Task<IList<int>> Handle(CreatePlayersCommand command, CancellationToken cancellationToken)
        {
            List<int> ids = new List<int>();
            for(int i = 0; i < command.Count; i++)
            {
                string[] name = _profileRepository.GetName();
                var player = _repository.Create(new(new(name[0], name[1], DateTime.Now)));

                _profileRepository.SetProfilePlayer(player.Profile);
                _repository.SetAttributes(player);
                ids.Add(player.Id);
            }

            return await Task.FromResult(ids);
        }
    }
}
