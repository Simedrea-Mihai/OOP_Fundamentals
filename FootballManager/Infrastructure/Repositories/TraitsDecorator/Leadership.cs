using Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.TraitsDecorator
{
    public class Leadership : PlayerTrait
    {

        public Leadership(IPlayerTraits traits) : base(traits)
        {
        }


        public override string Description()
        {
            return _repository.Description() + " | Leadership";
        }

        public override double ExtraOvr()
        {
            return _repository.ExtraOvr() + Values.Leadership;
        }
    }
}
