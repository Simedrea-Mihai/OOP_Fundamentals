using Application.Contracts;
using Application.Contracts.Persistence;
using Application.Features.Players.Commands.Create;
using Application.Features.Players.Commands.CreateMultiple;
using Application.Features.Players.Queries.GetPlayersList;
using AutoMapper;
using Domain;
using Infrastructure.Static_Methods;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentation.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/player")]
    public class PlayerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public PlayerController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("Players")]
        public async Task<IActionResult> ListAllAsync([FromServices] ILoggedInUserService loggedInUserService, CancellationToken cancellationToken)
        {
            var created = await _mediator.Send(new GetPlayerListQuery(), cancellationToken);

            foreach (var c in created)
                _mapper.Map<PlayerGetDto>(c);

            var result = (PlayerGetDto)created;

            return Ok(result);
        }

        [HttpGet("Available-Players")]
        public async Task<IList<PlayerListVm>> ListAllFreePlayersAsync(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetFreePlayerListQuery(), cancellationToken);
        }

        [HttpGet("Unavailable-Players")]
        public async Task<IList<PlayerListVm>> ListAllTakenPlayersAsync(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetTakenPlayerListQuery(), cancellationToken);
        }
        [HttpGet("Player-By-Ovr")]
        public async Task<IList<PlayerListVm>> ListByOvrAsync([FromQuery]GetPlayersByOvrQuery command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HttpGet("Player-By-Age")]
        public async Task<IList<PlayerListVm>> ListByAgeAsync([FromQuery] GetPlayersByAgeQuery command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HttpPost("Player")]
        public async Task<int> CreateAsyncPlayer([FromQuery]CreatePlayerCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }
       

        [HttpPost("Players")]
        public async Task<List<int>> CreateAsyncPlayers([FromQuery]CreatePlayersCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HttpDelete("Player")]
        public async Task<int> RemoveAsyncPlayerById([FromQuery]RemovePlayerCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

    }
}
