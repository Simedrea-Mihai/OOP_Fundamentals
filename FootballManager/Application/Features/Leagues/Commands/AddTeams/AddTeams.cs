using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain;

namespace Application.Features.Leagues.AddTeams
{
    public class AddTeams
    {
        public int LeagueId { get; set; }
        public IList<int> TeamIds { get; set; }

        public class AddTeamsHandler
        {
            public void Handle(AddTeams command)
            {
                League league = null; // TODO : Get league by command.LeagueId

            }
        }
    }
}
