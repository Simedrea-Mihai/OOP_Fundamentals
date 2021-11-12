using Application.Contracts.Persistence;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            manager.FreeAgent = true;
            _context.Managers.Add(manager);
            _context.SaveChanges();
            return manager;
        }

        public IList<Manager> ListAll()
        {
            return _context.Managers.Include(manger => manger.Profile).ToList();
        }

        public IList<Manager> ListFreeManagers()
        {
            return _context.Managers.Where(manager => manager.FreeAgent == true).Include(manager => manager.Profile).ToList();
        }

        public IList<Manager> ListTakenManagers()
        {
            return _context.Managers.Where(manager => manager.FreeAgent == false).Include(manager => manager.Profile).ToList();
        }
    }
}
