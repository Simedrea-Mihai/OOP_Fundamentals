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
using Microsoft.AspNetCore.Identity;
using Infrastructure.Identity.Models;
using Application.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/team")]
    public class TeamController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILoggedInUserService _loggedInUserService;

        public TeamController(IMediator mediator, IMapper mapper, UserManager<ApplicationUser> userManager, ILoggedInUserService loggedInUserService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _userManager = userManager;
            _loggedInUserService = loggedInUserService;
        }

        [HttpGet("Team/{Id}")]
        public async Task<IActionResult> ListById(int Id, CancellationToken cancellationToken)
        {
            GetTeamById command = new GetTeamById(Id);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(_mapper.Map<TeamGetDto>(result));
        }

        [Authorize]
        [HttpGet("Team")]
        public async Task<IActionResult> ListAllAsync(CancellationToken cancellationToken)
        {
            var created = await _mediator.Send(new GetTeamsListQuery(), cancellationToken);
            var list = new List<TeamGetDto>();
            foreach (var c in created)
                if (c.UserId == _loggedInUserService.UserId)
                    list.Add(_mapper.Map<TeamGetDto>(c));

            return Ok(list);
        }

        [HttpGet("Team-Nationality/{Id}")]
        public async Task<IActionResult> GetPopulationByCountry(int Id, CancellationToken cancellationToken)
        {
            GetPopulationByCountryQuery command = new GetPopulationByCountryQuery(Id);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("Team")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateTeamCommand command, CancellationToken cancellationToken)
        {
            // Some probs here
            var created = await _mediator.Send(command, cancellationToken);
            string userName = _loggedInUserService.UserId;
            var user = await _userManager.FindByNameAsync(userName);
            var result = _mapper.Map<TeamGetDto>(created);
            result.UserId = userName;
            return Ok(result);
        }

        [HttpPatch("Add-Manager")]
        public async Task<IActionResult> AddManagerAsync([FromQuery] AddManager command, CancellationToken cancellationToken)
        {
            var created = await _mediator.Send(command, cancellationToken);
            var result = _mapper.Map<ManagerGetDto>(created);
            return Ok(result);
        }
        
        [HttpPatch("Add-Players")]
        public async Task<IActionResult> AddPlayersAsync([FromQuery] AddPlayers command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPatch("Buy-Player")]
        public async Task<IActionResult> BuyPlayerAsync([FromQuery] BuyPlayer command, CancellationToken cancellationToken)
        {
            var created = await _mediator.Send(command, cancellationToken);
            var result = _mapper.Map<PlayerGetDto>(created);
            return Ok(result);
        }

        [HttpPatch("Fire-Player")]
        public async Task<IActionResult> FirePlayerAsync([FromQuery] FirePlayerCommand command, CancellationToken cancellationToken)
        {
            var created = await _mediator.Send(command, cancellationToken);
            var result = _mapper.Map<PlayerGetDto>(created);
            return Ok(result);
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

        [HttpPost("TeamEvent")]
        public async Task<IActionResult> CreateEvent([FromQuery] CreateTeamEventCommand command, CancellationToken cancellationToken)
        {
            var created = await _mediator.Send(command, cancellationToken);
            return Ok(created);
        }



    }
}
