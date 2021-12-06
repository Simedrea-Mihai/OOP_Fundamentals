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


        [HttpGet("Player/{Id}")]
        public async Task<IActionResult> ListById(int Id, CancellationToken cancellationToken)
        {
            GetPlayerById command = new GetPlayerById(Id);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(_mapper.Map<PlayerGetDto>(result));
        }

        [HttpGet("Players")]
        public async Task<IActionResult> ListAllAsync([FromServices] ILoggedInUserService loggedInUserService, CancellationToken cancellationToken)
        {
            var created = await _mediator.Send(new GetPlayerListQuery(), cancellationToken);

            var list = new List<PlayerGetDto>();

            foreach (var c in created)
                list.Add(_mapper.Map<PlayerGetDto>(c));

            return Ok(list);
        }

        [HttpGet("Available-Players")]
        public async Task<IActionResult> ListAllFreePlayersAsync(CancellationToken cancellationToken)
        {
            var created = await _mediator.Send(new GetFreePlayerListQuery(), cancellationToken);

            var list = new List<PlayerGetDto>();

            foreach (var c in created)
                list.Add(_mapper.Map<PlayerGetDto>(c));

            return Ok(list);
        }

        [HttpGet("Unavailable-Players")]
        public async Task<IActionResult> ListAllTakenPlayersAsync(CancellationToken cancellationToken)
        {
            var created = await _mediator.Send(new GetTakenPlayerListQuery(), cancellationToken);

            var list = new List<PlayerGetDto>();

            foreach (var c in created)
                list.Add(_mapper.Map<PlayerGetDto>(c));

            return Ok(list);
        }

        [HttpGet("Player-By-Position")]
        public async Task<IActionResult> ListByPositionAsync([FromQuery] GetPlayersByPosition command, CancellationToken cancellationToken)
        {
            var created = await _mediator.Send(command, cancellationToken);

            var list = new List<PlayerGetDto>();

            foreach (var c in created)
                list.Add(_mapper.Map<PlayerGetDto>(c));

            return Ok(list);
        }

        [HttpGet("Player-By-Ovr")]
        public async Task<IActionResult> ListByOvrAsync([FromQuery]GetPlayersByOvrQuery command, CancellationToken cancellationToken)
        {
            var created = await _mediator.Send(command, cancellationToken);

            var list = new List<PlayerGetDto>();

            foreach (var c in created)
                list.Add(_mapper.Map<PlayerGetDto>(c));

            return Ok(list);
        }

        [HttpGet("Player-By-Age")]
        public async Task<IActionResult> ListByAgeAsync([FromQuery] GetPlayersByAgeQuery command, CancellationToken cancellationToken)
        {
            var created = await _mediator.Send(command, cancellationToken);

            var list = new List<PlayerGetDto>();

            foreach (var c in created)
                list.Add(_mapper.Map<PlayerGetDto>(c));

            return Ok(list);
        }

        [HttpPost("Player")]
        public async Task<IActionResult> CreateAsyncPlayer([FromQuery]CreatePlayerCommand command, CancellationToken cancellationToken)
        {
            var created = await _mediator.Send(command, cancellationToken);
            var result = _mapper.Map<PlayerGetDto>(created);
            return Ok(result);
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
