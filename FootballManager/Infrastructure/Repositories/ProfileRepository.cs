using Application.Constants;
using Application.Contracts.Persistence;
using Bogus;
using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ApplicationDbContext _context;


        Faker<Profile> BillingDetailsFakerPlayer = new Faker<Profile>(locale: "ro")
            .RuleFor(x => x.FirstName, x => x.Name.FirstName(Bogus.DataSets.Name.Gender.Male))
            .RuleFor(x => x.LastName, x => x.Person.LastName)
            .RuleFor(x => x.BirthDate, x => x.Date.PastOffset(PlayerConstants.YearsToGoBack, DateTime.Now.AddYears(PlayerConstants.Offset)).Date);

        Faker<Profile> BillingDetailsFakerManager = new Faker<Profile>(locale: "ro")
            .RuleFor(x => x.FirstName, x => x.Name.FirstName(Bogus.DataSets.Name.Gender.Male))
            .RuleFor(x => x.LastName, x => x.Person.LastName)
            .RuleFor(x => x.BirthDate, x => x.Date.PastOffset(ManagerConstants.YearsToGoBack, DateTime.Now.AddYears(ManagerConstants.Offset)).Date);


        public ProfileRepository(ApplicationDbContext context) => _context = context;
        public async Task<string[]> GetName(CancellationToken cancellationToken)
        {
            string[] names = new string[2];

            names[0] = BillingDetailsFakerPlayer.Generate().FirstName;
            names[1] = BillingDetailsFakerPlayer.Generate().LastName;

            return await Task.FromResult(names);
        }

        public async Task<Profile> SetProfileManager(Profile profile, bool randomProfile, CancellationToken cancellationToken)
        {
            if(randomProfile)
                profile.BirthDate = BillingDetailsFakerManager.Generate().BirthDate; 

            profile.Age = (DateTime.Now.Year - profile.BirthDate.Year);
            
            return await Task.FromResult(profile);
        }

        public async Task<Profile> SetProfilePlayer(Profile profile, bool randomProfile, CancellationToken cancellationToken)
        {
            if(randomProfile)
                profile.BirthDate = BillingDetailsFakerPlayer.Generate().BirthDate;

            profile.Age = (DateTime.Now.Year - profile.BirthDate.Year);

            return await Task.FromResult(profile);
        }

    }
}

