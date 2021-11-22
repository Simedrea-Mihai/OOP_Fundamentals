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

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/manager")]
    public class ManagerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ManagerController(IMediator mediator) => _mediator = mediator;

        [HttpGet("list")]
        public async Task<IList<ManagerListVm>> ListAllAsync(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetManagerListQuery(), cancellationToken);
        }

        [HttpGet("list-free-managers")]
        public async Task<IList<ManagerListVm>> ListAllFreeManagersAsync(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetFreeManagerListQuery(), cancellationToken);
        }

        [HttpGet("list-taken-managers")]
        public async Task<IList<ManagerListVm>> ListAllTakenManagersAsync(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetTakenManagerListQuery(), cancellationToken);
        }

        [HttpPost("create-manager")]
        public async Task<int> CreateAsyncManager(CreateManagerCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("create-random-manager")]
        public async Task<int> CreateAsyncRandomManager(CreateRandomManagerCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("create-managers")]
        public async Task<List<int>> CreatesAsync(CreateManagersCommand command)
        {
            return (List<int>)await _mediator.Send(command);
        }

        [HttpDelete("delete-by-id")]
        public async Task<int> RemoveAsyncManagerById(RemoveManagerCommand command, CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }

    }
}
