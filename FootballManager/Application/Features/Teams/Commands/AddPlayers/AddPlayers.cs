using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Contracts.Persistence;
using Domain;

namespace Application.Teams.AddPlayers
{
    public class AddPlayers
    {
        public int TeamId { get; set; }
        public IList<int> PlayerIds { get; set; }

        public class AddPlayersHandler
        {
            public readonly IPlayerRepository _PlayerRepository;
            public readonly IProfileRepository _ProfileRepository;

            private Random rnd = new();

            public AddPlayersHandler(IPlayerRepository PlayerRepository, IProfileRepository ProfileRepository)
            {
                _PlayerRepository = PlayerRepository;
                _ProfileRepository = ProfileRepository;
            }

            public void Handle(AddPlayers command)
            {
                Team team = null; // TODO : Get team by command.TeamId
                
            }


        }

    }
}
