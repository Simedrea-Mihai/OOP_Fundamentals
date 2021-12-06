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
    public class GetTakenPlayerListQuery : IRequest<IList<Player>>
    {

    }

    public class GetPlayerTakenListQueryHandler : IRequestHandler<GetTakenPlayerListQuery, IList<Player>>
    {
        private readonly IPlayerRepository _repository;
        private readonly IMapper _mapper;

        public GetPlayerTakenListQueryHandler(IPlayerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<Player>> Handle(GetTakenPlayerListQuery request, CancellationToken cancellationToken)
        {
            var players = await _repository.ListTakenPlayers(cancellationToken);

            return _mapper.Map<IList<Player>>(players);
        }
    }
}
