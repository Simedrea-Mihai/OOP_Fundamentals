using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Players.Queries.GetPlayersList
{
    public class GetTakenPlayerListQuery : IRequest<IList<PlayerListVm>>
    {

    }

    public class GetPlayerTakenListQueryHandler : IRequestHandler<GetTakenPlayerListQuery, IList<PlayerListVm>>
    {
        private readonly IPlayerRepository _repository;
        private readonly IMapper _mapper;

        public GetPlayerTakenListQueryHandler(IPlayerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<IList<PlayerListVm>> Handle(GetTakenPlayerListQuery request, CancellationToken cancellationToken)
        {
            var players = _repository.ListTakenPlayers();

            return Task.FromResult(_mapper.Map<IList<PlayerListVm>>(players));
        }
    }
}
