using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence
{
    public interface IProfileRepository
    {
        string[] GetName();
        Profile SetProfileManager(Profile profile);
        Profile SetProfilePlayer(Profile profile);
    }
}
