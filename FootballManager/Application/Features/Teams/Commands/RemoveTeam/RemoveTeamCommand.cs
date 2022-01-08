﻿using Application.Contracts.Persistence;
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
    public class RemoveTeamCommand : IRequest<int>
    {
        public int Id { get; set; }

        public RemoveTeamCommand(int id)
        {
            Id = id;
        }
    }

    public class RemoveTeamCommandHandler : IRequestHandler<RemoveTeamCommand, int>
    {
        private readonly ITeamRepository _repository;

        public RemoveTeamCommandHandler(ITeamRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(RemoveTeamCommand command, CancellationToken cancellationToken)
        {
            await _repository.RemoveTeamByIdAsync(command.Id, cancellationToken);

            return command.Id;
        }
    }
}
