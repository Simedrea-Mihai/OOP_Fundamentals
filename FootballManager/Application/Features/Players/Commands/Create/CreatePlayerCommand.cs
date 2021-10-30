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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }

        public int OVR { get; set; }
        public int Potential { get; set; }

    }

    public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, int>
    {
        private readonly IPlayerRepository _repository;

        public CreatePlayerCommandHandler(IPlayerRepository repository) => _repository = repository;

        public int Handle(CreatePlayerCommand command)
        {
            Player player = new Player(command.FirstName, command.LastName, command.BirthDate);

            _repository.SetAttributes(player);
            _repository.Create(player);

            return player.Id;

        }

        public Task<int> Handle(CreatePlayerCommand command, CancellationToken cancellationToken)
        {
            Player player = new Player(command.FirstName, command.LastName, command.BirthDate);

            _repository.SetAttributes(player);
            _repository.Create(player);

            return Task.FromResult(player.Id);
        }
    }
}
