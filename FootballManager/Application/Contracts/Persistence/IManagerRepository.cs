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
        Manager Create(Manager manager);

        IList<Manager> ListAll();
        Task<IList<Manager>> ListAllAsync(CancellationToken cancellationToken);


        IList<Manager> ListFreeManagers();
        Task<IList<Manager>> ListFreeManagersAsync(CancellationToken cancellationToken);


        IList<Manager> ListTakenManagers();
        Task<IList<Manager>> ListTakenManagersAsync(CancellationToken cancellationToken);


        Task<int> RemoveManagerByIdAsync(int id, CancellationToken cancellationToken);
    }
}
