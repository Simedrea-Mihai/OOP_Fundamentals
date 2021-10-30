using Application.Features.Teams.Queries.GetTeamList;
using Application.Teams.CreateTeam;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("controller3")]
    public class TeamController
    {
        private readonly IMediator _mediator;
        public TeamController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("list")]
        public async Task<IList<TeamListVm>> ListAllAsync()
        {
            return await _mediator.Send(new GetTeamsListQuery());
        }

        [HttpPost("create")]
        public async Task<int> CreateAsync(CreateTeamCommand command)
        {
            return await _mediator.Send(command);
        }

    }
}
