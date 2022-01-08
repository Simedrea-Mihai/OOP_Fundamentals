using Application.Contracts.Persistence;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Teams.Commands.AddManager
{
    public class BuyPlayersCommand : IRequest<List<Player>>
    {

        [Required]
        public int TeamId { get; set; }
        [Required]
        public List<int> PlayerIds { get; set; }
    }

    public class BuyPlayersCommandHandler : IRequestHandler<BuyPlayersCommand, List<Player>>
    {
        public readonly IManagerRepository _repository;
        public readonly ITeamRepository _teamRepository;

        private Team Team = new Team("default", "default", "default");

        public BuyPlayersCommandHandler(IManagerRepository repository, ITeamRepository teamRepository)
        {
            _repository = repository;
            _teamRepository = teamRepository;
        }

        public async Task<List<Player>> Handle(BuyPlayersCommand command, CancellationToken cancellationToken)
        {
            Team.Id = command.TeamId;

            if (Team.Players == null)
                Team.Players = new List<Player>();

            var players = await _teamRepository.BuyPlayers(Team, command.PlayerIds, buy: true, cancellationToken);

            return players;
        }

    }
}
