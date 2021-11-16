using Domain;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Teams.Queries.GetTeamList
{
    public class TeamListVm
    {
        public int Id { get; set; }
        public double Budget { get; set; }
        public string Name { get; set; }
        public Manager Manager { get; set; }
        public IList<Player> Players { get; set; }
        public bool Taken { get; set; }
    }
}
