using Application.Contracts.Persistence;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Teams.Commands.AddManager
{
    public class AddManager
    {
        public int TeamId { get; set; }
        public Profile Profile { get; set; }
        public Team Team { get; set; }
    }

    public class AddManagerHandler
    {
        public readonly IManagerRepository _repository;


        public AddManagerHandler(IManagerRepository repository)
        {
            _repository = repository;
        }

        public int Handle(AddManager command)
        {
            Manager manager = new Manager(command.Profile);

            return manager.Id;

        }

        public Task<int> Handle(AddManager command, CancellationToken cancellationToken)
        {
            Manager manager = new Manager(command.Profile);


            return Task.FromResult(manager.Id);
        }

    }
}
