using Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ApplicationDbContext _context;
        string[] FirstNamesData = File.ReadAllLines(@"firstNamesDB.txt");
        string[] LastNamesData = File.ReadAllLines(@"lastNamesDB.txt");
        Random rnd = new Random();

        public ProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public string[] GetName()
        {

            string[] names = new string[2];
            names[0] = FirstNamesData[rnd.Next(0, FirstNamesData.Length)];
            names[1] = LastNamesData[rnd.Next(0, LastNamesData.Length)];


            return names;
        }

    }
}

