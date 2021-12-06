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

namespace Application.Teams.AddPlayers
{
    public class AddPlayers : IRequest<IList<int>>
    {
        [Required]
        public int TeamId { get; set; }
        [Required]
        public int PlayersCount { get; set; }
    }

    public class AddPlayersHandler : IRequestHandler<AddPlayers, IList<int>>
    {
        public readonly IPlayerRepository _PlayerRepository;
        public readonly IProfileRepository _ProfileRepository;
        public readonly ITeamRepository _teamRepository;

        private readonly Team Team = new Team("default");


        public AddPlayersHandler(IPlayerRepository PlayerRepository, IProfileRepository ProfileRepository, ITeamRepository teamRepository)
        {
            _PlayerRepository = PlayerRepository;
            _ProfileRepository = ProfileRepository;
            _teamRepository = teamRepository;
        }

        public async Task<IList<int>> Handle(AddPlayers command, CancellationToken cancellationToken)
        {
            Team.Id = command.TeamId;
            List<int> ids = new List<int>();

            for (int i = 0; i < command.PlayersCount; i++)
            {
                Player player = await _PlayerRepository.GetPlayer(cancellationToken);
                await _teamRepository.BuyPlayer(Team, player, buy: false, cancellationToken);
                ids.Add(player.Id);
            }

            return ids;
        }
    }
}
