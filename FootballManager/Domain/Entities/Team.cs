using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public sealed class Team : BaseEntity
    {
        public override int Id { get; set; }
        public double Budget { get; set; }
        public IList<Player> Players { get; set; }
        public Manager Manager { get; set; }
        public string Name { get; set; }
        public Player Player { get; set; }

        public Team(string name)
        {
            Name = name;
        }

    }
}
