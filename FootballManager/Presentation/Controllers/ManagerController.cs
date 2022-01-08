using Application.Features.Managers.Queries.GetManagersList;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Managers.Commands.Create;
using Application.Features.Teams.Commands.AddManager;
using Application.Contracts.Persistence;
using Domain;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Infrastructure.Static_Methods;
using Application.Features.Managers.Commands.CreateMultiple;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Application.Features.Players.Commands.Create;
using Presentation.DTOs;
using AutoMapper;
using Application.Features.Players.Queries.GetPlayersList;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/manager")]
    public class ManagerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ManagerController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }


        [HttpGet("Manager/{Id}")]
        public async Task<IActionResult> ListById(int Id, CancellationToken cancellationToken)
        {
            GetManagerById command = new GetManagerById(Id);
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(_mapper.Map<ManagerGetDto>(result));
        }

        [HttpGet("Manager")]
        public async Task<IActionResult> ListAllAsync(CancellationToken cancellationToken)
        {
            var created = await _mediator.Send(new GetManagerListQuery(), cancellationToken);

            var list = new List<ManagerGetDto>();

            foreach (var c in created)
                list.Add(_mapper.Map<ManagerGetDto>(c));

            return Ok(list);
        }

        [HttpGet("Available-Manager")]
        public async Task<IActionResult> ListAllFreeManagersAsync(CancellationToken cancellationToken)
        {
            var created = await _mediator.Send(new GetFreeManagerListQuery(), cancellationToken);

            var list = new List<ManagerGetDto>();

            foreach (var c in created)
                list.Add(_mapper.Map<ManagerGetDto>(c));

            return Ok(list);
        }

        [HttpGet("Unavailable-Manager")]
        public async Task<IActionResult> ListAllTakenManagersAsync(CancellationToken cancellationToken)
        {
            var created = await _mediator.Send(new GetTakenManagerListQuery(), cancellationToken);

            var list = new List<ManagerGetDto>();

            foreach (var c in created)
                list.Add(_mapper.Map<ManagerGetDto>(c));

            return Ok(list);
        }

        [HttpPost("Manager")]
        public async Task<IActionResult> CreateAsyncManager(CreateManagerCommand command)
        {
            var created = await _mediator.Send(command);
            var result = _mapper.Map<ManagerGetDto>(created);
            return Ok(result);
        }

        [HttpPost("Random-Manager")]
        public async Task<IActionResult> CreateAsyncRandomManager(CreateRandomManagerCommand command)
        {
            var created = await _mediator.Send(command);
            var result = _mapper.Map<ManagerGetDto>(created);
            return Ok(result);
        }

        [HttpPost("Managers")]
        public async Task<List<int>> CreatesAsync(CreateManagersCommand command)
        {
            return (List<int>)await _mediator.Send(command);
        }

        [HttpDelete("Manager")]
        public async Task<int> RemoveAsyncManagerById(RemoveManagerCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

    }
}
