using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Domain;
using MediatR;

namespace Application.Features.Leagues.AddTeams
{
    public class AddTeams : IRequest<IList<int>>
    {
        [Required]
        public int LeagueId { get; set; }

        [Required]
        public List<int> Teams { get; set; }
    }

    public class AddTeamsHandler : IRequestHandler<AddTeams, IList<int>>
    {
        private readonly ILeagueRepository _repository;
        private League League = new League("default");
        private List<Team> Teams = new List<Team>();

        public AddTeamsHandler(ILeagueRepository repository)
        {
            _repository = repository;
        }

        public async Task<IList<int>> Handle(AddTeams command, CancellationToken cancellationToken)
        {

            League.Id = command.LeagueId;

            for(int i = 0; i < command.Teams.Count(); i++)
            {
                Teams.Add(new Team("default"));
                Teams[i].Id = command.Teams[i];
            }

            League.Teams = Teams;

            var ids = await _repository.AddTeams(League, Teams, cancellationToken);

            return ids;
        }
    }
}
