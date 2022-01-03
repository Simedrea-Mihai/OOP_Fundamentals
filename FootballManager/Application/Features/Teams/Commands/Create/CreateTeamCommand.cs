using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Contracts.Persistence;
using Domain;
using MediatR;

namespace Application.Teams.CreateTeam
{
    public class CreateTeamCommand : IRequest<Team>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string HeaderDescription { get; set; }
        public double Budget { get; set; }

    }

    public class CreateTeamHandler : IRequestHandler<CreateTeamCommand, Team>
    {
        private readonly ITeamRepository _repository;
        private readonly Random rnd = new Random();

        public CreateTeamHandler(ITeamRepository repository) => _repository = repository;

        public async Task<Team> Handle(CreateTeamCommand command, CancellationToken cancellationToken)
        {
            Team team = new(command.Name, command.Description, command.HeaderDescription);

            if (command.Budget == 0)
                team.Budget = rnd.Next(1, 4) * 100000000;
            else
                team.Budget = command.Budget;

            Team createdTeam = await _repository.Create(team, cancellationToken);

            return createdTeam;
        }
    }

    
}
