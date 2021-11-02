using Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.TraitsDecorator
{
    public class Finesse_Shot : PlayerTrait
    {

        public Finesse_Shot(IPlayerTraits traits) : base(traits)
        {
        }


        public override string Description()
        {
            return _repository.Description() + " | Finesse Shot";
        }

        public override double ExtraOvr()
        {
            return _repository.ExtraOvr() + Values.Finesse_Shot;
        }
    }
}
