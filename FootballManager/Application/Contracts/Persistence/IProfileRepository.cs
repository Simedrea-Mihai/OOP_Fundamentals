using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence
{
    public interface IProfileRepository
    {
        Task<string[]> GetName(CancellationToken cancellationToken);
        Task<Profile> SetProfileManager(Profile profile, bool randomProfile, CancellationToken cancellationToken);
        Task<Profile> SetProfilePlayer(Profile profile, bool randomProfile, CancellationToken cancellationToken);
    }
}
