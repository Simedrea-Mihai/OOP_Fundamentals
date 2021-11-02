using Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.TraitsDecorator
{
    public class Fancy_Passes : PlayerTrait
    {
        public Fancy_Passes(IPlayerTraits traits) : base(traits)
        {
        }


        public override string Description()
        {
            return _repository.Description() + " | Fancy Passes";
        }

        public override double ExtraOvr()
        {
            return _repository.ExtraOvr() + Values.Fancy_Passes;
        }
    }
}
