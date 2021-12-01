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
    public class RemoveManagerCommand : IRequest<int>
    {
        [Required]
        public int PlayerId { get; set; }

    }

    public class RemoveManagerCommandHandler : IRequestHandler<RemoveManagerCommand, int>
    {
        private readonly IManagerRepository _repository;

        public RemoveManagerCommandHandler(IManagerRepository repository)
        {
            _repository = repository;
        }

        public Task<int> Handle(RemoveManagerCommand command, CancellationToken cancellationToken)
        {
            _repository.RemoveManagerByIdAsync(command.PlayerId, cancellationToken);

            return Task.FromResult(command.PlayerId);
        }
    }
}
