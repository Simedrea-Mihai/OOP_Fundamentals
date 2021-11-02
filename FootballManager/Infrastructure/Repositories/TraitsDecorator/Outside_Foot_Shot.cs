using Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.TraitsDecorator
{
    public class Outside_Foot_Shot : PlayerTrait
    {
        public Outside_Foot_Shot(IPlayerTraits traits) : base(traits)
        {
        }


        public override string Description()
        {
            return _repository.Description() + " | Outside Foot Shot";
        }

        public override double ExtraOvr()
        {
            return _repository.ExtraOvr() + Values.Outside_Foot_Shot;
        }
    }
}
