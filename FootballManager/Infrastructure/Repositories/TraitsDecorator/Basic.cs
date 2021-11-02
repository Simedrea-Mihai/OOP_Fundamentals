using Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.TraitsDecorator
{
    public class Basic : IPlayerTraits
    {

        public string Description()
        {
            return "Basic";
        }

        public double ExtraOvr()
        {
            return 0;
        }
    }
}
