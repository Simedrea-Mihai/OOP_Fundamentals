using Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.TraitsDecorator
{
    public class Technical_Dribbler : PlayerTrait
    {
        public Technical_Dribbler(IPlayerTraits traits) : base(traits)
        {
        }


        public override string Description()
        {
            return _repository.Description() + " | Technical Dribbler";
        }

        public override double ExtraOvr()
        {
            return _repository.ExtraOvr() + Values.Technical_Dribbler;
        }
    }
}
