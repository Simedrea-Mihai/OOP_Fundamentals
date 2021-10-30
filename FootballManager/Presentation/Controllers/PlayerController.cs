using Application.Features.Players.Commands.Create;
using Application.Features.Players.Queries.GetPlayersList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("controller2")]
    public class PlayerController
    {
        private readonly IMediator _mediator;
        public PlayerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("list")]
        public async Task<IList<PlayerListVm>> ListAllAsync()
        {
            return await _mediator.Send(new GetPlayerListQuery());
        }

        [HttpPost("create")]
        public async Task<int> CreateAsync(CreatePlayerCommand command)
        {
            return await _mediator.Send(command);
        }

    }
}
