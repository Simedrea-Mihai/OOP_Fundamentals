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
    public class TeamRepository : ITeamRepository
    {
        private readonly ApplicationDbContext _context;

        public TeamRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Manager AddManager(Team team, Manager manager)
        {
            Console.WriteLine(team.Id);
            Console.WriteLine(manager.TeamId);

            bool alreadyExist = _context.Managers.Any(manager => manager.TeamId == team.Id);
            Console.WriteLine(alreadyExist);

            if (alreadyExist == true)
                throw new Exception("The team has already a manager!");

            
            _context.Managers.Where(manager => manager.Profile.FirstName == team.Manager.Profile.FirstName).FirstOrDefault().Free_Agent = false;
            _context.Managers.Where(manager => manager.Profile.FirstName == team.Manager.Profile.FirstName).FirstOrDefault().TeamId = team.Id;
            _context.SaveChanges();

               
            return manager;
        }

        public Team Create(Team team)
        {
            team.Manager = new Manager(new Profile("random", "random", DateTime.Now));
            _context.Teams.Add(team);
            _context.SaveChanges();
            return team;
        }

        public IList<Team> ListAll()
        {
            return _context.Teams.Include(team => team.Manager).Include(team => team.Manager.Profile).ToList();
        }
    }
}
