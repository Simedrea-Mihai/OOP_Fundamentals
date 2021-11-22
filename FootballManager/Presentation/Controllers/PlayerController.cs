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
using System.Threading;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/player")]
    public class PlayerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PlayerController(IMediator mediator) => _mediator = mediator;

        [HttpGet("list")]
        public async Task<IList<PlayerListVm>> ListAllAsync(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetPlayerListQuery(), cancellationToken);
        }

        [HttpGet("list-free-players")]
        public async Task<IList<PlayerListVm>> ListAllFreePlayersAsync(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetFreePlayerListQuery(), cancellationToken);
        }

        [HttpGet("list-taken-players")]
        public async Task<IList<PlayerListVm>> ListAllTakenPlayersAsync(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetTakenPlayerListQuery(), cancellationToken);
        }

        [HttpPost("create-player")]
        public async Task<int> CreateAsyncPlayer(CreatePlayerCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }
       

        [HttpPost("create-players")]
        public async Task<List<int>> CreateAsyncPlayers(CreatePlayersCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HttpDelete("delete-by-id")]
        public async Task<int> RemoveAsyncPlayerById(RemovePlayerCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

    }
}
