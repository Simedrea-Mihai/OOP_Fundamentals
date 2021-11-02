using Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.TraitsDecorator
{
    public class Play_Maker : PlayerTrait
    {

        public Play_Maker(IPlayerTraits traits) : base(traits)
        {
        }


        public override string Description()
        {
            return _repository.Description() + " | Play-maker";
        }

        public override double ExtraOvr()
        {
            return _repository.ExtraOvr() + Values.Play_maker;
        }
    }
}
