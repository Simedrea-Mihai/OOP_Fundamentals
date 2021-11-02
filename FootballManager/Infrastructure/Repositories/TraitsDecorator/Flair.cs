using Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.TraitsDecorator
{
    public class Flair : PlayerTrait
    {

        public Flair(IPlayerTraits traits) : base(traits)
        {
        }


        public override string Description()
        {
            return _repository.Description() + " | Flair";
        }

        public override double ExtraOvr()
        {
            return _repository.ExtraOvr() + Values.Flair;
        }
    }
}
