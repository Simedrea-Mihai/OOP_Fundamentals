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

namespace Presentation.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/league")]
    public class LeagueController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LeagueController(IMediator mediator) => _mediator = mediator;

        [HttpGet("list")]
        public async Task<IList<LeagueListVm>> ListAllAsync(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetLeaguesListQuery(), cancellationToken);
        }

        [HttpPost("create")]
        public async Task<int> CreateAsync(CreateLeagueCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

        [HttpPatch("add-teams")]
        public async Task<int> AddTeamAsync(AddTeams command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }
    }
}
