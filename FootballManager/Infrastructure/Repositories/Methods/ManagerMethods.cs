using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Methods
{
    public static class ManagerMethods
    {
        public static async Task<int> RemoveManagerById(ApplicationDbContext context, int id, CancellationToken cancellationToken)
        {
            Manager manager = context.Managers.Where(p => p.Id == id).First();
            context.Managers.Remove(manager);
            await context.SaveChangesAsync(cancellationToken);
            return manager.Id;
        }
    }
}
