using Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.TraitsDecorator
{
    public class Power_Header : PlayerTrait
    {
        public Power_Header(IPlayerTraits traits) : base(traits)
        {
        }


        public override string Description()
        {
            return _repository.Description() + " | Power Header";
        }

        public override double ExtraOvr()
        {
            return _repository.ExtraOvr() + Values.Power_Header;
        }
    }
}
