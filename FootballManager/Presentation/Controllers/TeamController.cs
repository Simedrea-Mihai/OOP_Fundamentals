using Application.Features.Players.Commands.Create;
using Application.Features.Teams.Commands.AddManager;
using Application.Features.Teams.Queries.GetTeamList;
using Application.Teams.AddPlayers;
using Application.Teams.CreateTeam;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/team")]
    public class TeamController
    {
        private readonly IMediator _mediator;
        public TeamController(IMediator mediator) => _mediator = mediator;

        [HttpGet("Team")]
        public async Task<IList<TeamListVm>> ListAllAsync(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetTeamsListQuery(), cancellationToken);
        }

        [HttpPost("Team")]
        public async Task<int> CreateAsync([FromBody] CreateTeamCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HttpPatch("Add-Manager")]
        public async Task<int> AddManagerAsync([FromQuery] AddManager command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }
        
        [HttpPatch("Add-Players")]
        public async Task<int> AddPlayersAsync([FromQuery] AddPlayers command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HttpPatch("Buy-Player")]
        public async Task<int> BuyPlayerAsync([FromQuery] BuyPlayer command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HttpPatch("Fire-Player")]
        public async Task<int> FirePlayerAsync([FromQuery] FirePlayerCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        /*
        [HttpPatch("fire-all")]
        public async Task<int> DeleteAllAsync(RemovePlayers command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }*/

        [HttpDelete("Team")]
        public async Task<int> RemoveAsyncTeamById([FromQuery] RemoveTeamCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }


    }
}
