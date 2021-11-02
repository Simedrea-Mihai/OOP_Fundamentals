using Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.TraitsDecorator
{
    public class Team_Player : PlayerTrait
    {
        public Team_Player(IPlayerTraits traits) : base(traits)
        {
        }


        public override string Description()
        {
            return _repository.Description() + " | Team Player";
        }

        public override double ExtraOvr()
        {
            return _repository.ExtraOvr() + Values.Team_Player;
        }
    }
}
