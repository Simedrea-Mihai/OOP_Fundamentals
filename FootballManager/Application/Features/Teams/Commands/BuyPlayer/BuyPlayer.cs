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
    public class BuyPlayer : IRequest<int>
    {

        [Required]
        public int TeamId { get; set; }
        [Required]
        public int PlayerId { get; set; }
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

        public async Task<int> Handle(BuyPlayer command, CancellationToken cancellationToken)
        {
            Team.Id = command.TeamId;

            if (Team.Players == null)
                Team.Players = new List<Player>();

            Player.Id = command.PlayerId;
            Team.Players.Add(Player);


            await _teamRepository.BuyPlayerAsync(Team, Team.Players.Where(player => player.Id == command.PlayerId).First(), buy: true, cancellationToken);

            return await Task.FromResult(Team.Players.Where(player => player.Id == command.PlayerId).First().Id);
        }

    }
}
