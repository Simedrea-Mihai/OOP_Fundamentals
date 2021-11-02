using Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.TraitsDecorator
{
    public class Injury_Prone : PlayerTrait
    {

        public Injury_Prone(IPlayerTraits traits) : base(traits)
        {
        }


        public override string Description()
        {
            return _repository.Description() + " | Injury Prone";
        }

        public override double ExtraOvr()
        {
            return _repository.ExtraOvr() + Values.Injury_Prone;
        }
    }
}
