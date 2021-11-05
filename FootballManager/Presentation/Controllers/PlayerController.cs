using Application.Contracts.Persistence;
using Application.Features.Players.Commands.Create;
using Application.Features.Players.Commands.CreateMultiple;
using Application.Features.Players.Queries.GetPlayersList;
using Domain;
using Infrastructure.Static_Methods;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("controller")]
    public class PlayerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PlayerController(IMediator mediator) => _mediator = mediator;

        [HttpGet("list")]
        public async Task<IList<PlayerListVm>> ListAllAsync()
        {
            return await _mediator.Send(new GetPlayerListQuery());
        }

        [HttpPost("create a random player")]
        public async Task<int> CreateAsyncPlayer(CreatePlayerCommand command)
        {
            return await _mediator.Send(command);
        }
       

        [HttpPost("create players")]
        public async Task<List<int>> CreateAsync(CreatePlayersCommand command)
        {
            return (List<int>)await _mediator.Send(command);
        }


    }
}
