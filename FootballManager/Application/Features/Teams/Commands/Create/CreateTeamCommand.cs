﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Domain;
using MediatR;

namespace Application.Teams.CreateTeam
{
    public class CreateTeamCommand : IRequest<int>
    {
        public string Name { get; set; }
        public double Budget { get; set; }

    }

    public class CreateTeamHandler : IRequestHandler<CreateTeamCommand, int>
    {
        private readonly ITeamRepository _repository;
        private readonly Random rnd = new Random();

        public CreateTeamHandler(ITeamRepository repository) => _repository = repository;


        public Task<int> Handle(CreateTeamCommand command, CancellationToken cancellationToken)
        {
            Team team = new(command.Name);
            team.Budget = rnd.Next(100000000, 300000000);
            _repository.Create(team);

            return Task.FromResult(team.Id);
        }
    }

    
}
