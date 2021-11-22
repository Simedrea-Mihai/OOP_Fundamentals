using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Methods
{
    public static class ManagerMethods
    {
        public static void RemoveManagerById(ApplicationDbContext context, int id)
        {
            Manager manager = context.Managers.Where(p => p.Id == id).First();

            context.Managers.Remove(manager);

            context.SaveChanges();
        }
    }
}
