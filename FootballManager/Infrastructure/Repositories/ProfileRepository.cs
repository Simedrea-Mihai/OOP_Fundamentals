using Application.Contracts.Persistence;
using Bogus;
using Domain;
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

        Faker<Profile> BillingDetailsFaker = new Faker<Profile>(locale: "ro")
            .RuleFor(x => x.FirstName, x => x.Person.FirstName)
            .RuleFor(x => x.LastName, x => x.Person.LastName)
            .RuleFor(x => x.BirthDate, x => x.Date.PastOffset(40, DateTime.Now.AddYears(-18)).Date);


        public ProfileRepository(ApplicationDbContext context) => _context = context;
        public string[] GetName()
        {
            string[] names = new string[2];

            names[0] = BillingDetailsFaker.Generate().FirstName;
            names[1] = BillingDetailsFaker.Generate().LastName;

            return names;
        }

        public Profile SetProfileManager(Profile profile)
        {
            profile.BirthDate = BillingDetailsFaker.Generate().BirthDate;  // increase start age
            profile.Age = (DateTime.Now.Year - profile.BirthDate.Year);
            

            return profile;
        }

        public Profile SetProfilePlayer(Profile profile)
        {
            profile.BirthDate = BillingDetailsFaker.Generate().BirthDate;
            profile.Age = (DateTime.Now.Year - profile.BirthDate.Year);


            return profile;
        }

    }
}

