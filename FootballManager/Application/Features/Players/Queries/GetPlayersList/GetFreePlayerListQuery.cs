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
    public class GetFreePlayerListQuery : IRequest<IList<PlayerListVm>>
    {

    }

    public class GetPlayerFreeListQueryHandler : IRequestHandler<GetFreePlayerListQuery, IList<PlayerListVm>>
    {
        private readonly IPlayerRepository _repository;
        private readonly IMapper _mapper;

        public GetPlayerFreeListQueryHandler(IPlayerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<IList<PlayerListVm>> Handle(GetFreePlayerListQuery request, CancellationToken cancellationToken)
        {
            var players = _repository.ListFreePlayers();

            return Task.FromResult(_mapper.Map<IList<PlayerListVm>>(players));
        }
    }
}
