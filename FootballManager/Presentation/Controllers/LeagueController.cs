using Application.Features.Leagues.CreateLeague;
using Application.Features.Leagues.Queries.GetLeaguesList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeagueController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LeagueController(IMediator mediator)
        {
            _mediator = mediator; 
        }

        [HttpGet("list")]
        public async Task<IList<LeagueListVm>> ListAllAsync()
        {
           return await _mediator.Send(new GetLeaguesListQuery());
        }

        [HttpPost("create")]
        public async Task<int> CreateAsync(CreateLeagueCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
