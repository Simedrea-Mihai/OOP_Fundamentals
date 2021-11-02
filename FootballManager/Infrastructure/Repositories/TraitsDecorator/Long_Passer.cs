using Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.TraitsDecorator
{
    public class Long_Passer : PlayerTrait
    {

        public Long_Passer(IPlayerTraits traits) : base(traits)
        {
        }


        public override string Description()
        {
            return _repository.Description() + " | Long Passer";
        }

        public override double ExtraOvr()
        {
            return _repository.ExtraOvr() + Values.Long_Passer;
        }
    }
}
