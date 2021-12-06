using Application.Contracts.Persistence;
using AutoMapper;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Players.Queries.GetPlayersList
{
    public class GetFreePlayerListQuery : IRequest<IList<Player>>
    {

    }

    public class GetPlayerFreeListQueryHandler : IRequestHandler<GetFreePlayerListQuery, IList<Player>>
    {
        private readonly IPlayerRepository _repository;
        private readonly IMapper _mapper;

        public GetPlayerFreeListQueryHandler(IPlayerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<Player>> Handle(GetFreePlayerListQuery request, CancellationToken cancellationToken)
        {
            var players = await _repository.ListFreePlayers(cancellationToken);

            return _mapper.Map<IList<Player>>(players);
        }
    }
}
