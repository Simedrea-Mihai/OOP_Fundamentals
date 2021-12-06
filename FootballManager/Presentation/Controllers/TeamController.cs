using Application.Features.Players.Commands.Create;
using Application.Features.Teams.Commands.AddManager;
using Application.Features.Teams.Queries.GetTeamList;
using Application.Teams.AddPlayers;
using Application.Teams.CreateTeam;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Players.Queries.GetPlayersList;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/team")]
    public class TeamController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public TeamController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("Team/{Id}")]
        public async Task<IActionResult> ListById(int Id, CancellationToken cancellationToken)
        {
            GetTeamById command = new GetTeamById(Id);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(_mapper.Map<TeamGetDto>(result));
        }


        [HttpGet("Team")]
        public async Task<IActionResult> ListAllAsync(CancellationToken cancellationToken)
        {
            var created = await _mediator.Send(new GetTeamsListQuery(), cancellationToken);

            var list = new List<TeamGetDto>();

            foreach (var c in created)
                list.Add(_mapper.Map<TeamGetDto>(c));

            return Ok(list);
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
