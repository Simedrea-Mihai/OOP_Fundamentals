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
            manager.Free_Agent = true;
            _context.Managers.Add(manager);
            _context.SaveChanges();
            return manager;
        }

        public IList<Manager> ListAll()
        {
            return _context.Managers.Include(manger => manger.Profile).ToList();
        }
    }
}
