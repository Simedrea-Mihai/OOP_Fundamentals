using Application.Contracts.Persistence;
using Domain;
using Infrastructure.Repositories.Methods;
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

        public async Task<Manager> Create(Manager manager, CancellationToken cancellationToken)
        {
            _context.Managers.Add(manager);
            await _context.SaveChangesAsync(cancellationToken);
            return manager;
        }

        public async Task<IList<Manager>> ListAll(CancellationToken cancellationToken)
        {
            return await _context.Managers.Include(manger => manger.Profile).ToListAsync();
        }

        public async Task<Manager> ListById(int id, CancellationToken cancellationToken)
        {
            return await _context.Managers
                .Include(manager => manager.Profile)
                .Where(p => p.Id == id).FirstAsync();
        }


        public async Task<IList<Manager>> ListFreeManagers(CancellationToken cancellationToken)
        {
            return await _context.Managers.Where(manager => manager.FreeAgent == true).Include(manager => manager.Profile).ToListAsync();
        }


        public async Task<IList<Manager>> ListTakenManagers(CancellationToken cancellationToken)
        {
            return await _context.Managers.Where(manager => manager.FreeAgent == false).Include(manager => manager.Profile).ToListAsync();
        }

        public async Task<int> RemoveManagerByIdAsync(int id, CancellationToken cancellationToken)
        {
            int managerId = await ManagerMethods.RemoveManagerById(_context, id, cancellationToken);
            return managerId;
        }
    }
}
