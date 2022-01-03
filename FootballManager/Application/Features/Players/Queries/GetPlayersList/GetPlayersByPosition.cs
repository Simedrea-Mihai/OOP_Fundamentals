using Application.Contracts.Persistence;
using AutoMapper;
using Domain;
using Domain.Entities.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Players.Queries.GetPlayersList
{
    public class GetPlayersByPosition : IRequest<IList<Player>>
    {
        public PlayerPosition Position { get; set; }
        public int TeamId { get; set; }

        public GetPlayersByPosition() { }

        public GetPlayersByPosition(PlayerPosition position)
        {
            Position = position;
        }

    }

    public class GetPlayersByPositionHandler : IRequestHandler<GetPlayersByPosition, IList<Player>>
    {
        private readonly IPlayerRepository _repository;
        private readonly IMapper _mapper;

        public GetPlayersByPositionHandler(IPlayerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<Player>> Handle(GetPlayersByPosition command, CancellationToken cancellationToken)
        {
            var player = await _repository.ListByPosition(command.Position, command.TeamId, cancellationToken);

            return player;
        }
    }
}
