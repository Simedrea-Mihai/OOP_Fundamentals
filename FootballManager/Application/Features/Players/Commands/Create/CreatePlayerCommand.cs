using Application.Contracts.Persistence;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Players.Commands.Create
{
    public class CreatePlayerCommand : IRequest<int>
    {
        public Profile Profile { get; set; }
        public PlayerAttribute PlayerAttribute { get; set; }

    }

    public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, int>
    {
        private readonly IPlayerRepository _repository;

        public CreatePlayerCommandHandler(IPlayerRepository repository) => _repository = repository;

        public int Handle(CreatePlayerCommand command)
        {

            Player player = new Player(command.Profile);

            _repository.SetAttributes(player);
            _repository.Create(player);

            return player.Id;
            

        }

        public Task<int> Handle(CreatePlayerCommand command, CancellationToken cancellationToken)
        {
            Player player = new Player(command.Profile);

            _repository.SetAttributes(player);
            _repository.Create(player);

            return Task.FromResult(player.Id);
        }
    }
}
