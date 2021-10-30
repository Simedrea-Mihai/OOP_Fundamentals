using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Team : BaseEntity
    {
        public override int Id { get; set; }
        //public IList<Player> Players { get; set; }
        //public Manager Manager { get; set; }
        public string Name { get; private set; }

        public Team(string name)
        {
            Name = name;
        }

    }
}
