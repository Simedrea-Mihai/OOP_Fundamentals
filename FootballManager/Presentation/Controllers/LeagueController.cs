using Application.Features.Leagues.AddTeams;
using Application.Features.Leagues.CreateLeague;
using Application.Features.Leagues.Queries.GetLeaguesList;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Application.Features.Players.Commands.Create;

namespace Presentation.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/league")]
    public class LeagueController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LeagueController(IMediator mediator) => _mediator = mediator;

        [HttpGet("League")]
        public async Task<IList<LeagueListVm>> ListAllAsync(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetLeaguesListQuery(), cancellationToken);
        }

        [HttpPost("League")]
        public async Task<int> CreateAsync([FromQuery] CreateLeagueCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HttpPatch("Add-Teams")]
        public async Task<int> AddTeamAsync([FromQuery] AddTeams command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HttpDelete("League")]
        public async Task<int> RemoveAsyncLeagueById([FromQuery] RemoveLeagueCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }
    }
}
