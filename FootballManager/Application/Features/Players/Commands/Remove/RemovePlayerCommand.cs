using Application.Contracts.Persistence;
using Domain;
using Domain.Entities.CommandEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Players.Commands.Create
{
    public class RemovePlayerCommand : IRequest<int>
    {
        public int PlayerId { get; set; }

    }

    public class RemovePlayerCommandHandler : IRequestHandler<RemovePlayerCommand, int>
    {
        private readonly IPlayerRepository _repository;

        public RemovePlayerCommandHandler(IPlayerRepository repository)
        {
            _repository = repository;
        }

        public Task<int> Handle(RemovePlayerCommand command, CancellationToken cancellationToken)
        {
            _repository.RemovePlayerByIdAsync(command.PlayerId, cancellationToken);

            return Task.FromResult(command.PlayerId);
        }
    }
}
