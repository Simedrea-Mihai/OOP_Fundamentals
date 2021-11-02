using Application.Features.Managers.Queries.GetManagersList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Application.Features.Managers.Commands.Create;
using Application.Features.Teams.Commands.AddManager;
using Application.Contracts.Persistence;
using Domain;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Infrastructure.Static_Methods;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ManagerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProfileRepository _repository;
        public ManagerController(IMediator mediator, IProfileRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        [HttpGet("list")]
        public async Task<IList<ManagerListVm>> ListAllAsync()
        {
            return await _mediator.Send(new GetManagerListQuery());
        }

        [HttpPost("create a manager")]
        public async Task<int> CreateAsync(CreateManagerCommand command)
        {
            return await _mediator.Send(command);
        }

        /*[HttpPost("create_managers")]
        public async void CreateManagersAsync(CreateManagerCommand command)
        {
            /*
            for (int i = 0; i < StaticVariables.EntitiesCount; i++)
            {
                string[] names = _repository.GetName();
                var player = new Manager(new Profile(names[0], names[1], DateTime.Now));
                var json = JsonConvert.SerializeObject(player);

                var data = new StringContent(json, Encoding.UTF8, "application/json");

                using var client = new HttpClient();
                var url = @"https://localhost:5001/Manager/create_managers";

                var post = await client.PostAsync(url, data);
            }

        }*/


        // work in progress
        [HttpPut("edit")]
        public async Task<int> EditAsync(AddManager command)
        {
            return (int)await _mediator.Send(command);
        }

    }
}
