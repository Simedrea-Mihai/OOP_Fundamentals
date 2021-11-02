using Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.TraitsDecorator
{
    public class Speed_Dribbler : PlayerTrait
    {
        public Speed_Dribbler(IPlayerTraits traits) : base(traits)
        {
        }


        public override string Description()
        {
            return _repository.Description() + " | Speed Dribbler";
        }

        public override double ExtraOvr()
        {
            return _repository.ExtraOvr() + Values.Speed_Dribbler;
        }
    }
}
