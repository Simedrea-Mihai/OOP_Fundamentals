using Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public abstract class PlayerTrait : IPlayerTraits
    {
        protected IPlayerTraits _repository;

        public PlayerTrait(IPlayerTraits repository)
        {
            _repository = repository;
        }


        public abstract string Description();
        public abstract double ExtraOvr();

    }
}
