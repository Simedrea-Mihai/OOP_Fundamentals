using Application.Contracts.Persistence;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly ApplicationDbContext _context;

        public ManagerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Manager Create(Manager manager)
        {
            manager.Profile.Age = DateTime.Now.Year - manager.Profile.BirthDate.Year;

            if (manager.Profile.Age < 30)
                throw new Exception("Age must be more than 30");

            _context.Managers.Add(manager);
            _context.SaveChanges();
            return manager;
        }


        // LIST ALL
        public IList<Manager> ListAll()
        {
            return _context.Managers.Include(manger => manger.Profile).ToList();
        }

        // LIST ALL ASYNC
        public async Task<IList<Manager>> ListAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Managers.Include(manager => manager.Profile).ToListAsync().ConfigureAwait(false);
        }


        // LIST FREE MANAGERS
        public IList<Manager> ListFreeManagers()
        {
            return _context.Managers.Where(manager => manager.FreeAgent == true).Include(manager => manager.Profile).ToList();
        }

        // LIST FREE MANAGERS ASYNC
        public async Task<IList<Manager>> ListFreeManagersAsync(CancellationToken cancellationToken)
        {
            return await _context.Managers.Where(manager => manager.FreeAgent == true).Include(manager => manager.Profile).ToListAsync().ConfigureAwait(false);
        }


        // LIST TAKEN MANAGERS
        public IList<Manager> ListTakenManagers()
        {
            return _context.Managers.Where(manager => manager.FreeAgent == false).Include(manager => manager.Profile).ToList();
        }

        // LIST TAKEN MANAGERS ASYNC
        public async Task<IList<Manager>> ListTakenManagersAsync(CancellationToken cancellationToken)
        {
            return await _context.Managers.Where(manager => manager.FreeAgent == false).Include(manager => manager.Profile).ToListAsync().ConfigureAwait(false);
        }
    }
}
