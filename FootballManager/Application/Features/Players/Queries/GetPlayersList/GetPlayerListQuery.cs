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
    public class GetPlayerListQuery : IRequest<IList<PlayerListVm>>
    {

    }

    public class GetPlayerListQueryHandler : IRequestHandler<GetPlayerListQuery, IList<PlayerListVm>>
    {
        private readonly IPlayerRepository _repository;
        private readonly IMapper _mapper;

        public GetPlayerListQueryHandler(IPlayerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<IList<PlayerListVm>> Handle(GetPlayerListQuery request, CancellationToken cancellationToken)
        {
            var players = _repository.ListAll();

            return Task.FromResult(_mapper.Map<IList<PlayerListVm>>(players));
        }
    }
}
