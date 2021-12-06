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
using AutoMapper;
using Presentation.DTOs;
using Application.Features.Players.Queries.GetPlayersList;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/league")]
    public class LeagueController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public LeagueController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("League/{Id}")]
        public async Task<IActionResult> ListById(int Id, CancellationToken cancellationToken)
        {
            GetLeagueById command = new GetLeagueById(Id);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(_mapper.Map<LeagueGetDto>(result));
        }

        [HttpGet("League")]
        public async Task<IActionResult> ListAllAsync(CancellationToken cancellationToken)
        {
            var created = await _mediator.Send(new GetLeaguesListQuery(), cancellationToken);

            var list = new List<LeagueGetDto>();

            foreach (var c in created)
                list.Add(_mapper.Map<LeagueGetDto>(c));

            return Ok(list);
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
