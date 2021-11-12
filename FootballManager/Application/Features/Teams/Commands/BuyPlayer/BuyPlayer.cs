using Application.Contracts.Persistence;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Teams.Commands.AddManager
{
    public class BuyPlayer : IRequest<int>
    {

        public int TeamId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class BuyPlayerHandler : IRequestHandler<BuyPlayer, int>
    {
        public readonly IManagerRepository _repository;
        public readonly ITeamRepository _teamRepository;

        private Team Team = new Team("default");
        private Player Player = new Player(new("default", "default", DateTime.Now));

        public BuyPlayerHandler(IManagerRepository repository, ITeamRepository teamRepository)
        {
            _repository = repository;
            _teamRepository = teamRepository;
        }

        public Task<int> Handle(BuyPlayer command, CancellationToken cancellationToken)
        {
            Team.Id = command.TeamId;
            Team.Player = Player;

            Player.Profile.FirstName = command.FirstName;
            Player.Profile.LastName = command.LastName;
            Player.PlayerAttribute = new PlayerAttribute();


            _teamRepository.BuyPlayer(Team, Player);

            return Task.FromResult(Team.Player.Id);
        }

    }
}
