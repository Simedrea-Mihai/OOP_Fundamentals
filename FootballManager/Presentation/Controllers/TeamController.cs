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

        [HttpGet("list")]
        public async Task<IList<TeamListVm>> ListAllAsync(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetTeamsListQuery(), cancellationToken);
        }

        [HttpPost("create")]
        public async Task<int> CreateAsync(CreateTeamCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HttpPatch("add-manager")]
        public async Task<int> AddManagerAsync(AddManager command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }
        
        [HttpPatch("add-players")]
        public async Task<int> AddPlayersAsync(AddPlayers command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HttpPatch("buy-player")]
        public async Task<int> BuyPlayerAsync(BuyPlayer command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

    }
}
