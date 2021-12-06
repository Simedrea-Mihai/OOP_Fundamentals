using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Domain;

namespace Application.Contracts.Persistence
{
    public interface IManagerRepository
    {
        Task<Manager> Create(Manager manager, CancellationToken cancellationToken);
        Task<Manager> ListById(int id, CancellationToken cancellationToken);
        Task<IList<Manager>> ListAll(CancellationToken cancellationToken);
        Task<IList<Manager>> ListFreeManagers(CancellationToken cancellationToken);
        Task<IList<Manager>> ListTakenManagers(CancellationToken cancellationToken);
        Task<int> RemoveManagerByIdAsync(int id, CancellationToken cancellationToken);
    }
}
