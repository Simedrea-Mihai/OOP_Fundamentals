using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Manager : BaseEntity
    {
        public override int Id { get; set; }
        public int TeamId { get; set; }
        public Profile Profile { get; set; }
        public Team Team { get; set; }


    
        private Manager() { }

        public Manager(Profile profile)
        {
            Profile = profile;
        }


    }
}
